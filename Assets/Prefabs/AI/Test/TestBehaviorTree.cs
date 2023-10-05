using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BehaviorTree/TestBehaviorTree")]
public class TestBehaviorTree : BehaviorTree
{
    protected override void Construct(BTNode_Root root)
    {
        BTTask_Wait waitTask = ScriptableObject.CreateInstance<BTTask_Wait>();
        waitTask.SetWaitTime(3);
        
        BTTask_Log logTask = ScriptableObject.CreateInstance<BTTask_Log>();
        logTask.SetMessage("logging");

        Sequencer sequencer = ScriptableObject.CreateInstance<Sequencer>();
        sequencer.AddChild(waitTask);
        sequencer.AddChild(logTask);

        root.AddChild(waitTask);

    }
}
