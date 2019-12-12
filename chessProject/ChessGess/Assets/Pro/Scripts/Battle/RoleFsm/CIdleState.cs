//***************
// name : CIdleState
// createtime: 5/4/2019
// desc: 
// create by asvo
//***************
using Asvo.FSM;
using UnityEngine;

namespace Asvo
{
    public class CIdleState : CStateBase
    {
        private float SEARCH_INTERVAL = 0.5f;
        private float m_timer;
        protected CAttributeCom m_attirCom;
        public CIdleState(CFSMMgr fSMMgr) : base(fSMMgr)
        {
            StateID = (int)E_RoleState.Idle;
            m_fsmMgr.AddState(this);
            //TODO, add transitions            
        }

        public override void OnEnterState()
        {
            base.OnEnterState();
            m_attirCom = this.Entity.GetComponent<CAttributeCom>();
            var animCom = this.Entity.GetComponent<CAnimCom>();
            animCom.ForcePlayAnim(E_AnimatorState.walk);
            //find a target
         //   SearchTarget();
            m_timer = 0f;
        }

        public override void OnUpdate()
        {
            m_timer += Time.deltaTime;
            if (m_timer >= SEARCH_INTERVAL)
            {
                m_timer = 0f;
                SearchTarget();
            }
        }

        private void SearchTarget()
        {
            var battleSys = CBattleEntityMgr.Instance.GetSystem<CBattleEntitySys>();
            var target = battleSys.FindTargetOfEntity(this.Entity);
            if (null != target)
            {
                var attackCom = Entity.GetComponent<CAttackCom>();
                attackCom.TargetID = target.GetComponent<CAttributeCom>().ID;
                //check distance
                if (CBattleHelper.CheckTargetInAttackRange(Entity, target))
                {
                    StateTransite((int)E_RoleState.Attack);
                }
                else
                {
                    StateTransite((int)E_RoleState.Walking);
                }
            }
        }

        public override void OnExitState()
        {
            base.OnExitState();
        }
    }
}
