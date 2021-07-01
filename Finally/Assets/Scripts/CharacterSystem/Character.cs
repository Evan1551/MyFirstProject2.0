using BattleSystem;
using CommandSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardSystem;
using UnityEngine.UI;
using TurnSystem;

namespace CharacterSystem
{
    public class Character : MonoBehaviour
    {
        public Character_View character_View;
        public BattleFieldCheck BFC;
        public BattleFieldCheck targetBFC;
        internal CharacterInfo info;
        internal CharacterSubjects subjects;//里面有若干监听器，每一个具体行为都有相应的监听器备用
        public int footstep = 2;

        public PileDisplay_Ctrller uiManager;

        public EnemyAction EnemyAI;


        //开局先把info.Hp的值赋予fillAmount
        private void Start()
        {
            character_View = GetComponent<Character_View>();
            character_View.HPEffectImage.fillAmount = (float)info.Hp / (float)info.MaxHP;
            character_View.CurrentHPImage.fillAmount = (float)info.Hp / (float)info.MaxHP;
            if(info.CharacterType == CharacterClass.Hero)
            {
                subjects.actionStart.AddObserve(CommandLibrary.Instance.ResetAP);
                subjects.die.AddObserve(UI_Ctrller.GameOver);
                //subjects.actionStart.AddObserve(CommandLibrary.Instance.GainMP);
                //subjects.actionStart.AddObserve(CommandLibrary.Instance.GainMP);
            }
        }


        public void ActionStart()
        {
            subjects.actionStart.CheckAndApplyIssues();
        }

        public void ActionEnd()
        {
            subjects.actionEnd.CheckAndApplyIssues();
        }

        public void Die()
        {
            if (info.CharacterType == CharacterClass.Enemy)
            {
                Battle_Model.Instance.enemys.Remove(this);
                Turn_Model.Instance.actionSeqence.Remove(this);
                if (Battle_Model.Instance.Current==this)
                {
                    GameObject.Find("Turn_Ctrller").GetComponent<Turn_Ctrller>().EndAction();
                }


                Destroy(gameObject);
            }
            else
            {
                subjects.die.CheckAndApplyIssues();
            }

            if (Battle_Model.Instance.enemys.Count == 0)
            {
                UI_Ctrller.Congradulation();
            }
        }

        public int TakeDamage(int value)
        {
            info.Hp -= value;
            Debug.Log(info.Hp);
            StartCoroutine("UpdateHPInfo");
            if (info.Hp <= 0)
            {
                Die();
            }
            subjects.takeDamage.CheckAndApplyIssues();

            return info.Hp;
        }

        public void Move()
        {
            SetPos(this, targetBFC);
            subjects.move.CheckAndApplyIssues();
        }

        public void DealDamage()
        {
            subjects.dealDamage.CheckAndApplyIssues();
        }

        //由于模型的移动是一个持续的过程，所以将其单独写成一个函数，加入到BattleManager下的MovingList里
        public void ModelMove()
        {
            //当前地点
            Vector3 currentPosition = transform.position;
            //目标地点
            Vector3 targetPosition = targetBFC.transform.position;
            

            if (Vector3.Distance(currentPosition, targetPosition) > footstep)
            {
                Vector3 directionOfTravel = targetPosition - currentPosition;
                directionOfTravel.Normalize();
                transform.Translate(
                    (directionOfTravel.x * footstep),
                    (directionOfTravel.y * footstep),
                    (directionOfTravel.z * footstep),
                    Space.World);
            }
            else
            {
                GameObject.Find("Manager").GetComponent<Battle_Ctrller>().MovingList.RemoveObserve(ModelMove);//移动完成后从MovingList中移除
                Debug.Log("移动完成");
            }
        }

        public void MonseOnCharacter()
        {
          
        }

        /// <summary>
        /// 更新血量UI信息
        /// </summary>
        /// <returns></returns>
        IEnumerator UpdateHPInfo()
        {
            character_View.CurrentHPImage.fillAmount = (float)info.Hp / (float)info.MaxHP;
            while(character_View.HPEffectImage.fillAmount > character_View.CurrentHPImage.fillAmount)
            {
                character_View.HPEffectImage.fillAmount -= character_View.hurtSpeed;
                yield return new WaitForSeconds(0.005f);
            }

            if (character_View.HPEffectImage.fillAmount < character_View.CurrentHPImage.fillAmount)
                character_View.HPEffectImage.fillAmount = character_View.CurrentHPImage.fillAmount;
        }


        public static void SetPos(Character character, BattleFieldCheck BFC)
        {
            if (character.BFC != null)
            {
                character.BFC.bFC_View.character = null;
            }
            character.BFC = BFC;
            BFC.bFC_View.character = character;
        }

        public static int CalcuDistance(Character source, Character target)
        {
            int i = Mathf.Abs(source.BFC.x - target.BFC.x) + Mathf.Abs(source.BFC.y - target.BFC.y);
            return i;
        }
    }

}
