using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityExt
{
    public class WobbleMotion : MonoBehaviour
    {
        [SerializeField, Header("Wobble at Start()")] bool enableAtStart = true;
        [SerializeField, Header("Wobble period")] float cycle = 0.5f;
        [SerializeField, Header("Wobble amount")] float randomAmount = 1f;
        [SerializeField, Header("Wobble smooth speed")] float wobbleSmoothing = 0.1f;
        [SerializeField, Header("Wobble stop smooth speed")] float toNormalSmoothing = 0.07f;
        
        Transform tr;
        bool effectEnabled = false;
        Vector3 wobbleTarget = Vector3.zero;
        float cycleTimer = 0.0f;
        Vector3 normalVel = Vector3.zero;
        Vector3 wobbleVel = Vector3.zero;

        void Awake()
        {
            tr = transform;
            effectEnabled = enableAtStart;
            UpdateWobbleTarget();
            cycleTimer = 0.0f;
            normalVel = Vector3.zero;
            wobbleVel = Vector3.zero;
        }
        void UpdateWobbleTarget()
        {
            wobbleTarget = Vector3.one * Random.Range(-1f, 1f) * randomAmount;
            wobbleTarget.y = 0.0f;
        }
        public void StartEffect()
        {
            effectEnabled = true;
        }
        public void StopEffect()
        {
            effectEnabled = false;
        }
        void Update()
        {
            if (effectEnabled)
            {
                cycleTimer += Time.deltaTime;
                if (cycleTimer > cycle)
                {
                    UpdateWobbleTarget();
                    cycleTimer = 0.0f;
                }
                tr.localEulerAngles = Vector3.SmoothDamp(tr.localEulerAngles, wobbleTarget, ref wobbleVel, wobbleSmoothing);
            }
            else
            {
                tr.localEulerAngles = Vector3.SmoothDamp(tr.localEulerAngles, Vector3.zero, ref normalVel, toNormalSmoothing);
                cycleTimer = 0.0f;
            }
        }
    }
}