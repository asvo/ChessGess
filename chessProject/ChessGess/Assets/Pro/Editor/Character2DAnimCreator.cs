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
using System.Collections.Generic;

namespace Asvo
{
    public class Character2DAnimCreator : Editor
    {
        static string s_characterResPosFix = "*.png";
        static string fromPath = "Assets/Art/Character";
        static string s_toAnimClipPath = "Assets/Pro/GameRes/Character/AnimationClip";
        static string s_toAnimatorPath = "Assets/Pro/GameRes/Character/Animator";
        static string s_toModelPath = "Assets/Pro/GameRes/Character/Model/";

        [MenuItem("Asvo/Tools/生成动画文件")]
        static void Generate2DCharacters()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(fromPath);
            foreach (var dir in dirInfo.GetDirectories())
            {
                if (!dir.Name.Equals("black01"))
                    continue;
                Debug.LogWarningFormat("deal with directory: {0}", dir.Name);
                List<AnimationClip> clips = Generate2DClips(dir);
                var animatorController = Generate2DAnimator(clips, dir);
                BuildPrefab(animatorController, dir);
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        static void BuildPrefab(AnimatorController controller, DirectoryInfo directoryInfo)
        {
            string savePath = s_toModelPath + directoryInfo.Name + ".prefab";
            Debug.LogWarningFormat("save prefab to path={0}.", savePath);
            GameObject gobj = new GameObject(directoryInfo.Name);
            FileInfo defaultFileInfo = directoryInfo.GetDirectories()[0].GetFiles(s_characterResPosFix)[0];
            SpriteRenderer spriteRenderer = gobj.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(ParseFullPathToAssetPath(defaultFileInfo.FullName));
            Animator animator = gobj.AddComponent<Animator>();
            animator.runtimeAnimatorController = controller;            
            PrefabUtility.CreatePrefab(savePath, gobj);
            DestroyImmediate(gobj);            
        }
        
        /// <summary>
        /// 生成控制器animator
        /// </summary>
        static AnimatorController Generate2DAnimator(List<AnimationClip> clips, DirectoryInfo directoryInfo)
        {
            string controllerFile = s_toAnimatorPath + "/" + directoryInfo.Name + ".controller";
            Debug.LogWarningFormat("save controller to path={0}.", controllerFile);
            var animatorController = AnimatorController.CreateAnimatorControllerAtPath(controllerFile);
            AnimatorStateMachine stateMachine = animatorController.layers[0].stateMachine;
            
            foreach(var clip in clips)
            {
                AnimatorState animatorState = stateMachine.AddState(clip.name);
                animatorState.motion = clip;
                var stateTransistion = stateMachine.AddAnyStateTransition(animatorState);
                stateTransistion.hasExitTime = true;
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return animatorController;
        }

        /// <summary>
        /// 根据某角色的目录资源生成该角色动画文件clips
        /// </summary>
        /// <param name="directoryInfo">目标目录</param>
        static List<AnimationClip> Generate2DClips(DirectoryInfo directoryInfo)
        {
            List<AnimationClip> clips = new List<AnimationClip>();
            foreach (var dir in directoryInfo.GetDirectories())
            {
                FileInfo[] fileInfos = dir.GetFiles(s_characterResPosFix, SearchOption.TopDirectoryOnly);
                if (fileInfos.Length == 0)
                {
                    continue;
                }
                AnimationClip clip = GenerateAnimClip(directoryInfo.Name, dir.Name, fileInfos);
                clips.Add(clip);
            }
            return clips;
        }

        static AnimationClip GenerateAnimClip(string modelName, string clipName, FileInfo[] fileInfos)
        {
            AnimationClip clip = new AnimationClip();
            //AnimationUtility.SetAnimationType(clip, ModelImporterAnimationType.Generic);
            EditorCurveBinding editorCurveBinding = new EditorCurveBinding();
            editorCurveBinding.type = typeof(SpriteRenderer);
            editorCurveBinding.path = "";
            editorCurveBinding.propertyName = "m_Sprite";
            int totalSpriteCnt = fileInfos.Length;
            ObjectReferenceKeyframe[] objectReferenceKeyframes = new ObjectReferenceKeyframe[totalSpriteCnt];
            float ethenalTime = 0.2f;
            for (int i = 0; i < fileInfos.Length; ++i)
            {
                string loadPath = ParseFullPathToAssetPath(fileInfos[i].FullName);
                Debug.LogWarningFormat("load sprite path={0}", loadPath);
                Sprite sp = AssetDatabase.LoadAssetAtPath<Sprite>(loadPath);
                var objectReferenceKeyframe = new ObjectReferenceKeyframe();
                objectReferenceKeyframe.time = i * ethenalTime;
                objectReferenceKeyframe.value = sp;
                objectReferenceKeyframes[i] = objectReferenceKeyframe;
            }
            clip.frameRate = 30;
            //save clip
            string assetRelatePath = s_toAnimClipPath + "/" + modelName;
            string saveDir = Path.Combine(Application.dataPath, assetRelatePath.Replace("Assets/", string.Empty));
            Debug.LogWarningFormat("save clip {0} to path={1}.", clipName, assetRelatePath);
            if (!Directory.Exists(saveDir))
            {
                Directory.CreateDirectory(saveDir);
            }
            AnimationUtility.SetObjectReferenceCurve(clip, editorCurveBinding, objectReferenceKeyframes);
            assetRelatePath += string.Format("/{0}.anim", clipName);
            AssetDatabase.CreateAsset(clip, assetRelatePath);
            AssetDatabase.SaveAssets();
            return clip;
        }

        static string ParseFullPathToAssetPath(string fullPath)
        {
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                return fullPath.Substring(fullPath.IndexOf("Assets\\"));
            }
            else
            {
                return fullPath.Substring(fullPath.IndexOf("Assets/"));
            }
        }
    }
}
