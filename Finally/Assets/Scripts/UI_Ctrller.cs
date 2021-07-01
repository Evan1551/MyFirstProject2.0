using System.Collections;
using System.Collections.Generic;
using TurnSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Ctrller : MonoBehaviour
{
    public Hero_Model hero_Model;
    public UI_View uI_View;


    public void Ini()
    {
        uI_View = GetComponent<UI_View>();
        hero_Model = Hero_Model.Instance;
        RefreshAPandMP();
        hero_Model.MPorAPChange.AddObserve(RefreshAPandMP);

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
        uI_View.ArrowUI.SetActive(true);
    }

    /// <summary>
    /// 关闭箭头UI
    /// </summary>
    public void CloseArrowUI()
    {
        uI_View.ArrowUI.SetActive(false);
    }

    public void RefreshAPandMP()
    {
        uI_View.CurrentAP.text = hero_Model.CurrentAP.ToString();
        uI_View.MaxAP.text = hero_Model.MaxAP.ToString();
        uI_View.CurrentMP.text = hero_Model.CurrentMP.ToString();
        uI_View.MaxMP.text = hero_Model.MaxMP.ToString();
        foreach(var V in Battle_Model.Instance.BFC_List)
        {
            if(hero_Model.UsingCard)
            {
                BattleFieldCheck.RefreshMoveDistance();
                break;
            }
        }
    }

    public static void GameOver()
    {
        GameObject.Find("UI").GetComponent<UI_View>().GameOver.SetActive(true);
    }

    public static void Congradulation()
    {
        GameObject.Find("UI").GetComponent<UI_View>().Congradulation.SetActive(true);
    }

    public static void Reload()
    {
        Battle_Model.Instance.ini();
        Hero_Model.Instance.ini();
        Turn_Model.Instance.ini();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
