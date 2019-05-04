//***************
// name : CStateBase
// createtime: 5/4/2019
// desc: 
// create by asvo
//***************

namespace Asvo.FSM
{
	public abstract class CStateBase {
        public int StateID { get; protected set; }
        protected CFSMMgr m_fsmMgr;
        public CStateBase(CFSMMgr fSMMgr)
        {
            m_fsmMgr = fSMMgr;
        }

		public virtual void OnEnterState()
        {
#if ASVO_DEBUG
            Asvo.Common.CLog.Log(Common.SysLogType.FSM_DEBUG, "Enter state: {0}.", FsmHelper.GetRoleStateName(this.StateID));
#endif
        }

        public virtual void OnExitState()
        {
#if ASVO_DEBUG
            Asvo.Common.CLog.Log(Common.SysLogType.FSM_DEBUG, "Exit state: {0}.", FsmHelper.GetRoleStateName(this.StateID));
#endif
        }

        public virtual void OnUpdate()
        {

        }

        protected void StateTransite(int stateID)
        {
            m_fsmMgr.SetCurrentState(stateID);
        }
	}
}
