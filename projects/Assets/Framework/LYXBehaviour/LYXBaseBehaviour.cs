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
    /// 对象创建后调用
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

    /// <summary>
    /// 在update第一帧调用前调用
    /// </summary>
    protected virtual void Start()
    {

    }

    /// <summary>
    /// 每一帧调用
    /// </summary>
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

    /// <summary>
    /// unity 界面绘制调用
    /// </summary>
    protected virtual void OnGUI()
    {

    }

    /// <summary>
    /// 项目暂停时调用
    /// </summary>
    protected virtual void OnApplicationPause()
    {
    }

    /// <summary>
    /// 对象失去焦点时调用
    /// </summary>
    protected virtual void OnDisable()
    {
    }

    /// <summary>
    /// 对象销毁时调用
    /// </summary>
    protected virtual void OnDestroy()
    {
    }

    /// <summary>
    /// 项目退出时调用
    /// </summary>
    protected virtual void OnApplicationQuit()
    {
    }

}

