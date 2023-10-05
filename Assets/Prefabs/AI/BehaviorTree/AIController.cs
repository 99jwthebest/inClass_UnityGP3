using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] BehaviorTree behaviorTree;

    // Start is called before the first frame update
    void Start()
    {
        behaviorTree?.PreConstruct();  // question mark means null check, instead of having to write and if statement for null check
    }

    // Update is called once per frame
    void Update()
    {
        behaviorTree?.Update();
    }
}
