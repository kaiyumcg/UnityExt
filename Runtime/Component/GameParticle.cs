using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AttributeExt;

public class GameParticle : MonoBehaviour
{
    Transform tr;
    public Transform _Transform { get { return tr; } }
    [SerializeField, CanNotEdit] ParticleSystem[] allSys;
    [SerializeField, CanNotEdit] string status;
    public void Init()
    {
        tr = transform;
        allSys = GetComponentsInChildren<ParticleSystem>();
        Stop();
    }

    public void Stop(bool reactive = false)
    {
        if (allSys != null && allSys.Length > 0)
        {
            for (int i = 0; i < allSys.Length; i++)
            {
                var sys = allSys[i];
                if (sys == null) { continue; }
                if (reactive) { sys.Stop(); }
                else
                {
                    if (sys.isPlaying)
                    {
                        sys.Stop();
                    }
                }
            }
        }
        status = "Stop called";
    }

    public void Play(bool reactive = false)
    {
        if (allSys != null && allSys.Length > 0)
        {
            for (int i = 0; i < allSys.Length; i++)
            {
                var sys = allSys[i];
                if (sys == null) { continue; }
                if (reactive) 
                {
                    if (sys.isPlaying) { sys.Stop(); }
                    sys.Play(); 
                }
                else
                {
                    if (sys.isPlaying == false)
                    {
                        sys.Play();
                    }
                }
            }
        }
        status = "Play called";
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2f);
    }
#endif
}
