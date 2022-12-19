using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityExt
{
    public class FSMState : FSMStateBase
    {
        readonly Action onExecuteEv;
        readonly string taskName;
        public override string DebugName => taskName;
        public FSMState(Action onExecute, string taskName = "")
        {
            onExecuteEv = onExecute;
            this.taskName = taskName;
        }

        public override IEnumerator CompleteStateAsync()
        {
            yield return null;
            onExecuteEv?.Invoke();
        }
    }
    public class FSMStateAsync : FSMStateBase
    {
        readonly IEnumerator cor;
        readonly MonoBehaviour runner;
        readonly string taskName;
        public override string DebugName => taskName;
        public FSMStateAsync(IEnumerator cor, MonoBehaviour runner, string taskName = "")
        {
            this.cor = cor;
            this.runner = runner;
            this.taskName = taskName;
        }
        public override IEnumerator CompleteStateAsync()
        {
            yield return runner.StartCoroutine(cor);
        }
    }
    public class DelayState : FSMStateBase
    {
        public override string DebugName => nameof(DelayState);

        readonly float seconds;

        /// <param name="seconds">delay in seconds</param>
        public DelayState(float seconds)
        {
            this.seconds = seconds;
        }

        public override IEnumerator CompleteStateAsync()
        {
            var timer = 0.0f;
            while (true)
            {
                timer += Time.deltaTime;
                if (timer > seconds)
                {
                    break;
                }
                yield return null;
            }
        }
    }
}