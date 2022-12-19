using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace UnityExt
{
    public enum ListEffectPlayMode { Serial = 0, Random = 1 }
    public enum ManyEffectPlayMode { AtOnce = 0, Serially = 1 }
    public interface IEffectPlay : ICloneable
    {
        void SpawnAndPlay(MonoBehaviour callerContext);
        object ICloneable.Clone() { return null; }
    }
}