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
        /// 初始化
        /// </summary>
        public void Ini()
        {

                hero_Model.character.subjects.actionStart.AddObserve(pileDisplay_Ctrller.DisplayHandPile);


            int tempHeroCount=0;
            int tempEnemyCount=0;
            for(int i = 0; i < battle_Model.enemys.Count +1; i++)
            {
                //交替安排敌人与英雄的行动
                //#暂时#  最多只有两位英雄，只有第一次和第三次行动可能会轮到英雄
                if (i== 0 || i == 2)
                {
                    if (tempHeroCount < 1)//如果英雄已经全部置入行动序列，则将敌人置入
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
        /// 回合开始
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
        /// 行动结束
        /// </summary>
        public void EndAction()//也许需要添加一个bool变量判断Hero模型移动是否完成，完成了才能点击“结束回合”按钮
        {
            Battle_Model.Instance.Current = turn_Model.actionSeqence.First();
            Battle_Model.Instance.Current.ActionEnd();

            //将当前行动的角色重置到队尾
            turn_Model.actionSeqence.Remove(Battle_Model.Instance.Current);
            turn_Model.actionSeqence.Add(Battle_Model.Instance.Current);

            turn_Model.actionCounter++;
            Debug.Log($"第{turn_Model.actionCounter}次行动，结束");
            StartAction();
        }



       
    }

}
