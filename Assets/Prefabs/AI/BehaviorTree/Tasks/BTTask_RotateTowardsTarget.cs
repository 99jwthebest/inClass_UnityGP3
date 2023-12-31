using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTask_RotateTowardsTarget : BTNode
{
    [SerializeField] string keyName;
    [SerializeField] float acceptableAngle;

    GameObject target;
    GameObject owner;
    Blackboard blackboard;
    IMovementInterface movementInterface;

    protected override BTNodeResult Execute()
    {
        if (!GetBehaviorTree()) 
            return BTNodeResult.Failure;
        blackboard = GetBehaviorTree().GetBlackBoard();
        
        if(!blackboard) 
            return BTNodeResult.Failure;

        if(!blackboard.GetBlackboardData("owner", out owner))
            return BTNodeResult.Failure;

        movementInterface = owner.GetComponent<IMovementInterface>();
        if(movementInterface == null)
            return BTNodeResult.Failure;

        if (InAcceptableAngle())
        {
            return BTNodeResult.Success;
        }

        return BTNodeResult.InProgress;

    }

    protected override BTNodeResult Update()
    {
        if(InAcceptableAngle())
        {
            return BTNodeResult.Success;
        }

        movementInterface.RotateTowards(target);

        return BTNodeResult.InProgress;
    }

    private bool InAcceptableAngle()
    {
        return Vector3.Angle(owner.transform.forward, (target.transform.position - owner.transform.position)) < acceptableAngle; 
    }
 
}
