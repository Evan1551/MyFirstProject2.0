using CardSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : CardEffect
{
    public Move()
    {
        SetCardAsset(CardAsset.GetCardAsset(2));
    }

    public override void Effect()
    {
        if (Cost())
        {
            for(int i = 0; i < 2; i++)
            {
                CmdLib.GainMP();
            }

            CmdLib.DiscardThisCard(card_Model);
        }
        battle_Model.CommandList.RemoveObserve(Effect);
    }
}
