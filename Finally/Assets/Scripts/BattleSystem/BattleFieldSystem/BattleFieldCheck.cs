using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterSystem;
using CommandSystem;
using BattleSystem;

public class BattleFieldCheck: MonoBehaviour
{
    private Character_Ctrller character_Ctrller;
    public BFC_View bFC_View;
    internal BFC_Model bFC_Model;

    public int x;
    public int y;
    public int distance;

    protected CharacterSystem.CharacterInfo characterInfo;
    protected bool hasHeroOn;

    private CommandLibrary cmdLib;




    private void Start()
    {
        Battle_Model.Instance.BFC_List.Add(this);
        bFC_Model = new BFC_Model();
        bFC_Model.x = int.Parse(name[7].ToString());
        bFC_Model.y = int.Parse(name[8].ToString());
        x = bFC_Model.x;
        y = bFC_Model.y;
        distance = bFC_Model.Distance;
        cmdLib = CommandLibrary.Instance;
        character_Ctrller = Character_Ctrller.Instance;
    }
    public int CalcuDistance()
    {
        bFC_Model.Distance = (Mathf.Abs(Hero_Model.Instance.character.BFC.bFC_Model.x - bFC_Model.x) + Mathf.Abs(Hero_Model.Instance.character.BFC.bFC_Model.y - bFC_Model.y));
        return bFC_Model.Distance;
    }

    /// <summary>
    /// 当点击地板按钮时，玩家角色Hero移动
    /// </summary>
    public void ClickAndMove()
    {
        if (Hero_Model.Instance.CurrentMP >= bFC_Model.Distance&&bFC_View.character==null)
        {
            Hero_Model.Instance.CurrentMP -= bFC_Model.Distance;
            
            cmdLib.CharacterMove(Hero_Model.Instance.character, new GridVector2(x, y));
            RefreshMoveDistance();
        }
    }

    internal void ResetGridPos()    
    {
        x = (int)(transform.position.x + 350) / 100+1;
        y = (int)(transform.position.z + 150) / 100+1;
        //Debug.Log("x:"+x+" y:"+y);
    }

    public static void RefreshMoveDistance()
    {
        Battle_Model.Instance.CalcuAllDistance();
        foreach(var v in Battle_Model.Instance.BFC_List)
        {
            if (v.bFC_Model.Distance <= Hero_Model.Instance.CurrentMP && v.bFC_View.character == null)
            {
                v.bFC_View.Image_MoveDistance.SetActive(true);
            }
            else v.bFC_View.Image_MoveDistance.SetActive(false);
        }
    }

    public static void HideMoveDistance()
    {
        foreach (var v in Battle_Model.Instance.BFC_List)
        {
            v.bFC_View.Image_MoveDistance.SetActive(false);
        }
    }

    public static void RefreshCardDistance()
    {
        Battle_Model.Instance.CalcuAllDistance();
        foreach (var v in Battle_Model.Instance.BFC_List)
        {
            if (v.bFC_Model.Distance <= Hero_Model.Instance.CardDistance)
            {
                v.bFC_View.Image_CardDistance.SetActive(true);
            }
        }
    }

    public static void HideCardDistance()
    {
        foreach (var v in Battle_Model.Instance.BFC_List)
        {
            v.bFC_View.Image_CardDistance.SetActive(false);
        }
    }

}

