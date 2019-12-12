//***************
// name : CBattleEntityMgr
// createtime: 5/25/2019
// desc: 战斗中实体管理类
// create by asvo
//***************
using UnityEngine;
using Asvo.ECS;
using Asvo.Common;
using System.Collections.Generic;

namespace Asvo
{
    public class CBattleEntityMgr : ISingelton<CBattleEntityMgr>
    {
        private static uint s_EntityID = 0;
        public CBattleEntityMgr()
        {
        }

        public void CreateATestEntity(E_BattleGroupType battleGroup)
        {
            var attrCom = new CAttributeCom() { Config = new RoleConfig() { Name = "soldier01", AssetName = "black01" } };
            attrCom.BattleGroupType = battleGroup;
            attrCom.ID = ++s_EntityID;
            attrCom.Config.Name = string.Format("{0}_{1}", attrCom.Config.Name, s_EntityID);
            var t = typeof(E_AttributeType);
			foreach (E_AttributeType key in System.Enum.GetValues(t))
            {
                attrCom.SetAttr(key, (uint)UnityEngine.Random.Range(5, 20));
            }
            uint hp = attrCom.GetAttr(E_AttributeType.MaxHp);
            attrCom.SetAttr(E_AttributeType.CurrentHp, hp);
            attrCom.SetAttr(E_AttributeType.MoveSpeed, 20);
            attrCom.Hp = (int)hp;
            
            var fsmCom = new CFsmCom();
            new CRoleBorn(fsmCom.FsmMgr);
            new CIdleState(fsmCom.FsmMgr);
            new CWalkState(fsmCom.FsmMgr);
            new CAttackState(fsmCom.FsmMgr);
            new CDeadState(fsmCom.FsmMgr);
            
            var attackCom = new CAttackCom();
            var transCom = new CTransCom();
            var animCom = new CAnimCom();
            var entity = CEngine.Instance.CreateEntity(new System.Collections.Generic.List<AComponent>
                    {
                        attrCom,
                        transCom,
                        new CRenderCom(),
                        new CEntityTypeCom(){EntityType = E_EntityType.Role},
                        fsmCom,
                        attackCom,
                        animCom
                    });
            CLog.Log(SysLogType.Battle, "创建角色:{0}, hp = {1}", attrCom.ID, hp);
            CEngine.Instance.AddEntity(entity);
            fsmCom.FsmMgr.SetCurrentState((int)E_RoleState.Born);
        }

        public T GetSystem<T>() where T : ASystem
        {
            return CEngine.Instance.GetSystem<T>();
        }
    }

    public static class CBattleHelper
    {
        public static bool CheckTargetInAttackRange(CEntity attacker, CEntity target)
        {
            var attackerRender = attacker.GetComponent<CRenderCom>();
            var targetRender = target.GetComponent<CRenderCom>();
            Vector3 posA = attackerRender.GameObject.transform.position;
            Vector3 posB = targetRender.GameObject.transform.position;
            // float attackRange = attacker.GetComponent<CAttributeCom>().GetAttr(E_AttributeType.);
            float attackRange = 0.5f;
            return Vector3.Distance(posA, posB) <= attackRange;
        }
    }

    public static class SceneGlobal
    {
        public static Transform Root3D;
    }
}
