//***************
// name : CRenderCom
// createtime: 5/3/2019
// desc: Unity GameObject数据
// create by asvo
//***************
using Asvo.ECS;
using UnityEngine;

namespace Asvo
{
    public class CRenderCom : AComponent
    {
		/// <summary>
		/// 资源是否加载完成
		/// </summary>
		/// <value></value>
        public bool IsAssetLoaded { get; protected set; }
		public float LoadProgress;
		/// <summary>
		/// rendercom是否准备OK
		/// </summary>
		/// <value></value>
		public bool IsInited {get; protected set;}
        public GameObject GameObject;
        
        public void InitFinish()
        {
            IsAssetLoaded = true;
            IsInited = true;
        }

        protected override void OnDestroy()
        {
            ClearRenderer();
            IsAssetLoaded = false;
            IsInited = false;
        }

        private void ClearRenderer()
        {
            if (GameObject != null)
            {
                Object.Destroy(GameObject);
            }
        }
    }
}
