//***************
// name : ResourceMgr
// createtime: 5/4/2019
// desc: 
// create by asvo
//***************
using UnityEngine;

namespace Asvo.Common
{
    public class ResourceMgr : ISingelton<ResourceMgr>
    {
        public GameObject Load(E_ResourceType resourceType, string name)
        {
            string loadPath = string.Format("{0}/{1}.prefab", GetPath(resourceType), name);
            var gobj = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>(loadPath);
            return gobj;
        }

        private string GetPath(E_ResourceType resourceType)
        {
            return ResourceHelper.paths[(int)resourceType];
        }
	}

    public enum E_ResourceType
    {
        Model = 0,

    }

    public static class ResourceHelper
    {
        public static string[] paths = 
        {
            "Assets/Pro/GameRes/Character/Model"
        };
    }
}
