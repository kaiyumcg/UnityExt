using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityExt
{
    public abstract class SingletonBehaviourProtected<T> : MonoBehaviour where T : MonoBehaviour
    {
        static T _instance = null;
        bool validRef = false;
        protected static T instance
        {
            get
            {
                return _instance;
            }
        }
        void Awake()
        {
            validRef = false;
            if (_instance == null)
            {
                _instance = this as T;
                validRef = true;
                OnFirstLoad();
            }
            else if (_instance != this as T)
            {
                DestroyImmediate(gameObject);
            }
        }

        private IEnumerator Start()
        {
            if (validRef)
            {
                yield return null;
                StartCoroutine(OnAwakeBehaviourAsync());
            }
        }
        protected virtual IEnumerator OnAwakeBehaviourAsync() { yield return null; }
        protected virtual void OnFirstLoad() { }
    }
}