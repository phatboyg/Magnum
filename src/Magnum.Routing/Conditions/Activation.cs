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
namespace Magnum.Routing.Conditions
{
	using System.Collections.Generic;


	public interface Activation
	{
		void Activate(RouteContext context, string value);

		/// <summary>
		/// Used to inspect the condition network to find matching conditions
		/// </summary>
		/// <typeparam name="T"></typeparam>
		IEnumerable<T> Match<T>()
			where T : class;
	}
}