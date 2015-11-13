using System.Collections.Generic;
using LGame.LBase;
using LGame.LCommon;
using LGame.LDebug;
using LGame.LSource;
using UnityEngine;

namespace LGame.LUI
{
    /****
     * 
     * 
     *   ui 资源管理
     *  
     *   界面操作入口
     *  
     */
    public sealed class LCSUIManage
    {
        /// <summary>
        /// 私有话 
        /// 
        /// 不允许实例化
        /// </summary>
        private LCSUIManage() { }

        /// <summary>
        /// 所有打开界面的名字
        /// </summary>
        private static List<string> _winNames = new List<string>();

        /// <summary>
        /// ui 界面的根节点
        /// </summary>
        private static Transform _uiRoot;

        /// <summary>
        ///  创建界面
        /// </summary>
        /// <param name="winPath">加载资源路径</param>
        /// <param name="winName">打开界面的名字</param>
        private static LAUIBehaviour CreatePage(string winName, string winPath)
        {
            if (string.IsNullOrEmpty(winName))
            {
                LCSConsole.WriteError("打开的界面名字为空! pageName = " + winName);
                return null;
            }
            if (string.IsNullOrEmpty(winPath))
            {
                LCSConsole.WriteError("加载资源 AssetBundle 文件路径为空! bundlePath = " + winPath);
                return null;
            }
            GameObject ui = LCSManageSource.LoadSource(winName, winPath, typeof(GameObject));
            if (ui == null)
            {
                LCSConsole.WriteError("加载的资源不存在!");
                return null;
            }
            GameObject go = GameObject.Instantiate(ui) as GameObject;
            if (go == null) return null;
            LCSCompHelper.InitTransform(go, UIRoot);
            return LCSCompHelper.GetComponent<LAUIBehaviour>(go);
        }

        /// <summary>
        ///  尝试创建创建界面
        /// </summary>
        /// <param name="winPath">加载资源路径</param>
        /// <param name="winName">打开界面的名字</param>
        /// <param name="win"></param>
        private static bool TryCreatePage(string winName, string winPath, out LAUIBehaviour win)
        {
            win = CreatePage(winName, winPath);
            return win != null;
        }

        /// <summary>
        /// 增加打开的界面
        /// </summary>
        /// <param name="winName"></param>
        /// <param name="win"></param>
        private static void Add(string winName, LAUIBehaviour win)
        {
            LCSBaseManage.Add(typeof(LCSUIManage), winName, win);
            _winNames.Add(winName);
        }

        /// <summary>
        /// 找到当前界面数据
        /// </summary>
        /// <param name="winName">界面的名字</param>
        /// <returns></returns>
        private static LAUIBehaviour Find(string winName)
        {
            return (LAUIBehaviour)LCSBaseManage.Find(typeof(LCSUIManage), winName);
        }

        /// <summary>
        /// 找到当前界面数据
        /// </summary>
        /// <param name="winName">界面的名字</param>
        /// <param name="win">界面</param>
        /// <returns>是否成功</returns>
        private static bool TryFind(string winName, out LAUIBehaviour win)
        {
            return (win = Find(winName)) != null;
        }

        /// <summary>
        /// 查找当前所有的界面
        /// </summary>
        /// <returns></returns>
        private static LAUIBehaviour[] FindAll()
        {
            return LCSBaseManage.Find<LAUIBehaviour>(typeof(LCSUIManage));
        }

        /// <summary>
        /// 移出该类型数据
        /// </summary>
        private static void Remove()
        {
            LCSBaseManage.Remove(typeof(LCSUIManage));
            _winNames.Clear();
        }

        /// <summary>
        /// 移出
        /// </summary>
        /// <param name="winName"></param>
        private static void Remove(string winName)
        {
            LCSBaseManage.Remove(typeof(LCSUIManage), winName);
            _winNames.Remove(winName);
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
        /// 得到当前最高的 ui 界面
        /// </summary>
        /// <returns></returns>
        public static LAUIBehaviour TopWindow()
        {
            if (_winNames.Count <= 0) return null;
            string winName = _winNames[_winNames.Count - 1];
            return Find(winName);
        }

        /// <summary>
        /// 尝试得到最高的界面
        /// </summary>
        /// <param name="topWin"></param>
        /// <returns></returns>
        public static bool TryTopWindow(out LAUIBehaviour topWin)
        {
            return (topWin = TopWindow()) != null;
        }

        /// <summary>
        /// 同步打开界面
        /// </summary>
        /// <param name="winName">界面的名字，唯一</param>
        /// <param name="winPath">界面加载路径</param>
        public static void OpenWindow(string winName, string winPath)
        {
            LAUIBehaviour win = null;
            if (TryFind(winName, out win)) return;

            int depth = 1;
            // 当前最高的界面失去焦点
            LAUIBehaviour topWin = TopWindow();
            if (topWin != null)
            {
                depth = topWin.WinDepth + LCSConfig.DepthSpan;
                topWin.OnLostFocus();
            }

            if (!TryCreatePage(winName, winPath, out win))
            {
                LCSConsole.WriteWarning("创建 ui 界面 LAUIBehaviour 失败!");
                return;
            }

            // 初始化当前界面
            win.OnOpen(depth, winName);
            Add(winName, win);
        }

        /// <summary>
        /// 异步打开界面
        /// </summary>
        /// <param name="winName"></param>
        /// <param name="winPath"></param>
        public static void AsyncOpenWindow(string winName, string winPath)
        {
            LAUIBehaviour win = null;
            if (TryFind(winName, out win)) return;
        }

        /// <summary>
        /// 异步打开界面回调
        /// </summary>
        public static void AsyncOpenWindowCallback()
        {

        }

        /// <summary>
        /// 关闭最上层的界面
        /// </summary>
        public static void CloseWindow()
        {
            LAUIBehaviour win = null;
            if (!TryTopWindow(out win)) return;
            win.Destroy();
            Remove(win.WinName);
        }

        /// <summary>
        /// 关闭界面
        /// </summary>
        public static void CloseWindow(string winName)
        {
            LAUIBehaviour win = null;
            if (!TryFind(winName, out win)) return;
            win.Destroy();
            Remove(win.WinName);
            LAUIBehaviour topWin = null;
            if (!TryTopWindow(out topWin)) return;
            topWin.OnFocus();
        }

        /// <summary>
        /// 关闭所有的界面
        /// </summary>
        public static void CloseAllWindow()
        {
            LAUIBehaviour[] win = FindAll();
            if (win == null) return;
            for (int i = 0, len = win.Length; i < len; i++)
                win[i].Destroy();
            Remove();
        }

    }
}
