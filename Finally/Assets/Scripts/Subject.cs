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

    //����¼��б��Ƿ�Ϊ�ա��ǿ���ִ��
    public void CheckAndApplyIssues()
    {
        if (issues != null)
        {
            issues();
        }
    }
}


