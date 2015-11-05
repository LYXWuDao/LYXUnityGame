using Game.LYX.Behaviour;
using Game.LYX.Common;
using UnityEngine;
using System.Collections;

/*****
 * 
 * 
 * 自我毁坏
 * 
 */

namespace Game.LYX.Tools
{

    public class LYXSelfDestroy : LYXBaseBehaviour
    {

        /// <summary>
        /// 自我销毁时间
        /// </summary>
        public float mDtyTime = 0f;

        /// <summary>
        /// 增加一个销毁脚本
        /// 
        /// 开启一个销毁
        /// </summary>
        /// <returns></returns>
        public static LYXSelfDestroy Begin(GameObject go, float dtyTime)
        {
            LYXSelfDestroy dest = LYXCompHelper.FindComponet<LYXSelfDestroy>(go);
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
}
