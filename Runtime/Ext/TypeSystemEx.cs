using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace UnityExt
{
    public static class TypeSystemEx
    {
        //todo child mono type from UIF. i.e. ExGetChildScript<> 
        public static bool ExInterfaceIsImplementedOnClass<T>(this Type interfaceType)
        {
            return interfaceType.GetType().IsAssignableFrom(typeof(T));
        }
        public static T1 ExGetTypedScript<T1, T2>(this List<T2> scriptList) where T1 : class where T2 : class
        {
            T1 result = null;
            if (scriptList != null && scriptList.Count > 0)
            {
                for (int i = 0; i < scriptList.Count; i++)
                {
                    var sc = scriptList[i];
                    if (sc == null) { continue; }
                    if (sc.GetType() == typeof(T1))
                    {
                        result = sc as T1;
                        break;
                    }
                }
            }
            return result;
        }
        public static T1 ExGetTypedScript_NoListCheck<T1, T2>(this List<T2> scriptList) where T1 : class where T2 : class
        {
            T1 result = null;
            for (int i = 0; i < scriptList.Count; i++)
            {
                var sc = scriptList[i];
                if (sc.GetType() == typeof(T1))
                {
                    result = sc as T1;
                    break;
                }
            }
            return result;
        }
        public static List<T> ExConvertTo<T>(this List<MonoBehaviour> scripts) where T : class
        {
            var result = new List<T>();
            if (scripts != null && scripts.Count > 0)
            {
                for (int i = 0; i < scripts.Count; i++)
                {
                    var nt = scripts[i];
                    if (nt == null) { continue; }
                    var it = nt as T;
                    if (it == null) { continue; }
                    result.Add(it);
                }
            }
            return result;
        }
        public static List<T> ExConvertTo<T>(this MonoBehaviour[] scripts) where T : class
        {
            var result = new List<T>();
            if (scripts != null && scripts.Length > 0)
            {
                for (int i = 0; i < scripts.Length; i++)
                {
                    var nt = scripts[i];
                    if (nt == null) { continue; }
                    var it = nt as T;
                    if (it == null) { continue; }
                    result.Add(it);
                }
            }
            return result;
        }
    }
}