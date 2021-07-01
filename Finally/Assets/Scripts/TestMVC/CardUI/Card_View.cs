using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CardSystem;

public class Card_View : MonoBehaviour
{

    public Text Name;
    public Text Description;
    public Text Cost;
    public Text Type;
    public GameObject thisCard;

    [HideInInspector]
    public bool isInHandPile;//卡牌是否在手牌堆中

    public bool IsZoomIn = false;

    public int PreSiblingIndex;
    public float ZoomOffset = 60;
    public float HandPileHeight = 250f;


    /// <summary>
    /// 更新UI View上的信息
    /// </summary>
    /// <param name="data"></param>
    public void UpdateInfo(Card_Model data)
    {
        Name.text = data.Info.Name;
        Description.text = data.Info.Description;
        Debug.Log(data.Effect);
        Cost.text = data.Effect.CostValue.ToString();
        Type.text = data.Effect.CostType.ToString();
    }
}
