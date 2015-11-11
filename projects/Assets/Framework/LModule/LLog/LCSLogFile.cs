﻿using System;
using System.Collections.Generic;
using System.IO;
using Game.LCommon;

namespace Game.LDebug
{

    /*****
     * 
     * 
     * 将日志输出到文件中
     * 
     * 
     */

    public class LCSLogFile : LCSLog
    {

        private LCSLogFile() { }

        /// <summary>
        /// 文件日志列表
        /// </summary>
        private static List<string> _fileLogs = new List<string>();

        /// <summary>
        /// 默认缓存日志条数
        /// </summary>
        private static int CacheCount = 20;

        /// <summary>
        /// 日志文件大小
        /// </summary>
        private static int FileLength = 1024 * 1024;

        /// <summary>
        /// 将日志保存到日志文件中
        /// </summary>
        private static void SaveToFile()
        {
            if (!IsDebugMode || _fileLogs == null) return;
            string savePath = LCSPathHelper.UnityLogPath();
            FileStream stream = File.Open(savePath, FileMode.OpenOrCreate);
            if (stream.Length > FileLength)
            {
                stream.Close();
                File.WriteAllText(savePath, "");
            }
            stream.Close();
            stream = null;

            StreamWriter write = File.AppendText(savePath);
            for (int i = 0, len = _fileLogs.Count; i < len; i++)
            {
                write.WriteLine(_fileLogs[i]);
            }
            _fileLogs.Clear();
            write.Flush();
            write.Close();
            write = null;
        }

        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="msg"></param>
        private static void WriteToFile(string msg)
        {
            if (!IsDebugMode) return;
            if (string.IsNullOrEmpty(msg)) return;
            if (_fileLogs.Count >= CacheCount) SaveToFile();
            _fileLogs.Add(msg);
        }

        /// <summary>
        /// 写日志 log 类型的
        /// </summary>
        /// <param name="msg">输出日志</param>
        public static new void Write(object msg)
        {
            if (!IsDebugMode) return;
            WriteToFile(string.Format("[File log]:{0}", msg));
        }

        /// <summary>
        /// 输出格式化数据
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static new void Write(string msg, params object[] args)
        {
            if (!IsDebugMode) return;
            WriteToFile(string.Format("[File log]:" + msg, args));
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
            WriteToFile(string.Format("[File log]:{0}", sb));
        }

        /// <summary>
        /// 输出错误日志
        /// </summary>
        /// <param name="msg"></param>
        public static new void WriteError(object msg)
        {
            if (!IsDebugMode) return;
            WriteToFile(string.Format("[File Error]:{0}", msg));
        }

        /// <summary>
        /// 输出错误日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static new void WriteError(string msg, params object[] args)
        {
            if (!IsDebugMode) return;
            WriteToFile(string.Format("[File Error]:" + msg, args));
        }

        /// <summary>
        /// 输出错误日志
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
            WriteToFile(string.Format("[File Error]:{0}", sb));
        }

        /// <summary>
        /// 输出警告日志
        /// </summary>
        /// <param name="msg"></param>
        public static new void WriteWarning(object msg)
        {
            if (!IsDebugMode) return;
            WriteToFile(string.Format("[File Warning]:{0}", msg));
        }

        /// <summary>
        /// 输出警告日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static new void WriteWarning(string msg, params object[] args)
        {
            if (!IsDebugMode) return;
            WriteToFile(string.Format("[File Warning]:" + msg, args));
        }

        /// <summary>
        /// 输出警告日志
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
            WriteToFile(string.Format("[File Warning]:{0}", sb));
        }

        /// <summary>
        /// 清理数据
        /// </summary>
        public static new void Clear()
        {
            SaveToFile();
            _fileLogs.Clear();
        }

    }

}

