using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityExt
{
    public static class ExGetComp
    {
        public static T ExGetComponentInParent<T>(this GameObject g, bool includeInactive = false)
        {
            var ts = g.GetComponentsInParent<T>(includeInactive);
            return ts == null || ts.Length == 0 ? default : ts[0];
        }

        public static T ExGetComponentInParent<T>(this Transform t, bool includeInactive = false)
        {
            var ts = t.GetComponentsInParent<T>(includeInactive);
            return ts == null || ts.Length == 0 ? default : ts[0];
        }

        public static T ExGetComponentInParent<T>(this MonoBehaviour script, bool includeInactive = false)
        {
            var ts = script.GetComponentsInParent<T>(includeInactive);
            return ts == null || ts.Length == 0 ? default : ts[0];
        }

        public static T ExGetComponentInParent<T>(this Component component, bool includeInactive = false)
        {
            var ts = component.GetComponentsInParent<T>(includeInactive);
            return ts == null || ts.Length == 0 ? default : ts[0];
        }
        public static List<T> ExGetComponentsInChildrenSerially<T>(this Transform t, bool includeInactive = false)
        {
            return GetComInChildrenSerially(t, includeInactive);
            List<T> GetComInChildrenSerially(Transform startTransform, bool includeInactive)
            {
                var result = new List<T>();
                GetCom(ref result, startTransform);
                return result;
                void GetCom(ref List<T> compList, Transform t)
                {
                    if (compList == null) { compList = new List<T>(); }
                    var com = t.GetComponent<T>();
                    if (com != null && (t.gameObject.activeInHierarchy || includeInactive))
                    {
                        compList.Add(com);
                    }
                    if (t.childCount > 0)
                    {
                        for (int i = 0; i < t.childCount; i++)
                        {
                            var ch = t.GetChild(i);
                            GetCom(ref compList, ch);
                        }
                    }
                }
            }
        }
        public static List<T> ExGetComponentsInParentSerially<T>(this Transform t, bool includeInactive = false)
        {
            return GetComInParentSerially(t, includeInactive);
            List<T> GetComInParentSerially(Transform startTransform, bool includeInactive)
            {
                var result = new List<T>();
                GetCom(ref result, startTransform);
                return result;
                void GetCom(ref List<T> compList, Transform t)
                {
                    var ptr = t;
                    while (ptr != null)
                    {
                        var com = ptr.GetComponent<T>();
                        if (com != null && (ptr.gameObject.activeInHierarchy || includeInactive))
                        {
                            compList.Add(com);
                        }
                        ptr = ptr.parent;
                    }
                }
            }
        }
    }
}