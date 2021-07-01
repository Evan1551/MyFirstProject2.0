using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private float loadingProgress = 0;//当前进度值

    //private float targetProgress = 0;//目标进度值

    public Text percentText;//进度百分比

    public Slider slider;//进度条

    public GameObject PercentProgress;//进度条物体（？）

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 游戏开始点击事件
    /// </summary>
    public void StartClick()
    {
        LoadScene("");//开始场景加载
    }


    /// <summary>
    /// 游戏暂停点击事件
    /// </summary>
    public void PauseClick()
    {
        Debug.Log("游戏暂停");
        //UI出现
        Time.timeScale = 0;
    }


    /// <summary>
    /// 游戏继续点击事件
    /// </summary>
    public void ContinceClick()
    {
        Debug.Log("游戏继续");
        //UI关闭
        Time.timeScale = 1;
    }


    /// <summary>
    /// 退出游戏点击事件
    /// </summary>
    public void ExitClick()
    {
        Debug.Log("游戏退出");
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }


    /// <summary>
    /// 加载场景
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene("");//跳转到“加载界面”

        StartCoroutine(LoadAsync(sceneName));
    }


    /// <summary>
    /// 异步加载 + 进度条
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    IEnumerator LoadAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        PercentProgress.SetActive(true);

        while (operation.isDone == false)
        {
            //Debug.Log("Progress is" + operation.progress);

            //loadingProgress = Mathf.Clamp01(operation.progress / 0.9f);//进度条到0.9时加载成功
            ++loadingProgress;

            slider.value = loadingProgress;
            percentText.text = (loadingProgress * 100).ToString() + "%";

            yield return new WaitForEndOfFrame();
        }

        PercentProgress.SetActive(false);
        operation.allowSceneActivation = true;
    }


    /// <summary>
    /// 调整屏幕分辨率 —— 1920 * 1080 全屏
    /// </summary>
    public void ChangeScreenResolution_First()//
    {
        Screen.SetResolution(1920, 1080, true);
    }

    /// <summary>
    /// 调整屏幕分辨率 —— 1920 * 1080 不全屏
    /// </summary>
    public void ChangeScreenResolution_Second()//
    {
        Screen.SetResolution(1920, 1080, false);
    }

    /// <summary>
    /// 调整屏幕分辨率 —— 1280 * 720 全屏
    /// </summary>
    public void ChangeScreenResolution_Third()//
    {
        Screen.SetResolution(1280, 720, true);
    }

    /// <summary>
    /// 调整屏幕分辨率 —— 1280 * 720 不全屏
    /// </summary>
    public void ChangeScreenResolution_Fourth()//
    {
        Screen.SetResolution(1280, 720, false);
    }

    /// <summary>
    /// 是否全屏
    /// </summary>
    /// <param name="isFullScreen"></param>
    public void ChangeFullScreen(bool isFullScreen)
    {
        if (isFullScreen)
        {
            Debug.Log("全屏");
            Screen.fullScreen = true;
        }
        else
        {
            Debug.Log("取消全屏");
            Screen.fullScreen = false;
        }
    }





    public void AboutSomething()
    {
        Debug.Log("这里有一个未来可能会补充的文本");
    }
}
