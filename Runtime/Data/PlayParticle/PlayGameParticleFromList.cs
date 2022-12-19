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
        [SerializeField] Transform effectSpawnPositionOverride = null;
        [SerializeField] bool takeOverrideFromList = false;
        [SerializeField] List<Transform> spawnOverrideList = null;
        [SerializeField] Transform holder;
        [Header("Negative means it will not destroy, ever!")]
        [SerializeField] float lifeTime = -1.0f;
        int id = 0;
        int overrideID = 0;
        object ICloneable.Clone()
        {
            var newInst = new PlayGameParticleFromList
            {
                mode = mode,
                effectPrefabs = effectPrefabs,
                positionalOffset = positionalOffset,
                rotationalOffset = rotationalOffset,
                scale = scale,
                lifeTime = lifeTime
            };
            return newInst;
        }
        void IEffectPlay.SpawnAndPlay(MonoBehaviour callerContext)
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

            var overrideTr = effectSpawnPositionOverride;
            if (takeOverrideFromList)
            {
                overrideTr = spawnOverrideList[overrideID];
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