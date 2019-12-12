//***************
// name : CAttackState
// createtime: 5/4/2019
// desc: 
// create by asvo
//***************
using Asvo.FSM;
using Asvo.Common;
using UnityEngine;
using Asvo.ECS;

namespace Asvo
{
    public class CAttackState : CStateBase
    {
        protected CAttributeCom m_attriCom;
        protected CAnimCom m_animCom;
        protected CAttackCom m_attackCom;
        protected float m_timer;
        protected float m_attackInterval;
        protected bool m_isGoingAttack = false;
        private CBattleEntitySys mBattleSys;
        public CAttackState(CFSMMgr fSMMgr) : base(fSMMgr)
        {
            StateID = (int)E_RoleState.Attack;
            m_fsmMgr.AddState(this);
        }

        public override void OnEnterState()
        {
            base.OnEnterState();
            mBattleSys = CBattleEntityMgr.Instance.GetSystem<CBattleEntitySys>();
            m_attriCom = Entity.GetComponent<CAttributeCom>();
            m_animCom = Entity.GetComponent<CAnimCom>();
            m_attackCom = Entity.GetComponent<CAttackCom>();
            m_attackInterval = 1f;
            m_timer = 0f;
            AttackAction();
        }

        public override void OnUpdate()
        {
            m_timer += Time.deltaTime;
            if (m_timer >= m_attackInterval)
            {
                m_timer = 0f;
                AttackAction();
            }
        }

        private void AttackAction()
        {
            var targetEntity = mBattleSys.FindEntityByID(m_attackCom.TargetID);
            if (!CheckTargetValid(targetEntity))
            {
                StateTransite((int)E_RoleState.Idle);
                return;
            }
            CLog.Log(SysLogType.Battle_Role_State, "{0} 播放攻击行为", m_attriCom.ID);
            m_animCom.ForcePlayAnim(E_AnimatorState.attack);
            var targetAttackCom = targetEntity.GetComponent<CAttackCom>();
            targetAttackCom.OnHpChange(-(int)m_attriCom.GetAttr(E_AttributeType.Attack));

            var targetAttricom = targetEntity.GetComponent<CAttributeCom>();
            //refresh blood of ui

            if (!targetAttricom.IsAlive())
            {
                //change to dead
                var targetFsm = targetEntity.GetComponent<CFsmCom>();
                targetFsm.FsmMgr.SetCurrentState((int)E_RoleState.Dead);
            }         
        }

        private bool CheckTargetValid(CEntity targetEntity)
        {
            if (null == targetEntity)
                return false;
            var atriCom = targetEntity.GetComponent<CAttributeCom>();
            return atriCom.IsAlive();
        }

        private CEntity FindAnotherTarget()
        {
            return null;
        }
    }
}
