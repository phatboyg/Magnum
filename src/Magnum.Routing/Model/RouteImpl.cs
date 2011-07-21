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
	using System.Collections.Generic;


	public class RouteImpl :
		Route
	{
		readonly RouteParametersImpl _parameters;
		readonly UrlPattern _url;
		readonly RouteVariablesImpl _variables;

		public RouteImpl(string pattern, IEnumerable<RouteParameter> parameters,
		                 IEnumerable<RouteVariable> variables)
			: this(new UrlPattern(pattern), parameters, variables)
		{
		}

		public RouteImpl(UrlPattern url, IEnumerable<RouteParameter> parameters,
		                 IEnumerable<RouteVariable> variables)
		{
			_url = url;
			_parameters = new RouteParametersImpl(parameters);
			_variables = new RouteVariablesImpl(variables);
		}

		public string Url
		{
			get { return _url.ToString(); }
		}

		public RouteParameters Parameters
		{
			get { return _parameters; }
		}

		public RouteVariables Variables
		{
			get { return _variables; }
		}

		public override string ToString()
		{
			return _url.ToString();
		}
	}


	public class RouteImpl<TContext> :
		RouteImpl,
		Route<TContext>
	{
		public RouteImpl(string pattern, IEnumerable<RouteParameter> parameters, IEnumerable<RouteVariable> variables)
			: base(pattern, parameters, variables)
		{
		}

		public RouteImpl(UrlPattern url, IEnumerable<RouteParameter> parameters, IEnumerable<RouteVariable> variables)
			: base(url, parameters, variables)
		{
		}
	}
}