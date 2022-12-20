using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityExt
{
    [System.Serializable]
    public class PlayEffectAtPositions : IEffectPlay
    {
        [SerializeField] ManyEffectPlayMode mode = ManyEffectPlayMode.AtOnce;
        [SerializeField] GameParticle effectPrefab;
        [SerializeField] List<Transform> spawnPositions;
        [Header("Effects will be under this transform in hierarchy")]
        [SerializeField] Transform holder;
        [Header("Negative Or Zero means it will not destroy, ever!")]
        [SerializeField] float lifeTime = -1f;
        [SerializeField] float serialInterval = 0.3f;
        readonly List<GameParticle> gameParticles = new();
        object ICloneable.Clone()
        {
            var newInst = new PlayEffectAtPositions
            {
                mode = mode,
                lifeTime = lifeTime,
                serialInterval = serialInterval
            };
            return newInst;
        }
        IEnumerator SpawnAndPlaySingle(Transform spawnPos, bool destroyAtEnd)
        {
            var clone = GameObject.Instantiate(effectPrefab.gameObject) as GameObject;
            var cloneTr = clone.transform;
            cloneTr.SetParent(holder, true);

            cloneTr.position = spawnPos.position;
            var effect = clone.GetComponent<GameParticle>();
            effect.Init();
            effect.Play();
            if (lifeTime > 0.0f)
            {
                yield return new WaitForSeconds(lifeTime);
                if (destroyAtEnd)
                {
                    GameObject.Destroy(clone.gameObject);
                }
                else
                {
                    clone.gameObject.SetActive(false);
                }
            }
        }
        IEnumerator PlaySingle(GameParticle effect, bool destroyAtEnd)
        {
            effect.Play();
            if (lifeTime > 0.0f)
            {
                yield return new WaitForSeconds(lifeTime);
                if (destroyAtEnd)
                {
                    GameObject.Destroy(effect.gameObject);
                }
                else
                {
                    effect.gameObject.SetActive(false);
                }
            }
        }
        void SpawnSingle(Transform spawnPos)
        {
            var clone = GameObject.Instantiate(effectPrefab.gameObject) as GameObject;
            var cloneTr = clone.transform;
            cloneTr.SetParent(holder, true);

            cloneTr.position = spawnPos.position;
            var effect = clone.GetComponent<GameParticle>();
            effect.Init();
            gameParticles.Add(effect);
        }
        void IEffectPlay.CleanupForPool(MonoBehaviour callerContext)
        {
            if (gameParticles.ExIsValid() == false || gameParticles.ExHasAnyNull())
            {
                Debug.LogWarning("You have not spawned this effect or it is corrupted, yet you are trying to clean it up for pool. Ignoring");
                return;
            }
            gameParticles.ExForEachSafe((i) =>
            {
                i.Init();
            });   
        }
        void IEffectPlay.Play(MonoBehaviour callerContext, Action OnComplete, bool destroyAtEnd)
        {
            callerContext.StartCoroutine(COR());
            IEnumerator COR()
            {
                if (gameParticles != null && gameParticles.Count > 0)
                {
                    for (int i = 0; i < gameParticles.Count; i++)
                    {
                        var effect = gameParticles[i];
                        if (effect == null) { continue; }
                        callerContext.StartCoroutine(PlaySingle(effect, destroyAtEnd));
                        if (mode == ManyEffectPlayMode.Serially)
                        {
                            yield return new WaitForSeconds(serialInterval);
                        }
                    }
                }
                OnComplete?.Invoke();
            }
        }
        void IEffectPlay.Spawn(MonoBehaviour callerContext)
        {
            gameParticles.Clear();
            if (spawnPositions != null && spawnPositions.Count > 0)
            {
                for (int i = 0; i < spawnPositions.Count; i++)
                {
                    var pos = spawnPositions[i];
                    if (pos == null) { continue; }
                    SpawnSingle(pos);
                }
            }
        }
        void IEffectPlay.SpawnAndPlay(MonoBehaviour callerContext, System.Action OnComplete, bool destroyAtEnd)
        {
            callerContext.StartCoroutine(COR());
            IEnumerator COR()
            {
                if (spawnPositions != null && spawnPositions.Count > 0)
                {
                    for (int i = 0; i < spawnPositions.Count; i++)
                    {
                        var pos = spawnPositions[i];
                        if (pos == null) { continue; }
                        callerContext.StartCoroutine(SpawnAndPlaySingle(pos, destroyAtEnd));
                        if (mode == ManyEffectPlayMode.Serially)
                        {
                            yield return new WaitForSeconds(serialInterval);
                        }
                    }
                }
                OnComplete?.Invoke();
            }
        }
    }
}