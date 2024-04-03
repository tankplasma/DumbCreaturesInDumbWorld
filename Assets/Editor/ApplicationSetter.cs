using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using System.Collections.Generic;
using UnityToolbarExtender;
using System.Diagnostics;

static class ToolbarStyles
{
    public static readonly GUIStyle commandButtonStyle;

    static ToolbarStyles()
    {
        commandButtonStyle = new GUIStyle("Command")
        {
            fontSize = 12,
            alignment = TextAnchor.MiddleCenter,
            imagePosition = ImagePosition.ImageAbove,
            fontStyle = FontStyle.Normal,
            fixedWidth = 200,
        };
    }
}

[InitializeOnLoad]
public class ApplicationSetter
{
    private static int selectedSceneIndex = 0;
    private static string[] sceneNames;

    static ApplicationSetter()
    {
        ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUILeft);
    }

    static void OnToolbarGUILeft()
    {
        List<string> sceneNamesList = new List<string>();
        for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
        {
            string scenePath = EditorBuildSettings.scenes[i].path;
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            sceneNamesList.Add(sceneName);
        }
        sceneNames = sceneNamesList.ToArray();

        selectedSceneIndex = EditorGUILayout.Popup(selectedSceneIndex, sceneNames, ToolbarStyles.commandButtonStyle);

        if (GUI.changed)
        {
            string scenePath = EditorBuildSettings.scenes[selectedSceneIndex].path;
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            EditorSceneManager.OpenScene(scenePath);
        }
    }
}
