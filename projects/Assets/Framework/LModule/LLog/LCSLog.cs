using System;
using System.Collections.Generic;

namespace Game.LDebug
{

    /***
     * 
     *  debug  输出日志静态接口类
     * 
     */

    public class LCSLog
    {

        /// <summary>
        /// 是否打印日志
        /// 
        /// IsDebugMode = true; 是debug模式  开启日志打印
        /// IsDebugMode = false; 不是debug模式  关闭日志打印
        /// </summary>
        public static bool IsDebugMode = true;

        /// <summary>
        /// 写日志 log 类型的
        /// </summary>
        /// <param name="msg">输出日志</param>
        public static void Write(object msg)
        {
        }

        /// <summary>
        /// 输出格式化数据
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static void Write(string msg, params object[] args)
        {
        }

        /// <summary>
        /// 输出格式化数据
        /// </summary>
        /// <param name="args"></param>
        public static void Write(params object[] args)
        {
        }

        /// <summary>
        /// 输出错误
        /// </summary>
        /// <param name="msg"></param>
        public static void WriteError(object msg)
        {
        }

        /// <summary>
        /// 输出错误
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static void WriteError(string msg, params object[] args)
        {
        }

        /// <summary>
        /// 输出错误
        /// </summary>
        /// <param name="args"></param>
        public static void WriteError(params object[] args)
        {
        }

        /// <summary>
        /// 输出警告
        /// </summary>
        /// <param name="msg"></param>
        public static void WriteWarning(object msg)
        {
        }

        /// <summary>
        /// 输出警告
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static void WriteWarning(string msg, params object[] args)
        {
        }

        /// <summary>
        /// 输出警告
        /// </summary>
        /// <param name="args"></param>
        public static void WriteWarning(params object[] args)
        {
        }

        /// <summary>
        /// 清理数据
        /// </summary>
        public static void Clear() { }

    }

}
