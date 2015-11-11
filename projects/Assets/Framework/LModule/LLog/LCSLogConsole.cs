﻿using Game.LCommon;

namespace Game.LDebug
{

    /***
     * 
     * 
     * 控制台静态方法输出
     * 
     */

    public class LCSLogConsole : LCSLog
    {
        private LCSLogConsole() { }

        /// <summary>
        /// 输出 debug 日志
        /// </summary>
        /// <param name="msg">输入内容</param>
        /// <param name="logType">输出的类型</param>
        private static void WriteDebug(object msg, LogType logType)
        {
            if (!IsDebugMode) return;
            switch (logType)
            {
                case LogType.Log:
                    UnityEngine.Debug.Log(msg);
                    break;
                case LogType.Warning:
                    UnityEngine.Debug.LogWarning(msg);
                    break;
                case LogType.Error:
                    UnityEngine.Debug.LogError(msg);
                    break;
            }
        }

        /// <summary>
        /// 写日志 log 类型的
        /// </summary>
        /// <param name="msg">输出日志</param>
        public static new void Write(object msg)
        {
            WriteDebug(msg, LogType.Log);
        }

        /// <summary>
        /// 输出格式化数据
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static new void Write(string msg, params object[] args)
        {
            WriteDebug(string.Format(msg, args), LogType.Log);
        }

        /// <summary>
        /// 输出格式化数据
        /// </summary>
        /// <param name="args"></param>
        public static new void Write(params object[] args)
        {
            if (!IsDebugMode) return;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < args.Length; ++i)
            {
                sb.Append(args[i]);
                sb.Append(", ");
            }
            WriteDebug(sb.ToString(), LogType.Log);
        }

        /// <summary>
        /// 输出错误
        /// </summary>
        /// <param name="msg"></param>
        public static new void WriteError(object msg)
        {
            WriteDebug(msg, LogType.Error);
        }

        /// <summary>
        /// 输出错误
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static new void WriteError(string msg, params object[] args)
        {
            WriteDebug(string.Format(msg, args), LogType.Error);
        }

        /// <summary>
        /// 输出错误
        /// </summary>
        /// <param name="args"></param>
        public static new void WriteError(params object[] args)
        {
            if (!IsDebugMode) return;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < args.Length; ++i)
            {
                sb.Append(args[i]);
                sb.Append(", ");
            }
            WriteDebug(sb.ToString(), LogType.Error);
        }

        /// <summary>
        /// 输出警告
        /// </summary>
        /// <param name="msg"></param>
        public static new void WriteWarning(object msg)
        {
            WriteDebug(msg, LogType.Warning);
        }

        /// <summary>
        /// 输出警告
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static new void WriteWarning(string msg, params object[] args)
        {
            WriteDebug(string.Format(msg, args), LogType.Warning);
        }

        /// <summary>
        /// 输出警告
        /// </summary>
        /// <param name="args"></param>
        public static new void WriteWarning(params object[] args)
        {
            if (!IsDebugMode) return;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < args.Length; ++i)
            {
                sb.Append(args[i]);
                sb.Append(", ");
            }
            WriteDebug(sb.ToString(), LogType.Warning);
        }

    }

}

