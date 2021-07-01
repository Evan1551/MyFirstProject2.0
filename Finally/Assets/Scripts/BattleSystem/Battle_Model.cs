using CharacterSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle_Model
{
    Battle_Model()
    {

    }
    private static Battle_Model battle_Model = null;
    public static Battle_Model Instance
    {
        get
        {
            if (battle_Model == null)
            {
                battle_Model = new Battle_Model();
            }
            return battle_Model;
        }
    }
    //
    public Character Current;
    public List<Character> Targets=new List<Character>();

    //
    public List<int> CommandValues=new List<int>();
    public Subject CommandList=new Subject();

    //
    public List<BattleFieldCheck> BFC_List = new List<BattleFieldCheck>();

    //
    public List<Character> enemys=new List<Character>();//敌人实体的列表

    public void CalcuAllDistance()
    {
        foreach(var v in BFC_List)
        {
            v.CalcuDistance();
        }
    }

    public void ResetData()
    {

    }

   public void ini()
    {
        enemys.Clear();
        BFC_List.Clear();
        CommandValues.Clear();
        CommandList = new Subject();
        Targets.Clear();
        Current = null;
    }
}
