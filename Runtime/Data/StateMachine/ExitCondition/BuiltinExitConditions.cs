using UnityEngine.Events;

namespace UnityExt
{
    public delegate IFSMState GetStateFunc();
    public class ExitCondition : IExitCondition
    {
        readonly IFSMState nextState;
        public ExitCondition(IFSMState nextState)
        {
            this.nextState = nextState;
        }

        public bool Validate(out IFSMState nextState)
        {
            nextState = this.nextState;
            return true;
        }
    }
    public class UnityEventExitCondition : IExitCondition
    {
        IFSMState m_NextState;
        UnityEvent m_GameEvent;
        bool m_EventRaised;

        /// <param name="gameEvent">the event this link listens to</param>
        /// <param name="nextState">the next state</param>
        public UnityEventExitCondition(UnityEvent gameEvent, IFSMState nextState)
        {
            m_GameEvent = gameEvent;
            m_NextState = nextState;
        }

        public bool Validate(out IFSMState nextState)
        {
            nextState = null;
            bool result = false;

            if (m_EventRaised)
            {
                nextState = m_NextState;
                result = true;
            }

            return result;
        }

        void OnEventInvoke()
        {
            m_EventRaised = true;
        }

        public void Enable()
        {
            m_GameEvent.AddListener(OnEventInvoke);
            m_EventRaised = false;
        }

        public void Disable()
        {
            m_GameEvent.RemoveListener(OnEventInvoke);
            m_EventRaised = false;
        }
    }
    public class DynamicUnityEventLink : IExitCondition
    {
        GetStateFunc getStateMethod;
        UnityEvent m_GameEvent;
        bool m_EventRaised;

        /// <param name="gameEvent">the event this link listens to</param>
        /// <param name="nextState">the next state</param>
        public DynamicUnityEventLink(UnityEvent gameEvent, GetStateFunc getStateFunc)
        {
            m_GameEvent = gameEvent;
            this.getStateMethod = getStateFunc;
        }

        public bool Validate(out IFSMState nextState)
        {
            nextState = null;
            bool result = false;

            if (m_EventRaised)
            {
                nextState = getStateMethod.Invoke();
                result = true;
            }
            return result;
        }

        public void OnEventRaised()
        {
            m_EventRaised = true;
        }

        public void Enable()
        {
            m_GameEvent.AddListener(OnEventRaised);
            m_EventRaised = false;
        }

        public void Disable()
        {
            m_GameEvent.RemoveListener(OnEventRaised);
            m_EventRaised = false;
        }
    }
}