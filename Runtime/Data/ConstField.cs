using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityExt
{
    [System.Serializable]
    public class ConstField<T>
    {
        [SerializeField] T m_ConstValue;
        public T Value { get { return m_ConstValue; } }
    }
}