using BattleSystem;
using CardSystem;
using CharacterSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CommandSystem
{
    public class CommandLibrary
    {
        private Battle_Model battle_Model;

        private Pile_Ctrller pileManager;
        private BattleFieldManager bfManager;
        private Battle_Ctrller battleManager;
        internal Character ActingCharacter;

        CommandLibrary()
        {
            battle_Model = Battle_Model.Instance;
            pileManager = GameObject.Find("Pile_Ctrller").GetComponent<Pile_Ctrller>();
            bfManager = GameObject.Find("BattleFieldManager").GetComponent<BattleFieldManager>();
            battleManager = GameObject.Find("Manager").GetComponent<Battle_Ctrller>();
        }
        private static CommandLibrary commandLibrary = null;
        public static CommandLibrary Instance
        {
            get
            {
                if (commandLibrary == null)
                {
                    commandLibrary = new CommandLibrary();
                }
                return commandLibrary;
            }
        }



        /// <summary>
        /// #指令# 将所有手牌置入弃牌堆
        /// </summary>
        public void DiscardAllCards()
        {
            for (int i = pileManager.handPile.CardList.Count; i > 0; i--)
            {
                DiscardThisCard(pileManager.handPile.CardList.First());
            }
        }
        /// <summary>
        /// #指令# 将指定手牌置入弃牌堆
        /// </summary>
        /// <param name="card"></param>
        public void DiscardThisCard(Card_Model cardData)
        {
            Debug.Log(cardData.Info.CardID);
            //当手牌堆里已经没有牌的时候
            if (pileManager.handPile.CardList.Count == 0)
            {

            }
            else
            {  
                //手牌堆移除卡牌，加入手牌堆
                pileManager.SetCardPileToPile(cardData, pileManager.handPile, pileManager.discardPile);
            }
        }
        /// <summary>
        /// #指令# 从抽牌堆中抽一张卡
        /// </summary>
        public void DrawACard()
        {
            //当抽牌堆里已经没有牌的时候
            if (pileManager.drawPile.CardList.Count == 0)
            {
                Debug.Log("洗牌");
                //重置抽牌堆和弃牌堆
                pileManager.ResetDnDPile();

            }
            //拿到抽牌堆的最后一张牌
            Card_Model cardData = pileManager.drawPile.CardList.Last();
            //抽牌堆移除卡牌，加入手牌堆
          
            pileManager.SetCardPileToPile(cardData, pileManager.drawPile, pileManager.handPile);
            Debug.Log("抽牌");
        }
        /// <summary>
        /// #指令# 从抽牌堆中抽指定数量张卡
        /// </summary>
        public void DrawOneOrMoreCards(int Amount)
        {
           for(int i = 0; i < Amount; i++)
            {
                DrawACard();
            }
        }
        /// <summary>
        /// 指定角色移动
        /// </summary>
        /// <param name="character"></param>
        /// <param name="GridTargetPos"></param>

        //battleManager/turnManager 中存储当前Character，让移动单位默认为当前回合的角色

        //移动应该有三种方式：单格移动，固定格移动（象棋中的马），无视地形移动到某指定地点（怪物AI需要常用的方式）
        //前两种移动方式先根据方向和位移值，计算出最后目的地，然后调用第三种
        //if(character.info.characterType == Hero),还要在地图上显示相应的高亮地块。显示方法应该写在CardDisplayManager里，

        public void SingleCheckMove(Character character,int Direction)
        {

        }

        public void MultiChecksMove(Character character, int Direction, GridVector2 GridTranslation)
        {

        }

        public void CharacterMove(Character character, GridVector2 GridTargetPos)
        {
            character.targetBFC = GameObject.Find($"bfCheck{GridTargetPos.x}{GridTargetPos.y}").GetComponent<BattleFieldCheck>();
            battleManager.MovingList.AddObserve(character.ModelMove);
            character.Move();//执行
        }

        public void CharacterMove(Character character, BattleFieldCheck targetBFC)
        {
            character.targetBFC = targetBFC;
            battleManager.MovingList.AddObserve(character.ModelMove);
            character.Move();//执行
        }

        public void DealDamage()
        {
            battle_Model.Current.DealDamage();
            battle_Model.Targets[0].TakeDamage(battle_Model.CommandValues[0]);
            battle_Model.Targets.RemoveAt(0);
            battle_Model.CommandValues.RemoveAt(0);
        }

        public void DealDamage(int value)
        {
            Debug.Log(battle_Model.Current);
            battle_Model.Current.DealDamage();
            battle_Model.Targets[0].TakeDamage(value);
            battle_Model.Targets.RemoveAt(0);
        }

        public void ResetAP()
        {
            Hero_Model.Instance.CurrentAP = Hero_Model.Instance.MaxAP;
        }

        public void CostAP()
        {
            Hero_Model.Instance.CurrentAP--;
            
        }

        public void ResetMP()
        {
            Hero_Model.Instance.CurrentMP = Hero_Model.Instance.MaxMP;
        }

        public void GainMP()
        {
            Hero_Model.Instance.CurrentMP++;
        }

        public void CostMP()
        {
            Hero_Model.Instance.CurrentMP--;

        }
    }


}
