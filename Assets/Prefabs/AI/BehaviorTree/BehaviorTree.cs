using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "BehaviorTree/BehaviorTree")]
public class BehaviorTree : ScriptableObject
{
    [SerializeField]
    BTNode_Root rootNode;

    [SerializeField]
    List<BTNode> nodes;

    //getter or accssor for the nodes
    public List<BTNode> GetNodes() { return nodes; }
    public void PreConstruct()
    {
        if (!rootNode)
        {
            rootNode = CreateNode(typeof(BTNode_Root)) as BTNode_Root;
            SaveTree();
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

    public BTNode CreateNode(System.Type nodeType)
    {
        BTNode newNode = ScriptableObject.CreateInstance(nodeType) as BTNode;
        newNode.name = nodeType.Name;
        nodes.Add(newNode);
        AssetDatabase.AddObjectToAsset(newNode, this);

        SaveTree();

        return newNode;
    }

    public void SaveTree()
    {
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssetIfDirty(this);
    }

    public void RemoveNode(BTNode node)
    {
        nodes.Remove(node);
        AssetDatabase.RemoveObjectFromAsset(node);
        SaveTree();
    }
}
