using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleSystem;
namespace  CardSystem
{
    public class Pile : MonoBehaviour
    {
        public List<Card_Model> CardList;
        internal Transform DisplayArea;

        public bool isDeck;
        public bool isHandPile;


        public void Start()
        {
            CardList = new List<Card_Model>();
            if (isHandPile)
            {
                DisplayArea = transform;
            }
            else
            {
                GameObject UI = GameObject.Find("UI");
                DisplayArea = UI.transform.Find("PileShower/PileShowerBoard").transform;
            }
            //CardCount = 0;
        }

        public void AddCard(int cardAssetID)
        {
            
            Card_Model card = CardInfoManager.CardInfo_Manager.CreateNewCardModel(cardAssetID);

            if (card.IsConstructed)
            {
                
                CardList.Add(card);
                card.IndexInPile = CardList.Count;
                if (isDeck)
                {
                    card.Info.CardID = CardList.Count;
                    //Debug.Log(card.CardID);
                }
                else
                {
                    Pile_Ctrller.Instance.tempPile.CardList.Add(card);
                    card.Info.CardID =(Pile_Ctrller.Instance.tempPile.CardList.Count + 1000);
                    //Debug.Log(card.CardID);
                }
            }
        }

        public void RemoveCard(Card_Model card)
        {
            CardList.Remove(card);
        }

        public void ShowThisPile()
        {
             PileDisplay_Ctrller.Instance.DisplayNotHandPile(this);
        }
    }
}

