//***************
// name : CDeadState
// createtime: 5/4/2019
// desc: 
// create by asvo
//***************
using Asvo.FSM;


namespace Asvo
{
    public class CDeadState : CStateBase
    {
        private float DEAD_TIME = 0.5f;
        private float m_time = 0;
        private bool m_isActive;
        public CDeadState(CFSMMgr fSMMgr) : base(fSMMgr)
        {
			this.StateID = (int)E_RoleState.Dead;
			this.m_fsmMgr.AddState(this);
        }

		public override void OnEnterState()
		{
			base.OnEnterState();
			var animCom = Entity.GetComponent<CAnimCom>();
			animCom.ForcePlayAnim(E_AnimatorState.dead);

            m_time = 0;
            m_isActive = true;
        }

        public override void OnUpdate()
        {
            if (!m_isActive)
                return;
            m_time += UnityEngine.Time.deltaTime;
            if (m_time >= DEAD_TIME)
            {
                m_isActive = false;
                OnRoleDeadOver();                
            }
        }

        private void OnRoleDeadOver()
        {
            CBattleSignals.sig_onRoleDie.Dispatch(Entity.GetComponent<CAttributeCom>().ID);
            Asvo.ECS.CEngine.Instance.RemoveEntity(Entity);
        }

        public override void OnExitState()
		{
			base.OnExitState();
		}
    }
}
