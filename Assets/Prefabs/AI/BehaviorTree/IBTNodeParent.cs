using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// interface can be think of as an abstract class that has all abstract functions and no data members
// you can inherit from multiple interfaces but not multiples classes so that's why interfaces are important to use
public interface IBTNodeParent
{
    void SetChildren(List<BTNode> newChildren);
    List<BTNode> GetChildren();

    void RemoveChild(BTNode childToRemove);
    void AddChild(BTNode childToAdd);
    void SortChildren();
}
