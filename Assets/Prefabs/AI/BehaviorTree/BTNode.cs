using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BTNodeResult
{
    Success,
    InProgress,
    Failure
}

public abstract class BTNode : ScriptableObject
{
    bool isStarted = false;

    // UpdateNode will be called by an update function in a monobehavior in the future.
    public BTNodeResult UpdateNode()
    {
        if(!isStarted)
        {
            BTNodeResult executeResult = Execute();
            isStarted = true;
            // if not in progress, we have wither failed or succesded
            if(executeResult != BTNodeResult.InProgress)
            {
                End();
                return executeResult;
            }
        }

        BTNodeResult updateResult = Update();
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
}
