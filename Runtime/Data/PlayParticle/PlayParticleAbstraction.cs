using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using AttributeExt;

namespace UnityExt
{
    public enum ListEffectPlayMode { Serial = 0, Random = 1 }
    public enum ManyEffectPlayMode { AtOnce = 0, Serially = 1 }
    public interface IEffectPlay : ICloneable
    {
        void Play(MonoBehaviour callerContext, System.Action OnComplete = null, bool destroyAtEnd = true);
        void Spawn(MonoBehaviour callerContext);
        void CleanupForPool(MonoBehaviour callerContext);
        void SpawnAndPlay(MonoBehaviour callerContext, System.Action OnComplete = null, bool destroyAtEnd = true);
        object ICloneable.Clone() { return null; }
    }

    [System.Serializable]
    public class GameParticlePlayer
    {
        [SerializeReference, SerializeReferenceButton] IEffectPlay effect;
        public IEffectPlay Effect { get { return effect; } }
    }
}