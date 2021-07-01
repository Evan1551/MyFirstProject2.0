using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterSystem
{
    public enum CharacterClass { Hero, Enemy }
    [CreateAssetMenu(menuName = "Create CharcaterAsset")]
    public class CharacterAsset : ScriptableObject
    {
        [Header("��������")]

        //����ID
        [SerializeField, SetProperty("CharacterAssetID")]
        private int _characterAssetID;
        public int CharacterAssetID
        {
            get { return _characterAssetID; }
            set { _characterAssetID  = value; }
        }

        //��������
        [SerializeField, SetProperty("Name")]
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        //�������Ѫ��
        [SerializeField, SetProperty("MaxHP")]
        private int _maxHp;
        public int MaxHP
        {
            get { return _maxHp; }
            set { _maxHp = value; }
        }

        //ģ��Prefab
        [SerializeField, SetProperty("Model")]
        private GameObject _model;
        public GameObject Model
        {
            get { return _model; }
            set { _model = value; }
        }

        //��������
        [SerializeField, SetProperty("CharacterType")]
        private CharacterClass _characterType;
        public CharacterClass CharacterType
        {
            get { return _characterType; }
            set { _characterType = value; }
        }
    }

}
