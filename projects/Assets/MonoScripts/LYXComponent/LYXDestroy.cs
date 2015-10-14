using UnityEngine;
using System.Collections;

/*****
 * 
 * 
 * 自我毁坏
 * 
 */

public class LYXDestroy : LYXBehaviour
{

    /// <summary>
    /// 自我销毁时间
    /// </summary>
    public float mDtyTime = 0f;

    /// <summary>
    /// 增加一个销毁脚本
    /// </summary>
    /// <returns></returns>
    public static LYXDestroy Begin(GameObject go, float dtyTime)
    {
        LYXDestroy dest = LYXCompHelper.FindComponet<LYXDestroy>(go);
        dest.mDtyTime = dtyTime;
        return dest;
    }

    protected override void OnUpdate(float deltaTime)
    {
        if (mDtyTime > 0)
        {
            mDtyTime -= deltaTime;
            return;
        }
        GameObject.Destroy(gameObject);
    }

}
