using BattleSystem;
using CardSystem;
using CharacterSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace CardSystem
{
    //与其说是CardPisplayManager，其实取名PileDisplayer更好，卡牌自身的拖拽等都卸载卡牌自身上了
    //这个类需要处理多个卡牌模型之间的排列关系，以及弃卡等行为时的销毁

    //这个相当于Pile的MVC框架的Controller部分
    //但是还有一个数据更新功能没完成（
    
    public class PileDisplay_Ctrller : MonoBehaviour
    {

        private static PileDisplay_Ctrller cardDisplay_Manager = null;
        public static PileDisplay_Ctrller Instance
        {
            get
            {
                if(cardDisplay_Manager==null)
                {
                    cardDisplay_Manager = GameObject.Find("CardDisplayManager").GetComponent<PileDisplay_Ctrller>();
                }
                return cardDisplay_Manager;
            }
        }


        static internal List<GameObject> handPileInstances;//手牌实体的集合
        static internal List<GameObject> otherPileInstances;//当前显示的非手牌Pile中的卡牌实体的集合（每次只会显示一个pile,所以通用一个List）

  
        //internal PileManager pileManager;
        internal bool isShowingPile;//指非HandPile的其他pile
        public GameObject CardBase;//卡牌Prefab，所有卡牌的基础模型框架
        public GameObject pileShower;//同下
        public Transform pileShowerBoard;//otherPile显示的地方。通用
        public int angle;//手牌的旋转角度
        public int spacing;//手牌之间的间隔

        void Start()
        {
            isShowingPile = false;
            handPileInstances = new List<GameObject>();
            otherPileInstances = new List<GameObject>();
            //pileManager = GameObject.Find("PileManager").GetComponent<PileManager>();
        }


        /// <summary>
        /// 显示HandPile中的牌
        /// </summary>
        /// <param name="DrawCount"></param>
        public void DisplayHandPile()
        {
            int HandCount = Pile_Ctrller.Instance.handPile.CardList.Count;//更新手牌数量

            bool isOdd;//是否为奇数
            Vector3 LeftPos = new Vector3(0, 0, 0);//最左边卡牌的位置
            float i = Mathf.Floor(HandCount / 2); //i 用以判断哪张卡在左，哪张在右

            //根据奇偶性与spacing（间隔）设置最左边卡牌的位置
            if (HandCount % 2 == 1)
            {
                isOdd = true;//设置为奇数
                LeftPos = new Vector3(-i * spacing, 0, 0);
            }
            else
            {
                isOdd = false;//设置为偶数
                LeftPos = new Vector3(-i * spacing + spacing / 2, 0, 0);
            }
            float temp = 0;//temp记录了此时为第几张牌

            foreach (Card_Model cardData in Pile_Ctrller.Instance.handPile.CardList)
            {

                temp++;

                ////实例化Prefab并将父物体设为Hand(手牌）
                GameObject card = CreateCardPrefab(cardData, Pile_Ctrller.Instance.handPile);
               
                //将卡牌设置到最左处，然后根据当前为第几张牌来向右Translate
                card.transform.Translate(LeftPos);
                card.transform.Translate((temp - 1) * spacing, 0, 0);


                //旋转 #未完成#
                if (!isOdd)
                {
                    if (temp < i)
                    {
                        card.transform.Rotate(0, 0, angle * (i - temp + 1));
                    }
                    if (temp >= i)
                    {
                        card.transform.Rotate(0, 0, angle * (i - temp + 2));
                    }
                }
            }
        }



        /// <summary>
        /// 显示非HandPile的Pile中的牌
        /// </summary>
        /// <param name="thisPile"></param>
        public void DisplayNotHandPile(Pile thisPile)
        {

            pileShower.SetActive(true);
           
            int temp = 0;

            if (!thisPile.isHandPile&&!isShowingPile)
            {
                isShowingPile = true;
                foreach (Card_Model cardInfo in thisPile.CardList)
                {
                    temp++;
                    GameObject card = CreateCardPrefab(cardInfo, thisPile);

                }
            }
            else if (isShowingPile)
            {
                DestoryAllCardInstances(thisPile);
                isShowingPile = false;
                pileShower.SetActive(false);
            }
        }
        
        
        
        /// <summary>
        /// 新建卡牌Prefab
        /// </summary>
        /// <param name="cardData">卡牌的信息</param>
        /// <param name="pile">卡牌所在的Pile</param>
        /// <returns></returns>
        public GameObject CreateCardPrefab(Card_Model cardData, Pile pile)
        {
            GameObject card = Instantiate(CardBase);
            card.GetComponent<Card_Ctrller>().card_Model = cardData;

            card.transform.SetParent(pile.DisplayArea, false);//
            SetCardInfo(card.GetComponent<Card_View>(), cardData);//将实际数据置入Card实例的信息成员中。然后传递到卡面上
            //SetCardEffect(card.GetComponent<CardView>());//将卡牌行为置入
            if (pile.isHandPile)
            {
                card.GetComponent<Card_View>().isInHandPile = true;
                handPileInstances.Add(card);
                card.name = "HandCard" + cardData.Info.CardID;
            }
            else
            {
                card.GetComponent<Card_View>().isInHandPile = false;
                otherPileInstances.Add(card);
                card.name = "OtherCard" + cardData.Info.CardID;
            }
            //将Pile管理器置入Card的成员中

            return card;
        }
        
        
        
        /// <summary>
        /// 数据设置函数
        /// </summary>
        /// <param name="card"></param>
        /// <param name="cardInfo"></param>
        public void SetCardInfo(Card_View card_View, Card_Model card_Model)
        {
            //cardView.Info = cardInfo;
            /*cardView.Name.text = cardModelInfo.Name;
            cardView.Type.text = cardModelInfo.ToString();
            cardView.Cost.text = cardModelInfo.Cost.ToString();
            cardView.Description.text = cardModelInfo.Description;*/


            card_View.UpdateInfo(card_Model);
        }
        
 
        
        /// <summary>
        /// 销毁指定牌堆中的所有卡牌Prefab
        /// </summary>
        /// <param name="pile"></param>
        public void DestoryAllCardInstances(Pile pile)
        {
            if (pile.isHandPile)
            {
                for (int i = 0; i < handPileInstances.Count; i++)
                {
                    Destroy(handPileInstances[i], 1);
                }
            }
            else
            {
                int i;
                for (i = 0; i < otherPileInstances.Count; i++)
                {
                    Destroy(otherPileInstances[i], 0.02f);
                }
            }      
        }



        /// <summary>
        /// 获取卡牌实体在手牌中的索引值
        /// </summary>
        /// <param name="CardInstance"></param>
        /// <returns></returns>
        public int GetHandCardIndex(GameObject CardInstance)
        {
            int i = handPileInstances.IndexOf(CardInstance);
            return i;
        }

    }
}

