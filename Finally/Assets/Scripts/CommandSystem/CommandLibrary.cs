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
        /// #ָ��# �����������������ƶ�
        /// </summary>
        public void DiscardAllCards()
        {
            for (int i = pileManager.handPile.CardList.Count; i > 0; i--)
            {
                DiscardThisCard(pileManager.handPile.CardList.First());
            }
        }
        /// <summary>
        /// #ָ��# ��ָ�������������ƶ�
        /// </summary>
        /// <param name="card"></param>
        public void DiscardThisCard(Card_Model cardData)
        {
            Debug.Log(cardData.Info.CardID);
            //�����ƶ����Ѿ�û���Ƶ�ʱ��
            if (pileManager.handPile.CardList.Count == 0)
            {

            }
            else
            {  
                //���ƶ��Ƴ����ƣ��������ƶ�
                pileManager.SetCardPileToPile(cardData, pileManager.handPile, pileManager.discardPile);
            }
        }
        /// <summary>
        /// #ָ��# �ӳ��ƶ��г�һ�ſ�
        /// </summary>
        public void DrawACard()
        {
            //�����ƶ����Ѿ�û���Ƶ�ʱ��
            if (pileManager.drawPile.CardList.Count == 0)
            {
                Debug.Log("ϴ��");
                //���ó��ƶѺ����ƶ�
                pileManager.ResetDnDPile();

            }
            //�õ����ƶѵ����һ����
            Card_Model cardData = pileManager.drawPile.CardList.Last();
            //���ƶ��Ƴ����ƣ��������ƶ�
          
            pileManager.SetCardPileToPile(cardData, pileManager.drawPile, pileManager.handPile);
            Debug.Log("����");
        }
        /// <summary>
        /// #ָ��# �ӳ��ƶ��г�ָ�������ſ�
        /// </summary>
        public void DrawOneOrMoreCards(int Amount)
        {
           for(int i = 0; i < Amount; i++)
            {
                DrawACard();
            }
        }
        /// <summary>
        /// ָ����ɫ�ƶ�
        /// </summary>
        /// <param name="character"></param>
        /// <param name="GridTargetPos"></param>

        //battleManager/turnManager �д洢��ǰCharacter�����ƶ���λĬ��Ϊ��ǰ�غϵĽ�ɫ

        //�ƶ�Ӧ�������ַ�ʽ�������ƶ����̶����ƶ��������е��������ӵ����ƶ���ĳָ���ص㣨����AI��Ҫ���õķ�ʽ��
        //ǰ�����ƶ���ʽ�ȸ��ݷ����λ��ֵ����������Ŀ�ĵأ�Ȼ����õ�����
        //if(character.info.characterType == Hero),��Ҫ�ڵ�ͼ����ʾ��Ӧ�ĸ����ؿ顣��ʾ����Ӧ��д��CardDisplayManager�

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
            character.Move();//ִ��
        }

        public void CharacterMove(Character character, BattleFieldCheck targetBFC)
        {
            character.targetBFC = targetBFC;
            battleManager.MovingList.AddObserve(character.ModelMove);
            character.Move();//ִ��
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
