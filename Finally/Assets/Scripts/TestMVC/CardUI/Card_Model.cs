using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardSystem;

namespace CardSystem
{

    public enum InWhichPile { Draw, Hand, Discard, Desk }

    public class Card_Model
    {
        public CardInfo Info;
        public CardEffect Effect;

        //卡牌 锁
        private bool cardModelKey;
        public bool CardModelKey { get; set; }

        //卡牌在牌堆中的序号
        private int indexInPile;
        public int IndexInPile { get; set; }

        //卡牌所在牌组的类型
        private InWhichPile pileType;
        public InWhichPile PileType { get; set; }

        //是否完成构造
        private bool isConstructed;
        public bool IsConstructed { get; set; }


        //数据相关操作

        //初始化
        public Card_Model(CardAsset asset)
        {
            //SetCardAsset(asset);
            Info = new CardInfo(asset);
            Effect = EffectLibrary.GetEffect(asset.CardID);
            Effect.card_Model = this;
            //Debug.Log(Effect);
            CardModelKey = false;
            IndexInPile = 0;

            this.IsConstructed = Info.IsConstructed;
            //this.IsConstructed = true;
        }

    }
}
