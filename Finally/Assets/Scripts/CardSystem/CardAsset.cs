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
        [Header("����")]
        [Space] 

        //���Ʊ��   
        [SerializeField, SetProperty("CardID")]
        private int _ID;
        public int CardID { get { return _ID;} set { _ID=value;} }

        //���Ʒ���
        [SerializeField, SetProperty("Cost")]
        private int _cost;
        public int Cost { get { return _cost; } set { _cost = value; } }

        //���ĵķ�������
        [SerializeField, SetProperty("CostType")]
        private CostClass _costType;
        public CostClass CostType { get { return _costType; } set { _costType = value; } }

        //��������
        [SerializeField, SetProperty("Name")]
        private string _name;
        public string Name { get { return _name; } set { _name = value; } }

        //��������
        [TextArea][SerializeField, SetProperty("Description")]
        private string _description;
        public string Description { get { return _description; } set { _description = value; } }

        //��������
        [SerializeField, SetProperty("CardType")]
        private CardClass _cardType;
        public CardClass CardType { get { return _cardType; } set { _cardType = value; } }

        //��������ͼƬ
        [SerializeField, SetProperty("Image")]
        private Sprite _image;
        public Sprite Image { get { return _image; } set { _image = value; } }

        [Header("��ѡ����")]
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

        [Header("��ֵ�б�")]
        [Space]
        [SerializeField, SetProperty("Values")]
        private List<int> _values;
        public List<int> Values { get { return _values; } set { _values = value; } }

        /// <summary>
        /// ��ȡcardID��Ӧ��CardAsset
        /// </summary>
        /// <param name="cardID"></param>
        /// <returns></returns>
        public static CardAsset GetCardAsset(int cardAssetID)
        {
            CardAsset Asset = Resources.Load($"Card/Card_{cardAssetID}") as CardAsset;

            //��ʱʹ��Resources.Load(String Path)����
            //������Ҫ����AssetBundle��ط���

            return Asset;
        }


    }
}

