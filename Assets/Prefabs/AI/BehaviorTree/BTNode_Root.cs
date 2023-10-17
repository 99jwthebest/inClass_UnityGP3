using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTNode_Root : BTNode, IBTNodeParent
{
    [SerializeField]
    BTNode child;

    public override BTNodePortType GetInputPortType()
    {
        return BTNodePortType.None;
    }

    public override BTNodePortType GetOutputPortType()
    {
        return BTNodePortType.Single;
    }

    public void AddChild(BTNode childToAdd)
    {
        child = childToAdd;
    }

    public List<BTNode> GetChildren()
    {
        return new List<BTNode> { child };
    }

    public void RemoveChild(BTNode childToRemove)
    {
        if (child == childToRemove)
        {
            child = null;
        }
    }

    public void SetChildren(List<BTNode> newChildren)
    {
        if (newChildren.Count != 0)
        {
            child = newChildren[0];
        }
    }

    protected override BTNodeResult Execute()
    {
        return BTNodeResult.InProgress;
    }

    protected override BTNodeResult Update()
    {
        return child.UpdateNode();
    }

    public override bool Contains(BTNode node)
    {
        if (child.Contains(node))
        {
            return true;
        }

        return base.Contains(node);
    }
}
