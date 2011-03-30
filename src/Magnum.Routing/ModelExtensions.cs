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
	using System.Linq.Expressions;
	using System.Reflection;
	using Model;


	public static class ModelExtensions
	{
		public static RouteParameter Get<T>(this RouteParameters parameters, Expression<Func<T, object>> expression)
		{
			return parameters[expression.GetMemberName()];
		}

		public static RouteVariable Get<T>(this RouteVariables parameters, Expression<Func<T, object>> expression)
		{
			return parameters[expression.GetMemberName()];
		}

		public static Route ToRouteDefinition(this UrlPattern pattern)
		{
			return RouteFactory.Current.New(pattern);
		}

		static string GetMemberName<T>(this Expression<Func<T, object>> expression)
		{
			var body = expression.Body as MemberExpression;
			if (body == null)
				throw new ArgumentException("The expression must be a member expression");

			if (body.Expression != expression.Parameters[0])
				throw new ArgumentException("The expression must target the lambda argument");

			if (body.Member.MemberType != MemberTypes.Property && body.Member.MemberType != MemberTypes.Field)
				throw new ArgumentException("The expression must be a property or a field");

			return body.Member.Name;
		}
	}
}