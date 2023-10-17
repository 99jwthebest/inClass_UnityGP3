using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Compositor : BTNode, IBTNodeParent
{
    LinkedList<BTNode> children = new LinkedList<BTNode>();

    LinkedListNode<BTNode> current = null;

    public override BTNodePortType GetOutputPortType()
    {
        return BTNodePortType.Multi;
    }

    //move to the next avaliable node, return true if there is one.
    protected bool Next()
    {
        current = current.Next;
        return current != null;
    }

    protected override BTNodeResult Execute()
    {
        if (children.Count == 0) return BTNodeResult.Success;

        current = children.First;
        return BTNodeResult.InProgress;
    }

    protected BTNodeResult UpdateCurrent()
    {
        return current.Value.UpdateNode();
    }

    protected override void End()
    {
        base.End();
    }

    public void AddChild(BTNode childToAdd)
    {
        children.AddLast(childToAdd);
    }

    public List<BTNode> GetChildren()
    {
        return children.ToList();
    }

    public void RemoveChild(BTNode childToRemove)
    {
        children.Remove(childToRemove);
    }

    public void SetChildren(List<BTNode> newChildren)
    {
        children.Clear();
        foreach (BTNode child in newChildren)
        {
            children.AddLast(child);
        }
    }

    public override bool Contains(BTNode node)
    {
        foreach (BTNode child in children)
        {
            if (child.Contains(node))
            {
                return true;
            }
        }

        return base.Contains(node);
    }

}
