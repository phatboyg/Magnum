namespace Magnum.FileSystem
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using Channels;
    using Events;
    using Extensions;
    using Fibers;
    using Internal;

    public class PollingFileSystemEventProducer :
        IDisposable
    {
        readonly UntypedChannel _channel;
        readonly TimeSpan _checkInterval;
        readonly ChannelConnection _connection;
        readonly string _directory;
        readonly Fiber _fiber;
        readonly FileSystemEventProducer _fileSystemEventProducer;
        readonly Dictionary<string, Guid> _hashes;
        readonly Scheduler _scheduler;
        bool _disposed;
        ScheduledAction _scheduledAction;

        /// <summary>
        /// Creates a PollingFileSystemEventProducer
        /// </summary>		
        /// <param name="directory">The directory to watch</param>
        /// <param name="channel">The channel where events should be sent</param>
        /// <param name="scheduler">Event scheduler</param>
        /// <param name="fiber">Fiber to schedule on</param>
        /// <param name="checkInterval">The maximal time between events or polls on a given file</param>
        public PollingFileSystemEventProducer(string directory,
                                              UntypedChannel channel,
                                              Scheduler scheduler,
                                              Fiber fiber,
                                              TimeSpan checkInterval) :
                                                  this(directory, channel, scheduler, fiber, checkInterval, true)

        {
        }

        /// <summary>
        /// Creates a PollingFileSystemEventProducer
        /// </summary>		
        /// <param name="directory">The directory to watch</param>
        /// <param name="channel">The channel where events should be sent</param>
        /// <param name="scheduler">Event scheduler</param>
        /// <param name="fiber">Fiber to schedule on</param>
        /// <param name="checkInterval">The maximal time between events or polls on a given file</param>
        /// <param name="checkSubDirectory">Indicates if subdirectories will be checked or ignored</param>
        public PollingFileSystemEventProducer(string directory,
                                              UntypedChannel channel,
                                              Scheduler scheduler,
                                              Fiber fiber,
                                              TimeSpan checkInterval,
                                              bool checkSubDirectory)
        {
            _directory = directory;
            _channel = channel;
            _fiber = fiber;
            _hashes = new Dictionary<string, Guid>();
            _scheduler = scheduler;
            _checkInterval = checkInterval;

            _fiber.Add(HashFileSystem);

            var myChannel = new ChannelAdapter();

            _connection = myChannel.Connect(x =>
                {
                    x.AddConsumerOf<FileSystemChanged>()
                        .UsingConsumer(HandleFileSystemChangedAndCreated)
                        .HandleOnFiber(_fiber);
                    x.AddConsumerOf<FileSystemCreated>()
                        .UsingConsumer(HandleFileSystemChangedAndCreated)
                        .HandleOnFiber(_fiber);
                    x.AddConsumerOf<FileSystemRenamed>()
                        .UsingConsumer(HandleFileSystemRenamed)
                        .HandleOnFiber(_fiber);
                    x.AddConsumerOf<FileSystemDeleted>()
                        .UsingConsumer(HandleFileSystemDeleted)
                        .HandleOnFiber(_fiber);
                });

            _fileSystemEventProducer = new FileSystemEventProducer(directory, myChannel, checkSubDirectory);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        void HandleFileSystemChangedAndCreated(FileSystemEvent fileEvent)
        {
            HandleHash(fileEvent.Path, GenerateHashForFile(fileEvent.Path));
        }

        void HandleFileSystemRenamed(FileSystemRenamed fileEvent)
        {
            HandleFileSystemChangedAndCreated(fileEvent);
            HandleFileSystemDeleted(new FileSystemDeletedImpl(fileEvent.OldName, fileEvent.OldPath));
        }

        void HandleFileSystemDeleted(FileSystemEvent fileEvent)
        {
            RemoveHash(fileEvent.Path);
        }

        void HandleHash(string key, Guid newHash)
        {
            if (string.IsNullOrEmpty(key))
                return;

            try
            {
                if (_hashes.ContainsKey(key))
                {
                    if (_hashes[key] != newHash)
                    {
                        _hashes[key] = newHash;
                        _channel.Send(new FileChangedImpl(Path.GetFileName(key), key));
                    }
                }
                else
                {
                    _hashes.Add(key, newHash);
                    _channel.Send(new FileCreatedImpl(Path.GetFileName(key), key));
                }
            }
            catch
            {
            }
        }

        void RemoveHash(string key)
        {
            if (string.IsNullOrEmpty(key))
                return;

            try
            {
                _hashes.Remove(key);
                _channel.Send(new FileSystemDeletedImpl(Path.GetFileName(key), key));
            }
            catch
            {
            }
        }

        void HashFileSystem()
        {
            try
            {
                var newHashes = new Dictionary<string, Guid>();

                ProcessDirectory(newHashes, _directory);

                List<KeyValuePair<string, Guid>> existing = _hashes
                    .Where(x => !string.IsNullOrEmpty(x.Key))
                    .ToList();

                // process all the new hashes found
                newHashes.Each(x => HandleHash(x.Key, x.Value));

                List<string> removed = existing
                    .Where(x => !newHashes.ContainsKey(x.Key))
                    .Select(x => x.Key)
                    .ToList();

                // remove any hashes we couldn't process
                removed.Each(RemoveHash);
            }
            catch
            {
                // while bad, we really don't care we'll do this again
            }
            finally
            {
                _scheduledAction = _scheduler.Schedule(_checkInterval, _fiber, HashFileSystem);
            }
        }

        void ProcessDirectory(Dictionary<string, Guid> hashes, string baseDirectory)
        {
            string[] files = Directory.GetFiles(baseDirectory);

            foreach (string file in files)
            {
                string fullFileName = Path.Combine(baseDirectory, file);
                if (string.IsNullOrEmpty(fullFileName))
                    continue;

                hashes.Add(fullFileName, GenerateHashForFile(fullFileName));
            }

            Directory.GetDirectories(baseDirectory)
                .Each(directory => ProcessDirectory(hashes, directory));
        }

        static Guid GenerateHashForFile(string file)
        {
            try
            {
                string hashValue;
                using (FileStream f = File.OpenRead(file))
                using (var md5 = new MD5CryptoServiceProvider())
                {
                    byte[] fileHash = md5.ComputeHash(f);

                    hashValue = BitConverter.ToString(fileHash).Replace("-", "");

                    f.Close();
                }

                return new Guid(hashValue);
            }
            catch (Exception)
            {
                // chew up exception and say empty hash
                // can we do something more interesting than this?
                return Guid.Empty;
            }
        }

        ~PollingFileSystemEventProducer()
        {
            Dispose(false);
        }

        void Dispose(bool disposing)
        {
            if (_disposed)
                return;
            if (disposing)
            {
                _scheduledAction.Cancel();
                _connection.Dispose();
                _fileSystemEventProducer.Dispose();
            }

            _disposed = true;
        }
    }
}