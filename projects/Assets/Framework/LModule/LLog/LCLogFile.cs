using System;
using System.Collections.Generic;
using System.IO;
using Game.LBehaviour;
using Game.LCommon;

namespace Game.LDebug
{

    /****
     * 
     * 
     * 用文件保存日志文件
     * 
     */

    public class LCLogFile : LABehaviour, LILog
    {

        /// <summary>
        /// 保存文件日志实例
        /// </summary>
        private static LCLogFile _instance = null;

        public static LCLogFile Instance
        {
            get
            {
                if (_instance != null) return _instance;
                UnityEngine.GameObject create = LCSCompHelper.Create("_File Debug");
                return (_instance = LCSCompHelper.FindComponet<LCLogFile>(create));
            }
        }

        /// <summary>
        /// 写日志 log 类型的
        /// </summary>
        /// <param name="msg">输出日志</param>
        public void Write(object msg)
        {
            LCSLogFile.Write(msg);
        }

        /// <summary>
        /// 输出格式化数据
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public void Write(string msg, params object[] args)
        {
            LCSLogFile.Write(msg, args);
        }

        /// <summary>
        /// 输出格式化数据
        /// </summary>
        /// <param name="args"></param>
        public void Write(params object[] args)
        {
            LCSLogFile.Write(args);
        }

        public void WriteError(object msg)
        {
            LCSLogFile.WriteError(msg);
        }

        public void WriteError(string msg, params object[] args)
        {
            LCSLogFile.WriteError(msg, args);
        }

        public void WriteError(params object[] args)
        {
            LCSLogFile.WriteError(args);
        }

        public void WriteWarning(object msg)
        {
            LCSLogFile.WriteWarning(msg);
        }

        public void WriteWarning(string msg, params object[] args)
        {
            LCSLogFile.WriteWarning(msg, args);
        }

        public void WriteWarning(params object[] args)
        {
            LCSLogFile.WriteWarning(args);
        }

        /// <summary>
        /// 清理数据
        /// </summary>
        public override void OnClear()
        {
            _instance = null;
            LCSLogFile.Clear();
        }

        /// <summary>
        /// 程序退出时
        /// 
        /// 将缓存的日志保存到文件中
        /// </summary>
        public override void OnApplicationQuit()
        {
            OnClear();
        }

    }

}