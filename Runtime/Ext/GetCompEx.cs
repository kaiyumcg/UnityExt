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
    }
}