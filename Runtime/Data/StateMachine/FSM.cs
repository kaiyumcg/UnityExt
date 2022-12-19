using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityExt
{
    public class FSM
    {
        Coroutine fsmHandle, stateHandle;
        bool fsmLock;
        MonoBehaviour runner;
        public FSM(MonoBehaviour runner)
        {
            this.runner = runner;
            fsmHandle = stateHandle= null;
            fsmLock = false;
        }

        public bool IsRunning => fsmHandle != null;
        public IFSMState CurrentState { get; private set; }
        protected virtual void OnStartRunState(IFSMState state) { }
        public void RunStateOnFSM(IFSMState state) 
        {
            if (fsmHandle == null) { return; }
            RunState(state);
        }
        void RunState(IFSMState state)
        {
            if (state == null)
                throw new Exception(nameof(state)+" can not be null");

            //first exit current one
            if (CurrentState != null && stateHandle != null) 
            {
                SkipCurrentState();
            }
            
            CurrentState = state;
            runner.StartCoroutine(Play());
            OnStartRunState(state);
            IEnumerator Play()
            {
                if (!fsmLock)
                {
                    fsmLock = true;
                    CurrentState.OnEnterState();
                    stateHandle = runner.StartCoroutine(CurrentState.CompleteStateAsync());
                    yield return stateHandle;
                    stateHandle = null;
                }
            }
        }
        void SkipCurrentState()
        {
            if (CurrentState == null)
            {
                throw new Exception($"{nameof(CurrentState)} is null!");
            }
            if (stateHandle != null)
            {
                runner.StopCoroutine(stateHandle);
                CurrentState.OnExitState();
                stateHandle = null;
                fsmLock = false;
            }
        }
        public virtual void StartFSMWith(IFSMState state)
        {
            RunState(state);
            StartFSM();
        }
        public virtual void StartFSM() 
        {
            if (fsmHandle != null) { return; }
            fsmHandle = runner.StartCoroutine(Loop());
        }
        public void Stop()
        {
            if (fsmHandle == null) { return; }
            if (CurrentState != null && stateHandle != null) 
            {
                SkipCurrentState();
            }
            
            runner.StopCoroutine(fsmHandle);
            fsmHandle = null;
            CurrentState = null;
        }
        protected virtual IEnumerator Loop()
        {
            while (true)
            {
                if (CurrentState != null && stateHandle == null) //current state is done playing
                {
                    if (CurrentState.CheckExitCondition(out var nextState))
                    {
                        if (fsmLock)
                        {
                            //finalize current state
                            CurrentState.OnExitState();
                            fsmLock = false;
                        }
                        CurrentState.DisableConditions();
                        RunState(nextState);
                        CurrentState.EnableConditions(); 
                    }
                }
                yield return null;
            }
        }
    }
}