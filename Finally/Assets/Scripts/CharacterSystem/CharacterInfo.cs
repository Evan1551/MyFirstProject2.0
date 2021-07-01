using BattleSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterSystem
{
    public class CharacterInfo
    {
        //角色资源编号
        public int CharacterAssetID { get; set; }
        //角色最大血量
        public int MaxHP { get; set; }
        //角色名字
        public string Name { get; set; }
        //模型Prefab
        public GameObject Model { get; set; }
        //角色类型
        public CharacterClass CharacterType { get; set; }

        //角色编号
        public int CharacterID { get; set; }
        //角色血量
        public int Hp { get; set; }
        //位置
        public GridVector2 Position { get; set; }
        //每回合抽牌数量
        public int DrawAmount { get; set; }

        public CharacterInfo(CharacterAsset asset)
        {
            SetCharacterAsset(asset);
            if(CharacterType == CharacterClass.Hero)
            {
                DrawAmount = 5;
            }
        }


        public bool CheckPos()
        {
            if (Position.x != 0&&Position.y!=0)
            {
                return false;
            }
            return true;
        }



        public void Die()
        {

        }

        private void SetCharacterAsset( CharacterAsset asset)
        {
            CharacterAssetID = asset.CharacterAssetID;
            MaxHP = asset.MaxHP;
            Hp = asset.MaxHP;
            Name = asset.Name;
            Model = asset.Model;
            CharacterType = asset.CharacterType;
        }
    }

}
