using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum BTNodeResult
{
    Success,
    InProgress,
    Failure
}

public enum BTNodePortType
{
    None,
    Single,
    Multi
}

public abstract class BTNode : ScriptableObject
{
    public delegate void OnNodesStateChanged(BTNodeResult newState);
    public event OnNodesStateChanged onNodeStateChanged;
    bool isStarted = false;

    [SerializeField]
    [HideInInspector]
    Vector2 graphPos;

    [SerializeField]
    [HideInInspector]
    string guid = "";


    public virtual BTNodePortType GetInputPortType()
    {
        return BTNodePortType.Single;
    }

    public virtual BTNodePortType GetOutputPortType()
    {
        return BTNodePortType.None;
    }



    // UpdateNode will be called by an update function in a monobehavior in the future.
    public BTNodeResult UpdateNode()
    {
        if(!isStarted)
        {
            BTNodeResult executeResult = Execute();
            onNodeStateChanged?.Invoke(executeResult);
            isStarted = true;
            // if not in progress, we have wither failed or succesded
            if(executeResult != BTNodeResult.InProgress)
            {
                End();
                return executeResult;
            }
        }

        BTNodeResult updateResult = Update();
        onNodeStateChanged?.Invoke(updateResult);

        if(updateResult != BTNodeResult.InProgress)
        {
            End();
        }

        return updateResult;
    }

    protected virtual void End()
    {
        isStarted = false;
    }

    protected virtual BTNodeResult Update()
    {
        return BTNodeResult.Success;
    }

    protected virtual BTNodeResult Execute()
    {
        return BTNodeResult.Success;
    }

    public void SetGraphPosition(Vector2 newPos)
    {
        graphPos = newPos;
    }

    public Vector2 GetGraphPosition()
    {
        return graphPos;
    }

    public virtual bool Contains(BTNode node)
    {
        return this == node;
    }

    public string GetGUID()
    {
        if (guid == "")
        {
            guid = GUID.Generate().ToString();
        }

        return guid;
    }

    public virtual BTNode CloneNode() 
    {
        return Instantiate(this);
    }
}
