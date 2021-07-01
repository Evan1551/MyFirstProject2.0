using CardSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectLibrary
{
   public static Dictionary<int, CardEffect>  CardDic = new Dictionary<int, CardEffect>();

    /// <summary>
    /// ��ʼ��EffectLib
    /// </summary>
    public static void CardDicIni()
    {
        CardDic.Add(1, new Slash());
        CardDic.Add(2, new Move());
        CardDic.Add(1001, new Slash_Trained());
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ID">���Ƶ�ID</param>
    public static CardEffect GetEffect(int ID)
    {
        if (CardDic.Count == 0)
        {
            CardDicIni();
        }
        CardEffect effect;
        CardDic.TryGetValue(ID, out effect);
        return effect;
    }
}
