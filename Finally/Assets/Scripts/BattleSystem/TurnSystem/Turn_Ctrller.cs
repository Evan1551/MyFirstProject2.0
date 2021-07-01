using CommandSystem;
using CharacterSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using CardSystem;
using BattleSystem;
using System.Text;

namespace TurnSystem
{
    public class Turn_Ctrller : MonoBehaviour
    {
        private Battle_Model battle_Model;
        private Hero_Model hero_Model;
        public Turn_Model turn_Model;
        //private CardDisplayManager cardDisplayManager;
        private Character_Ctrller character_Ctrller;
        private CommandLibrary cmdLib;





  

        PileDisplay_Ctrller pileDisplay_Ctrller;
        void Start()
        {
            battle_Model = Battle_Model.Instance;
            hero_Model = Hero_Model.Instance;
            turn_Model = Turn_Model.Instance;
            //turnCounter = 0;
            turn_Model.actionCounter = 0;
            turn_Model.actionSeqence = new List<Character>();

            cmdLib = CommandLibrary.Instance;
            character_Ctrller = GameObject.Find("Character_Ctrller").GetComponent<Character_Ctrller>();
            pileDisplay_Ctrller = PileDisplay_Ctrller.Instance;
        }


        /// <summary>
        /// ��ʼ��
        /// </summary>
        public void Ini()
        {

                hero_Model.character.subjects.actionStart.AddObserve(pileDisplay_Ctrller.DisplayHandPile);


            int tempHeroCount=0;
            int tempEnemyCount=0;
            for(int i = 0; i < battle_Model.enemys.Count +1; i++)
            {
                //���氲�ŵ�����Ӣ�۵��ж�
                //#��ʱ#  ���ֻ����λӢ�ۣ�ֻ�е�һ�κ͵������ж����ܻ��ֵ�Ӣ��
                if (i== 0 || i == 2)
                {
                    if (tempHeroCount < 1)//���Ӣ���Ѿ�ȫ�������ж����У��򽫵�������
                    {
                        turn_Model.actionSeqence.Add(hero_Model.character);
                        tempHeroCount++;
                    }
                    else
                    {
                        turn_Model.actionSeqence.Add(battle_Model.enemys[tempEnemyCount].GetComponent<Character>());
                        tempEnemyCount++;
                    }
                }
                else
                {
                    turn_Model.actionSeqence.Add(battle_Model.enemys[tempEnemyCount].GetComponent<Character>());
                    tempEnemyCount++;
                }
            }
        }


        /// <summary>
        /// �غϿ�ʼ
        /// </summary>
        public void StartAction()
        {
            Battle_Model.Instance.Current = turn_Model.actionSeqence.First();
            Battle_Model.Instance.Current.ActionStart();


            if (Battle_Model.Instance.Current.info.CharacterType == CharacterClass.Enemy)
            {
                Battle_Model.Instance.Current.EnemyAI.DoAction();
                EndAction();
            }
        }

        /// <summary>
        /// �ж�����
        /// </summary>
        public void EndAction()//Ҳ����Ҫ���һ��bool�����ж�Heroģ���ƶ��Ƿ���ɣ�����˲��ܵ���������غϡ���ť
        {
            Battle_Model.Instance.Current = turn_Model.actionSeqence.First();
            Battle_Model.Instance.Current.ActionEnd();

            //����ǰ�ж��Ľ�ɫ���õ���β
            turn_Model.actionSeqence.Remove(Battle_Model.Instance.Current);
            turn_Model.actionSeqence.Add(Battle_Model.Instance.Current);

            turn_Model.actionCounter++;
            Debug.Log($"��{turn_Model.actionCounter}���ж�������");
            StartAction();
        }



       
    }

}
