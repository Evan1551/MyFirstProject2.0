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
        /// #δ���#          ʵ����һ���µĿ��ơ�
        /// </summary>
        /// <param name="cardID"></param>
        /// <returns>���Ƶ�ʵ��</returns>
        public CardInfo CreateNewCardInfo(int cardAssetID)
        { 
            //��ȡ������Դ
            CardAsset asset = CardAsset.GetCardAsset(cardAssetID);
            //����������Ϣ��ʵ��
            CardInfo info = new CardInfo(asset);
            //������ɹ���
            info.IsConstructed = true;
            return info;
        }


        /// <summary>
        /// ����cardAssetID�����µĿ���ģ��
        /// </summary>
        /// <param name="cardAssetID">������ԴID</param>
        /// <returns></returns>
        public Card_Model CreateNewCardModel(int cardAssetID)
        {
            CardAsset Asset = CardAsset.GetCardAsset(cardAssetID);
            Card_Model card = new Card_Model(Asset);
            card.IsConstructed = true;

            return card;
        }


      


        /// <summary>
        /// ǿ�����ơ�����Ϊ������Ϣ��ʵ��      δ�޸ģ�������
        /// </summary>
        public CardInfo GetUpdatedCard(CardInfo info)
        {
            info = CreateNewCardInfo(info.CardAssetID + 1000);
            info.IsUpdated = true;
            return info;
        }

        /// <summary>
        /// #δ���#       ͨ�û����ơ�����Ϊ���Ƶ�ʵ��
        /// </summary>
        public void CommonizeThisCard(CardInfo info)
        {
            info.IsCommon = true;
        }

        /// <summary>
        /// ����ֵvalue����Ŀ��card�����ĵ�order������������
        /// </summary>
        /// <param name="order"></param>
        /// <param name="value"></param>
        public void SetValueToDescription(CardInfo info, int order, int value)
        {
            int count = 1;
            int valueLength = 1;//��¼��ǰnumberӵ�е�λ��
            bool valueMark = false;//��⵽number��valueMark��Ϊtrue,��⵽��ǰchar����number������Ϊfalse
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

