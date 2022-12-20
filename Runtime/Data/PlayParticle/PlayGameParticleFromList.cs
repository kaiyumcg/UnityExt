using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityExt
{
    [System.Serializable]
    public class PlayGameParticleFromList : IEffectPlay
    {
        [SerializeField] ListEffectPlayMode mode = ListEffectPlayMode.Random;
        [SerializeField] List<GameParticle> effectPrefabs;
        [SerializeField] Vector3 positionalOffset, rotationalOffset;
        [SerializeField] Vector3 scale = Vector3.one;
        [Header("If set none then position comes from caller context")]
        [SerializeField] Transform spawnPosition = null;
        [SerializeField] bool takePositionFromList = false;
        [SerializeField] List<Transform> spawnPositions = null;
        [Header("Effects will be under this transform in hierarchy")]
        [SerializeField] Transform holder;
        [Header("Negative Or Zero means it will not destroy, ever!")]
        [SerializeField] float lifeTime = -1.0f;
        int id = 0;
        int overrideID = 0;
        GameParticle spawnedEffect;
        object ICloneable.Clone()
        {
            var newInst = new PlayGameParticleFromList
            {
                mode = mode,
                effectPrefabs = effectPrefabs,
                positionalOffset = positionalOffset,
                rotationalOffset = rotationalOffset,
                scale = scale,
                lifeTime = lifeTime,
                takePositionFromList = takePositionFromList
            };
            return newInst;
        }
        void IEffectPlay.Play(MonoBehaviour callerContext, Action OnComplete, bool destroyAtEnd)
        {
            if (spawnedEffect == null)
            {
                Debug.LogWarning("You have not spawned this effect, yet you are trying to play it. Ignoring");
                return;
            }
            spawnedEffect.Play();
            if (lifeTime >= 0.0f)
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
        void IEffectPlay.CleanupForPool(MonoBehaviour callerContext)
        {
            if (spawnedEffect == null)
            {
                Debug.LogWarning("You have not spawned this effect, yet you are trying to clean it up for pool. Ignoring");
                return;
            }
            spawnedEffect.Init();
        }
        void IEffectPlay.Spawn(MonoBehaviour callerContext)
        {
            GameParticle selEff;
            if (mode == ListEffectPlayMode.Random)
            {
                selEff = effectPrefabs[UnityEngine.Random.Range(0, effectPrefabs.Count)];
            }
            else
            {
                selEff = effectPrefabs[id];
                id++;
                if (id > effectPrefabs.Count - 1) { id = 0; }
            }

            var clone = GameObject.Instantiate(selEff.gameObject) as GameObject;
            var cloneTr = clone.transform;
            cloneTr.SetParent(holder);
            cloneTr.ExResetLocal();
            var root = callerContext.transform;

            var overrideTr = spawnPosition;
            if (takePositionFromList)
            {
                overrideTr = spawnPositions[overrideID];
                if (overrideTr != null)
                {
                    overrideID++;
                    if (id > effectPrefabs.Count - 1) { overrideID = 0; }
                }
            }

            cloneTr.position = overrideTr == null ?
                root.position + positionalOffset : overrideTr.position + positionalOffset;
            cloneTr.rotation = overrideTr == null ? root.rotation : overrideTr.rotation;
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