//***************
// name : CEntityTypeCom
// createtime: 5/3/2019
// desc: 
// create by asvo
//***************
using Asvo.ECS;

namespace Asvo
{
	public class CEntityTypeCom : AComponent {
		public E_EntityType EntityType;

        protected override void OnDestroy()
        {
            EntityType = E_EntityType.None;
        }
    }
	
	public enum E_EntityType
	{
		None = 0,
		Role,
	}
}
