//***************
// name : CLoadSys
// createtime: 5/3/2019
// desc: 
// create by asvo
//***************
using Asvo.ECS;
using Asvo.Common;
using UnityEngine;

namespace Asvo
{
	public class CLoadSys : ASystem {
		public CLoadSys()
		{
			m_process = new ASystemProcess(this, typeof(CRenderCom), typeof(CAttributeCom), typeof(CEntityTypeCom));
		}

        public override void UpdateAddEntity(CEntity entity)
        {
            LoadEntity(entity);
        }

        private void LoadEntity(CEntity entity)
        {
            var attributeCom = entity.GetComponent<CAttributeCom>();
            var gobj = ResourceMgr.Instance.Load(E_ResourceType.Model, attributeCom.Config.AssetName);            
            GameObject newGobj = GameObject.Instantiate(gobj);
            newGobj.name = attributeCom.Config.Name;
            var renderCom = entity.GetComponent<CRenderCom>();
            renderCom.GameObject = newGobj;
            renderCom.InitFinish();
        }
	}
}
