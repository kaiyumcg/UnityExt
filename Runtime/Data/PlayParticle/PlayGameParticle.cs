using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityExt
{
    [System.Serializable]
    public class PlayGameParticle : IEffectPlay
    {
        [SerializeField] GameParticle effectPrefab;
        [SerializeField] Vector3 positionalOffset, rotationalOffset;
        [SerializeField] Vector3 scale = Vector3.one;
        [SerializeField] Transform effectSpawnPositionOverride = null;
        [SerializeField] Transform holder;
        [Header("Negative means it will not destroy, ever!")]
        [SerializeField] float lifeTime = -1.0f;
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
        void IEffectPlay.SpawnAndPlay(MonoBehaviour callerContext)
        {
            var clone = GameObject.Instantiate(effectPrefab.gameObject) as GameObject;
            var cloneTr = clone.transform;
            cloneTr.SetParent(holder);
            cloneTr.ExResetLocal();
            var root = callerContext.transform;
            cloneTr.position = effectSpawnPositionOverride == null ?
                root.position + positionalOffset : effectSpawnPositionOverride.position + positionalOffset;
            cloneTr.rotation = effectSpawnPositionOverride == null ? root.rotation : effectSpawnPositionOverride.rotation;
            cloneTr.localEulerAngles += rotationalOffset;
            cloneTr.localScale = scale;
            var effect = clone.GetComponent<GameParticle>();
            effect.Init();
            effect.Play();

            if (lifeTime >= 0.0f)
            {
                callerContext.StartCoroutine(COR());
                IEnumerator COR()
                {
                    yield return new WaitForSeconds(lifeTime);
                    GameObject.Destroy(clone);
                }
            }
        }
    }
}