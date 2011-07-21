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
namespace Magnum.Benchmark
{
	using System;


	public class BenchmarkRunResult
	{
		public string Description { get; set; }
		public int Iterations { get; set; }
		public string ModelName { get; set; }
		public string SerializerName { get; set; }
		public int SerializedBytesLength { get; set; }

		public long Duration { get; set; }

//		public bool Success
//		{
//			get
//			{
//				return TotalSerializationTicks > -1
//				       && TotalDeserializationTicks > -1;
//			}
//		}
//
//		public decimal AvgTicksPerIteration
//		{
//			get
//			{
//				return Math.Round(
//				                  (TotalSerializationTicks + TotalDeserializationTicks)
//				                  /(decimal)Iterations, 4);
//			}
//		}
//
//		public long TotalTicks
//		{
//			get { return TotalSerializationTicks + TotalDeserializationTicks; }
//		}

		public decimal TimesSlowerThanBest { get; set; }
		public decimal TimesLargerThanBest { get; set; }
	}
}


