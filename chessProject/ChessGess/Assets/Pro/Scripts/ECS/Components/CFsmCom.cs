//***************
// name : CFsmCom
// createtime: 5/4/2019
// desc: 
// create by asvo
//***************
using Asvo.ECS;
using Asvo.FSM;

namespace Asvo
{
    public class CFsmCom : AComponent
    {
        public CFSMMgr FsmMgr { get; protected set; }

        public CFsmCom()
        {            
            FsmMgr = new CFSMMgr();
        }

        protected override void OnBindEntity()
        {
            FsmMgr.Owner = this.Entity;
        }

        protected override void OnDestroy()
        {
            FsmMgr.Clear();
            FsmMgr.Owner = null;
            FsmMgr = null;
        }
    }
}
