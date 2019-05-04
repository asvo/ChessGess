/* ***********************************************
 * DelEditorPrefs
 * author :  created by asvo
 * function: EditorPrefs.DeleteAll
 *  ref: https://forum.unity.com/threads/exceptions-on-multiple-assets-ongui.476287/
 * history:  created at .
 * ***********************************************/

using UnityEngine;
using UnityEditor;

public class DeleteAllExample : ScriptableObject
{
    [MenuItem("Examples/EditorPrefs/Clear all Editor Preferences")]
    static void deleteAllExample()
    {
        if (EditorUtility.DisplayDialog("Delete all editor preferences.",
                "Are you sure you want to delete all the editor preferences? " +
                "This action cannot be undone.", "Yes", "No"))
        {
            Debug.Log("yes");
            EditorPrefs.DeleteAll();
        }
    }
}