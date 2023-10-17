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
        style.left = node.GetGraphPosition().x;
        style.top = node.GetGraphPosition().y;
    }

    public override void SetPosition(Rect newPos)
    {
        base.SetPosition(newPos);
        Node.SetGraphPosition(new Vector2(newPos.xMin, newPos.yMin));

    }
}
