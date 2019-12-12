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

			CEngine.Instance.AddLogicSys(new CFsmSys());
			CEngine.Instance.AddLogicSys(new CBattleEntitySys());

			//init scene
			SceneGlobal.Root3D = GameObject.Find("RootB").transform;
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
            if (GUILayout.Button("Test-Battle"))
            {
                CBattleEntityMgr.Instance.CreateATestEntity(E_BattleGroupType.Teammate);
				CBattleEntityMgr.Instance.CreateATestEntity(E_BattleGroupType.Enemy);
            }
        }
	}
}
