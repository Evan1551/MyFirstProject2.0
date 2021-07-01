using BattleSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterSystem
{
    public class CharacterInfo
    {
        //��ɫ��Դ���
        public int CharacterAssetID { get; set; }
        //��ɫ���Ѫ��
        public int MaxHP { get; set; }
        //��ɫ����
        public string Name { get; set; }
        //ģ��Prefab
        public GameObject Model { get; set; }
        //��ɫ����
        public CharacterClass CharacterType { get; set; }

        //��ɫ���
        public int CharacterID { get; set; }
        //��ɫѪ��
        public int Hp { get; set; }
        //λ��
        public GridVector2 Position { get; set; }
        //ÿ�غϳ�������
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
