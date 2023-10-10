using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BTGraphNode : Node
{
    public BTNode Node { get; private set; }
    public BTGraphNode(BTNode node)
    {
        Node = node;
        title = node.GetType().Name;
    }
}
