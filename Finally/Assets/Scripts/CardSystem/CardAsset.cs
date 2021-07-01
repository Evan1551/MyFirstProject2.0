using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


namespace CardSystem
{
    public enum CardClass { Attack, Tactics, Climax, Mad }
    public enum CostClass { AP, MP }
    public enum DistanceClass { Human, Sky, Earth }
    [CreateAssetMenu(menuName = "Create CardAsset ")]
    public class CardAsset : ScriptableObject
    {
        [Header("卡牌")]
        [Space] 

        //卡牌编号   
        [SerializeField, SetProperty("CardID")]
        private int _ID;
        public int CardID { get { return _ID;} set { _ID=value;} }

        //卡牌费用
        [SerializeField, SetProperty("Cost")]
        private int _cost;
        public int Cost { get { return _cost; } set { _cost = value; } }

        //消耗的费用类型
        [SerializeField, SetProperty("CostType")]
        private CostClass _costType;
        public CostClass CostType { get { return _costType; } set { _costType = value; } }

        //卡牌名称
        [SerializeField, SetProperty("Name")]
        private string _name;
        public string Name { get { return _name; } set { _name = value; } }

        //卡牌描述
        [TextArea][SerializeField, SetProperty("Description")]
        private string _description;
        public string Description { get { return _description; } set { _description = value; } }

        //卡牌种类
        [SerializeField, SetProperty("CardType")]
        private CardClass _cardType;
        public CardClass CardType { get { return _cardType; } set { _cardType = value; } }

        //卡面立绘图片
        [SerializeField, SetProperty("Image")]
        private Sprite _image;
        public Sprite Image { get { return _image; } set { _image = value; } }

        [Header("可选属性")]
        [Space]
        [SerializeField, SetProperty("DistanceType")]
        private DistanceClass _distanceType;
        public DistanceClass DistanceType { get { return _distanceType; } set { _distanceType = value; } }

        [SerializeField, SetProperty("DistanceValue")]
        private int _distanceValue;
        public int DistanceValue { get { return _distanceValue; } set { _distanceValue = value; } }

        [SerializeField, SetProperty("RangeValue")]
        private int _rangeValue;
        public int RangeValue { get { return _rangeValue; } set { _rangeValue = value; } }

        [Header("数值列表")]
        [Space]
        [SerializeField, SetProperty("Values")]
        private List<int> _values;
        public List<int> Values { get { return _values; } set { _values = value; } }

        /// <summary>
        /// 获取cardID相应的CardAsset
        /// </summary>
        /// <param name="cardID"></param>
        /// <returns></returns>
        public static CardAsset GetCardAsset(int cardAssetID)
        {
            CardAsset Asset = Resources.Load($"Card/Card_{cardAssetID}") as CardAsset;

            //暂时使用Resources.Load(String Path)方法
            //将来需要换成AssetBundle相关方法

            return Asset;
        }


    }
}

