using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CharacterSystem;
using CardSystem;

namespace AttackDistanceSystem
{
    /// <summary>
    /// 卡牌种类 Sky（天）：无施法距离；  Earth（地）：人物施法距离 + 指定攻击区域； Human（人）：人物施法距离
    /// </summary>
    public enum DistanceType { Sky, Earth, Human }//大概能用到


    public class ShowAttackDistance : MonoBehaviour
    {
        private DistanceType _cardType;
        public DistanceType CardType { get { return _cardType; } set { _cardType = value; } }

        public string HitThingName;
        private Ray ray;
        //private GameObject gameObject;

        //public GameObject[] attackButtons;
        //private BattleFieldCheck battleFieldCheck;

        private List<GameObject> DisplayedButton;

        void Start()//test
        {
            //attackButtons = GameObject.FindGameObjectsWithTag("ADCheck");
            DisplayedButton = new List<GameObject>();

            //isShowAttackDistance(5, 3, 1);
        }


        /*void Update()
        {
            gameObject = GameObject.Find(HitThingName);
        }*/



        /// <summary>
        /// 显示攻击距离
        /// </summary>
        /// <param name="CharacterPosition_x"></param>
        /// <param name="CharacterPosition_y"></param>
        /// <param name="DistanceValue"></param>
        public void isShowAttackDistance(int CharacterPosition_x, int CharacterPosition_y, int DistanceValue)//x:角色所在坐标的x坐标； y：角色所在坐标的y坐标
        {
            noShowAttackDistance();

            for (int i = 0; i <= DistanceValue; i++)//在攻击范围为1的情况下，角色上和右的按钮变蓝色
            {
                for (int j = 0; j <= DistanceValue - i; j++)
                {
                    int test_x = CharacterPosition_x + i;//列 3 4
                    int test_y = CharacterPosition_y + j;//行 3 3
                    GameObject ButtonAroundCharacter = GameObject.Find("BattleField/Panel/bfCheck" + test_x + test_y);
                    ButtonAroundCharacter.GetComponent<Image>().color = Color.blue;//在攻击范围为1的情况下，角色周围1格的按钮变成蓝色
                    DisplayedButton.Add(ButtonAroundCharacter);

                }
            }

            for (int i = 0; i <= DistanceValue; i++)//在攻击范围为1的情况下，角色下和左的按钮变蓝色
            {
                for (int j = 0; j <= DistanceValue - i; j++)
                {
                    int test_x = CharacterPosition_x - i;
                    int test_y = CharacterPosition_y - j;
                    GameObject ButtonAroundCharacter = GameObject.Find("BattleField/Panel/bfCheck" + test_x + test_y);

                    ButtonAroundCharacter.GetComponent<Image>().color = Color.blue;//在攻击范围为1的情况下，角色周围1格的按钮变成蓝色
                    DisplayedButton.Add(ButtonAroundCharacter);
                }
            }

            /*foreach(var attackButton in attackButtons)
            {
                if (Mathf.Abs((attackButton.GetComponent<RectTransform>().anchoredPosition.x + 50)/100 - CharacterPosition_x) + Mathf.Abs(Mathf.Abs((attackButton.GetComponent<RectTransform>().anchoredPosition.y - 50)/100) - CharacterPosition_y) <= DistanceValue)
                {
                    //Debug.Log(attackButton.GetComponent<RectTransform>().localPosition.x);
                    attackButton.GetComponent<BattleFieldCheck>().AttackDistanceButton.SetActive(true);
                    attackList.Add(attackButton);
                }
            }*/
        }



        /// <summary>
        /// 取消攻击距离的显示
        /// </summary>
        /// <param name="CharacterPosition_x"></param>
        /// <param name="CharacterPosition_y"></param>
        /// <param name="DistanceValue"></param>
        public void noShowAttackDistance()
        {
            foreach(var displayedButton in DisplayedButton)
            {
                displayedButton.GetComponent<Image>().color = Color.white;
            }
            DisplayedButton.Clear();
        }




        /// <summary>
        /// 检测光标所指人物（名称 or Tag）
        /// </summary>
        public void Ray_Line()
        {
            InvokeRepeating("PointAtCharacter", 0.1f, Time.deltaTime);//

        }


        /// <summary>
        /// 从摄像机发射射线到鼠标的位置
        /// </summary>
        private void PointAtCharacter()
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 2000))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.red);

                switch (hit.transform.name)
                {
                    case "Enemy1":
                        {
                            HitThingName = "Enemy1";
                            break;
                        }
                    case "Enemy2":
                        {
                            HitThingName = "Enemy2";
                            break;
                        }
                    case "Hero1":
                        {
                            Debug.Log("选择目标错误");
                            break;
                        }
                }
            }
        }
    }

}