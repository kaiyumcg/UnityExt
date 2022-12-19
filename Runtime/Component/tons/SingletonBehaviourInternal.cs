using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityExt
{
    public abstract class SingletonBehaviourInternal<T> : SingletonBehaviourProtected<T> where T : MonoBehaviour
    {
        internal static T Instance
        {
            get
            {
                return instance;
            }
        }
    }
}