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
namespace Magnum.Specs.Extensions
{
	using System;
	using System.Diagnostics;
	using System.Linq;
	using System.Net;
	using System.Text.RegularExpressions;
	using Magnum.Extensions;
	using NUnit.Framework;
	using TestFramework;


	[TestFixture, Explicit]
	public class UriExtension_Specs
	{
		[Test]
		public void Should_properly_resolve_an_ip_address()
		{
			var uri = new Uri("http://www.codebetter.com");

			IPEndPoint endpoint = uri.ResolveHostName().Single();

			endpoint.Address.ShouldEqual(RetrieveCodeBetterIpAddress());
			endpoint.Port.ShouldEqual(80);
		}

		[Test]
		public void Should_properly_resolve_an_ip_address_with_any_local_address()
		{
			var uri = new Uri("bogus://0.0.0.0:8008/");

			IPEndPoint endpoint = uri.ResolveHostName().Single();

			endpoint.Address.ShouldEqual(IPAddress.Any);
			endpoint.Port.ShouldEqual(8008);
		}

		[Test]
		public void Should_properly_resolve_an_ip_address_with_ftp()
		{
			var uri = new Uri("ftp://www.codebetter.com");

			IPEndPoint endpoint = uri.ResolveHostName().Single();

			endpoint.Address.ShouldEqual(RetrieveCodeBetterIpAddress());
			endpoint.Port.ShouldEqual(21);
		}

		[Test]
		public void Should_properly_resolve_an_ip_address_with_multiple_options()
		{
			var uri = new Uri("http://www.microsoft.com/");

			IPEndPoint[] endpoint = uri.ResolveHostName().ToArray();

			endpoint.Length.ShouldBeGreaterThanOrEqualTo(1);
		}

		[Test]
		public void Should_properly_resolve_an_ip_address_with_ssl()
		{
			var uri = new Uri("https://www.codebetter.com");

			IPEndPoint endpoint = uri.ResolveHostName().Single();

			endpoint.Address.ShouldEqual(RetrieveCodeBetterIpAddress());
			endpoint.Port.ShouldEqual(443);
		}

	    static IPAddress CodebetterIPAddress;

        static IPAddress RetrieveCodeBetterIpAddress()
        {
            if (CodebetterIPAddress != null)
                return CodebetterIPAddress;

            var rgx = new Regex(@"Address: *(?<ipaddress>[0-9.]*)");
            
            var startInfo = new ProcessStartInfo("nslookup.exe", "www.codebetter.com");
		    startInfo.UseShellExecute = false;
		    startInfo.RedirectStandardOutput = true;
		    startInfo.RedirectStandardError = true;
		    startInfo.WindowStyle = ProcessWindowStyle.Hidden;

		    var p = Process.Start(startInfo);

		    var output = p.StandardOutput.ReadToEnd();
		    p.WaitForExit(60000);

            var matches = rgx.Matches(output);

            if (matches.Count <= 1)
                Assert.Fail("Failed to perform a DNS lookup for www.codebetter.com");

            CodebetterIPAddress = IPAddress.Parse(matches.Cast<Match>().Select(x => x.Groups["ipaddress"]).Last().Value);
            return CodebetterIPAddress;
        }
	}
}