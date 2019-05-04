//***************
// name : CWorld
// createtime: 5/3/2019
// desc: game world
// create by asvo
//***************
using Asvo.Common;
using Asvo.ECS;
using UnityEngine;

namespace Asvo
{
	public class CWorld : MonoBehaviour {
		
		protected void Start()
		{
			CEngine.Instance.AddPhysicSys(new CMoveSys());
            CEngine.Instance.AddLogicSys(new CLoadSys());            
		}
		
		protected void Update()
		{
			CEngine.Instance.Update(Time.deltaTime);
		}

		protected void FixedUpdate()
		{
			CEngine.Instance.FixUpdate(Time.fixedDeltaTime);
		}

        //test
        void OnGUI()
        {
            if (GUILayout.Button("Test-CreateEntity"))
            {
                CreateAEntity();
            }
        }

        private void CreateAEntity()
        {
            var fsmCom = new CFsmCom();
            new CRoleBorn(fsmCom.FsmMgr);
            new CIdleState(fsmCom.FsmMgr);
            var entity = CEngine.Instance.CreateEntity(new System.Collections.Generic.List<AComponent>
                    {
                        new CAttributeCom(){Config = new RoleConfig(){Name = "soldier01", AssetName = "black01"}},
                        new CTransCom(),
                        new CRenderCom(),
                        new CEntityTypeCom(){EntityType = E_EntityType.Role},
                        fsmCom
                    });
            CEngine.Instance.AddEntity(entity);
        }
	}
}
