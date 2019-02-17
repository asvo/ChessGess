//***************
// name : ScriptTemplateDos 
// createtime: #CREATEDATE#
// desc: 
// create by asvo
//***************
using UnityEngine;
using UnityEditor;

namespace Asvo
{
	public class ScriptTemplateDos : UnityEditor.AssetModificationProcessor{
		static void OnWillCreateAsset (string path)
		{
			path = path.Replace(".meta", "");
			if (!path.EndsWith(".cs") && !path.EndsWith(".js") && !path.EndsWith(".lua"))
			{
				return;
			}
			int idx = Application.dataPath.LastIndexOf("Assets");
			path = Application.dataPath.Substring(0, idx) + path;
			string fileTxt = System.IO.File.ReadAllText(path, System.Text.Encoding.UTF8);

			fileTxt = fileTxt.Replace("#CREATEDATE#", System.DateTime.Now.ToString("d"));
			
			System.IO.File.WriteAllText(path, fileTxt, System.Text.Encoding.UTF8);
			AssetDatabase.Refresh();
		}
	}
}
