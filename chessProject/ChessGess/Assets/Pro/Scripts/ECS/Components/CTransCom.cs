//***************
// name : CTransCom
// createtime: 5/3/2019
// desc: 
// create by asvo
//***************
using Asvo.ECS;
using UnityEngine;

namespace Asvo
{
	public class CTransCom :AComponent {

        public bool IsPosDirty;
        public Vector2 Postion {get; protected set;}

        public CTransCom()
        {
            IsPosDirty = false;
            Postion = Vector2.zero;
        }

        public void SetPos(Vector2 pos)
        {
            Postion = pos;
            IsPosDirty = true;
        }

        protected override void OnDestroy()
        {
        }
	}
}
