using BattleSystem;
using CharacterSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSubject : Subject { 
    public delegate void MoveIssue(Vector3 TargetPos);
    public MoveIssue moveIssues;

    public void AddObserve(MoveIssue Observe)
    {
        moveIssues += Observe;
    }
    public void RemoveObserve(MoveIssue Observe)
    {
        moveIssues -= Observe;
    }

}
