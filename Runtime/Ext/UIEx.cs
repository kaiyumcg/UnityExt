using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityExt
{
    public static class UIEx
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

        //todo blink possibly do not work, test
        #region Fade
        public static List<Tween> ExFade(this List<MaskableGraphic> graphics, float alpha, float duration)
        {
            return _ExFade(graphics, alpha, duration, null, null, null);
        }
        public static List<Tween> ExFade(this List<MaskableGraphic> graphics, float alpha, float duration,
            MonoBehaviour mono, System.Action OnComplete)
        {
            return _ExFade(graphics, alpha, duration, mono, (list) => { OnComplete?.Invoke(); }, null);
        }
        public static List<Tween> ExFade(this List<MaskableGraphic> graphics, float alpha, float duration,
            params MaskableGraphic[] exceptions)
        {
            return _ExFade(graphics, alpha, duration, null, null, exceptions);
        }
        public static List<Tween> ExFade(this List<MaskableGraphic> graphics, float alpha, float duration,
            MonoBehaviour mono, System.Action OnComplete, params MaskableGraphic[] exceptions)
        {
            return _ExFade(graphics, alpha, duration, mono, (list) => { OnComplete?.Invoke(); }, exceptions);
        }
        public static List<Tween> ExFadeSequential(this List<MaskableGraphic> graphics, float alpha, float duration)
        {
            return _ExFade(graphics, alpha, duration, null, null, null, true);
        }
        public static List<Tween> ExFadeSequential(this List<MaskableGraphic> graphics, float alpha, float duration,
            MonoBehaviour mono, System.Action OnComplete)
        {
            return _ExFade(graphics, alpha, duration, mono, (list) => { OnComplete?.Invoke(); }, null, true);
        }
        public static List<Tween> ExFadeSequential(this List<MaskableGraphic> graphics, float alpha, float duration,
            params MaskableGraphic[] exceptions)
        {
            return _ExFade(graphics, alpha, duration, null, null, exceptions, true);
        }
        public static List<Tween> ExFadeSequential(this List<MaskableGraphic> graphics, float alpha, float duration,
            MonoBehaviour mono, System.Action OnComplete, params MaskableGraphic[] exceptions)
        {
            return _ExFade(graphics, alpha, duration, mono, (list) => { OnComplete?.Invoke(); }, exceptions, true);
        }
        static List<Tween> _ExFade(List<MaskableGraphic> graphics, float alpha, float duration,
            MonoBehaviour mono, System.Action<List<Tween>> OnComplete, MaskableGraphic[] exceptions, bool sequential = false)
        {
            if (graphics.ExIsValid() == false) { return null; }

            var result = new List<Tween>();
            result = result.ExGetListWithCount(graphics.ExNotNullCount());
            int validExceptionCount = 0;
            if (exceptions.ExIsValid())
            {
                validExceptionCount = exceptions.ExNotNullCount();
            }

            var completedCount = 0;
            if (sequential == false)
            {
                graphics.ExForEach((graphic, i) =>
                {
                    if (graphic != null)
                    {
                        var doIt = true;
                        if (exceptions.ExIsValid())
                        {
                            if (exceptions.ExContains(graphic)) { doIt = false; }
                        }
                        if (doIt)
                        {
                            result[i] = graphic.DOFade(alpha, duration).OnComplete(() =>
                            {
                                completedCount++;
                            });
                        }
                    }
                });
            }

            if (mono != null)
            {
                mono.StartCoroutine(COR());
            }
            return result;
            IEnumerator COR()
            {
                if (sequential)
                {
                    if (graphics.ExIsValid())
                    {
                        for (int i = 0; i < graphics.Count; i++)
                        {
                            var graphic = graphics[i];
                            if (graphic == null) { continue; }
                            var doIt = true;
                            if (exceptions.ExIsValid())
                            {
                                if (exceptions.ExContains(graphic)) { doIt = false; }
                            }
                            if (doIt)
                            {
                                var done = false;
                                result[i] = graphic.DOFade(alpha, duration).OnComplete(() =>
                                {
                                    completedCount++;
                                    done = true;
                                });
                                while (!done) { yield return null; }
                            }
                        }
                    }
                }
                else
                {
                    var exceptionValid = exceptions.ExIsValid();
                    while (true)
                    {
                        if (exceptionValid)
                        {
                            if (completedCount >= (graphics.Count - validExceptionCount)) { break; }
                        }
                        else
                        {
                            if (completedCount >= graphics.Count) { break; }
                        }
                        yield return null;
                    }
                }
                OnComplete?.Invoke(result);
            }
        }
        #endregion

        #region Color
        public static List<Tween> ExColor(this List<MaskableGraphic> graphics, Color endColor, float duration)
        {
            return _ExColor(graphics, endColor, duration, null, null, null);
        }
        public static List<Tween> ExColor(this List<MaskableGraphic> graphics, Color endColor, float duration,
            MonoBehaviour mono, System.Action OnComplete)
        {
            return _ExColor(graphics, endColor, duration, mono, (list) => { OnComplete?.Invoke(); }, null);
        }
        public static List<Tween> ExColor(this List<MaskableGraphic> graphics, Color endColor, float duration,
            params MaskableGraphic[] exceptions)
        {
            return _ExColor(graphics, endColor, duration, null, null, exceptions);
        }
        public static List<Tween> ExColor(this List<MaskableGraphic> graphics, Color endColor, float duration,
            MonoBehaviour mono, System.Action OnComplete, params MaskableGraphic[] exceptions)
        {
            return _ExColor(graphics, endColor, duration, mono, (list) => { OnComplete?.Invoke(); }, exceptions);
        }
        public static List<Tween> ExColorSequential(this List<MaskableGraphic> graphics, Color endColor, float duration)
        {
            return _ExColor(graphics, endColor, duration, null, null, null, true);
        }
        public static List<Tween> ExColorSequential(this List<MaskableGraphic> graphics, Color endColor, float duration,
            MonoBehaviour mono, System.Action OnComplete)
        {
            return _ExColor(graphics, endColor, duration, mono, (list) => { OnComplete?.Invoke(); }, null, true);
        }
        public static List<Tween> ExColorSequential(this List<MaskableGraphic> graphics, Color endColor, float duration,
            params MaskableGraphic[] exceptions)
        {
            return _ExColor(graphics, endColor, duration, null, null, exceptions, true);
        }
        public static List<Tween> ExColorSequential(this List<MaskableGraphic> graphics, Color endColor, float duration,
            MonoBehaviour mono, System.Action OnComplete, params MaskableGraphic[] exceptions)
        {
            return _ExColor(graphics, endColor, duration, mono, (list) => { OnComplete?.Invoke(); }, exceptions, true);
        }
        static List<Tween> _ExColor(List<MaskableGraphic> graphics, Color endColor, float duration,
            MonoBehaviour mono, System.Action<List<Tween>> OnComplete, MaskableGraphic[] exceptions, bool sequential = false)
        {
            if (graphics.ExIsValid() == false) { return null; }

            var result = new List<Tween>();
            result = result.ExGetListWithCount(graphics.ExNotNullCount());
            int validExceptionCount = 0;
            if (exceptions.ExIsValid())
            {
                validExceptionCount = exceptions.ExNotNullCount();
            }

            var completedCount = 0;
            if (sequential == false)
            {
                graphics.ExForEach((graphic, i) =>
                {
                    if (graphic != null)
                    {
                        var doIt = true;
                        if (exceptions.ExIsValid())
                        {
                            if (exceptions.ExContains(graphic)) { doIt = false; }
                        }
                        if (doIt)
                        {
                            result[i] = graphic.DOColor(endColor, duration).OnComplete(() =>
                            {
                                completedCount++;
                            });
                        }
                    }
                });
            }

            if (mono != null)
            {
                mono.StartCoroutine(COR());
            }
            return result;
            IEnumerator COR()
            {
                if (sequential)
                {
                    if (graphics.ExIsValid())
                    {
                        for (int i = 0; i < graphics.Count; i++)
                        {
                            var graphic = graphics[i];
                            if (graphic == null) { continue; }
                            var doIt = true;
                            if (exceptions.ExIsValid())
                            {
                                if (exceptions.ExContains(graphic)) { doIt = false; }
                            }
                            if (doIt)
                            {
                                var done = false;
                                result[i] = graphic.DOColor(endColor, duration).OnComplete(() =>
                                {
                                    completedCount++;
                                    done = true;
                                });
                                while (!done) { yield return null; }
                            }
                        }
                    }
                }
                else
                {
                    var exceptionValid = exceptions.ExIsValid();
                    while (true)
                    {
                        if (exceptionValid)
                        {
                            if (completedCount >= (graphics.Count - validExceptionCount)) { break; }
                        }
                        else
                        {
                            if (completedCount >= graphics.Count) { break; }
                        }
                        yield return null;
                    }
                }
                OnComplete?.Invoke(result);
            }
        }
        #endregion

        #region FillAmount
        public static Tween ExFillAmount(this Image image, float fillTarget, float duration, System.Action<float> OnUpdate, System.Action OnComplete, Ease ease = Ease.Linear)
        {
            float tvar = image.fillAmount;
            var tw = DOTween.To(() =>
            {
                return tvar;
            },
           x =>
           {
               tvar = x;
               image.fillAmount = tvar;
               OnUpdate?.Invoke(tvar);
           }
           , fillTarget, duration).OnComplete((() =>
           {
               OnComplete?.Invoke();
           })).SetEase(Ease.Linear);
            return tw;
        }
        public static List<Tween> ExFillAmount(this List<Image> images, float fillTarget, float duration)
        {
            return _ExFillAmount(images, fillTarget, duration, null, null, null);
        }
        public static List<Tween> ExFillAmount(this List<Image> images, float fillTarget, float duration,
            MonoBehaviour mono, System.Action OnComplete)
        {
            return _ExFillAmount(images, fillTarget, duration, mono, (list) => { OnComplete?.Invoke(); }, null);
        }
        public static List<Tween> ExFillAmount(this List<Image> images, float fillTarget, float duration,
            params Image[] exceptions)
        {
            return _ExFillAmount(images, fillTarget, duration, null, null, exceptions);
        }
        public static List<Tween> ExFillAmount(this List<Image> images, float fillTarget, float duration,
            MonoBehaviour mono, System.Action OnComplete, params Image[] exceptions)
        {
            return _ExFillAmount(images, fillTarget, duration, mono, (list) => { OnComplete?.Invoke(); }, exceptions);
        }
        public static List<Tween> ExFillAmountSequential(this List<Image> images, float fillTarget, float duration)
        {
            return _ExFillAmount(images, fillTarget, duration, null, null, null, true);
        }
        public static List<Tween> ExFillAmountSequential(this List<Image> images, float fillTarget, float duration,
            MonoBehaviour mono, System.Action OnComplete)
        {
            return _ExFillAmount(images, fillTarget, duration, mono, (list) => { OnComplete?.Invoke(); }, null, true);
        }
        public static List<Tween> ExFillAmountSequential(this List<Image> images, float fillTarget, float duration,
            params Image[] exceptions)
        {
            return _ExFillAmount(images, fillTarget, duration, null, null, exceptions, true);
        }
        public static List<Tween> ExFillAmountSequential(this List<Image> images, float fillTarget, float duration,
            MonoBehaviour mono, System.Action OnComplete, params Image[] exceptions)
        {
            return _ExFillAmount(images, fillTarget, duration, mono, (list) => { OnComplete?.Invoke(); }, exceptions, true);
        }
        static List<Tween> _ExFillAmount(List<Image> images, float fillTarget, float duration,
            MonoBehaviour mono, System.Action<List<Tween>> OnComplete, Image[] exceptions, bool sequential = false)
        {
            if (images.ExIsValid() == false) { return null; }

            var result = new List<Tween>();
            result = result.ExGetListWithCount(images.ExNotNullCount());
            int validExceptionCount = 0;
            if (exceptions.ExIsValid())
            {
                validExceptionCount = exceptions.ExNotNullCount();
            }

            var completedCount = 0;
            if (sequential == false)
            {
                images.ExForEach((graphic, i) =>
                {
                    if (graphic != null)
                    {
                        var doIt = true;
                        if (exceptions.ExIsValid())
                        {
                            if (exceptions.ExContains(graphic)) { doIt = false; }
                        }
                        if (doIt)
                        {
                            result[i] = graphic.DOFillAmount(fillTarget, duration).OnComplete(() =>
                            {
                                completedCount++;
                            });
                        }
                    }
                });
            }

            if (mono != null)
            {
                mono.StartCoroutine(COR());
            }
            return result;
            IEnumerator COR()
            {
                if (sequential)
                {
                    if (images.ExIsValid())
                    {
                        for (int i = 0; i < images.Count; i++)
                        {
                            var graphic = images[i];
                            if (graphic == null) { continue; }
                            var doIt = true;
                            if (exceptions.ExIsValid())
                            {
                                if (exceptions.ExContains(graphic)) { doIt = false; }
                            }
                            if (doIt)
                            {
                                var done = false;
                                result[i] = graphic.DOFillAmount(fillTarget, duration).OnComplete(() =>
                                {
                                    completedCount++;
                                    done = true;
                                });
                                while (!done) { yield return null; }
                            }
                        }
                    }
                }
                else
                {
                    var exceptionValid = exceptions.ExIsValid();
                    while (true)
                    {
                        if (exceptionValid)
                        {
                            if (completedCount >= (images.Count - validExceptionCount)) { break; }
                        }
                        else
                        {
                            if (completedCount >= images.Count) { break; }
                        }
                        yield return null;
                    }
                }
                OnComplete?.Invoke(result);
            }
        }
        #endregion

        #region Blink
        public static Coroutine ExBlinkContinue(this MaskableGraphic graphic, MonoBehaviour scriptCaller, float cycleTime)
        {
            return _ExBlink(scriptCaller, graphic, null, cycleTime, -1f, null);
        }
        public static Coroutine ExBlinkUntil(this MaskableGraphic graphic, MonoBehaviour scriptCaller,
            float cycleTime, WhenToDoFunc stopperCondition, System.Action OnComplete = null)
        {
            return _ExBlink(scriptCaller, graphic, stopperCondition, cycleTime, -1f, OnComplete);
        }
        public static Coroutine ExBlinkUntil(this MaskableGraphic graphic, MonoBehaviour scriptCaller,
            float cycleTime, float maxTime, System.Action OnComplete = null)
        {
            return _ExBlink(scriptCaller, graphic, null, cycleTime, maxTime, OnComplete);
        }
        public static Coroutine ExBlinkUntilConditionOrTime(this MaskableGraphic graphic, MonoBehaviour scriptCaller,
            float cycleTime, float maxTime, WhenToDoFunc stopperCondition, System.Action OnComplete = null)
        {
            return _ExBlink(scriptCaller, graphic, stopperCondition, cycleTime, maxTime, OnComplete);
        }
        static Coroutine _ExBlink(MonoBehaviour mono, MaskableGraphic graphic,
            WhenToDoFunc stopperCondition, float cycleTime, float maxTime, System.Action OnComplete)
        {
            return mono.StartCoroutine(TapToStartBlink(graphic, stopperCondition, cycleTime, maxTime, OnComplete));
            IEnumerator TapToStartBlink(MaskableGraphic graphic, WhenToDoFunc stopperCondition,
                float cycleTime, float maxTime, System.Action OnComplete)
            {
                graphic.gameObject.SetActive(true);
                var timer = 0.0f;
                while (true)
                {
                    if ((maxTime > 0.0f && timer > maxTime) || (stopperCondition != null && stopperCondition.Invoke()))
                    {
                        graphic.ExSetAlpha(0.0f);
                        OnComplete?.Invoke();
                        yield break;
                    }

                    {
                        var fadeIn = false;
                        graphic.ExSetAlpha(0.0f);
                        graphic.DOFade(1.0f, cycleTime).OnComplete(() => { fadeIn = true; });
                        while (!fadeIn)
                        {
                            if (stopperCondition != null && stopperCondition.Invoke()) { OnComplete?.Invoke(); yield break; }

                            timer += Time.deltaTime;
                            yield return null;
                        }
                    }

                    {
                        var fadeOut = false;
                        graphic.ExSetAlpha(1.0f);
                        graphic.DOFade(0.0f, cycleTime).OnComplete(() => { fadeOut = true; });
                        while (!fadeOut)
                        {
                            if (stopperCondition != null && stopperCondition.Invoke()) { OnComplete?.Invoke(); yield break; }

                            timer += Time.deltaTime;
                            yield return null;
                        }
                    }
                    yield return null;
                }
            }
        }
        #endregion
    }
}