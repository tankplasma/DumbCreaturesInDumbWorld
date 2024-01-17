using DG.Tweening;
using Oculus.Platform;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Plastic.Newtonsoft.Json.Bson;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PathEditorLinkerView : GraphView
{
    List<PathNode> pathNodes = new List<PathNode>();

    PathManager pathManager;

    public PathEditorLinkerView()
    {
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());
        
        var grid = new GridBackground();
        Insert(0 , grid );
        grid.StretchToParentSize();
        
        InitAllPorts();

        graphViewChanged += OnViewChange;
    }

    GraphViewChange OnViewChange(GraphViewChange graphViewChange)
    {
        if (graphViewChange.edgesToCreate != null)
        {
            foreach (var edge in graphViewChange.edgesToCreate)
            {
                PathNode outputNode = edge.output.node as PathNode;
                PathNode inputNode = edge.input.node as PathNode;

                outputNode.path.SetNewCheckpoint(inputNode.SceneObject);
            }
        }

        if (graphViewChange.elementsToRemove != null)
        {
            foreach (var item in graphViewChange.elementsToRemove)
            {
                if ((Edge)item != null) //element is an edge
                {
                    Edge edge = (Edge)item;
                    PathNode outputNode = edge.output.node as PathNode;
                    PathNode inputNode = edge.input.node as PathNode;

                    outputNode.path.RemoveCheckpoint(inputNode.SceneObject);
                }
            }
        }

        EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());

        return graphViewChange;
    }

    Port GeneratePort(PathNode node, Direction portDirection, Port.Capacity capacity)
    {
        return node.InstantiatePort(Orientation.Horizontal, portDirection, capacity, typeof(IPath));
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        List<Port> result  = new List<Port>();
        ports.ForEach(p =>
        {
            if (p != startPort && startPort.node != p.node)
                result.Add(p);
        });

        return result;
    }

    void InitAllPorts()
    {
        MonoBehaviour[] scriptsInScene = Resources.FindObjectsOfTypeAll<MonoBehaviour>();

        foreach (var script in scriptsInScene)
        {
            if (script.GetType().GetInterfaces().Contains(typeof(IPath)))
            {
                PathNode node = GeneratePathNode((IPath)script, script);

                pathNodes.Add(node);

                AddElement(node);
            }
            if (script.GetType() == typeof(PathManager))
            {
                pathManager = (PathManager)script;
            }
        }

        foreach (var node in pathNodes)
        {
            List<MonoBehaviour> mono = node.path.GetCheckpoints();

            foreach (var path in pathNodes)
            {
                if (mono.Contains(path.SceneObject))
                {
                    MakeConnexion((Port)node.outputContainer[0], (Port)path.inputContainer[0]); 
                }
            }
        }
    }

    void MakeConnexion(Port outputPort , Port InputPort)
    {
        Edge edge = new()
        {
            output = outputPort,
            input = InputPort,
        };

        edge.input.Connect(edge);
        edge.output.Connect(edge);

        Add(edge);
    }


    private PathNode GeneratePathNode(IPath path , MonoBehaviour mono)
    {
        PathNode node = new PathNode
        {
            title = mono.name,
            path = path,
            SceneObject = mono
        };

         
        Port generatedOutputPort = GeneratePort(node, Direction.Output, Port.Capacity.Multi);
        generatedOutputPort.name = "next";
        node.outputContainer.Add(generatedOutputPort);
        
        Port generatedInputPort = GeneratePort(node, Direction.Input, Port.Capacity.Multi);
        generatedInputPort.name = "start";
        node.inputContainer.Add(generatedInputPort);

        node.RefreshExpandedState();
        node.RefreshPorts();

        node.SetPosition(new Rect(path.NodePos.x, path.NodePos.y, 100, 150));

        return node;
    }

    public void SaveGraph()
    {
        foreach (var node in pathNodes)
        {
            node.path.NodePos = node.GetPosition().position;
        }
    }
}
