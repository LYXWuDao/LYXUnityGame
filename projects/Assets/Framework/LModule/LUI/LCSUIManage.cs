﻿using System.Collections.Generic;
using Game.LCommon;
using Game.LDebug;
using Game.LSource;
using Game.UI;
using UnityEngine;

namespace Game.LUI
{
    /****
     * 
     * 
     *   ui 资源管理
     *  
     *   界面操作入口
     *  
     */
    public static class LCSUIManage
    {

        /// <summary>
        ///  缓存所有的界面数据
        /// </summary>
        private static Dictionary<string, LCUIPage> _uiManage = new Dictionary<string, LCUIPage>();

        /// <summary>
        ///  界面名字列表
        /// </summary>
        private static List<string> _pageNameList = new List<string>();

        /// <summary>
        /// ui 界面的根节点
        /// </summary>
        private static Transform _uiRoot;

        /// <summary>
        /// 界面的深度
        /// </summary>
        private static int PageDepth = 1;

        /// <summary>
        /// 界面深度的跨度
        /// </summary>
        private static int DepthSpan = 30;

        /// <summary>
        /// 增加界面名字
        /// </summary>
        /// <param name="pageName"></param>
        private static void AddPageName(string pageName)
        {
            if (string.IsNullOrEmpty(pageName)) return;
            if (_pageNameList.Contains(pageName)) return;
            _pageNameList.Add(pageName);
        }

        /// <summary>
        /// 移出界面名字
        /// </summary>
        private static void RemovePageName(string pageName)
        {
            if (string.IsNullOrEmpty(pageName)) return;
            if (_pageNameList.Contains(pageName)) return;
            _pageNameList.Remove(pageName);
        }

        /// <summary>
        ///  创建界面
        /// </summary>
        /// <param name="bundlePath">加载资源路径</param>
        /// <param name="pageName">打开界面的名字</param>
        private static LCUIPage CreatePage(string pageName, string bundlePath)
        {
            if (string.IsNullOrEmpty(pageName))
            {
                LCSConsole.WriteError("打开的界面名字为空! pageName = " + pageName);
                return null;
            }
            if (string.IsNullOrEmpty(bundlePath))
            {
                LCSConsole.WriteError("加载资源 AssetBundle 文件路径为空! bundlePath = " + bundlePath);
                return null;
            }
            GameObject ui = LCSManageSource.LoadSource(pageName, bundlePath, typeof(GameObject));
            if (ui == null)
            {
                LCSConsole.WriteError("加载的资源不存在!");
                return null;
            }
            GameObject go = GameObject.Instantiate(ui) as GameObject;
            if (go == null) return null;
            LCSCompHelper.InitTransform(go, UIRoot);
            return LCSCompHelper.FindComponet<LCUIPage>(go);
        }

        /// <summary>
        /// 界面根节点
        /// </summary>
        public static Transform UIRoot
        {
            get
            {
                if (_uiRoot == null) _uiRoot = UIMainCamera.gameObject.transform;
                return _uiRoot;
            }
        }

        /// <summary>
        /// 得到 2d 主摄像机
        /// </summary>
        public static UICamera UIMainCamera
        {
            get
            {
                return GameObject.FindObjectOfType<UICamera>();
            }
        }

        /// <summary>
        /// 界面是否打开
        /// </summary>
        /// <param name="pageName">界面的名字</param>
        /// <returns></returns>
        public static bool HasPage(string pageName)
        {
            if (string.IsNullOrEmpty(pageName)) return false;
            return _uiManage.ContainsKey(pageName);
        }

        /// <summary>
        /// 得到最顶层的页面
        /// </summary>
        public static LCUIPage GetTopLevelPage()
        {
            if (_pageNameList == null) return null;
            int len = _pageNameList.Count;
            if (len <= 0) return null;
            return GetPageInfo(_pageNameList[len - 1]);
        }

        /// <summary>
        /// 得到页面的信息
        /// </summary>
        public static LCUIPage GetPageInfo(string pageName)
        {
            if (string.IsNullOrEmpty(pageName)) return null;
            return HasPage(pageName) ? _uiManage[pageName] : null;
        }

        /// <summary>
        /// 打开ui界面
        /// <param name="pageName"> 界面的名字 </param>
        /// </summary>
        public static LCUIPage OpenPage(string pageName, string bundlePath)
        {
            if (HasPage(pageName)) return GetPageInfo(pageName);
            LCUIPage topPage = GetTopLevelPage();
            if (topPage != null) topPage.OnLostFocus();
            LCUIPage page = CreatePage(pageName, bundlePath);
            page.OnOpen(PageDepth);
            PageDepth += DepthSpan;
            _uiManage.Add(pageName, page);
            AddPageName(pageName);
            return page;
        }

        /// <summary>
        /// 关闭 ui
        /// </summary>
        /// <param name="pageName">界面名字</param>
        public static bool ClosePage(string pageName)
        {
            if (string.IsNullOrEmpty(pageName) || !HasPage(pageName)) return false;
            LCUIPage page = GetPageInfo(pageName);
            page.OnClose();
            LCSManageSource.RemoveSource(pageName);
            PageDepth -= DepthSpan;
            LCUIPage topPage = GetTopLevelPage();
            if (topPage != null) topPage.OnFocus();
            RemovePageName(pageName);
            return _uiManage.Remove(pageName);
        }

    }
}
