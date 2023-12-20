using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PathNode : Node
{
    public MonoBehaviour SceneObject;
    public IPath path;
}
