//***************
// name : CUnitProperty
// createtime: 6/3/2019
// desc: 
// create by asvo
//***************
using UnityEngine;

namespace Asvo
{
	[CreateAssetMenu(fileName = "New_battleUnitAsset", menuName = "Battle/创建角色属性")]
	[System.Serializable]
	public class CUnitProperty : ScriptableObject{
		// public AttributeSlot[] AttrList;
		public uint MaxHp;
		public uint Attack;
		public uint Defend;
		public uint MoveSpeed;
		//技能配置
	//	public uint[] SkillId;
		public CSkillConfig[] SkillList;
	}
}
