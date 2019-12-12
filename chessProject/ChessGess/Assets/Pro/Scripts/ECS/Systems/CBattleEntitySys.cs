//***************
// name : CBattleEntitySys
// createtime: 5/26/2019
// desc: 
// create by asvo
//***************
using UnityEngine;
using Asvo.ECS;
using Asvo.Common;

namespace Asvo
{
	public class CBattleEntitySys : ASystem {
		
		public CBattleEntitySys()
		{
			this.m_process = new ASystemProcess(this, typeof(CAttackCom), typeof(CAttributeCom), typeof(CRenderCom));
		}

        protected override void RegisterSignals()
        {
            CBattleSignals.sig_onRoleDie.AddListener(OnRoleDie);
        }

        protected override void UnregisterSignals()
        {
            CBattleSignals.sig_onRoleDie.RemoveListener(OnRoleDie);
        }

        private void OnRoleDie(uint id)
        {
            var roleEntity = FindEntityByID(id);
            if (null == roleEntity)
                return;
            //check if all of one team die
            var attrib = roleEntity.GetComponent<CAttributeCom>();
            if (CheckIfBattleGroupAllDie(attrib.BattleGroupType))
            {
                //notify battle over
                CLog.Log(SysLogType.Battle, "Batle Over~");
            }
        }

        private bool CheckIfBattleGroupAllDie(E_BattleGroupType groupType)
        {
            for (var node = m_process.EntityList.head; node != null; node = node.next)
            {
                var tempAttr = node.GetComponent<CAttributeCom>();
                if (groupType != tempAttr.BattleGroupType && tempAttr.IsAlive())
                {
                    return true;
                }
            }
            return false;
        }

		/// <summary>
		/// 寻找attacker的攻击目标.!-- 暂先不考虑中立
		/// </summary>
		/// <param name="attacker">攻击者</param>
		/// <returns></returns>
		public CEntity FindTargetOfEntity(CEntity attacker)
		{
			var attackCamp = attacker.GetComponent<CAttributeCom>().BattleGroupType;
			for (var node = m_process.EntityList.head; node != null; node = node.next)
			{
				var tempAttr = node.GetComponent<CAttributeCom>();
				if (tempAttr.IsAlive() && attackCamp != tempAttr.BattleGroupType)
				{
					return node.Entity;
				}
			}
			return null;
		}

		public CEntity FindEntityByID(uint id)
		{
			for (var node = m_process.EntityList.head; node != null; node = node.next)
			{
				var tempAttr = node.GetComponent<CAttributeCom>();
				if (id == tempAttr.ID)
					return node.Entity;
			}
			return null;
		}

        public override void Destroy()
        {
            base.Destroy();
        }
    }
}
