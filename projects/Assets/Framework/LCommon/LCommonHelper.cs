using System;
using System.Diagnostics;
using UnityEngine;

/****
*
* 
* 公用 枚举和实体 
* 
* 
*/

namespace Game.LCommon
{

    #region  框架内 公用配置

    /// <summary>
    /// 公用配置
    /// </summary>
    public static class LCSConfig
    {

        /// <summary>
        /// 是否打印日志
        /// 
        /// IsDebugMode = true; 是debug模式  开启日志打印
        /// IsDebugMode = false; 不是debug模式  关闭日志打印
        /// </summary>
        public static bool IsDebugMode = true;

        /// <summary>
        /// 是否分析内存
        /// </summary>
        public static bool IsProfiler = true;

        /// <summary>
        /// 1M  大小
        /// </summary>
        public static float KbSize = 1024.0f * 1024.0f;

    }

    #endregion

    #region 框架内 公用枚举

    /// <summary>
    /// debug 日志输出的类型
    /// </summary>
    public enum LogType
    {
        Log = 0, // default
        Warning = 2,
        Error = 3,
    }

    #endregion

    #region 框架内 公用实体

    /***
     * 
     * 导入的二进制文件实体
     * 
     */

    public class LoadSourceEntity
    {

        /// <summary>
        /// 资源的id
        /// </summary>
        public string SourceId = string.Empty;

        /// <summary>
        ///  资源 AssetBundle
        /// </summary>
        public AssetBundle Bundle = null;

        /// <summary>
        /// 资源的名字
        /// </summary>
        public string ResName = string.Empty;

        /// <summary>
        /// 加载 资源 AssetBundle 的路径
        /// </summary>
        public string BundlePath = string.Empty;

        /// <summary>
        /// 加载的资源
        /// </summary>
        public GameObject LoadObj = null;

    }

    /// <summary>
    /// 观察程序运行时间实体
    /// </summary>
    public class RecordWatchEntity
    {
        /// <summary>
        /// 观察 key
        /// </summary>
        public string WatchKey = string.Empty;

        /// <summary>
        /// 开始时间
        /// </summary>
        public float StartTime = 0;

        /// <summary>
        /// 结束时间
        /// </summary>
        public float EndTime = 0;

        /// <summary>
        /// 时间差
        /// </summary>
        public float DiffTime
        {
            get
            {
                return EndTime - StartTime;
            }
        }

        /// <summary>
        /// 添加性能观察, 使用C#内置
        /// </summary>
        public Stopwatch Watch = null;

        /// <summary>
        /// 是否正在观察
        /// </summary>
        public bool IsWatch
        {
            set;
            get;
        }

        /// <summary>
        /// 开始观察
        /// </summary>
        public void BeginWatch()
        {
            IsWatch = true;
            Watch = new Stopwatch();
            Watch.Start();
        }

        /// <summary>
        /// 结束观察
        /// </summary>
        public void EndWatch()
        {
            IsWatch = false;
            if (Watch == null) return;
            Watch.Stop();
        }

        /// <summary>
        /// 得到观察时间
        /// 
        /// 单位 秒
        /// </summary>
        public double GetWatchTime()
        {
            if (Watch == null || Watch.IsRunning) return -1;
            //  获取当前实例测量得出的总时间
            TimeSpan timespan = Watch.Elapsed;
            // 转换成秒
            return timespan.TotalSeconds;
        }

    }

    /// <summary>
    /// 使用 gui 输出日志内容实体类
    /// </summary>
    public class GuiLogEntity
    {

        /// <summary>
        /// 日志内容
        /// </summary>
        public string LogString = string.Empty;

        /// <summary>
        /// 日志输出的颜色
        /// 
        /// 默认白色
        /// </summary>
        public Color LogColor = Color.white;

    }

    #endregion

}