using System;
using UnityEngine;
using System.Collections;
/****
 * 
 * 
 * 组件 辅助类
 * 
 * 包括自动创建组件，删除 等
 * 
 * 
 */
using UnityEngine.UI;

/****
 * 
 * 
 * 自定义组件帮助类
 * 
 * 
 */

public class LYXCompHelper
{

    public static GameObject Create()
    {
        return Create(string.Empty);
    }

    public static GameObject Create(string name)
    {
        GameObject gameobject = new GameObject(name);
        return gameobject;
    }

    public static GameObject Create(string name, Type t)
    {
        GameObject gameobject = new GameObject(name, t);
        return gameobject;
    }

    /// <summary>
    /// 加载资源
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static GameObject LoadResource(string path)
    {
        if (string.IsNullOrEmpty(path)) return null;
        return Resources.Load(path) as GameObject;
    }

    /// <summary>
    /// 导入资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    public static T LoadResource<T>(string path) where T : Component
    {
        GameObject load = LoadResource(path);
        if (load == null) return default(T);
        GameObject init = GameObject.Instantiate(load) as GameObject;
        if (init == null) return default(T);
        return AddComponet<T>(init);
    }

    /// <summary>
    /// 导入并且实例化
    /// </summary>
    /// <param name="path">资源的路径</param>
    /// <param name="parent">资源父节点</param>
    /// <returns></returns>
    public static GameObject LoadAndInstance(string path, Transform parent)
    {
        GameObject load = LoadResource(path);
        if (load == null) return null;
        GameObject init = GameObject.Instantiate(load) as GameObject;
        if (init == null) return null;
        Transform trans = init.transform;
        if (parent != null) trans.parent = parent;
        trans.localPosition = Vector3.zero;
        trans.localRotation = Quaternion.identity;
        trans.localScale = Vector3.one;
        return init;
    }

    /// <summary>
    /// 导入并且实例化
    /// </summary>
    /// <param name="path">资源的路径</param>
    /// <param name="parent">资源父节点</param>
    /// <returns></returns>
    public static T LoadAndInstance<T>(string path, Transform parent) where T : Component
    {
        GameObject load = LoadAndInstance(path, parent);
        if (load == null) return default(T);
        return AddComponet<T>(load);
    }

    /// <summary>
    /// 查找组件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="gameobject"></param>
    /// <returns></returns>
    public static T GetComponent<T>(GameObject gameobject) where T : Component
    {
        if (gameobject == null) return default(T);
        return gameobject.GetComponent<T>();
    }

    /// <summary>
    /// 增加组件
    /// </summary>
    /// <typeparam name="T">组件的类型</typeparam>
    /// <param name="gameobject">增加组件的 gameobject </param>
    /// <returns></returns>
    public static T AddComponet<T>(GameObject gameobject) where T : Component
    {
        T t = GetComponent<T>(gameobject);
        if (t != null) return t;
        return gameobject.AddComponent<T>();
    }

    /// <summary>
    /// 发现子节点
    /// </summary>
    /// <param name="source"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    public static Transform FindTransform(GameObject source, string path)
    {
        return FindTransform(source.transform, path);
    }

    /// <summary>
    /// 发现子节点
    /// </summary>
    /// <param name="source"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    public static Transform FindTransform(Transform source, string path)
    {
        if (source == null || string.IsNullOrEmpty(path)) return null;
        return source.Find(path);
    }

    /// <summary>
    /// 获得屏幕的高和宽
    /// </summary>
    /// <param name="isNgui">是否需要判读 ngui </param>
    /// <returns></returns>
    public static Vector2 SceneWidthAndHeight(bool isNgui)
    {
        Vector2 vect = new Vector2(Screen.width, Screen.height);
        UIRoot root = GameObject.FindObjectOfType<UIRoot>();
        if (root != null && isNgui)
        {
            float s = (float)root.activeHeight / Screen.height;
            int height = Mathf.CeilToInt(Screen.height * s);
            int width = Mathf.CeilToInt(Screen.width * s);
            vect = new Vector2(width, height);
        }
        return vect;
    }

    /// <summary>
    /// 计算粒子播放的时长
    /// </summary>
    /// <param name="transform"></param>
    /// <returns></returns>
    public static float ParticleSystemLength(Transform transform)
    {
        if (transform == null) return 0f;
        ParticleSystem[] particleSystems = transform.GetComponentsInChildren<ParticleSystem>();
        if (particleSystems == null || particleSystems.Length <= 0) return 0f;
        float maxDuration = 0;
        for (int i = 0, len = particleSystems.Length; i < len; i++)
        {
            ParticleSystem ps = particleSystems[i];
            if (ps == null || !ps.enableEmission) continue;
            if (ps.loop) return -1f;

            float dunration = 0f;
            if (ps.emissionRate <= 0)
            {
                dunration = ps.startDelay + ps.startLifetime;
            }
            else
            {
                dunration = ps.startDelay + Mathf.Max(ps.duration, ps.startLifetime);
            }
            if (dunration > maxDuration)
            {
                maxDuration = dunration;
            }
        }
        return maxDuration;
    }

}
