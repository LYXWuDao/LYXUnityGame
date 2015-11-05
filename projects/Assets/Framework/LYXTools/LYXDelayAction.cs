using System;
using System.Collections.Generic;
using Game.LYX.Behaviour;
using Game.LYX.Common;
using UnityEngine;

namespace Game.LYX.Tools
{

    public class LYXDelayAction : LYXBaseBehaviour
    {

        /// <summary>
        /// 当前回调 action
        /// </summary>
        private Action mActionBack = null;

        /// <summary>
        /// 调用 action 间隔时间
        /// </summary>
        private float mActionTime = 0;

        protected override void OnUpdate(float deltaTime)
        {
            if (mActionBack == null) return;
            if (mActionTime > 0)
            {
                mActionTime -= deltaTime;
                return;
            }
            mActionBack();
            mActionBack = null;
            RemoveAction();
        }

        public static LYXDelayAction BeginAction(GameObject go, float delayTime, Action action)
        {
            if (go == null && action == null) return null;
            if (go == null || delayTime <= 0)
            {
                action();
                return null;
            }
            LYXDelayAction delact = LYXCompHelper.FindComponet<LYXDelayAction>(go);
            delact.mActionBack = action;
            delact.mActionTime = delayTime;
            return delact;
        }

        /// <summary>
        /// 移出当前脚本
        /// </summary>
        public void RemoveAction()
        {
            Destroy(this);
        }

    }

}