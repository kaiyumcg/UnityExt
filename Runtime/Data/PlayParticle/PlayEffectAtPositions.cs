using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityExt
{
    [System.Serializable]
    public class PlayEffectAtPositions : IEffectPlay
    {
        [SerializeField] ManyEffectPlayMode mode = ManyEffectPlayMode.AtOnce;
        [SerializeField] GameParticle effectPrefab;
        [SerializeField] List<Transform> spawnPositions;
        [SerializeField] float lifeTime = -1f, serialInterval = 0.3f;
        [SerializeField] Transform holder;

        object ICloneable.Clone()
        {
            return default;
        }
        IEnumerator PlaySingle(Transform spawnPos)
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
                GameObject.Destroy(clone);
            }
        }

        void IEffectPlay.SpawnAndPlay(MonoBehaviour callerContext)
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
                        callerContext.StartCoroutine(PlaySingle(pos));
                        if (mode == ManyEffectPlayMode.Serially)
                        {
                            yield return new WaitForSeconds(serialInterval);
                        }
                    }
                }
            }
        }
    }
}