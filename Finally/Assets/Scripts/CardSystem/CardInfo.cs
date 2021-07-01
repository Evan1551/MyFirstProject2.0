using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace CardSystem
{
    public class CardInfo
    {

        //����ʵ�����
        public  int CardID { get; set; }

        #region Properties From Asset
        
        //������Դ���
        public int CardAssetID { get; set; }
        //��������
        public int Cost { get; set; }
        //���ĵķ�������
        public CostClass CostType { get; set; }
        //��������
        public string Name { get; set; }
        //��������
        public string Description { get; set; }
        //��������
        public CardClass CardType { get; set; }
        //��������ͼƬ
        public Sprite Image { get; set; }

        //�ƿ��е�˳��
        public int OrderOfDeckIndex { get; set; }
        #endregion

        //�Ƿ���ɹ���
        public bool IsConstructed { get; set; }
        //�Ƿ�ͨ��
        public bool IsCommon { get; set; }
        //�Ƿ�ǿ��
        public bool IsUpdated { get; set; }



        /// <summary>
        /// ���캯��
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
        /// ���뿨����Դ
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
                    Debug.LogError("�������ʹ���");
                    break;
            }

        }


    }
}