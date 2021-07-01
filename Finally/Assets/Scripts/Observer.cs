using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 观察者抽象类
/// </summary>
public abstract class Observe
{
    public Observe()
    {
    }
    /// <summary>
    /// 的到通知后的响应
    /// </summary>
    public abstract void Responce();
}
