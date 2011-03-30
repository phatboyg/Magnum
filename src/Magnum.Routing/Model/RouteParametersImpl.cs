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
namespace Magnum.Routing.Model
{
	using System.Collections;
	using System.Collections.Generic;
	using Internals;


	class RouteParametersImpl :
		RouteParameters
	{
		Cache<string, RouteParameter> _values;

		public RouteParametersImpl(IEnumerable<RouteParameter> parameters)
		{
			_values = new DictionaryCache<string, RouteParameter>(x => x.Name, parameters);
		}

		public RouteParameter this[string name]
		{
			get { return _values[name]; }
		}

		public string[] AllNames
		{
			get { return _values.GetAllKeys(); }
		}

		public int Count
		{
			get { return _values.Count; }
		}

		public IEnumerator<RouteParameter> GetEnumerator()
		{
			return _values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public bool Has(string name)
		{
			return _values.Has(name);
		}
	}
}