// Copyright 2007-2008 The Apache Software Foundation.
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
namespace Magnum.TestFramework
{
	using System.IO;
	using System.Reflection;
	using log4net.Config;
	using NUnit.Framework;

	[SetUpFixture]
	public class LogSetup
	{
		[SetUp]
		public void SetupLog4Net()
		{
			string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

			string file = Path.Combine(path, "Magnum.TestFramework.log4net.xml");

			XmlConfigurator.Configure(new FileInfo(file));
		}
	}
}