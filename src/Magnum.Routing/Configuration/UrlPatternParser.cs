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
namespace Magnum.Routing
{
	using System;
	using System.Collections.Generic;
	using Model;


	public class UrlPatternParser
	{
		const char BeginParameter = '{';
		const char EndParameter = '}';
		const char SegmentSeparator = '/';

		public IEnumerable<RouteParameter> Parse(UrlPattern pattern)
		{
			IList<RouteParameter> parameters = new List<RouteParameter>();

			ParsePattern(pattern.ToString(), parameterName => { parameters.Add(new RouteParameterImpl(parameterName)); });

			return parameters;
		}

		void ParsePattern(string value, Action<string> parameterCallback)
		{
			int index = 0;
			int segment = 0;
			ParsePattern(value, ref index, ref segment, parameterCallback);
		}

		void ParsePattern(string value, ref int index, ref int segment, Action<string> parameterCallback)
		{
			int length = value.Length;

			while (index < length)
			{
				char ch = value[index];

				if (ch == BeginParameter)
				{
					bool doubled = index + 1 < length && value[index + 1] == BeginParameter;
					if (doubled)
						index++;
					else
						ParseParameter(value, ref index, parameterCallback);
				}
				else if (ch == SegmentSeparator)
					segment++;

				index++;
			}
		}

		void ParseParameter(string value, ref int index, Action<string> parameterCallback)
		{
			if (value[index] != BeginParameter)
				return;

			int start = index + 1;
			int length = value.Length;

			if(index + 1 < length && value[index+1] == EndParameter)
				throw new ArgumentException("A parameter name must not be empty");

			while (++index < length)
			{
				if (value[index] == EndParameter)
				{
					string name = value.Substring(start, index - start);
					parameterCallback(name);
					return;
				}

				if (value[index] == SegmentSeparator)
				{
					throw new ArgumentException("A parameter name must not contain a separator");
				}
			}

			throw new ArgumentException("A parameter name must be properly terminated");
		}
	}
}