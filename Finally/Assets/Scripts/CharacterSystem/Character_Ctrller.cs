using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;//List<>.Last()在这个命名空间中
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
        //internal CharacterInfo Hero;//英雄信息   #未来需要使用存档信息#

        void Start()
        {
            hero_Model = Hero_Model.Instance;
            battle_Model = Battle_Model.Instance;
            cmdLib = CommandLibrary.Instance;
            bfManager = GameObject.Find("BattleFieldManager").GetComponent<BattleFieldManager>();
        }

        ///// <summary>
        ///// 游戏最开始时初次建立英雄信息
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
        //        Debug.LogError("初始化英雄信息时使用了非英雄资源的ID");
        //    }
        //}

        /// <summary>
        /// 获取人物资源
        /// </summary>
        /// <param name="ID"></param>
        /// 
        public CharacterAsset GetCharacterAsset(int CharacterAssetID)
        {

            CharacterAsset asset = Resources.Load($"Character/Character_{CharacterAssetID}") as CharacterAsset;//人物资源根据ID编号

            //暂时使用Resources.Load(String Path)方法
            //将来需要换成AssetBundle相关方法

            return asset;
        }

        /// <summary>
        ///  根据在网格坐标的位置新建人物实体
        /// </summary>
        /// <param name="ID">角色资源ID</param>
        /// <param name="x">在Grid坐标的X</param>
        /// <param name="y">在Grid坐标的Y</param>
        internal void CreateCharacter(int ID, int x, int y)
        {
            Vector3 worldPos = bfManager.GridPosConvert(x, y);//使用bfManager中的函数，将网格坐标下的位置转换为世界坐标下的位置
            Character thisCharacter = NewInstance(ID, worldPos);
            thisCharacter.info.Position = new GridVector2(x, y);
        }

        /// <summary>
        /// 根据在网格坐标的位置新建人物实体
        /// </summary>
        /// <param name="character"></param>
        /// <param name="asset"></param>
        internal Character CreateCharacter(int ID, GridVector2 gridPos)
        {
            Vector3 worldPos = bfManager.GridPosToWorld(gridPos);//使用bfManager中的函数，将网格坐标下的位置转换为世界坐标下的位置
            Character thisCharacter= NewInstance(ID, worldPos);
            //将bf和character互相填写
            Character.SetPos(thisCharacter, GameObject.Find($"bfCheck{gridPos.x}{gridPos.y}").GetComponent<BattleFieldCheck>());
            thisCharacter.info.Position = gridPos;
            return thisCharacter;
        }

        /// <summary>
        /// #未完工#   根据在世界坐标下的位置新建人物实体
        /// ！敌人的方法应该和英雄分开！
        /// 英雄只需要游戏开始时创建，而非战斗开始时。战斗结束后不销毁，这样就能做到保留角色信息。
        /// 敌人应该根据不同的敌人种类获取相应的敌人脚本，这样才能找到他的行为？（该怎么做呢？？）
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="worldPos"></param>
        internal Character NewInstance(int ID, Vector3 worldPos)
        {
            CharacterAsset asset = GetCharacterAsset(ID);
            Character thisCharacter = Instantiate(asset.Model, worldPos, Quaternion.Euler(0, 0, 0)).GetComponent<Character>();

            if (asset.CharacterType == CharacterClass.Enemy)
            {
                //将实体加入敌人实体列表
                battle_Model.enemys.Add(thisCharacter);
                thisCharacter.name = "Enemy" + battle_Model.enemys.Count;//实体重命名
                //必须先拥有info才能对其内容赋值
                thisCharacter.info = new CharacterInfo(asset);//敌人将会用asset内的数据新建
                thisCharacter.info.CharacterID = battle_Model.enemys.Count + 1000;//实体编号重写
            }
            else if (asset.CharacterType == CharacterClass.Hero)
            {

                hero_Model.character = thisCharacter;
                thisCharacter.name = "Hero";//实体重命名
                thisCharacter.info = new CharacterInfo(asset);//英雄将会用保留下来的数据
                thisCharacter.info.CharacterID = 1;//实体编号重写
            }

            InitializeSubjects(thisCharacter);
            return thisCharacter;
        }

        /// <summary>
        /// 对Character的Subjects初始化
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


