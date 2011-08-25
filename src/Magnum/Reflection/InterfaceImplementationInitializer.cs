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
namespace Magnum.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Threading;
    using Extensions;


    public class InterfaceImplementationInitializer
    {
        readonly Dictionary<Type, Func<object, IDictionary<string, object>, object>> _cache;
        readonly ReaderWriterLockSlim _lock;

        public InterfaceImplementationInitializer()
        {
            _cache = new Dictionary<Type, Func<object, IDictionary<string, object>, object>>();
            _lock = new ReaderWriterLockSlim();
        }

        public object InitializeFromDictionary(object obj, IDictionary<string, object> values)
        {
            if (obj == null)
                return null;

            if (values == null)
                return obj;

            GetObjectInitializer(obj, values)(values);

            return obj;
        }


        Action<IDictionary<string, object>> GetObjectInitializer(object obj, IDictionary<string, object> values)
        {
            _lock.EnterUpgradeableReadLock();
            try
            {
                Func<object, IDictionary<string, object>, object> initializer;
                if (!_cache.TryGetValue(obj.GetType(), out initializer))
                {
                    _lock.EnterWriteLock();
                    try
                    {
                        if (!_cache.TryGetValue(obj.GetType(), out initializer))
                        {
                            initializer = CreateInitializer(obj.GetType(), values);
                            _cache[obj.GetType()] = initializer;
                        }
                    }
                    finally
                    {
                        _lock.ExitWriteLock();
                    }
                }
                return dictionary => initializer(obj, dictionary);
            }
            finally
            {
                _lock.ExitUpgradeableReadLock();
            }
        }

        static Func<object, IDictionary<string, object>, object> CreateInitializer(Type objType,
                                                                                   IDictionary<string, object> values)
        {
            BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy;
            Dictionary<string, PropertyInfo> properties = objType.GetProperties(bindingFlags).ToDictionary(x => x.Name);

            string[] unknown = values
                .Where(x => !properties.ContainsKey(x.Key))
                .Select(x => x.Key)
                .ToArray();
            if (unknown.Length > 0)
            {
                throw new ArgumentException("The dictionary has values that are not properties of the object: " +
                                            string.Join(",", unknown));
            }

            IEnumerable<PropertyInfo> setters = values
                .Select(x => properties[x.Key]);


            var dm = new DynamicMethod(string.Empty, typeof(object),
                                       new[] {typeof(object), typeof(IDictionary<string, object>)}, objType);
            ILGenerator il = dm.GetILGenerator();

            Type dictType = typeof(Dictionary<string, object>);

            MethodInfo getMethod = dictType.GetMethod("get_Item", new[]{typeof(string)});

            setters.Each(property =>
                {
                    il.Emit(OpCodes.Ldarg_0); // object

                    il.Emit(OpCodes.Ldarg_1); // dictionary
                    il.Emit(OpCodes.Ldstr, property.Name); // key
                    il.EmitCall(OpCodes.Callvirt, getMethod, null);

                    if(property.PropertyType.IsValueType)
                        il.Emit(OpCodes.Unbox_Any, property.PropertyType);

                    il.EmitCall(OpCodes.Callvirt, property.GetSetMethod(true), null);
                });

            // finally load Dictionary and return
            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Ret);

            return (Func<object, IDictionary<string, object>, object>)
                   dm.CreateDelegate(typeof(Func<object, IDictionary<string, object>, object>));
        }
    }
}