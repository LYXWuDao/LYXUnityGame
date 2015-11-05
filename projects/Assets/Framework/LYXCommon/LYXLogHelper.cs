/*****
 * 
 * 
 * 日志帮助类
 * 
 * 
 */
using UnityEngine;

namespace Game.LYX.Common
{

    public class LYXLogHelper
    {

        /// <summary>
        /// 是否打印日志
        /// 
        /// IsDebugMode = true; 是debug模式  开启日志打印
        /// IsDebugMode = false; 不是debug模式  关闭日志打印
        /// </summary>
        public static bool IsDebugMode = true;

        /// <summary>
        /// 输出日志
        /// </summary>
        /// <param name="msg"></param>
        public static void Log(object msg)
        {
            if (!IsDebugMode) return;
            Debug.Log(msg);
        }

        /// <summary>
        /// 输出错误
        /// </summary>
        /// <param name="msg"></param>
        public static void Error(object msg)
        {
            if (!IsDebugMode) return;
            Debug.LogError(msg);
        }

        /// <summary>
        /// 输入警告
        /// </summary>
        /// <param name="msg"></param>
        public static void Warning(object msg)
        {
            if (!IsDebugMode) return;
            Debug.LogWarning(msg);
        }

    }
}

