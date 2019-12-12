//***************
// name : CSkillConifg
// createtime: 11/8/2019
// desc: 
// create by asvo
//***************
using UnityEngine;

namespace Asvo
{
	[CreateAssetMenu(fileName = "New_SkillConfigAsset", menuName = "Battle/创建技能配置")]
	[System.Serializable]
	public class CSkillConfig : ScriptableObject
	{
		public uint SkillId;
		//技能动作
		public E_AnimatorState AttackMotion;
		public CSKillKeyframe[] Keyframes;
	}
	
	/// <summary>
	/// 技能关键帧
	/// </summary>
	[System.Serializable]
	public class CSKillKeyframe {
		//时间点
		public float Time;
		//技能效果
		public CSkillEffect[] Effects;
		//特效
		public string VfxName;
	}

	[System.Serializable]
	public class CSkillEffect {
		//效果类型
		public E_SkillEffectType EffectType;
		//效果值（追加值）
		public uint EffectValue;
	}
}
