using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUI : MonoBehaviour
{
    //箭头UI
    public GameObject ArrowUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 显示箭头UI
    /// </summary>
    public void ShowArrowUI()
    {
        ArrowUI.SetActive(true);
    }

    /// <summary>
    /// 关闭箭头UI
    /// </summary>
    public void CloseArrowUI()
    {
        ArrowUI.SetActive(false);
    }
}
