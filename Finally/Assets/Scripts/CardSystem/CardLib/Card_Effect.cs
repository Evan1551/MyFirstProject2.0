
using CardSystem;
using CommandSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CardSystem
{
    public abstract class CardEffect
    {
        

        public Battle_Model battle_Model = Battle_Model.Instance;
        protected CommandLibrary CmdLib = CommandLibrary.Instance;

        //��ֵ�б�
        public List<int> valueList { get; set; }
        //���Ƶ���������
        public CostClass CostType { get; set; }
        //���Ƶ�����ֵ
        public int CostValue { get; set; }
        //���Ƶ��ͷž�������
        public DistanceClass DistanceType { get; set; }
        //���Ƶ�ʹ�þ���ֵ
        public int DistanceValue { get; set; }
        //���Ƶķ�Χֵ
        public int RangeValue { get; set; }

        public Card_Model card_Model { get; set; }

        public abstract void Effect();
        public void UseCard()
        {
            battle_Model.CommandList.AddObserve(Effect);
            battle_Model.CommandList.CheckAndApplyIssues();
        }

        public void SetCardAsset(CardAsset asset)
        {
            valueList = new List<int>();
            valueList.Clear();
            for (int i = 0; i < asset.Values.Count; i++)
            {
                valueList.Add(asset.Values[i]);
            }

            CostType = asset.CostType;
            CostValue = asset.Cost;
            DistanceType = asset.DistanceType;
            switch (DistanceType)
            {
                case DistanceClass.Human:
                    DistanceValue = asset.DistanceValue;
                    break;
                case DistanceClass.Sky:

                    break;
                case DistanceClass.Earth:
                    DistanceValue = asset.DistanceValue;
                    RangeValue = asset.RangeValue;
                    break;
                default:
                    Debug.LogError("�������ʹ���");
                    break;
            }
        }

        public bool Cost()
        {
            
            if(CostType == CostClass.AP)
            {
                if (CostValue <= Hero_Model.Instance.CurrentAP)
                {
                    for (int i = 0; i < CostValue; i++)
                    {
                        CmdLib.CostAP();
                    }
                    return true;
                }
                else
                {
                    Debug.Log("��������");
                    return false;
                }

                   
                   
            }
            else if(CostType == CostClass.MP)
            {
                if (CostValue <= Hero_Model.Instance.CurrentAP)
                {
                    for (int i = 0; i < CostValue; i++)
                    {
                        CmdLib.CostMP();
                    }
                    return true;
                }
                else
                {
                    Debug.Log("MP����");
                    return false;
                }
            }
            else
            {
                Debug.LogError("������������");
                return false;
            }
            
        }

        
    }
}

