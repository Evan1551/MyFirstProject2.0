using CardSystem;
using CharacterSystem;
using CommandSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : CardEffect
{
    public Slash()
    {
        SetCardAsset(CardAsset.GetCardAsset(1));
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
