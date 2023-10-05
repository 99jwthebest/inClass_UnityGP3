using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Compositor : BTNode, IBTNodeParent
{
    LinkedList<BTNode> children = new LinkedList<BTNode>();

    LinkedListNode<BTNode> current = null;

    // move to the next avaliable node, return true if there is one.
    protected bool Next()
    {
        if(current == null)
            return false;
        
        current = current.Next;
        return current != null;
    }

    protected override BTNodeResult Execute()
    {
        if(children.Count == 0) return BTNodeResult.Success;

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
        foreach(BTNode child in newChildren)
        {
            children.AddLast(child);
        }
    }

}
