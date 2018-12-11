using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace takeshi
{
    public abstract class StatedObjectBase<T, TEnum> : MonoBehaviour
        where T : class where TEnum : System.IConvertible
    {
        protected Dictionary<TEnum, State<T>> stateList = new Dictionary<TEnum, State<T>>();

        protected StateMachine<T> stateMachine;

        public virtual void ChangeState(TEnum state) {
            if (stateMachine != null && stateList.ContainsKey(state)) {
                stateMachine.ChangeState(stateList[state]);
            }
        }

        public virtual bool CheckCurrentStaet (TEnum state) {
            // if stateMachine == null return false, else return check result  
            return stateMachine != null && stateMachine.CurrentState == stateList[state];
        }

        protected virtual void Update()
        {
            if (stateMachine != null)
            {
                stateMachine.Update();
            }
        }
    }
}