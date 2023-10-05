using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTree : ScriptableObject
{
    BTNode_Root rootNode;
    public void PreConstruct()
    {
        if (!rootNode)
        {
            rootNode = ScriptableObject.CreateInstance<BTNode_Root>();
        }

        Construct(rootNode);
    }

    protected virtual void Construct(BTNode_Root root)
    {

    }

    public void Update()
    {
        rootNode.UpdateNode();
    }
}
