using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PathEditorLinker : EditorWindow
{
    PathEditorLinkerView m_View;

    [MenuItem("PredeterminedPath/Path Linker")]
    public static void OpenPathEditorLinker()
    {
        PathEditorLinker window = GetWindow<PathEditorLinker>();
        window.titleContent = new GUIContent("Path Linker Editor");
    }

    private void OnEnable()
    {
        ConstructGraphView();
    }

    private void ConstructGraphView()
    {
        m_View = new PathEditorLinkerView
        {
            name = "path editor linker view"
        };

        m_View.StretchToParentSize();
        rootVisualElement.Add(m_View);
    }

    private void OnDisable()
    {
        m_View.SaveGraph();
        rootVisualElement.Remove(m_View);
    }

}
