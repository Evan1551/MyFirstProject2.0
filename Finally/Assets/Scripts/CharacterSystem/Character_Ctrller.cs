using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;//List<>.Last()����������ռ���
using UnityEngine;
using BattleSystem;
using CommandSystem;

namespace CharacterSystem
{
    public class Character_Ctrller : MonoBehaviour
    {
        private Hero_Model hero_Model;
        private Battle_Model battle_Model;
        private CommandLibrary cmdLib;

        private static Character_Ctrller character_Ctrller = null;
        public static Character_Ctrller Instance
        {
            get
            {
                if (character_Ctrller == null)
                {
                    character_Ctrller = GameObject.Find("Character_Ctrller").GetComponent<Character_Ctrller>();
                }
                return character_Ctrller;
            }
        }

        
      
        private BattleFieldManager bfManager;
        //internal CharacterInfo Hero;//Ӣ����Ϣ   #δ����Ҫʹ�ô浵��Ϣ#

        void Start()
        {
            hero_Model = Hero_Model.Instance;
            battle_Model = Battle_Model.Instance;
            cmdLib = CommandLibrary.Instance;
            bfManager = GameObject.Find("BattleFieldManager").GetComponent<BattleFieldManager>();
        }

        ///// <summary>
        ///// ��Ϸ�ʼʱ���ν���Ӣ����Ϣ
        ///// </summary>
        ///// <param name="ID"></param>
        //public void InitializeHeroInfo(int ID)
        //{
        //    if (GetCharacterAsset(ID).CharacterType == CharacterClass.Hero)
        //    {
        //        Hero = new CharacterInfo(GetCharacterAsset(ID));
        //    }
        //    else
        //    {
        //        Debug.LogError("��ʼ��Ӣ����Ϣʱʹ���˷�Ӣ����Դ��ID");
        //    }
        //}

        /// <summary>
        /// ��ȡ������Դ
        /// </summary>
        /// <param name="ID"></param>
        /// 
        public CharacterAsset GetCharacterAsset(int CharacterAssetID)
        {

            CharacterAsset asset = Resources.Load($"Character/Character_{CharacterAssetID}") as CharacterAsset;//������Դ����ID���

            //��ʱʹ��Resources.Load(String Path)����
            //������Ҫ����AssetBundle��ط���

            return asset;
        }

        /// <summary>
        ///  ���������������λ���½�����ʵ��
        /// </summary>
        /// <param name="ID">��ɫ��ԴID</param>
        /// <param name="x">��Grid�����X</param>
        /// <param name="y">��Grid�����Y</param>
        internal void CreateCharacter(int ID, int x, int y)
        {
            Vector3 worldPos = bfManager.GridPosConvert(x, y);//ʹ��bfManager�еĺ����������������µ�λ��ת��Ϊ���������µ�λ��
            Character thisCharacter = NewInstance(ID, worldPos);
            thisCharacter.info.Position = new GridVector2(x, y);
        }

        /// <summary>
        /// ���������������λ���½�����ʵ��
        /// </summary>
        /// <param name="character"></param>
        /// <param name="asset"></param>
        internal Character CreateCharacter(int ID, GridVector2 gridPos)
        {
            Vector3 worldPos = bfManager.GridPosToWorld(gridPos);//ʹ��bfManager�еĺ����������������µ�λ��ת��Ϊ���������µ�λ��
            Character thisCharacter= NewInstance(ID, worldPos);
            //��bf��character������д
            Character.SetPos(thisCharacter, GameObject.Find($"bfCheck{gridPos.x}{gridPos.y}").GetComponent<BattleFieldCheck>());
            thisCharacter.info.Position = gridPos;
            return thisCharacter;
        }

        /// <summary>
        /// #δ�깤#   ���������������µ�λ���½�����ʵ��
        /// �����˵ķ���Ӧ�ú�Ӣ�۷ֿ���
        /// Ӣ��ֻ��Ҫ��Ϸ��ʼʱ����������ս����ʼʱ��ս�����������٣�������������������ɫ��Ϣ��
        /// ����Ӧ�ø��ݲ�ͬ�ĵ��������ȡ��Ӧ�ĵ��˽ű������������ҵ�������Ϊ��������ô���أ�����
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="worldPos"></param>
        internal Character NewInstance(int ID, Vector3 worldPos)
        {
            CharacterAsset asset = GetCharacterAsset(ID);
            Character thisCharacter = Instantiate(asset.Model, worldPos, Quaternion.Euler(0, 0, 0)).GetComponent<Character>();

            if (asset.CharacterType == CharacterClass.Enemy)
            {
                //��ʵ��������ʵ���б�
                battle_Model.enemys.Add(thisCharacter);
                thisCharacter.name = "Enemy" + battle_Model.enemys.Count;//ʵ��������
                //������ӵ��info���ܶ������ݸ�ֵ
                thisCharacter.info = new CharacterInfo(asset);//���˽�����asset�ڵ������½�
                thisCharacter.info.CharacterID = battle_Model.enemys.Count + 1000;//ʵ������д
            }
            else if (asset.CharacterType == CharacterClass.Hero)
            {

                hero_Model.character = thisCharacter;
                thisCharacter.name = "Hero";//ʵ��������
                thisCharacter.info = new CharacterInfo(asset);//Ӣ�۽����ñ�������������
                thisCharacter.info.CharacterID = 1;//ʵ������д
            }

            InitializeSubjects(thisCharacter);
            return thisCharacter;
        }

        /// <summary>
        /// ��Character��Subjects��ʼ��
        /// </summary>
        /// <param name="thisCharacter"></param>
        public void InitializeSubjects(Character thisCharacter)
        {

            thisCharacter.subjects = new CharacterSubjects();
            if (thisCharacter.info.CharacterType == CharacterClass.Hero)
            {
                for (int i = 0; i < thisCharacter.info.DrawAmount; i++)
                {
                    thisCharacter.subjects.actionStart.AddObserve(cmdLib.DrawACard);
                }
                thisCharacter.subjects.actionEnd.AddObserve(cmdLib.DiscardAllCards);
            }
        }
    }


}


