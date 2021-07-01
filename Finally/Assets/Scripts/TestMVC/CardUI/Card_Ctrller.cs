using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardSystem;
using UnityEngine.EventSystems;
using CharacterSystem;
using CommandSystem;

public class Card_Ctrller : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private Card_View card_View;
    public Card_Model card_Model;
    public int i;
    //internal CardDisplayManager cardDisplayManager;//(?)


    private Vector2 offsetPos;  //临时记录点击点与UI的相对位置
    private Vector3 ScreenSpacePos;

    private UI_Ctrller showUI;

    private void Awake()
    {
        card_View = GetComponent<Card_View>();
        showUI = GameObject.Find("UI").GetComponent<UI_Ctrller>();
       
    }


    private void Update()
    {
        i = card_Model.Info.CardID;
    }
    /// <summary>
    /// 鼠标光标进入卡牌时触发
    /// </summary>
    public void MonseOnCard()
    {
        CardZoomIn();
        CheckCardDistance();
    }


    /// <summary>
    /// 鼠标光标离开卡牌时触发
    /// </summary>
    public void MouseExitCard()
    {
        CardZoomOut();
        StopCheckCardDistance();
    }


    /// <summary>
    /// 鼠标在卡牌上时的放大效果
    /// </summary>
    public void CardZoomIn()
    {
        if (card_View.IsZoomIn == false)
        {
            card_View.IsZoomIn = true;

            transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
            if (card_View.isInHandPile)
            {
                card_View.PreSiblingIndex = transform.GetSiblingIndex();//获取卡牌当前的层次
                transform.SetAsLastSibling();//视图上置于最底层（最靠近摄像机）
                                             //GetComponent<RectTransform>().anchoredPosition3D += new Vector3(0, ZoomOffset, 0);
            }
        }
    }


    /// <summary>
    /// 鼠标离开卡牌上时的还原缩放效果
    /// </summary>
    public void CardZoomOut()
    {
        if (card_View.IsZoomIn == true)
        {
            card_View.IsZoomIn = false;

            transform.localScale = new Vector3(1f, 1f, 1f);
            if (card_View.isInHandPile)
            {
                transform.SetSiblingIndex(card_View.PreSiblingIndex);
                //GetComponent<RectTransform>().anchoredPosition3D -= new Vector3(0, ZoomOffset, 0);
            }
        }
    }


    /// <summary>
    /// 返还一个GameObject[],其中GameObject[0]为左侧卡牌，GameObject[1]为右侧卡牌
    /// </summary>
    public GameObject[] GetNeighbourCards()
    {
        GameObject[] LeftAndRight = new GameObject[2];
        if (card_View.isInHandPile)
        {
            int i = PileDisplay_Ctrller.Instance.GetHandCardIndex(card_View.thisCard);
            GameObject Left, Right = null;
            if (i == 0)
            {
                if (i != PileDisplay_Ctrller.handPileInstances.Count - 1)
                {
                    Right = PileDisplay_Ctrller.handPileInstances[i + 1];
                    LeftAndRight[1] = Right;
                    Debug.Log(LeftAndRight[1]);
                }
            }

            else if (i == PileDisplay_Ctrller.handPileInstances.Count - 1)
            {
                Left = PileDisplay_Ctrller.handPileInstances[i - 1];
                LeftAndRight[0] = Left;

            }
            else
            {
                Left = PileDisplay_Ctrller.handPileInstances[i - 1];
                LeftAndRight[0] = Left;

                Right = PileDisplay_Ctrller.handPileInstances[i + 1];
                LeftAndRight[1] = Right;
            }
        }
        return LeftAndRight;
    }

    public void CheckCardDistance()
    {
        if (card_View.isInHandPile)
        {
            Hero_Model.Instance.CardDistance = card_Model.Effect.DistanceValue;
            BattleFieldCheck.RefreshCardDistance();
        }
    }

    public void StopCheckCardDistance()
    {
        if (card_View.isInHandPile&&!Hero_Model.Instance.UsingCard)
        {
            Hero_Model.Instance.CardDistance = 0;
            BattleFieldCheck.HideCardDistance();
        }
    }

    public void ToUseCard()
    {
        Hero_Model.Instance.UsingCard = true;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void UseCard()
    {
        if(card_Model.Effect.DistanceType==DistanceClass.Human|| card_Model.Effect.DistanceType == DistanceClass.Earth)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 2000))
            {
                if (hit.collider.CompareTag("Character"))
                {
                    Debug.Log("检测到角色");
                    Battle_Model.Instance.Targets.Add(hit.collider.GetComponent<Character>());
                    if (Character.CalcuDistance(Battle_Model.Instance.Targets[0], Battle_Model.Instance.Current) <= card_Model.Effect.DistanceValue)
                    {
                        card_Model.Effect.card_Model = card_Model;
                            card_Model.Effect.UseCard();
                    }
                    else Battle_Model.Instance.Targets.Clear();
                }
            }
        }
        else if(card_Model.Effect.DistanceType == DistanceClass.Sky)
        {
            if (Input.mousePosition.y > card_View.HandPileHeight)
            {
                card_Model.Effect.card_Model = card_Model;
                card_Model.Effect.UseCard();
         
            }
            ResetCardPosition();
        }
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        Hero_Model.Instance.UsingCard = false;
        Hero_Model.Instance.CardDistance = 0;
        BattleFieldCheck.HideCardDistance();
    }

    /// <summary>
    /// #接口IDragHandler#  拖拽的效果。
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        if (card_View.isInHandPile)
        {
            if (eventData.position.y < card_View.HandPileHeight && card_Model.Effect.DistanceType != DistanceClass.Sky)
            {
                //Debug.Log(cardView.HandPileHeight);
                //Debug.Log(eventData.position.y);
            }

            if (card_Model.Effect.DistanceType == DistanceClass.Sky || (eventData.position.y < card_View.HandPileHeight && card_Model.Effect.DistanceType != DistanceClass.Sky))
            {
                //计算在投影空间中的新位置
                Vector3 NewCameraSpacePos;
                NewCameraSpacePos.x = (eventData.position - offsetPos).x;
                NewCameraSpacePos.y = (eventData.position - offsetPos).y;
                NewCameraSpacePos.z = ScreenSpacePos.z;
                //转换成在世界空间中的位置
                GetComponent<RectTransform>().transform.position = Camera.main.ScreenToWorldPoint(NewCameraSpacePos);
            }
            else if (eventData.position.y >= card_View.HandPileHeight && card_Model.Effect.DistanceType != DistanceClass.Sky)
            {
                ResetCardPosition();
            }
        }

    }

    /// <summary>
    ///#接口IPointerDownHandler#  鼠标按下时效果
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        if (card_Model.Effect.DistanceType == DistanceClass.Earth || card_Model.Effect.DistanceType == DistanceClass.Human)
            showUI.ShowArrowUI();

        if (card_View.isInHandPile)
        {
            //ShowDistance();
            //记录卡牌原本在屏幕空间中的位置ScreenSpacePos，以及鼠标与卡牌在屏幕空间中的偏移Offset
            ScreenSpacePos = Camera.main.WorldToScreenPoint(transform.position);
            offsetPos = eventData.position - (Vector2)ScreenSpacePos;
        }


    }

    /// <summary>
    /// #接口IPointerUpHandler#  鼠标松开时效果
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        showUI.CloseArrowUI();

        //if (card_View.isInHandPile)
        //{
        //    ResetCardPosition();
        //    //ExecuteCard();
        //    //根据ScreenSpacePos算出原本在世界空间中的位置
        //}
    }

    //private void ExecuteCard()
    //{
    //    card_View.Effect.CommandList.CheckAndApplyIssues();
    //}

    private void ResetCardPosition()
    {

        GetComponent<RectTransform>().transform.position = Camera.main.ScreenToWorldPoint(ScreenSpacePos);

    }
}
