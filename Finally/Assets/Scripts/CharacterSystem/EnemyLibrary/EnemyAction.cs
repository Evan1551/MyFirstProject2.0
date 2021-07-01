using CharacterSystem;
using CommandSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAction:MonoBehaviour
{
    //int I { get; set; }

    //private int _i;
    //public int I { get { return _i; } set { _i = value; _i++; } }

    public Hero_Model hero_Model;
    public CommandLibrary cmdLib;
    public EnemyAction()
    {
        hero_Model = Hero_Model.Instance;
        cmdLib = CommandLibrary.Instance;
    }
    //显示意图
    public void ShowIntention()
    {

    }
    //执行意图
    public abstract void DoAction();
}
