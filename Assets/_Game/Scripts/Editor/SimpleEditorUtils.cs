using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public static class SimpleEditorUtils
{
    static SimpleEditorUtils()
    {
        EditorSceneManager.playModeStartScene =
            AssetDatabase.LoadAssetAtPath<SceneAsset>(EditorBuildSettings.scenes[0].path);
    }
}