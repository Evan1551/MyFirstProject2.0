using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BattleSystem;

namespace CardSystem
{
    public class Pile_Ctrller : MonoBehaviour
    {

        private static Pile_Ctrller pile_Ctrller = null;
        public static Pile_Ctrller Instance
        {
            get
            {
                if(pile_Ctrller==null)
                {
                    pile_Ctrller = GameObject.Find("Pile_Ctrller").GetComponent<Pile_Ctrller>();
                }
                return pile_Ctrller;
            }
        }


        //�������������
        private System.Random random;
        protected int seed = 0;

        //private CardDisplayManager cardDisplayManager;

        //�����ͬ��Pile�ֱ��¼ ��ǰս�������е���(������ʱ������),���ƣ����ƶ�,���ƶ�,�ƿ⡣
        internal Pile tempPile;
        internal Pile handPile;
        internal Pile drawPile;
        internal Pile discardPile;
        internal Pile deck;

        public void Start()
        {
            random = new System.Random();
            tempPile = GameObject.Find("TempPile").GetComponent<Pile>();
            handPile = GameObject.Find("HandPile").GetComponent<Pile>();
            drawPile = GameObject.Find("DrawPile").GetComponent<Pile>();
            discardPile = GameObject.Find("DiscardPile").GetComponent<Pile>();
            deck = GameObject.Find("Deck").GetComponent<Pile>();

            handPile.isHandPile = true;
            deck.isDeck = true;

            //cardDisplayManager = GameObject.Find("CardDisplayManager").GetComponent<CardDisplayManager>();

        }

        /// <summary>
        /// ս����ʼʱ���á���ʼ�������ƶ�
        /// </summary>
        public void InitializePiles()
        {
            //��deck�еĿ��Ƹ���TempPile
            deck.CardList.ForEach(a => tempPile.CardList.Add(a));
            //��deck�м�¼�Ŀ�����������TempPile
            //tempPile.CardCount = deck.CardCount;
            tempPile.CardList.ForEach(a => drawPile.CardList.Add(a));
            RandomizeDrawPile();
            Debug.Log("�����ʼ�����");
        }

        /// <summary>
        /// DnDָDrawAndDiscard���鿨��������
        /// </summary>
        public void ResetDnDPile()
        {
            //��discardPile��������drawPile��
            discardPile.CardList.ForEach(a => drawPile.CardList.Add(a));
            discardPile.CardList.Clear();

            RandomizeDrawPile();
        }

        /// <summary>
        /// �����ƴ�A_Pileת�Ƶ�B_Pile
        /// </summary>
        /// <param name="card"></param>
        /// <param name="Source"></param>
        /// <param name="Target"></param>
        public void SetCardPileToPile(Card_Model card, Pile Source, Pile Target)
        {
            Source.CardList.Remove(card);
            Target.CardList.Add(card);
            if (Source.isHandPile)
            {
                //����������ʾ����
                Debug.Log(PileDisplay_Ctrller.handPileInstances);
                GameObject cardInstance = PileDisplay_Ctrller.handPileInstances.Find(e => e.GetComponent<Card_Ctrller>().card_Model.Info.CardID == card.Info.CardID);
                PileDisplay_Ctrller.handPileInstances.Remove(cardInstance);
                Destroy(cardInstance);
            }
        }

        /// <summary>
        /// ʹ���ƶ������
        /// </summary>
        private void RandomizeDrawPile()
        {
            //���ƶ����������ϴ�ƣ�����ÿ��Ԫ�أ������������Ԫ�ؽ���λ��
            for (int j = 0; j < drawPile.CardList.Count - 1; j++)
            {
                int rd = random.Next(0, drawPile.CardList.Count - 1);
                var t = drawPile.CardList[rd];
                drawPile.CardList[rd] = drawPile.CardList[j];
                drawPile.CardList[j] = t;
            }
            //����������ã�����ÿ��ϴ�ƽ����ͬ
            random = new System.Random(++seed);
        }
    }
}

