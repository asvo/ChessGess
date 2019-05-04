//***************
// name : CAttributeCom
// createtime: 5/3/2019
// desc: 属性
// create by asvo
//***************
using System.Collections.Generic;
using Asvo.ECS;

namespace Asvo
{
	public class CAttributeCom : AComponent {
        public uint ID;
        public RoleConfig Config;
		public uint Hp;
		protected Dictionary<E_AttributeType, uint> m_attriDict;

		public CAttributeCom()
		{
			m_attriDict = new Dictionary<E_AttributeType, uint>();
			Hp = 1;
		}

		public void SetAttr(E_AttributeType attributeType, uint value)
		{
			m_attriDict[attributeType] = value;
		}

		public uint GetAttr(E_AttributeType attributeType)
		{
			return m_attriDict[attributeType];
		}

        protected override void OnDestroy()
        {
			Hp = 0;
			m_attriDict.Clear();
			m_attriDict = null;
        }
    }
	
	public enum E_AttributeType
	{
		None = 0,
		CurrentHp,
		MaxHp,
		Attack,
		Defence,
		MoveSpeed,
		AttackSpeed
	}

#region config-data
    public class RoleConfig
    {
        public string Name;
        public byte UnitType;
        public string AssetName;
    }
#endregion config-data
}
