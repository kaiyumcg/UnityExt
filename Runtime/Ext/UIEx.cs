using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityExt
{
    public static class ExUI
    {
        #region Alpha
        public static void ExSetAlpha(this MaskableGraphic graphic, float alpha)
        {
            var col = graphic.color;
            col.a = alpha;
            graphic.color = col;
        }
        public static void ExSetAlpha(this List<MaskableGraphic> graphics, float alpha, params MaskableGraphic[] exceptions)
        {
            if (graphics != null && graphics.Count > 0)
            {
                for (int i = 0; i < graphics.Count; i++)
                {
                    var graphic = graphics[i];
                    if (graphic == null) { continue; }
                    if (exceptions != null)
                    {
                        if (exceptions.ExContains(graphic)) { continue; }
                    }
                    graphic.ExSetAlpha(alpha);
                }
            }
        }
        #endregion

        #region Activation
        public static void ExSetActive(this List<MaskableGraphic> graphics, bool enable)
        {
            if (graphics != null && graphics.Count > 0)
            {
                for (int i = 0; i < graphics.Count; i++)
                {
                    var graphic = graphics[i];
                    if (graphic == null) { continue; }
                    graphic.enabled = enable;
                }
            }
        }
        public static void ExSetActive(this List<MaskableGraphic> graphics, bool enable, params MaskableGraphic[] exceptions)
        {
            if (graphics != null && graphics.Count > 0)
            {
                for (int i = 0; i < graphics.Count; i++)
                {
                    var graphic = graphics[i];
                    if (graphic == null) { continue; }
                    if (exceptions != null)
                    {
                        if (exceptions.ExContains(graphic)) { continue; }
                    }
                    graphic.enabled = enable;
                }
            }
        }
        #endregion
    }
}