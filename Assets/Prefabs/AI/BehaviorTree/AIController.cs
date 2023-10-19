using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] BehaviorTree behaviorTreeAsset;

    BehaviorTree behaviorTree;

    // Start is called before the first frame update
    void Start()
    {
        behaviorTree = behaviorTreeAsset.CloneTree();
        behaviorTree?.PreConstruct();  // question mark means null check, instead of having to write and if statement for null check
    }

    public BehaviorTree GetBehaviorTree() 
    {
        if (behaviorTree)
        {
            return behaviorTree; 
        }

        return behaviorTreeAsset;
    }

    // Update is called once per frame
    void Update()
    {
        behaviorTree?.Update();
    }
}
