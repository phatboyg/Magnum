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
namespace Magnum.Routing.Specs
{
	using System.Collections.Specialized;
	using System.Web;


	public class StubHttpRequestForRouting : 
		HttpRequestBase
	{
		string _appPath;
		string _requestUrl;

		public StubHttpRequestForRouting(string appPath, string requestUrl)
		{
			_appPath = appPath;
			_requestUrl = requestUrl;
		}

		public void SetRequestUrl(string requestUrl)
		{
			_requestUrl = requestUrl;
		}

		public override string ApplicationPath
		{
			get { return _appPath; }
		}

		public override string AppRelativeCurrentExecutionFilePath
		{
			get { return _requestUrl; }
		}

		public override string PathInfo
		{
			get { return ""; }
		}

		public override NameValueCollection ServerVariables
		{
			get { return new NameValueCollection(); }
		}
	}
}