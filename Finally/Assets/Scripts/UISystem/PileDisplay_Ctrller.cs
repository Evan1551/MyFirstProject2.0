using BattleSystem;
using CardSystem;
using CharacterSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace CardSystem
{
    //����˵��CardPisplayManager����ʵȡ��PileDisplayer���ã������������ק�ȶ�ж�ؿ�����������
    //�������Ҫ����������ģ��֮������й�ϵ���Լ���������Ϊʱ������

    //����൱��Pile��MVC��ܵ�Controller����
    //���ǻ���һ�����ݸ��¹���û��ɣ�
    
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


        static internal List<GameObject> handPileInstances;//����ʵ��ļ���
        static internal List<GameObject> otherPileInstances;//��ǰ��ʾ�ķ�����Pile�еĿ���ʵ��ļ��ϣ�ÿ��ֻ����ʾһ��pile,����ͨ��һ��List��

  
        //internal PileManager pileManager;
        internal bool isShowingPile;//ָ��HandPile������pile
        public GameObject CardBase;//����Prefab�����п��ƵĻ���ģ�Ϳ��
        public GameObject pileShower;//ͬ��
        public Transform pileShowerBoard;//otherPile��ʾ�ĵط���ͨ��
        public int angle;//���Ƶ���ת�Ƕ�
        public int spacing;//����֮��ļ��

        void Start()
        {
            isShowingPile = false;
            handPileInstances = new List<GameObject>();
            otherPileInstances = new List<GameObject>();
            //pileManager = GameObject.Find("PileManager").GetComponent<PileManager>();
        }


        /// <summary>
        /// ��ʾHandPile�е���
        /// </summary>
        /// <param name="DrawCount"></param>
        public void DisplayHandPile()
        {
            int HandCount = Pile_Ctrller.Instance.handPile.CardList.Count;//������������

            bool isOdd;//�Ƿ�Ϊ����
            Vector3 LeftPos = new Vector3(0, 0, 0);//����߿��Ƶ�λ��
            float i = Mathf.Floor(HandCount / 2); //i �����ж����ſ�������������

            //������ż����spacing���������������߿��Ƶ�λ��
            if (HandCount % 2 == 1)
            {
                isOdd = true;//����Ϊ����
                LeftPos = new Vector3(-i * spacing, 0, 0);
            }
            else
            {
                isOdd = false;//����Ϊż��
                LeftPos = new Vector3(-i * spacing + spacing / 2, 0, 0);
            }
            float temp = 0;//temp��¼�˴�ʱΪ�ڼ�����

            foreach (Card_Model cardData in Pile_Ctrller.Instance.handPile.CardList)
            {

                temp++;

                ////ʵ����Prefab������������ΪHand(���ƣ�
                GameObject card = CreateCardPrefab(cardData, Pile_Ctrller.Instance.handPile);
               
                //���������õ����󴦣�Ȼ����ݵ�ǰΪ�ڼ�����������Translate
                card.transform.Translate(LeftPos);
                card.transform.Translate((temp - 1) * spacing, 0, 0);


                //��ת #δ���#
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
        /// ��ʾ��HandPile��Pile�е���
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
        /// �½�����Prefab
        /// </summary>
        /// <param name="cardData">���Ƶ���Ϣ</param>
        /// <param name="pile">�������ڵ�Pile</param>
        /// <returns></returns>
        public GameObject CreateCardPrefab(Card_Model cardData, Pile pile)
        {
            GameObject card = Instantiate(CardBase);
            card.GetComponent<Card_Ctrller>().card_Model = cardData;

            card.transform.SetParent(pile.DisplayArea, false);//
            SetCardInfo(card.GetComponent<Card_View>(), cardData);//��ʵ����������Cardʵ������Ϣ��Ա�С�Ȼ�󴫵ݵ�������
            //SetCardEffect(card.GetComponent<CardView>());//��������Ϊ����
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
            //��Pile����������Card�ĳ�Ա��

            return card;
        }
        
        
        
        /// <summary>
        /// �������ú���
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
        /// ����ָ���ƶ��е����п���Prefab
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
        /// ��ȡ����ʵ���������е�����ֵ
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

