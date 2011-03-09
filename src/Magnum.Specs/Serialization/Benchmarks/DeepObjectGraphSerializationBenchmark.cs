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
	using System;
	using System.Collections.Generic;


	public class DeepObjectGraphSerializationBenchmark :
		Benchmark<SerializationRunner>
	{
		readonly TopLevel _obj;

		public DeepObjectGraphSerializationBenchmark()
		{
			_obj = new TopLevel
				{
					Body = new MiddleLevel
						{
							Boolean = true,
							Decimal = 123.45m,
							Double = 123.45d,
							Float = 123.45f,
							Integer = 12345,
							Long = 1234567890,
							Name = "Crazy Clown",
							Now = DateTime.Now,
						},
					Headers = new Dictionary<string, string>
						{
							{"Content-type", "text/html; encoding=utf8"},
							{"Content-encoding", "gzip"},
							{"Content-length", "9823231"},
							{"Created-by", "TigerBlood"},
							{"Expires", "Never"},
						},
				};
		}

		public IEnumerable<int> Iterations
		{
			get { return new[] {50000}; }
		}

		public void WarmUp(SerializationRunner instance)
		{
			byte[] data = instance.Serialize(_obj);

			instance.Deserialize<TopLevel>(data);
		}

		public void Shutdown(SerializationRunner instance)
		{
		}

		public void Run(SerializationRunner instance, int iterationCount)
		{
			for (int i = 0; i < iterationCount; i++)
			{
				byte[] data = instance.Serialize(_obj);

				var result = instance.Deserialize<TopLevel>(data);
			}
		}


		[Serializable]
		class MiddleLevel
		{
			public int Integer { get; set; }
			public long Long { get; set; }
			public double Double { get; set; }
			public float Float { get; set; }
			public bool Boolean { get; set; }
			public decimal Decimal { get; set; }
			public DateTime Now { get; set; }
			public string Name { get; set; }
		}

		[Serializable]
		class TopLevel
		{
			public MiddleLevel Body { get; set; }
			public IDictionary<string, string> Headers { get; set; }
		}
	}
}