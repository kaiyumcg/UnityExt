using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

namespace UnityExt
{
    [System.Serializable]
    public class PlayGameParticle : IEffectPlay
    {
        [SerializeField] GameParticle effectPrefab;
        [SerializeField] Vector3 positionalOffset, rotationalOffset;
        [SerializeField] Vector3 scale = Vector3.one;
        [Header("If set none then position comes from caller context")]
        [SerializeField] Transform spawnPosition = null;
        [Header("Effects will be under this transform in hierarchy")]
        [SerializeField] Transform holder;
        [Header("Negative Or Zero means it will not destroy, ever!")]
        [SerializeField] float lifeTime = -1.0f;
        GameParticle spawnedEffect;
        void IEffectPlay.CleanupForPool(MonoBehaviour callerContext)
        {
            if (spawnedEffect == null)
            {
                Debug.LogWarning("You have not spawned this effect, yet you are trying to clean it up for pool. Ignoring");
                return;
            }
            spawnedEffect.Init();
        }

        object ICloneable.Clone()
        {
            var newInst = new PlayGameParticle
            {
                effectPrefab = effectPrefab,
                positionalOffset = positionalOffset,
                rotationalOffset = rotationalOffset,
                scale = scale,
                lifeTime = lifeTime
            };
            return newInst;
        }

        void IEffectPlay.Play(MonoBehaviour callerContext, System.Action OnComplete, bool destroyAtEnd)
        {
            if (spawnedEffect == null) 
            {
                Debug.LogWarning("You have not spawned this effect, yet you are trying to play it. Ignoring");
                return; 
            }
            spawnedEffect.Play();
            if (lifeTime > 0.0f)
            {
                callerContext.StartCoroutine(COR());
                IEnumerator COR()
                {
                    yield return new WaitForSeconds(lifeTime);
                    if (destroyAtEnd)
                    {
                        GameObject.Destroy(spawnedEffect.gameObject);
                    }
                    else
                    {
                        spawnedEffect.gameObject.SetActive(false);
                    }
                    OnComplete?.Invoke();
                }
            }
        }
        void IEffectPlay.Spawn(MonoBehaviour callerContext)
        {
            var clone = GameObject.Instantiate(effectPrefab.gameObject) as GameObject;
            var cloneTr = clone.transform;
            cloneTr.SetParent(holder);
            cloneTr.ExResetLocal();
            var root = callerContext.transform;
            cloneTr.position = spawnPosition == null ?
                root.position + positionalOffset : spawnPosition.position + positionalOffset;
            cloneTr.rotation = spawnPosition == null ? root.rotation : spawnPosition.rotation;
            cloneTr.localEulerAngles += rotationalOffset;
            cloneTr.localScale = scale;
            var effect = clone.GetComponent<GameParticle>();
            effect.Init();
            spawnedEffect = effect;
        }
        void IEffectPlay.SpawnAndPlay(MonoBehaviour callerContext, System.Action OnComplete, bool destroyAtEnd)
        {
            var inst = this as IEffectPlay;
            inst.Spawn(callerContext);
            inst.Play(callerContext, OnComplete, destroyAtEnd);
        }
    }
}