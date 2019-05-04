//***************
// name : CMoveCom
// createtime: 5/3/2019
// desc: 
// create by asvo
//***************
using Asvo.ECS;
using UnityEngine;

namespace Asvo
{
    public class CMoveCom : AComponent
    {
		public Vector2 Postion;

        protected override void OnDestroy()
        {
			Postion = Vector2.zero;
        }
    }
}
