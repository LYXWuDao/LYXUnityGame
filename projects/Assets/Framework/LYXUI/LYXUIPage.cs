using System;
using System.Collections.Generic;
using Game.LYX.Behaviour;
using Game.LYX.Common;
using Game.LYX.Source;
using UnityEngine;

namespace Game.LYX.UI
{
    /****
     * 
     * 
     * 界面操作
     * 
     */
    public class LYXUIPage : LYXUIBehaviour
    {

        /// <summary>
        /// 界面上所以的panel
        /// </summary>
        private UIPanel[] mPanels;

        /// <summary>
        /// 界面上所有的 Collider
        /// </summary>
        private Collider[] mBoxColliders;

        /// <summary>
        /// 打开界面
        /// </summary>
        public override void OnOpen()
        {
            mPanels = gameObject.GetComponentsInChildren<UIPanel>();
            mBoxColliders = gameObject.GetComponentsInChildren<Collider>();
        }

        /// <summary>
        /// 打开界面 并设置界面的panel 深度
        /// </summary>
        /// <param name="depth"></param>
        public override void OnOpen(int depth)
        {
            mPanels = gameObject.GetComponentsInChildren<UIPanel>();
            mBoxColliders = gameObject.GetComponentsInChildren<Collider>();
            if (mPanels == null) return;
            LYXLogHelper.Error(mPanels.Length);
            for (int i = 0, len = mPanels.Length; i < len; i++)
            {
                mPanels[i].depth = depth + i;
            }
        }

        /// <summary>
        /// 关闭界面
        /// </summary>
        public override void OnClose()
        {
            Destroy(gameObject);
        }

        /// <summary>
        /// 获得焦点
        /// </summary>
        public override void OnFocus()
        {
            if (mBoxColliders == null) return;
            for (int i = 0, len = mBoxColliders.Length; i < len; i++)
            {
                mBoxColliders[i].enabled = true;
            }
        }

        /// <summary>
        /// 失去焦点
        /// </summary>
        public override void OnLostFocus()
        {
            if (mBoxColliders == null) return;
            for (int i = 0, len = mBoxColliders.Length; i < len; i++)
            {
                mBoxColliders[i].enabled = false;
            }
        }

        /// <summary>
        /// 显示界面
        /// </summary>
        public override void OnShow()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// 隐藏界面
        /// </summary>
        public override void OnHide()
        {
            gameObject.SetActive(false);
        }

    }
}
