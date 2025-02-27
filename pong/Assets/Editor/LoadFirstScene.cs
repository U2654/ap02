// https://stackoverflow.com/questions/35586103/unity3d-load-a-specific-scene-on-play-mode
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public class EditorInit
{
    static EditorInit()
    {
        var pathOfFirstScene = EditorBuildSettings.scenes[0].path;
        var sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(pathOfFirstScene);
        EditorSceneManager.playModeStartScene = sceneAsset;
        Debug.Log(pathOfFirstScene + " was set as default play mode scene");
    }
}
#endif
