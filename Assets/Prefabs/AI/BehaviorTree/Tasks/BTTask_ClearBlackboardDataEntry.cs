using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTask_ClearBlackboardDataEntry : BTNode
{
    [SerializeField] string entryKeyName;

    protected override BTNodeResult Execute()
    {
        if (!GetBehaviorTree()) return BTNodeResult.Failure;

        Blackboard blackboard = GetBehaviorTree().GetBlackBoard();
        if (!blackboard) return BTNodeResult.Failure;

       
        blackboard.ClearBlackboardData(entryKeyName);
        return BTNodeResult.Success;
    }
}
