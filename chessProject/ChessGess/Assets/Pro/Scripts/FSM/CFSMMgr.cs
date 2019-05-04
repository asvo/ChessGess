//***************
// name : CFSMMgr
// createtime: 5/4/2019
// desc: 状态类管理。
//      (设置添加的第一个状态为初始状态。)
// create by asvo
//***************
using System.Collections.Generic;

namespace Asvo.FSM
{
	public class CFSMMgr {
        protected Dictionary<int, CStateBase> m_stateDict;
        protected CStateBase m_curretState;
        public CFSMMgr()
        {
            m_stateDict = new Dictionary<int, CStateBase>(4);
            m_curretState = null;
        }

        public void AddState(CStateBase state)
        {
            m_stateDict.Add(state.StateID, state);
            //set first added-state to default state
            if (m_curretState == null)
                SetCurrentState(state.StateID);
        }

        protected CStateBase GetState(int stateID)
        {
            CStateBase state = null;
            m_stateDict.TryGetValue(stateID, out state);
            return state;
        }

        public void SetCurrentState(int stateID)
        {
            var targetState = GetState(stateID);
            if (null == targetState)
            {
                //no target state, do nothing.
                return;
            }
            if (m_curretState != null)
                m_curretState.OnExitState();
            m_curretState = targetState;
            if (null != m_curretState)
                m_curretState.OnEnterState();
        }

        public void Update()
        {
            if (null == m_curretState)
                return;
            m_curretState.OnUpdate();
        }

        public void Clear()
        {            
            m_stateDict.Clear();
            m_stateDict = null;
            m_curretState = null;
        }
	}
}
