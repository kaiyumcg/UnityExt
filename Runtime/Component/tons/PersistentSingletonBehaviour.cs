using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityExt
{
    public abstract class PersistentSingletonBehaviour<T> : PersistentSingletonBehaviourProtected<T> where T : MonoBehaviour
    {
        public static T Instance
        {
            get
            {
                return instance;
            }
        }
    }
}