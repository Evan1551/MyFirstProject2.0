using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_View : MonoBehaviour
{
    //Ѫ������
    public Image HPEffectImage;
    //��ǰѪ��
    public Image CurrentHPImage;

    //Ѫ���۳��ٶ�
    [SerializeField]
    public float hurtSpeed = 0.005f;
}
