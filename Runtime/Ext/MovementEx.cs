using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityExt
{
    public struct ArcMoveDescription
    {
        public Vector3 from, to, rotationUpDir;
        public bool followRotation;
        public float rotationSpeed;
        public int forwardDirection;
    }
    public static class MovementEx
    {
        //todo global variants and other methods around for other axis
        //todo PingPong translation along xyz axis stopper condition
        //todo pingpong along custom axis
        //todo set rotation around x and z
        //todo move object in spline rather than beizier?
        //todo velocity of an agent transform based
        //todo move to certain pt with a velocity
        //todo directly average from an array of float or long or int or vector

        #region ScaleUpDown
        public static Coroutine ExScaleUpDownContinue(this Transform transform, MonoBehaviour scriptCaller, float from, float to, float cycleTime)
        {
            return _ExScaleUpDown(transform, scriptCaller, null, cycleTime, from, to, -1f, null);
        }
        public static Coroutine ExScaleUpDownUntil(this Transform transform, MonoBehaviour scriptCaller,
             float from, float to, float cycleTime, WhenToDoFunc stopperCondition, System.Action OnComplete = null)
        {
            return _ExScaleUpDown(transform, scriptCaller, stopperCondition, cycleTime, from, to, -1f, OnComplete);
        }
        public static Coroutine ExScaleUpDownUntil(this Transform transform, MonoBehaviour scriptCaller,
             float from, float to, float cycleTime, float maxTime, System.Action OnComplete = null)
        {
            return _ExScaleUpDown(transform, scriptCaller, null, cycleTime, from, to, maxTime, OnComplete);
        }
        public static Coroutine ExScaleUpDownUntilConditionOrTime(this Transform transform, MonoBehaviour scriptCaller, float cycleTime, float from, float to,
            float maxTime, WhenToDoFunc stopperCondition, System.Action OnComplete = null)
        {
            return _ExScaleUpDown(transform, scriptCaller, stopperCondition, cycleTime, from, to, maxTime, OnComplete);
        }

        static Coroutine _ExScaleUpDown(Transform transform, MonoBehaviour scriptCaller, WhenToDoFunc stopperCondition,
            float cycleTime, float from, float to, float maxTime, System.Action OnComplete)
        {
            return scriptCaller.StartCoroutine(ScaleUpDown(transform, stopperCondition, cycleTime, from, to, maxTime, OnComplete));
            IEnumerator ScaleUpDown(Transform tr, WhenToDoFunc stopperCondition,
                float cycleTime, float from, float to, float maxTime, System.Action OnComplete)
            {
                tr.gameObject.SetActive(true);
                var originalScale = tr.localScale;
                var fromScale = originalScale * (from);
                var toScale = originalScale * (to);
                var timer = 0.0f;
                while (true)
                {
                    if ((maxTime > 0.0f && timer > maxTime) || (stopperCondition != null && stopperCondition.Invoke()))
                    {
                        tr.localScale = originalScale;
                        OnComplete?.Invoke();
                        yield break;
                    }

                    {
                        var fadeIn = false;
                        tr.DOScale(toScale, cycleTime).OnComplete(() => { fadeIn = true; });
                        while (!fadeIn)
                        {
                            if (stopperCondition != null && stopperCondition.Invoke()) { OnComplete?.Invoke(); yield break; }

                            timer += Time.deltaTime;
                            yield return null;
                        }
                    }

                    {
                        var fadeOut = false;
                        tr.DOScale(fromScale, cycleTime).OnComplete(() => { fadeOut = true; });
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

        /// <summary>
        /// Might not be working with the list
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="amount"></param>
        /// <param name="within"></param>
        /// <param name="tweens"></param>
        /// <param name="OnComplete"></param>
        public static void ExPoc(this Transform transform, float amount, float within,
            List<TweenerCore<Vector3, Vector3, VectorOptions>> tweens, System.Action OnComplete = null)
        {
            if (tweens == null) { tweens = new List<TweenerCore<Vector3, Vector3, VectorOptions>>(); }
            var oriSc = transform.localScale;
            var targetSc = oriSc * (1 + amount);
            var tw1 = transform.DOScale(targetSc, within * 0.5f).OnComplete(() =>
            {
                var tw2 = transform.DOScale(oriSc, within * 0.5f).OnComplete(() =>
                {
                    OnComplete?.Invoke();
                });
                tweens.Add(tw2);
            });
            tweens.Add(tw1);
        }
        public static void ExPoc(this Transform transform, float amount, float within, System.Action OnComplete = null)
        {
            var oriSc = transform.localScale;
            var targetSc = oriSc * (1 + amount);
            transform.DOScale(targetSc, within * 0.5f).OnComplete(() =>
            {
                transform.DOScale(oriSc, within * 0.5f).OnComplete(() =>
                {
                    OnComplete?.Invoke();
                });
            });
        }

        //todo bug-vector one init scale nao hote pare!
        public static void ExPocWithInitScale(this Transform transform, Vector3 originalScale, float amount, float within, System.Action OnComplete = null)
        {
            var targetSc = originalScale * (1 + amount);
            transform.DOScale(targetSc, within * 0.5f).OnComplete(() =>
            {
                transform.DOScale(originalScale, within * 0.5f).OnComplete(() =>
                {
                    OnComplete?.Invoke();
                });
            });
        }

        public static void ExPocThenScaleDown(this Transform transform, float amount, float within, System.Action OnComplete = null)
        {
            var oriSc = transform.localScale;
            var targetSc = oriSc * (1 + amount);
            transform.DOScale(targetSc, within * 0.5f).OnComplete(() =>
            {
                transform.DOScale(0.0f, within * 0.5f).OnComplete(() =>
                {
                    OnComplete?.Invoke();
                });
            });
        }

        public static void ExPingPongAlongYAxis(this Transform mover, MonoBehaviour coroutineScript,
            float upwardAmount = 1.2f, float cycleDuration = 0.6f)
        {
            coroutineScript.StartCoroutine(COR());
            IEnumerator COR()
            {
                Vector3 initLocalPos;
                initLocalPos = mover.localPosition;
                while (true)
                {
                    var upDone = false;
                    var upTarget = initLocalPos + mover.up * upwardAmount;
                    mover.DOLocalMove(upTarget, cycleDuration).OnComplete(() =>
                    {
                        upDone = true;
                    });
                    while (!upDone) { yield return null; }

                    var normalDone = false;
                    mover.DOLocalMove(initLocalPos, cycleDuration).OnComplete(() =>
                    {
                        normalDone = true;
                    });
                    while (!normalDone) { yield return null; }
                    yield return null;
                }
            }
        }

        public static void ExMoveObjectInArc(this Transform mover, MonoBehaviour coroutineScript, ArcMoveDescription desc,
            float within, System.Action OnComplete, float sag = 6f, int curvePoint = 10)
        {
            var path = new BezierPathMinimal(curvePoint);
            path.DeletePath();



            Vector2 t1 = new Vector2(desc.from.x, desc.from.z);
            Vector2 t2 = new Vector2(desc.to.x, desc.to.z);
            var towardsTarget = t2 - t1;
            var targetForwardDir = Vector2.Perpendicular(desc.forwardDirection < 0 ? -towardsTarget : towardsTarget);
            var y = (desc.from.y + desc.to.y) * 0.5f;
            var aheadDir = new Vector3(targetForwardDir.x, y, targetForwardDir.y);


            var p1 = desc.from.ExGetPointAtNormalizedDistance(0.2f, desc.to) + aheadDir * sag;
            var p2 = desc.from.ExGetPointAtNormalizedDistance(0.6f, desc.to) + aheadDir.normalized * sag * 2;
            var pts = new List<Vector3>() { desc.from, p1, p2, desc.to };
            path.CreateCurve(pts, desc.to);
            var totalDistance = 0.0f;
            if (path != null && path.pathPoints != null && path.pathPoints.Count > 1)
            {
                for (int i = 1; i < path.pathPoints.Count; i++)
                {
                    Vector3 prev_pt = path.pathPoints[i - 1];
                    Vector3 this_pt = path.pathPoints[i];
                    totalDistance += Vector3.Distance(prev_pt, this_pt);

                    Debug.DrawLine(prev_pt, this_pt, Color.red);

                    if (i == 1) { mover.position = prev_pt; }
                }
            }

            coroutineScript.StartCoroutine(Mover());
            IEnumerator Mover()
            {
                Quaternion qot = default;
                while (true)
                {
                    if (path != null && path.pathPoints != null && path.pathPoints.Count > 1)
                    {
                        for (int i = 1; i < path.pathPoints.Count; i++)
                        {
                            Vector3 prev_pt = path.pathPoints[i - 1];
                            Vector3 this_pt = path.pathPoints[i];
                            var lookDir = this_pt - prev_pt;
                            if (Mathf.Approximately(lookDir.magnitude, 0.0f) == false)
                            {
                                qot = Quaternion.LookRotation(lookDir, desc.rotationUpDir);
                            }

                            var distPercent = Vector3.Distance(prev_pt, this_pt) / totalDistance;
                            var reqTime = within * distPercent;
                            var done = false;
                            if (reqTime < Time.deltaTime)
                            {
                                mover.position = this_pt;
                                done = true;
                                mover.rotation = Quaternion.Slerp(mover.rotation, qot, desc.rotationSpeed * Time.deltaTime);
                                yield return null;
                            }
                            else
                            {
                                mover.DOMove(this_pt, reqTime).OnComplete(() => { done = true; }).SetEase(Ease.Linear);
                            }

                            while (!done)
                            {
                                mover.rotation = Quaternion.Slerp(mover.rotation, qot, desc.rotationSpeed * Time.deltaTime);
                                yield return null;
                            }
                        }
                    }
                    OnComplete?.Invoke();
                    yield break;
                }
            }
        }
    }
}