using CharacterSystem;
using CommandSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero_Model
{
    Hero_Model()
    {
        ini();
    }
    private static Hero_Model hero_Model = null;
    public static Hero_Model Instance
    {
        get
        {
            if (hero_Model == null)
            {
                hero_Model = new Hero_Model();
            }
            return hero_Model;
        }
    }
    public Subject MPorAPChange = new Subject();
    //行动点
    #region 属性 CurrentAP
    private int _currentAP;
    public int CurrentAP
    {
        get { return _currentAP; }
        set
        {
            _currentAP = value;
            MPorAPChange.CheckAndApplyIssues();
        }
    }
    #endregion
    #region 属性 MaxAP
    private int _maxAP;
    public int MaxAP
    {
        get { return _maxAP; }
        set
        {
            _maxAP = value;
            MPorAPChange.CheckAndApplyIssues();
        }
    }
    #endregion
    //移动点
    #region 属性 CurrentMP
    private int _currentMP;
    public int CurrentMP
    {
        get { return _currentMP; }
        set
        {
            if (value > MaxMP) { _currentMP = MaxMP; }
            else if (value < 0) { _currentMP = 0;
            }
            else
            {
                _currentMP = value;
            }
            MPorAPChange.CheckAndApplyIssues();
        }
    }
    #endregion
    #region 属性 MaxMP
    private int _maxMP;
    public int MaxMP
    {
        get { return _maxMP; }
        set
        {
            _maxMP = value;
            MPorAPChange.CheckAndApplyIssues();
        }
    }
    #endregion

    public Character character;
    public bool UsingCard=false;
    public int CardDistance;

    public void ini()
    {
        MaxAP = 3;
        MaxMP = 3;
        CurrentAP = 3;
        CurrentMP = 1;
        character = null;
        UsingCard = false;
        CardDistance = 0;
    }
}



   

