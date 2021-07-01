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


        //随机数和其种子
        private System.Random random;
        protected int seed = 0;

        //private CardDisplayManager cardDisplayManager;

        //五个不同的Pile分别记录 当前战斗中所有的牌(包括临时产生的),手牌，抽牌堆,弃牌堆,牌库。
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
        /// 战斗开始时调用。初始化所有牌堆
        /// </summary>
        public void InitializePiles()
        {
            //将deck中的卡牌赋予TempPile
            deck.CardList.ForEach(a => tempPile.CardList.Add(a));
            //将deck中记录的卡牌数量传入TempPile
            //tempPile.CardCount = deck.CardCount;
            tempPile.CardList.ForEach(a => drawPile.CardList.Add(a));
            RandomizeDrawPile();
            Debug.Log("牌组初始化完成");
        }

        /// <summary>
        /// DnD指DrawAndDiscard，抽卡和弃卡堆
        /// </summary>
        public void ResetDnDPile()
        {
            //将discardPile重新置入drawPile中
            discardPile.CardList.ForEach(a => drawPile.CardList.Add(a));
            discardPile.CardList.Clear();

            RandomizeDrawPile();
        }

        /// <summary>
        /// 将卡牌从A_Pile转移到B_Pile
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
                //遍历卡牌显示器的
                Debug.Log(PileDisplay_Ctrller.handPileInstances);
                GameObject cardInstance = PileDisplay_Ctrller.handPileInstances.Find(e => e.GetComponent<Card_Ctrller>().card_Model.Info.CardID == card.Info.CardID);
                PileDisplay_Ctrller.handPileInstances.Remove(cardInstance);
                Destroy(cardInstance);
            }
        }

        /// <summary>
        /// 使抽牌堆随机化
        /// </summary>
        private void RandomizeDrawPile()
        {
            //抽牌堆用随机种子洗牌：遍历每个元素，将它和随机的元素交换位置
            for (int j = 0; j < drawPile.CardList.Count - 1; j++)
            {
                int rd = random.Next(0, drawPile.CardList.Count - 1);
                var t = drawPile.CardList[rd];
                drawPile.CardList[rd] = drawPile.CardList[j];
                drawPile.CardList[j] = t;
            }
            //随机种子重置，以免每次洗牌结果相同
            random = new System.Random(++seed);
        }
    }
}

