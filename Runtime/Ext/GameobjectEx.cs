using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace UnityExt
{
    public static class GameobjectEx
    {
        public static void ExSetActiveObjects(this List<GameObject> objectLists, bool active)
        {
            if (objectLists != null && objectLists.Count > 0)
            {
                for (int i = 0; i < objectLists.Count; i++)
                {
                    var obj = objectLists[i];
                    if (obj == null) { continue; }
                    if (obj.activeInHierarchy == active) { continue; }
                    obj.SetActive(active);
                }
            }
        }
        public static void ExSetLayer(this GameObject gameObject, string layerName)
        {
            gameObject.layer = LayerMask.NameToLayer(layerName);
        }
        public static void ExSetLayerToAllChilds(this GameObject gameObject, string layerName)
        {
            gameObject.transform.ExForEachChilds((t) =>
            {
                t.gameObject.ExSetLayer(layerName);
            });
        }
        public static void ExSetLayerToImmediateChilds(this GameObject gameObject, string layerName)
        {
            gameObject.transform.ExForEachImmediateChilds((t) =>
            {
                t.gameObject.ExSetLayer(layerName);
            });
        }
        public static void ExSetLayerToParents(this GameObject gameObject, string layerName)
        {
            gameObject.transform.ExForEachParentOnChain((t) =>
            {
                t.gameObject.ExSetLayer(layerName);
            });
        }
    }
}