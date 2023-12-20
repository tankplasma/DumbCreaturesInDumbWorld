using Codice.Client.Common;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;

/*public enum connexionType
{
    input,
    output
}

public class PathNode
{
    public PathNode(MonoBehaviour script)   
    {
        SceneObject = script;
        path = (IPath)script;
        node = new Rect(100,100,200,200);
    }

    public Rect node;
    public int id;
    public MonoBehaviour SceneObject;
    public IPath path;
}*/



// The second argument in the EditorToolAttribute flags this as a Component tool. That means that it will be instantiated
// and destroyed along with the selection. EditorTool.targets will contain the selected objects matching the type.
public class PathEditorTool : EditorWindow
{
    /*private List<PathNode> pathNodes = new List<PathNode>();

    [MenuItem("PredeterminedPath/PathLinker")]
    private static void ShowWindow()
    {
        PathEditorTool pathEditor = GetWindow<PathEditorTool>("Path Linker");
        pathEditor.Init();
    }

    Vector2 mousePos;

    bool isClicking , isOnSelection;

    Vector2 startLinePos;

    KeyValuePair<PathNode,connexionType> firstNodeInteraction;
    KeyValuePair<PathNode,connexionType> secondNodeInteraction;

    public void Init()
    {
        pathNodes.Clear();

        MonoBehaviour[] scriptsInScene = Resources.FindObjectsOfTypeAll<MonoBehaviour>();

        foreach (var script in scriptsInScene)
        {
            if (script.GetType().GetInterfaces().Contains(typeof(IPath)))
            {
                pathNodes.Add(new PathNode(script));
            }
        }
    }

    void OnGUI()
    {
        if (GUILayout.Button("refresh"))
        {
            Init(); 
        }

        mousePos = Event.current.mousePosition;

        if (isOnSelection)
        {
            DrawNodeCurve(Vector2.down*5, Vector2.right*5);
        }

        if (isClicking && Event.current.type == EventType.MouseUp)
        {
            isClicking = false;
            OnClickedRelease();
        }
        if (!isClicking && Event.current.type == EventType.MouseDown)
        {
            isClicking= true;
            OnClicked();
        }

        BeginWindows();
        for (int i = 0; i < pathNodes.Count; i++)
        {
            pathNodes[i].node = GUI.Window(i, pathNodes[i].node, DrawNodeWindow, pathNodes[i].SceneObject.name);
            pathNodes[i].id = i;
        }
        EndWindows();
    }

    void OnClicked()
    {
        
    }

    void OnClickedRelease()
    {

    }

    PathNode GetPathNodeByID(int id)
    {
        return pathNodes.Where(node => node.id == id).ToList()[0];
    }

    void DrawNodeWindow(int id)
    {  
        PathNode node = GetPathNodeByID(id);

        // Dessiner une image à gauche de la fenêtre
        Rect leftImageRect = new Rect(0, 100, 20, 20);
        Texture leftImage = AssetDatabase.LoadAssetAtPath<Texture>("Assets\\Oculus\\SampleFramework\\Core\\DebugUI\\Textures/button.png");
        GUI.DrawTexture(leftImageRect, leftImage);
        
        // Dessiner une image à droite de la fenêtre
        Rect rightImageRect = new Rect(180, 100, 20, 20);
        Texture rightImage = AssetDatabase.LoadAssetAtPath<Texture>("Assets\\Oculus\\SampleFramework\\Core\\DebugUI\\Textures/button.png");
        GUI.DrawTexture(rightImageRect, rightImage);

        if (isOnSelection)
        {
            if (!isClicking)
            {
                isOnSelection = false;

                if (leftImageRect.Contains(Event.current.mousePosition))
                {
                    isOnSelection = true;
                    secondNodeInteraction = new KeyValuePair<PathNode, connexionType>(node, connexionType.input);
                    Debug.Log("end left selection");
                }
                else if (rightImageRect.Contains(Event.current.mousePosition))
                {
                    isOnSelection = true;
                    secondNodeInteraction = new KeyValuePair<PathNode, connexionType>(node, connexionType.output);
                    Debug.Log("end right selection");
                }
            }
        }
        else
        {
            if (isClicking)
            {
                if (leftImageRect.Contains(Event.current.mousePosition))
                {
                    isOnSelection = true;
                    startLinePos = mousePos;
                    firstNodeInteraction = new KeyValuePair<PathNode, connexionType>(node, connexionType.input);
                    Debug.Log("start left selection");
                }
                else if (rightImageRect.Contains(Event.current.mousePosition))
                {
                    isOnSelection = true;
                    startLinePos = mousePos;
                    firstNodeInteraction = new KeyValuePair<PathNode, connexionType>(node, connexionType.output);
                    Debug.Log("start right selection");
                }
                else
                {
                    GUI.DragWindow();
                }
            }
        }
    }

    void DrawPointNode() 
    {
        
    }

    void DrawNodeCurve(Vector2 start, Vector2 end)
    {
        Debug.Log("drawing");

        Vector3 startPos = new Vector3(start.x , start.y, 0);
        Vector3 endPos = new Vector3(end.x, end.y, 0);
        Vector3 startTan = startPos + Vector3.right * 50;
        Vector3 endTan = endPos + Vector3.left * 50;
        Color shadowCol = new Color(0, 0, 0, 0.06f);
        for (int i = 0; i < 3; i++) // Draw a shadow
            Handles.DrawBezier(startPos, endPos, startTan, endTan, shadowCol, null, (i + 1) * 5);
        Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.black, null, 1);
    }*/
}
