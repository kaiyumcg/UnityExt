using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityExt;

namespace UnityExt
{
    public abstract class MultitonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        static List<T> _instances = null;
        protected static List<T> instances
        {
            get
            {
                return _instances;
            }
        }
        protected virtual void Awake()
        {
            if (_instances == null) { _instances = new List<T>(); }
            _instances = _instances.ExRemoveNulls();
            _instances.ExRemoveDuplicates();
            if (_instances == null) { _instances = new List<T>(); }
            var data = this as T;
            if (_instances.Contains(data) == false)
            {
                _instances.Add(data);
            }
        }
        public static T1 GetInstance<T1>() where T1 : MonoBehaviour
        {
            T1 result = null;
            instances.ExForEachSafe((i) =>
            {
                if (i.GetType() == typeof(T1))
                {
                    result = i as T1;
                }
            });
            return result;
        }
        public static List<T1> GetInstances<T1>() where T1 : MonoBehaviour
        {
            List<T1> result = new List<T1>();
            instances.ExForEachSafe((i) =>
            {
                if (i.GetType() == typeof(T1))
                {
                    var res = i as T1;
                    result.Add(res);
                }
            });
            return result;
        }
    }
}