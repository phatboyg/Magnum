// Copyright 2007-2010 The Apache Software Foundation.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace Magnum.Specs.Serialization.Benchmarks
{
	using System.IO;
	using Magnum.Serialization;


	class FastTextSerializerSerializationRunner :
		SerializationRunner
	{
		readonly Serializer _serializer;

		public FastTextSerializerSerializationRunner()
		{
			_serializer = new FastTextSerializer();
		}

		public byte[] Serialize<T>(T obj)
		{
			using (var sout = new MemoryStream())
			using (var wout = new StreamWriter(sout))
			{
				_serializer.Serialize(obj, wout);
				wout.Flush();

				return sout.ToArray();
			}
		}

		public T Deserialize<T>(byte[] data)
		{
			using (var sin = new MemoryStream(data))
			using (var win = new StreamReader(sin))
				return _serializer.Deserialize<T>(win);
		}
	}
}