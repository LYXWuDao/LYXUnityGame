﻿using System.IO;
using UnityEngine;

/*****
 * 
 * 
 *  框架所有路径配置
 *  
 * 
 */

namespace LGame.LCommon
{

    public static class LCSPathHelper
    {

        /// <summary>
        /// untiy  aeests 文件目录
        /// </summary>
        /// <returns></returns>
        public static string UnityAssets()
        {
            return Application.dataPath + "/";
        }

        /// <summary>
        /// untiy 内部资源 Resources 路径
        /// </summary>
        /// <returns></returns>
        public static string UntiyResource()
        {
            return Application.dataPath + "Resources/";
        }

        /// <summary>
        /// 
        /// 缺省的内部资源路径
        /// 
        /// 例: Assets/Resources
        /// 
        /// </summary>
        /// <returns></returns>
        public static string UnityDefaultResource()
        {
            return "Assets/Resources/";
        }

        /// <summary>
        /// untiy  streamingAssets 文件目录
        /// </summary>
        /// <returns></returns>
        public static string UnityStreamingAssets()
        {
            return Application.streamingAssetsPath + "/";
        }

        /// <summary>
        /// 
        /// 资源打包存放根目录
        /// 
        /// untiy assets 目录下 SourceAssets 文件夹
        /// </summary>
        /// <returns></returns>
        public static string UnityBuildRootPath()
        {
            return UnityAssets() + "SourceAssets/";
        }

        /// <summary>
        /// 
        /// 资源打包存放根目录
        /// 
        /// untiy assets 目录下 SourceAssets/UI 文件夹
        /// </summary>
        /// <returns></returns>
        public static string UnityBuildUiPath()
        {
            return UnityBuildRootPath() + "UI/";
        }

        /// <summary>
        /// 游戏打包时,更新包资源根目录的位置
        /// 
        /// untiy  StreamingAssets/Assets 路径 
        /// </summary>
        /// <returns></returns>
        public static string UnityStreamingSourcePath()
        {
            return UnityStreamingAssets() + "Assets/";
        }

        /// <summary>
        /// 日志文件保存路径
        /// </summary>
        /// <returns></returns>
        public static string UnityLogFilePath()
        {
            return UnityAssets() + "log.txt";
        }

    }
}

