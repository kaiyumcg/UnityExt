using System.Collections;
using UnityEngine;

namespace UnityExt
{
    /// <summary>
    /// An interface for the states of state machines
    /// </summary>
    public interface IFSMState
    {
        void OnEnterState();
        IEnumerator CompleteStateAsync();
        void OnExitState();
        void AddExitCondition(IExitCondition condition);
        void RemoveExitCondition(IExitCondition condition);
        void RemoveAllExitConditions();
        bool CheckExitCondition(out IFSMState nextState);
        void EnableConditions();
        void DisableConditions();
    }
}