//***************
// name : CAnimCom
// createtime: 5/3/2019
// desc: 
// create by asvo
//***************
using Asvo.ECS;
using UnityEngine;
using Asvo.Common;
using System.Collections.Generic;

namespace Asvo
{
    public class CAnimCom : AComponent
    {
		private Animator m_animator;
		public Animator Animator{get {return m_animator;} set{m_animator = value;}}
		private Dictionary<E_AnimatorState, string> m_stateDict = null;
		public AnimatorStateInfo m_currentState { get; private set; }



		public CAnimCom()
		{
			SetupAnimator();
		}

		private void SetupAnimator()
		{
			m_stateDict = new Dictionary<E_AnimatorState, string>(8);
			var t = typeof(E_AnimatorState);
			foreach (E_AnimatorState key in System.Enum.GetValues(t))
            {
                m_stateDict[key] = System.Enum.GetName(t, key);
            }
		}

		private void StateEnter(Animator animator, AnimatorStateInfo info)
        {
            if (m_animator == null || animator != m_animator) return;
            
            m_currentState = info;
		}

		/// <summary>
        /// 程序融合切换动作
        /// </summary>
        /// <param name="targetState">目标动作</param>
        /// <param name="duration">融合时间</param>
        /// <param name="reset">如果当前动作是目标动作，是否重新播放</param>
        public void ForcePlayAnim(E_AnimatorState targetState,float duration = 0.0f,bool reset = false)
        {
#if ASVO_DEBUG
            var attri = Entity.GetComponent<CAttributeCom>();
            if (attri != null)
            	CLog.Log(SysLogType.Battle, "【动作ForcePlayAnim】UID = {0} 转到{1}动作", attri.ID, targetState);
#endif

            if (m_currentState.IsName(m_stateDict[targetState]))
            {
                CLog.Log(SysLogType.Battle, "已经在播放此动作，reset={0}",reset);
                if (reset)
                    m_animator.Play(m_stateDict[targetState], 0,0);
            }
            else
            {
                if (m_animator.HasState(0, Animator.StringToHash(m_stateDict[targetState])))
                {
                    if (duration == 0)//无融合时间，直切
                        m_animator.Play(m_stateDict[targetState],0,0);
                    else
                        m_animator.CrossFade(m_stateDict[targetState], duration);
                }
                else
                    CLog.Error(SysLogType.Animation, "{0}没有此动作", m_animator.name);
            }
        }

        protected override void OnDestroy()
        {
            m_animator = null;
			m_stateDict.Clear();
        }
    }
}
