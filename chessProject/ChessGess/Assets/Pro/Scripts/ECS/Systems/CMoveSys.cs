//***************
// name : CMoveSys
// createtime: 5/3/2019
// desc: 
// create by asvo
//***************
using Asvo.ECS;

namespace Asvo
{
	public class CMoveSys : ASystem{
		public CMoveSys()
		{
			this.m_process = new ASystemProcess(this, typeof(CTransCom), typeof(CRenderCom), typeof(CAttributeCom));
		}

		public override void Update(CEntity entity)
		{
			CRenderCom renderCom = entity.GetComponent<CRenderCom>();
            if (!renderCom.IsInited)
                return;
            CTransCom transCom = entity.GetComponent<CTransCom>();
            if (transCom.IsPosDirty)
                renderCom.GameObject.transform.position = transCom.Postion;
		}
	}
}
