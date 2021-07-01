using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowUI : MonoBehaviour
{
    [Tooltip("箭头节点")]
    public GameObject ArrowNodePrefab;

    [Tooltip("箭头头部")]
    public GameObject ArrowHeadPrefab;

    [Tooltip("节点尺寸")]
    public float scaleFactor = 1f;

    [Tooltip("节点数量")]
    public int ArrowNodeNum;

    //[Tooltip("玩家角色")]
    //public GameObject Hero1;


    private RectTransform origin;

    /// <summary>
    /// 每个节点的位置
    /// </summary>
    private List<RectTransform> arrowNodes = new List<RectTransform>();

    /// <summary>
    /// 列表中有三个节点
    /// </summary>
    private List<Vector2> controlPoints = new List<Vector2>();

    /// <summary>
    /// P1,P2位置的向量因子
    /// </summary>
    private readonly List<Vector2> controlPointsFactors = new List<Vector2> { new Vector2(-0.5f, 2f), new Vector2(1f, 2.1f) };

    /// <summary>
    /// 玩家角色的屏幕坐标
    /// </summary>
    private Vector2 HeroSrceenPosition;



    private void Awake()
    {
        //HeroSrceenPosition = Camera.main.WorldToScreenPoint(Hero1.transform.position);

        this.origin = this.GetComponent<RectTransform>();

        for (int i = 0; i < this.ArrowNodeNum; ++i)
        {
            this.arrowNodes.Add(Instantiate(this.ArrowNodePrefab, this.transform).GetComponent<RectTransform>());
        }
        this.arrowNodes.Add(Instantiate(this.ArrowHeadPrefab, this.transform).GetComponent<RectTransform>());

        this.arrowNodes.ForEach(a => a.GetComponent<RectTransform>().position = new Vector2(-1000, -1000));

        for (int i = 0; i < 4; ++i)
        {
            this.controlPoints.Add(Vector2.zero);
        }
    }


    // Update is called once per frame
    private void Update()
    {
        HeroSrceenPosition = Camera.main.WorldToScreenPoint(GameObject.Find("Hero").transform.position);

        //P0是Hero1的屏幕坐标
        this.controlPoints[0] = new Vector2(HeroSrceenPosition.x - Screen.width / 2f, HeroSrceenPosition.y - Screen.height / 2f);

        //P3是鼠标的位置
        this.controlPoints[3] = new Vector2(Input.mousePosition.x - Screen.width / 2f, Input.mousePosition.y - Screen.height / 2f);

        this.controlPoints[1] = this.controlPoints[0] + (this.controlPoints[3] - this.controlPoints[0]) * this.controlPointsFactors[0];
        this.controlPoints[2] = this.controlPoints[0] + (this.controlPoints[3] - this.controlPoints[0]) * this.controlPointsFactors[1];

        for (int i = 0; i < this.arrowNodes.Count; ++i)
        {
            var t = Mathf.Log(1f * i / (this.arrowNodes.Count - 1) + 1f, 2f);

            arrowNodes[i].GetComponent<RectTransform>().anchoredPosition3D =
                Mathf.Pow(1 - t, 3) * this.controlPoints[0] +
                3 * Mathf.Pow(1 - t, 2) * t * this.controlPoints[1] +
                3 * (1 - t) * Mathf.Pow(t, 2) * this.controlPoints[2] +
                Mathf.Pow(t, 3) * this.controlPoints[3];

            if (i > 0)
            {
                var eular = new Vector3(0, 0, Vector2.SignedAngle(Vector2.up, this.arrowNodes[i].position - this.arrowNodes[i - 1].position));
                this.arrowNodes[i].localRotation = Quaternion.Euler(eular);
            }

            var scale = this.scaleFactor * (1f - 0.03f * (this.arrowNodes.Count - 1 - i));
            this.arrowNodes[i].localScale = new Vector3(scale, scale, 1f);
        }

        this.arrowNodes[0].transform.rotation = this.arrowNodes[1].transform.rotation;
    }
}

