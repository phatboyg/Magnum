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
	using System.Web;


	public class StubHttpContextForRouting :
		HttpContextBase
	{
		StubHttpRequestForRouting _request;
		StubHttpResponseForRouting _response;

		public StubHttpContextForRouting(string appPath = "/", string requestUrl = "~/")
		{
			_request = new StubHttpRequestForRouting(appPath, requestUrl);
			_response = new StubHttpResponseForRouting();
		}

		public override HttpRequestBase Request
		{
			get { return _request; }
		}

		public override HttpResponseBase Response
		{
			get { return _response; }
		}

		public void SetRequestUrl(string requestUrl)
		{
			_request.SetRequestUrl(requestUrl);
		}
	}
}