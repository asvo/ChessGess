//***************
// name : CFsmSys
// createtime: 5/4/2019
// desc: 
// create by asvo
//***************
using Asvo.ECS;

namespace Asvo
{
	public class CFsmSys : ASystem {
		public CFsmSys()
        {
            m_process = new ASystemProcess(this, typeof(CFsmCom));
        }

        public override void Update(CEntity entity)
        {
            CFsmCom fsmCom = entity.GetComponent<CFsmCom>();
            if (fsmCom.FsmMgr != null)
            {
                fsmCom.FsmMgr.Update();
            }
        }
    }

    public static class FsmHelper
    {
        public static string GetRoleStateName(int roleID)
        {
            return ((E_RoleState)roleID).ToString();
        }
    }
}
