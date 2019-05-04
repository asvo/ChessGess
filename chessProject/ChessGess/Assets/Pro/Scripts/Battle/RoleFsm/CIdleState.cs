//***************
// name : CIdleState
// createtime: 5/4/2019
// desc: 
// create by asvo
//***************
using Asvo.FSM;

namespace Asvo
{
    public class CIdleState : CStateBase
    {
        public CIdleState(CFSMMgr fSMMgr) : base(fSMMgr)
        {
            StateID = (int)E_RoleState.Idle;
            m_fsmMgr.AddState(this);
            //TODO, add transitions
            
        }
    }
}
