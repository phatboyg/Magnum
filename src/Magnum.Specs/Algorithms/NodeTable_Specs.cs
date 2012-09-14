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
namespace Magnum.Specs.Algorithms
{
	using System;
	using Magnum.Algorithms.Implementations;
	using TestFramework;


	public class Given_a_node_table<T>
	{
		protected NodeTable<T> NodeTable;

		[Given]
		public void A_node_table()
		{
			NodeTable = new NodeTable<T>();
		}
	}

	[Scenario]
	public class Adding_the_same_node_to_a_node_table :
		Given_a_node_table<string>
	{
		[Then]
		public void Should_return_the_same_index()
		{
			int first = NodeTable["A"];
			int second = NodeTable["A"];

			first.ShouldEqual(second);
		}
	}


	[Scenario]
	public class Adding_two_different_nodes_to_a_node_table :
		Given_a_node_table<string>
	{
		[Then]
		public void Should_return_consecutive_indexes()
		{
			int first = NodeTable["A"];
			int second = NodeTable["B"];

			first.ShouldEqual(second - 1);
		}
	}


	[Scenario]
	public class Adding_two_equal_reference_types_to_a_node_table :
		Given_a_node_table<Uri>
	{
		[Then]
		public void Should_return_the_same_index()
		{
			Uri firstUri = new Uri("http://localhost/");
			Uri secondUri = new Uri("http://localhost/");

			int first = NodeTable[firstUri];
			int second = NodeTable[secondUri];

			first.ShouldEqual(second);
		}
	}
}