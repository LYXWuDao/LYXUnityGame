using System;
using System.Collections.Generic;
using UnityEngine;


/****
 * 
 * 
 *  unity 脚本的生命周期
 *  
 *  行为组件基类，所以行为都需要继承它
 * 
 */

public class LYXBaseBehaviour : MonoBehaviour
{

    /// <summary>
    /// 
    /// </summary>
    protected virtual void Awake()
    {
    }

    /// <summary>
    /// 获得焦点
    /// </summary>
    protected virtual void OnEnable()
    {
    }

    protected virtual void FixedUpdate()
    {
        OnFixedUpdate(Time.fixedTime);
    }

    protected virtual void OnFixedUpdate(float fixedTime)
    {
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        OnUpdate(Time.deltaTime);
    }

    protected virtual void OnUpdate(float deltaTime)
    {
    }

    protected virtual void LateUpdate()
    {

    }

    protected virtual void OnGUI()
    {

    }

    protected virtual void OnApplicationPause()
    {
    }

    protected virtual void OnDisable()
    {
    }

    protected virtual void OnDestroy()
    {
    }

    protected virtual void OnApplicationQuit()
    {
    }

}

