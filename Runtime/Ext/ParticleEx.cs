using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityExt
{
    public static class ParticleEx
    {
        #region HouseKeeping
        public static void ExInitParticles(this List<GameParticle> particles)
        {
            particles.ExForEachSafe(p => { if (p != null) { p.Init(); } });
        }
        public static void ExInitParticles(this GameParticle[] particles)
        {
            particles.ExForEachSafe(p => { if (p != null) { p.Init(); } });
        }
        #endregion

        #region NormalPlayback
        public static void ExPlayParticles(this ParticleSystem[] sysList)
        {
            sysList.ExForEachSafe(sys =>
            {
                if (sys != null)
                {
                    if (sys.isPlaying)
                    {
                        sys.Stop();
                    }
                    sys.Play();
                }
            });
        }
        public static void ExPlayParticles(this List<ParticleSystem> sysList)
        {
            sysList.ExForEachSafe(sys =>
            {
                if (sys != null)
                {
                    if (sys.isPlaying)
                    {
                        sys.Stop();
                    }
                    sys.Play();
                }
            });
        }
        public static void ExStopParticles(this ParticleSystem[] sysList)
        {
            sysList.ExForEachSafe(sys =>
            {
                if (sys != null)
                {
                    if (sys.isPlaying)
                    {
                        sys.Stop();
                    }
                }
            });
        }
        public static void ExStopParticles(this List<ParticleSystem> sysList)
        {
            sysList.ExForEachSafe(sys =>
            {
                if (sys != null)
                {
                    if (sys.isPlaying)
                    {
                        sys.Stop();
                    }
                }
            });
        }
        public static void ExPlayParticles(this List<GameParticle> particles)
        {
            particles.ExForEachSafe(sys =>
            {
                if (sys != null)
                {
                    sys.Play();
                }
            });
        }
        public static void ExPlayParticles(this GameParticle[] particles)
        {
            particles.ExForEachSafe(sys =>
            {
                if (sys != null)
                {
                    sys.Play();
                }
            });
        }
        public static void ExStopParticles(this List<GameParticle> particles)
        {
            particles.ExForEachSafe(sys =>
            {
                if (sys != null)
                {
                    sys.Stop();
                }
            });
        }
        public static void ExStopParticles(this GameParticle[] particles)
        {
            particles.ExForEachSafe(sys =>
            {
                if (sys != null)
                {
                    sys.Stop();
                }
            });
        }
        #endregion

        #region SequentialPlayback
        public static void ExPlayParticlesSerially(this List<GameParticle> particles, MonoBehaviour handle, float interval, System.Action OnComplete = null)
        {
            handle.StartCoroutine(COR());
            IEnumerator COR()
            {
                var waiter = new WaitForSeconds(interval);
                if (particles != null && particles.Count > 0)
                {
                    for (int i = 0; i < particles.Count; i++)
                    {
                        var eff = particles[i];
                        if (eff == null) { continue; }
                        eff.Play();
                        yield return waiter;
                    }
                }
                OnComplete?.Invoke();
            }
        }
        public static void ExPlayParticlesSerially(this GameParticle[] particles, MonoBehaviour handle, float interval)
        {
            handle.StartCoroutine(COR());
            IEnumerator COR()
            {
                var waiter = new WaitForSeconds(interval);
                if (particles != null && particles.Length > 0)
                {
                    for (int i = 0; i < particles.Length; i++)
                    {
                        var eff = particles[i];
                        if (eff == null) { continue; }
                        eff.Play();
                        yield return waiter;
                    }
                }
            }
        }
        public static void ExStopParticlesSerially(this List<GameParticle> particles, MonoBehaviour handle, float interval)
        {
            handle.StartCoroutine(COR());
            IEnumerator COR()
            {
                var waiter = new WaitForSeconds(interval);
                if (particles != null && particles.Count > 0)
                {
                    for (int i = 0; i < particles.Count; i++)
                    {
                        var eff = particles[i];
                        if (eff == null) { continue; }
                        eff.Stop();
                        yield return waiter;
                    }
                }
            }
        }
        public static void ExStopParticlesSerially(this GameParticle[] particles, MonoBehaviour handle, float interval)
        {
            handle.StartCoroutine(COR());
            IEnumerator COR()
            {
                var waiter = new WaitForSeconds(interval);
                if (particles != null && particles.Length > 0)
                {
                    for (int i = 0; i < particles.Length; i++)
                    {
                        var eff = particles[i];
                        if (eff == null) { continue; }
                        eff.Stop();
                        yield return waiter;
                    }
                }
            }
        }
        public static void ExPlayParticlesSerially(this List<GameParticle> particles, MonoBehaviour handle, List<float> intervals)
        {
            handle.StartCoroutine(COR());
            IEnumerator COR()
            {
                if (particles != null && particles.Count > 0)
                {
                    for (int i = 0; i < particles.Count; i++)
                    {
                        var eff = particles[i];
                        if (eff == null) { continue; }
                        eff.Play();
                        if (i == particles.Count - 1) { continue; }
                        yield return new WaitForSeconds(intervals[i]);
                    }
                }
            }
        }
        public static void ExPlayParticlesSerially(this List<GameParticle> particles, MonoBehaviour handle, float[] intervals)
        {
            handle.StartCoroutine(COR());
            IEnumerator COR()
            {
                if (particles != null && particles.Count > 0)
                {
                    for (int i = 0; i < particles.Count; i++)
                    {
                        var eff = particles[i];
                        if (eff == null) { continue; }
                        eff.Play();
                        if (i == particles.Count - 1) { continue; }
                        yield return new WaitForSeconds(intervals[i]);
                    }
                }
            }
        }
        public static void ExPlayParticlesSerially(this GameParticle[] particles, MonoBehaviour handle, List<float> intervals)
        {
            handle.StartCoroutine(COR());
            IEnumerator COR()
            {
                if (particles != null && particles.Length > 0)
                {
                    for (int i = 0; i < particles.Length; i++)
                    {
                        var eff = particles[i];
                        if (eff == null) { continue; }
                        eff.Play();
                        if (i == particles.Length - 1) { continue; }
                        yield return new WaitForSeconds(intervals[i]);
                    }
                }
            }
        }
        public static void ExPlayParticlesSerially(this GameParticle[] particles, MonoBehaviour handle, float[] intervals)
        {
            handle.StartCoroutine(COR());
            IEnumerator COR()
            {
                if (particles != null && particles.Length > 0)
                {
                    for (int i = 0; i < particles.Length; i++)
                    {
                        var eff = particles[i];
                        if (eff == null) { continue; }
                        eff.Play();
                        if (i == particles.Length - 1) { continue; }
                        yield return new WaitForSeconds(intervals[i]);
                    }
                }
            }
        }
        public static void ExStopParticlesSerially(this List<GameParticle> particles, MonoBehaviour handle, List<float> intervals)
        {
            handle.StartCoroutine(COR());
            IEnumerator COR()
            {
                if (particles != null && particles.Count > 0)
                {
                    for (int i = 0; i < particles.Count; i++)
                    {
                        var eff = particles[i];
                        if (eff == null) { continue; }
                        eff.Stop();
                        if (i == particles.Count - 1) { continue; }
                        yield return new WaitForSeconds(intervals[i]);
                    }
                }
            }
        }
        public static void ExStopParticlesSerially(this List<GameParticle> particles, MonoBehaviour handle, float[] intervals)
        {
            handle.StartCoroutine(COR());
            IEnumerator COR()
            {
                if (particles != null && particles.Count > 0)
                {
                    for (int i = 0; i < particles.Count; i++)
                    {
                        var eff = particles[i];
                        if (eff == null) { continue; }
                        eff.Play();
                        if (i == particles.Count - 1) { continue; }
                        yield return new WaitForSeconds(intervals[i]);
                    }
                }
            }
        }
        public static void ExStopParticlesSerially(this GameParticle[] particles, MonoBehaviour handle, List<float> intervals)
        {
            handle.StartCoroutine(COR());
            IEnumerator COR()
            {
                if (particles != null && particles.Length > 0)
                {
                    for (int i = 0; i < particles.Length; i++)
                    {
                        var eff = particles[i];
                        if (eff == null) { continue; }
                        eff.Play();
                        if (i == particles.Length - 1) { continue; }
                        yield return new WaitForSeconds(intervals[i]);
                    }
                }
            }
        }
        public static void ExStopParticlesSerially(this GameParticle[] particles, MonoBehaviour handle, float[] intervals)
        {
            handle.StartCoroutine(COR());
            IEnumerator COR()
            {
                if (particles != null && particles.Length > 0)
                {
                    for (int i = 0; i < particles.Length; i++)
                    {
                        var eff = particles[i];
                        if (eff == null) { continue; }
                        eff.Play();
                        if (i == particles.Length - 1) { continue; }
                        yield return new WaitForSeconds(intervals[i]);
                    }
                }
            }
        }
        #endregion
    }
}