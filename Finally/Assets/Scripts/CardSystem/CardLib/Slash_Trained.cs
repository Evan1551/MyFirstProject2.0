using CardSystem;
using CommandSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash_Trained : CardEffect
{
    public Slash_Trained()
    {
        SetCardAsset(CardAsset.GetCardAsset(1001));
    }

    public override void Effect()
    {
        if (Cost())
        {
            CmdLib.DealDamage(valueList[0]);
            CmdLib.DiscardThisCard(card_Model);
        }
        battle_Model.CommandList.RemoveObserve(Effect);
    }
}
