using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

namespace UnityExt
{
    public abstract class FSMStateBase : IFSMState
    {
        public virtual string DebugName { get; set; }
        readonly List<IExitCondition> exitConditions = new ();
        public virtual void OnEnterState()
        {
        }
        public abstract IEnumerator CompleteStateAsync();
        public virtual void OnExitState()
        {
        }
        public virtual void AddExitCondition(IExitCondition condition)
        {
            if (!exitConditions.Contains(condition))
            {
                exitConditions.Add(condition);
            }
        }
        public virtual void RemoveExitCondition(IExitCondition condition)
        {
            if (exitConditions.Contains(condition))
            {
                exitConditions.Remove(condition);
            }
        }
        public virtual void RemoveAllExitConditions()
        {
            exitConditions.Clear();
        }
        public virtual bool CheckExitCondition(out IFSMState nextState)
        {
            if (exitConditions != null && exitConditions.Count > 0)
            {
                foreach (var con in exitConditions)
                {
                    var result = con.Validate(out nextState);
                    if (result)
                    {
                        return true;
                    }
                }
            }
            nextState = null;
            return false;
        }
        public void EnableConditions()
        {
            exitConditions.ExForEachSafe((i) => { i.Enable(); });
        }
        public void DisableConditions()
        {
            exitConditions.ExForEachSafe((i) => { i.Disable(); });
        }
    }
}