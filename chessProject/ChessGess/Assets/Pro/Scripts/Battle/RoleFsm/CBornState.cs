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
        public CRoleBorn(CFSMMgr fSMMgr) : base(fSMMgr)
        {
            StateID = (int)E_RoleState.Born;
            m_fsmMgr.AddState(this);
        }

        public override void OnEnterState()
        {
            base.OnEnterState();
        }

        public override void OnExitState()
        {
            base.OnExitState();
        }
    }
}
