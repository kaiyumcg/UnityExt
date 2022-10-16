using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityExt
{
    public static class OptimizerEx
    {
        public static void ExOptimizeSkinnedRenderersInside(this Transform tr, bool shadow, bool occlusionCulling)
        {
            var rnds1 = tr.GetComponentsInChildren<SkinnedMeshRenderer>();
            if (rnds1 != null && rnds1.Length > 0)
            {
                for (int j = 0; j < rnds1.Length; j++)
                {
                    var rn = rnds1[j];
                    if (rn == null) { continue; }
                    rn.shadowCastingMode = shadow ? UnityEngine.Rendering.ShadowCastingMode.On : UnityEngine.Rendering.ShadowCastingMode.Off;
                    rn.receiveShadows = shadow;
                    rn.lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;
                    rn.motionVectorGenerationMode = MotionVectorGenerationMode.ForceNoMotion;
                    rn.reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;
                    rn.allowOcclusionWhenDynamic = occlusionCulling;
                    rn.quality = SkinQuality.Bone1;
                    rn.skinnedMotionVectors = false;
                }
            }
        }
        public static void ExOptimizeMeshRenderersInside(this Transform tr, bool shadow, bool occlusionCulling)
        {
            var rnds = tr.GetComponentsInChildren<MeshRenderer>();
            if (rnds != null && rnds.Length > 0)
            {
                for (int i = 0; i < rnds.Length; i++)
                {
                    var rn = rnds[i];
                    if (rn == null) { continue; }
                    rn.shadowCastingMode = shadow ? UnityEngine.Rendering.ShadowCastingMode.On : UnityEngine.Rendering.ShadowCastingMode.Off;
                    rn.receiveShadows = shadow;
                    rn.lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;
                    rn.lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;
                    rn.motionVectorGenerationMode = MotionVectorGenerationMode.ForceNoMotion;
                    rn.reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;
                    rn.allowOcclusionWhenDynamic = occlusionCulling;
                }
            }
        }
        public static void ExOptimize(this SkinnedMeshRenderer renderer, bool shadow, bool occlusionCulling)
        {
            renderer.shadowCastingMode = shadow ? UnityEngine.Rendering.ShadowCastingMode.On : UnityEngine.Rendering.ShadowCastingMode.Off;
            renderer.receiveShadows = shadow;
            renderer.lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;
            renderer.motionVectorGenerationMode = MotionVectorGenerationMode.ForceNoMotion;
            renderer.reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;
            renderer.allowOcclusionWhenDynamic = occlusionCulling;
            renderer.quality = SkinQuality.Bone1;
            renderer.skinnedMotionVectors = false;
        }
        public static void ExOptimize(this MeshRenderer renderer, bool shadow, bool occlusionCulling)
        {
            renderer.shadowCastingMode = shadow ? UnityEngine.Rendering.ShadowCastingMode.On : UnityEngine.Rendering.ShadowCastingMode.Off;
            renderer.receiveShadows = shadow;
            renderer.lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;
            renderer.motionVectorGenerationMode = MotionVectorGenerationMode.ForceNoMotion;
            renderer.reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;
            renderer.allowOcclusionWhenDynamic = occlusionCulling;
        }
    }
}