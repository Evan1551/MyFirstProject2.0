using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace CardSystem
{
    public class CardInfo
    {

        //卡牌实例编号
        public  int CardID { get; set; }

        #region Properties From Asset
        
        //卡牌资源编号
        public int CardAssetID { get; set; }
        //卡牌消耗
        public int Cost { get; set; }
        //消耗的费用类型
        public CostClass CostType { get; set; }
        //卡牌名称
        public string Name { get; set; }
        //卡牌描述
        public string Description { get; set; }
        //卡牌种类
        public CardClass CardType { get; set; }
        //卡面立绘图片
        public Sprite Image { get; set; }

        //牌库中的顺序
        public int OrderOfDeckIndex { get; set; }
        #endregion

        //是否完成构造
        public bool IsConstructed { get; set; }
        //是否通用
        public bool IsCommon { get; set; }
        //是否强化
        public bool IsUpdated { get; set; }



        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cardID"></param>
        public CardInfo(CardAsset asset)
        {
            IsUpdated = false;
            IsCommon = false;
            IsConstructed = false;
            SetCardAsset(asset);
        }
        /// <summary>
        /// 置入卡牌资源
        /// </summary>
        /// <param name="asset"></param>
        public void SetCardAsset(CardAsset asset)
        {
            CardAssetID = asset.CardID;
            Cost = asset.Cost;
            CostType = asset.CostType;
            Name = asset.Name;
            Description = asset.Description;
            CardType = asset.CardType;
            Image = asset.Image;

            switch (CardType)
            {
                case CardClass.Attack:
                    break;
                case CardClass.Tactics:
                    
                    break;
                case CardClass.Climax:

                    break;
                case CardClass.Mad:

                    break;
                default:
                    Debug.LogError("卡牌类型错误");
                    break;
            }

        }


    }
}