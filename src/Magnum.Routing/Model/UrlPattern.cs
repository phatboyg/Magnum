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
	using System;


	public class UrlPattern
	{
		static readonly Uri _segmentBase = new Uri("http://localhost/");
		string _pattern;

		public UrlPattern(string pattern)
		{
			_pattern = pattern;
		}

		public string Pattern
		{
			get { return _pattern; }
		}

		public void Append(string patternPart)
		{
			_pattern += "/" + patternPart;
			_pattern = _pattern.Replace("//", "/").TrimStart('/');
		}

		public void Prepend(string prefix)
		{
			if (string.IsNullOrEmpty(prefix))
				return;

			// in case it is called multiple times?
			if (_pattern.StartsWith(prefix))
				return;

			_pattern = prefix.TrimEnd('/') + "/" + _pattern;
		}

		public bool Equals(UrlPattern other)
		{
			if (ReferenceEquals(null, other))
				return false;
			if (ReferenceEquals(this, other))
				return true;
			return Equals(other._pattern, _pattern);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
				return false;
			if (ReferenceEquals(this, obj))
				return true;
			if (obj.GetType() != typeof(UrlPattern))
				return false;
			return Equals((UrlPattern)obj);
		}

		public override int GetHashCode()
		{
			return (_pattern != null ? _pattern.GetHashCode() : 0);
		}

		public override string ToString()
		{
			return string.Format("{0}", _pattern);
		}

		public string[] GetSegments()
		{
			return new Uri(_segmentBase, _pattern).Segments;
		}
	}
}