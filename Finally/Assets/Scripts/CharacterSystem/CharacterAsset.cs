using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterSystem
{
    public enum CharacterClass { Hero, Enemy }
    [CreateAssetMenu(menuName = "Create CharcaterAsset")]
    public class CharacterAsset : ScriptableObject
    {
        [Header("人物属性")]

        //人物ID
        [SerializeField, SetProperty("CharacterAssetID")]
        private int _characterAssetID;
        public int CharacterAssetID
        {
            get { return _characterAssetID; }
            set { _characterAssetID  = value; }
        }

        //人物名字
        [SerializeField, SetProperty("Name")]
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        //人物最大血量
        [SerializeField, SetProperty("MaxHP")]
        private int _maxHp;
        public int MaxHP
        {
            get { return _maxHp; }
            set { _maxHp = value; }
        }

        //模型Prefab
        [SerializeField, SetProperty("Model")]
        private GameObject _model;
        public GameObject Model
        {
            get { return _model; }
            set { _model = value; }
        }

        //人物类型
        [SerializeField, SetProperty("CharacterType")]
        private CharacterClass _characterType;
        public CharacterClass CharacterType
        {
            get { return _characterType; }
            set { _characterType = value; }
        }
    }

}
