//***************
// name : CRoleBorn
// createtime: 5/4/2019
// desc: 
// create by asvo
//***************
using Asvo.FSM;

namespace Asvo
{
    public class CRoleBorn : CStateBase
    {
        private CRenderCom m_renderCom;
        public CRoleBorn(CFSMMgr fSMMgr) : base(fSMMgr)
        {
            StateID = (int)E_RoleState.Born;
            m_fsmMgr.AddState(this);
        }

        public override void OnEnterState()
        {
            base.OnEnterState();
            m_renderCom = Entity.GetComponent<CRenderCom>();
        }
        
        public override void OnUpdate(){
            if (m_renderCom.IsInited)
            {
                //play born vfx...
                //
                StateTransite((int)E_RoleState.Idle);
            }
        }
        
        public override void OnExitState()
        {
            base.OnExitState();
            m_renderCom = null;
        }
    }
}
