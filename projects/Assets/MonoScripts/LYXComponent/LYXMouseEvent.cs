using UnityEngine;
using System.Collections;

/****
 * 
 * 
 *  鼠标点击响应事件组件
 *  
 *  分别响应鼠标左键，中键，右键 按下，抬起 持续事件
 * 
 */

public class LYXMouseEvent : MonoBehaviour
{

    protected virtual void Awake()
    {

    }

    // Use this for initialization
    protected virtual void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            OnMouseLeftDown();
        }

        if (Input.GetMouseButtonUp(0))
        {
            OnMouseLeftUp();
        }

        if (Input.GetMouseButtonDown(1))
        {
            OnMouseRightDown();
        }

        if (Input.GetMouseButtonUp(1))
        {
            OnMouseRightUp();
        }

        if (Input.GetMouseButtonDown(2))
        {
            OnMouseCenterDown();
        }

        if (Input.GetMouseButtonUp(2))
        {
            OnMouseCenterUp();
        }

        if (Input.GetMouseButton(0))
        {
            OnMouseLeftButton();
        }

        if (Input.GetMouseButton(1))
        {
            OnMouseRightButton();
        }

        if (Input.GetMouseButton(2))
        {
            OnMouseCenterButton();
        }

        OnUpdate(Time.deltaTime);
    }

    /// <summary>
    ///  可重写更新函数
    /// </summary>
    /// <param name="deltaTime">
    /// 
    /// deltaTime，它表示距上一次调用Update或FixedUpdate 所用的时间。 
    /// 
    /// </param>
    protected virtual void OnUpdate(float deltaTime)
    {

    }

    /// <summary>
    ///  当鼠标左键按下   
    /// </summary>
    protected virtual void OnMouseLeftDown()
    {

    }

    /// <summary>
    ///  当鼠标左键抬起
    /// </summary>
    protected virtual void OnMouseLeftUp()
    {
    }

    /// <summary>
    /// 鼠标左键保持按下的过程
    /// </summary>
    protected virtual void OnMouseLeftButton()
    {

    }

    /// <summary>
    ///  当鼠标右键按下
    /// </summary>
    protected virtual void OnMouseRightDown()
    {

    }

    /// <summary>
    ///  当鼠标右键抬起
    /// </summary>
    protected virtual void OnMouseRightUp()
    {

    }

    /// <summary>
    /// 鼠标右键保持按下的过程
    /// </summary>
    protected virtual void OnMouseRightButton()
    {

    }

    /// <summary>
    ///  当鼠标中键按下
    /// </summary>
    protected virtual void OnMouseCenterDown()
    {

    }

    /// <summary>
    ///  当鼠标中键抬起
    /// </summary>
    protected virtual void OnMouseCenterUp()
    {

    }

    /// <summary>
    /// 鼠标中键保持按下的过程
    /// </summary>
    protected virtual void OnMouseCenterButton()
    {

    }
}
