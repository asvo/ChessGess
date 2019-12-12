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

        public ScriptableObject LoadAsset(E_ResourceType assetType, string name)
        {
            string loadPath = string.Format("{0}/{1}", GetPath(assetType), name);
            return UnityEditor.AssetDatabase.LoadAssetAtPath<ScriptableObject>(loadPath);
        }
        
        private string GetPath(E_ResourceType resourceType)
        {
            return ResourceHelper.paths[(int)resourceType];
        }
	}

    public enum E_ResourceType
    {
        Model = 0,
        SkillAsset,
        UnitAsset
    }

    public static class ResourceHelper
    {
        public static string[] paths = 
        {
            "Assets/Pro/GameRes/Character/Model",
            "Assets/Pro/GameRes/Battle/Skills",
            "Assets/Pro/GameRes/Battle/Units",
        };

        
    }
}
