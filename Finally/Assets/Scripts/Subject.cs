using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subject
{
    public delegate void Issue();
    public Issue issues;
    public int issuesCount;



    public void AddObserve(Issue Observe)
    {
        issues += Observe;
        issuesCount++;
    }
    public void RemoveObserve(Issue Observe)
    {
        issues -= Observe;
        issuesCount--;
    }

    //检测事件列表是否为空。非空则执行
    public void CheckAndApplyIssues()
    {
        if (issues != null)
        {
            issues();
        }
    }
}


