using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class ConstructableNode : Node
{
    public string nodeName;
    public PathCheckpoint checkpoint;
    public List<ConstructableNode> nextCheckpoints = new List<ConstructableNode>();

    public List<Edge> LinkNodes(ConstructableNode parentNode)
    {
        List<Edge> edges = new List<Edge>();

        Edge tempEdge = new Edge
        {
            output = (Port)parentNode.outputContainer[0],
            input = (Port)inputContainer[0]
        };

        tempEdge.input.Connect(tempEdge);
        tempEdge.output.Connect(tempEdge);

        edges.Add(tempEdge);

        if (nextCheckpoints.Count < 1)
            return edges;

        foreach (var item in nextCheckpoints)
        {
            edges.AddRange(item.LinkNodes(this));
        }
        return edges;
    }
}

[System.Serializable]
public class NodeContainer
{
    [SerializeField]
    public string pathName;
    [SerializeField]
    public PathCheckpoint checkpoint;

    public void DrawNodeInInspector()
    {
        try
        {
            GUILayout.BeginVertical();

            pathName = GUILayout.TextField(pathName);
            checkpoint = (PathCheckpoint)EditorGUILayout.ObjectField(checkpoint, typeof(PathCheckpoint), true);

            GUILayout.EndVertical();
        }
        catch (System.Exception)
        {
            throw;
        }

    }
}

[CreateAssetMenu(fileName ="Path Center", menuName ="Scriptables/PathCenter")]
public class ScriptablePathCenter : ScriptableObject
{
    public List<ConstructableNode> nodes = new List<ConstructableNode>();

    public void AddNode(NodeContainer n)
    {
        Debug.Log(n.pathName);
        ConstructableNode node = new ConstructableNode
        {
            nodeName = n.pathName,
            checkpoint = n.checkpoint,
        };
        nodes.Add(node);
    }

    public void RemoveNode(NodeContainer n)
    {
        foreach (ConstructableNode node in nodes)
        {
            try
            {
                if (node.name == n.pathName)
                    nodes.Remove(node);
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    } 
}
