//***************
// name : CWalikState
// createtime: 5/25/2019
// desc: 
// create by asvo
//***************
using UnityEngine;
using Asvo.ECS;
using Asvo.Common;
using Asvo.FSM;

namespace Asvo
{
    public class CWalkState : CStateBase
    {
		private GameObject m_moveGobj;
		private float m_moveSpeed;
		private CEntity m_targetEntity;
		private CAttributeCom m_attirCom;
        private Vector3 m_moveVec;
        public CWalkState(CFSMMgr fSMMgr) : base(fSMMgr)
        {
			StateID = (int)E_RoleState.Walking;
			m_fsmMgr.AddState(this);
        }

		public override void OnEnterState()
		{
			base.OnEnterState();
			m_attirCom = Entity.GetComponent<CAttributeCom>();
			m_moveSpeed = m_attirCom.GetAttr(E_AttributeType.MoveSpeed) / 10f;
			m_moveGobj = Entity.GetComponent<CRenderCom>().GameObject;
			var attackCom = Entity.GetComponent<CAttackCom>();
			var battleSys = CBattleEntityMgr.Instance.GetSystem<CBattleEntitySys>();
			m_targetEntity = battleSys.FindEntityByID(attackCom.TargetID);
			if (null == m_targetEntity)
			{
				//back to idle
				StateTransite((int)E_RoleState.Idle);
				return;
			}
			//look at target
			var targetRenderer = m_targetEntity.GetComponent<CRenderCom>();
            //m_moveGobj.transform.LookAt(targetRenderer.GameObject.transform);
            SetLookAt(m_moveGobj.transform, targetRenderer.GameObject.transform);
			
			var animCom = this.Entity.GetComponent<CAnimCom>();
            animCom.ForcePlayAnim(E_AnimatorState.walk);
		}

        private void SetLookAt(Transform a, Transform bToLooked)
        {
            if (a.position.x > bToLooked.position.x)
            {
                a.eulerAngles = new Vector3(0, 180, 0);
                m_moveVec = Vector3.left;
            }
            else
            {
                a.eulerAngles = Vector3.zero;
                m_moveVec = Vector3.right;
            }
        }

		public override void OnUpdate()
		{
			Move();
			if (CBattleHelper.CheckTargetInAttackRange(Entity, m_targetEntity))
			{
				//move over
				StateTransite((int)E_RoleState.Attack);
			}
		}

		private void Move()
		{
			m_moveGobj.transform.position += m_moveVec * Time.deltaTime * m_moveSpeed;
		}

		public override void OnExitState()
		{
			base.OnExitState();
		}
    }
}
