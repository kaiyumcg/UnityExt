using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityExt
{
    public abstract class PersistentSingletonBehaviourInternal<T> : PersistentSingletonBehaviourProtected<T> where T : MonoBehaviour
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