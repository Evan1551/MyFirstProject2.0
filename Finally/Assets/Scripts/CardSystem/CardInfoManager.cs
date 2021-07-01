using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace CardSystem
{
    public class CardInfoManager : MonoBehaviour
    {

        private static CardInfoManager cardInfo_Manager = null;
        public static CardInfoManager CardInfo_Manager
        {
            get
            {
                if(cardInfo_Manager==null)
                {
                    cardInfo_Manager = GameObject.Find("CardInfoManager").GetComponent<CardInfoManager>();
                }
                return cardInfo_Manager;
            }
        }



        /// <summary>
        /// #未完成#          实例化一张新的卡牌。
        /// </summary>
        /// <param name="cardID"></param>
        /// <returns>卡牌的实例</returns>
        public CardInfo CreateNewCardInfo(int cardAssetID)
        { 
            //获取卡牌资源
            CardAsset asset = CardAsset.GetCardAsset(cardAssetID);
            //创建卡牌信息的实例
            CardInfo info = new CardInfo(asset);
            //卡牌完成构建
            info.IsConstructed = true;
            return info;
        }


        /// <summary>
        /// 根据cardAssetID创建新的卡牌模型
        /// </summary>
        /// <param name="cardAssetID">卡牌资源ID</param>
        /// <returns></returns>
        public Card_Model CreateNewCardModel(int cardAssetID)
        {
            CardAsset Asset = CardAsset.GetCardAsset(cardAssetID);
            Card_Model card = new Card_Model(Asset);
            card.IsConstructed = true;

            return card;
        }


      


        /// <summary>
        /// 强化卡牌。参数为卡牌信息的实例      未修改！！！！
        /// </summary>
        public CardInfo GetUpdatedCard(CardInfo info)
        {
            info = CreateNewCardInfo(info.CardAssetID + 1000);
            info.IsUpdated = true;
            return info;
        }

        /// <summary>
        /// #未完成#       通用化卡牌。参数为卡牌的实例
        /// </summary>
        public void CommonizeThisCard(CardInfo info)
        {
            info.IsCommon = true;
        }

        /// <summary>
        /// 将数值value传入目标card描述的第order个整型数据中
        /// </summary>
        /// <param name="order"></param>
        /// <param name="value"></param>
        public void SetValueToDescription(CardInfo info, int order, int value)
        {
            int count = 1;
            int valueLength = 1;//记录当前number拥有的位数
            bool valueMark = false;//检测到number后将valueMark置为true,检测到当前char不是number后再置为false
            string description = info.Description;
            StringBuilder DsrptionSB = new StringBuilder();
            DsrptionSB.Append(description);
            for (int i = 0; i <= description.Length - 1; i++)
            {
                if (char.IsNumber(description, i) && !valueMark)
                {
                    valueMark = true;
                }
                else if (char.IsNumber(description, i) && valueMark)
                {
                    valueLength++;
                }
                else if (!char.IsNumber(description, i) && valueMark)
                {
                    Debug.Log(valueLength);
                    if (count == order)
                    {
                        DsrptionSB.Remove(i - valueLength, valueLength);
                        DsrptionSB.Insert(i - valueLength, value);
                        info.Description = DsrptionSB.ToString();
                        break;
                    }
                    count++;
                    valueLength = 1;
                    valueMark = false;
                }
            }
        }
    }
}

