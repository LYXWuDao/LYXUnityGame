using System;
using System.Collections.Generic;
using UnityEngine;


/****
 * 
 * 
 * unity 脚本的生命周期
 * 
 */

public class LYXBehaviour : MonoBehaviour
{

    protected virtual void Awake()
    {
    }

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

