using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_View : MonoBehaviour
{
    //血量缓冲
    public Image HPEffectImage;
    //当前血量
    public Image CurrentHPImage;

    //血量扣除速度
    [SerializeField]
    public float hurtSpeed = 0.005f;
}
