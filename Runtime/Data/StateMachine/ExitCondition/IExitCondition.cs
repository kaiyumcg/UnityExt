using System;
using System.Collections.Generic;

namespace UnityExt
{
    public interface IExitCondition
    {
        bool Validate(out IFSMState nextState);
        void Enable(){}
        void Disable(){}
    }
}