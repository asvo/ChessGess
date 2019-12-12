//***************
// name : ECommonDef
// createtime: 5/4/2019
// desc: 枚举定义文件
// create by asvo
//***************

namespace Asvo
{
    public enum E_AnimatorState
    {
        idle = 0,
        walk = 1,
        attack = 2,
        dead = 3,
        Count
    }
    
	public enum E_RoleState
    {
        None = 0,
        Born,
        Idle,
        Walking,
        Attack,
        Damage,
        Dead
    }

    public enum E_BattleGroupType
    {
        Teammate,
        Enemy,
        //中立
        Mid,
    }

    public enum E_SkillEffectType
    {
        //伤害
        Damage = 0,
        //治疗
        Cure,
        //属性变化（提升攻击等）
        Attribute_Change,
    }
}
