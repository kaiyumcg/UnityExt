using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityExt
{
    [System.Serializable]
    public class PlayRareGameParticle : IEffectPlay
    {
        [SerializeField] string prefabIdentifier = "coin";
        [SerializeField] ListEffectPlayMode sampleMode = ListEffectPlayMode.Random;
        [SerializeField] List<GameParticle> normalEffects, rareEffects;
        [SerializeField] int normalEffectCount = 5;
        [SerializeField, Range(0.0f, 1.0f)] float rareEffectProbabilityAfterNormalCount = 0.8f;

        [Header("normalOffsets")]
        [SerializeField] Vector3 normalPositionalOffset = Vector3.zero;
        [SerializeField] Vector3 normalRotationalOffset = Vector3.zero;
        [SerializeField] Vector3 normalScale = Vector3.one;

        [Header("rareOffsets")]
        [SerializeField] Vector3 rarePositionalOffset = Vector3.zero;
        [SerializeField] Vector3 rareRotationalOffset = Vector3.zero;
        [SerializeField] Vector3 rareScale = Vector3.one;
        [Header("If set none then position comes from caller context")]
        [SerializeField] Transform spawnPosition = null;
        [Header("Effects will be under this transform in hierarchy")]
        [SerializeField] Transform holder;
        [Header("Negative Or Zero means it will not destroy, ever!")]
        [SerializeField] float lifeTime = -1.0f;
        static List<IdentifierData> idData = new List<IdentifierData>();
        GameParticle spawnedEffect;
        public class IdentifierData
        {
            public string identifier = "";
            public int normalID = 0, rareID = 0;
            public int normalCount = 0;
        }

        IdentifierData GetOrCreateIdentifier(string identifier, ref int dataIndex)
        {
            IdentifierData result = null;
            if (idData == null) { idData = new List<IdentifierData>(); }
            idData.ExRemoveNulls();
            if (idData == null) { idData = new List<IdentifierData>(); }

            var _id = 0;
            idData.ExForEachSafe((i, Index) =>
            {
                if (i != null && i.identifier == identifier)
                {
                    result = i;
                    _id = Index;
                }
            });

            if (result == null)
            {
                result = new IdentifierData { identifier = identifier, normalID = 0, rareID = 0, normalCount = 0 };
                _id = idData.Count;
            }
            idData.Add(result);
            dataIndex = _id;
            return result;
        }

        object ICloneable.Clone()
        {
            var newInst = new PlayRareGameParticle
            {
                sampleMode = sampleMode,
                normalEffects = normalEffects,
                rareEffects = rareEffects,
                normalEffectCount = normalEffectCount,
                rareEffectProbabilityAfterNormalCount = rareEffectProbabilityAfterNormalCount,
                normalPositionalOffset = normalPositionalOffset,
                normalRotationalOffset = normalRotationalOffset,
                normalScale = normalScale,
                rarePositionalOffset = rarePositionalOffset,
                rareRotationalOffset = rareRotationalOffset,
                rareScale = rareScale,
                lifeTime = lifeTime
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
            var index = 0;
            var id = GetOrCreateIdentifier(prefabIdentifier, ref index);
            GameParticle selEff;
            bool isRare = false;
            if (id.normalCount >= normalEffectCount)
            {
                id.normalCount = 0;
                if (UnityEngine.Random.value < rareEffectProbabilityAfterNormalCount)
                {
                    isRare = true;
                }
            }

            if (sampleMode == ListEffectPlayMode.Random)
            {
                var effectPrefabs = isRare ? rareEffects : normalEffects;
                selEff = effectPrefabs[UnityEngine.Random.Range(0, effectPrefabs.Count)];
                if (!isRare) { id.normalCount++; }
            }
            else
            {
                if (isRare)
                {
                    selEff = rareEffects[id.rareID];
                    id.rareID++;
                    if (id.rareID > rareEffects.Count - 1) { id.rareID = 0; }
                }
                else
                {
                    selEff = normalEffects[id.normalID];
                    id.normalID++;
                    if (id.normalID > normalEffects.Count - 1) { id.normalID = 0; }
                    id.normalCount++;
                }
            }

            idData[index] = id;

            var clone = GameObject.Instantiate(selEff.gameObject) as GameObject;
            var cloneTr = clone.transform;
            cloneTr.SetParent(holder);
            cloneTr.ExResetLocal();
            var root = callerContext.transform;
            cloneTr.position = spawnPosition == null ?
                root.position + (isRare ? rarePositionalOffset : normalPositionalOffset) :
                spawnPosition.position + (isRare ? rarePositionalOffset : normalPositionalOffset);
            cloneTr.rotation = spawnPosition == null ? root.rotation : spawnPosition.rotation;
            cloneTr.localEulerAngles += isRare ? rareRotationalOffset : normalRotationalOffset;
            cloneTr.localScale = isRare ? rareScale : normalScale;
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