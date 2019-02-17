//***************
// name : Character2DAnimCreator 
// createtime: 1/19/2019
// desc:  根据指定目录下的2d动画帧序列文件，生成2D动画.
// create by asvo
//***************
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEditor.Animations;

namespace Asvo
{
	public class Character2DAnimCreator : Editor {
		
		static string fromPath = "Art/Character";
			static string toAnimPath = "Pro/GameRes/Character/Animator";
			static string toModelPath = "Pro/GameRes/Character/Model";
		static void Generate2DAnimators()
		{
			
			//for temp
			fromPath += "/black/1001";
			string animName = "attack";

			DirectoryInfo dirInfo = new DirectoryInfo(fromPath);
			if (!dirInfo.Exists)
				return;
			FileInfo[] fileInfos = dirInfo.GetFiles("*.png", SearchOption.TopDirectoryOnly);
			if (fileInfos.Length == 0)
				return;
			GameObject gobj = new GameObject("Anim_" + animName);
			gobj.AddComponent<SpriteRenderer>();
			Animator genAnimator = gobj.AddComponent<Animator>();
			foreach(var fileInfo in fileInfos)
			{
				Sprite sp = AssetDatabase.LoadAssetAtPath<Sprite>(Path.Combine(fromPath, fileInfo.Name));

			}
		}

		static void Generate2DMotion()
		{
			
		}
	}
}
