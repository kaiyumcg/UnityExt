using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityExt
{
    public abstract class SingletonBehaviour<T> : SingletonBehaviourProtected<T> where T : MonoBehaviour
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