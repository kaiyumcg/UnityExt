using AttributeExt2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityExt
{
    public class GameTrail : MonoBehaviour
    {
        Transform tr;
        public Transform _Transform { get { return tr; } }
        [SerializeField, ReadOnly]TrailRenderer trail;
        [SerializeField, ReadOnly] string status;
        public TrailRenderer Trail { get { return trail; } }
        public void Init()
        {
            tr = transform;
            trail = GetComponentInChildren<TrailRenderer>();
            Clear();
        }

        public void Clear()
        {
            if (trail != null)
            {
                trail.Clear();
            }
            status = "Clear called";
        }
        public void Hide()
        {
            if (trail != null)
            {
                trail.enabled = false;
            }
            status = "Hide called";
        }
        public void ForceEmiting()
        {
            if (trail != null)
            {
                trail.emitting = true;
            }
            status = "Force emitting called";
        }
    }
}