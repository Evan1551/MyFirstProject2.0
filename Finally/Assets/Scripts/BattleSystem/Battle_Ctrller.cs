using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleSystem;
using CardSystem;
using CharacterSystem;

using CommandSystem;
using TurnSystem;
using System.Linq;

public class Battle_Ctrller : MonoBehaviour
{
    public Hero_Model hero_Model;
    public Pile_Ctrller pile_Ctrller;
    public Character_Ctrller character_Ctrller;
    public CardInfoManager cardManager;
    //public PileManager pileManager;
    //public CardDisplayManager cardDisplayManager;
    public Turn_Ctrller turnManager;
    private CommandLibrary cmdLib;
    public Subject MovingList { get; set; }


    void Start()
    {
        GameObject.Find("UI").GetComponent<UI_Ctrller>().Ini();
        hero_Model = Hero_Model.Instance;
        pile_Ctrller = Pile_Ctrller.Instance;
        cmdLib = CommandLibrary.Instance;
        character_Ctrller = GameObject.Find("Character_Ctrller").GetComponent<Character_Ctrller>();
       MovingList = new Subject();

        pile_Ctrller.deck.AddCard(1);
        pile_Ctrller.deck.AddCard(1);
        pile_Ctrller.deck.AddCard(2);
        pile_Ctrller.deck.AddCard(2);
        pile_Ctrller.deck.AddCard(1001);
        pile_Ctrller.deck.AddCard(1001);

        pile_Ctrller.InitializePiles();
        Debug.Log(character_Ctrller);
        character_Ctrller.CreateCharacter(1, new GridVector2(1, 2));
        character_Ctrller.CreateCharacter(4, new GridVector2(8, 2));
        //character_Ctrller.CreateCharacter(3, new GridVector2(8, 4));
        
        turnManager.Ini();
        turnManager.StartAction();
    }
    private void Update()
    {
  
    }

    void LateUpdate()
    {
        MovingList.CheckAndApplyIssues();
    }
}
