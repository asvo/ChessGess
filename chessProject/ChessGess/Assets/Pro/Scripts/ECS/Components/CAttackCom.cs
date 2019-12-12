//***************
// name : CAttackCom
// createtime: 5/25/2019
// desc: 
// create by asvo
//***************
using UnityEngine;
using Asvo.ECS;
using Asvo.Common;

namespace Asvo
{
    public class CAttackCom : AComponent
    {
		//目标ID (entity base id)
		public uint TargetID;
		
        public void OnHpChange(int changeVal)
        {
            var attriCom = Entity.GetComponent<CAttributeCom>();
            #if ASVO_DEBUG
                CLog.Log(SysLogType.Battle, "{0} Hp changed {1}.", attriCom.ID, changeVal);
            #endif
            attriCom.Hp = attriCom.Hp + changeVal;

            if (attriCom.Hp <= 0)
            {
                attriCom.Hp = 0;
            }
        }

        protected override void OnDestroy()
        {
            
        }
    }
}
