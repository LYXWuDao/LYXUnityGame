﻿using System;
using System.Collections;
using System.IO;
using LGame.LCommon;
using LGame.LDebug;
using UnityEngine;

/****
 * 
 * 
 *  导入资源
 * 
 * 
 */

namespace LGame.LSource
{

    public static class LCSLoadSource
    {

        /// <summary>
        /// 导入资源
        /// 
        /// 直接加载  Resources 文件夹下资源
        /// </summary>
        /// <param name="name">
        /// 
        /// 资源名字
        /// 
        /// Resources夹下路径
        /// 例如：Scenes/battleScene.scene
        /// </param>
        /// <returns>加载的资源</returns>
        private static GameObject LoadResources(string name)
        {
            return LoadResources(name, typeof(GameObject));
        }

        /// <summary>
        /// 导入资源
        /// 
        /// 直接加载  Resources 文件夹下资源
        /// </summary>
        /// <param name="name">
        /// 
        /// 资源名字
        /// 
        /// Resources夹下路径
        /// 例如：Scenes/battleScene.scene
        /// </param>
        /// <param name="type">资源类型</param>
        /// <returns>
        /// 加载的资源
        /// 
        /// return null : 资源名字为空，或者资源不存在， 或者资源类型不对
        /// 
        /// </returns>
        private static GameObject LoadResources(string name, Type type)
        {
            if (string.IsNullOrEmpty(name))
            {
                LCSConsole.WriteError("导入资源名字为空,name = " + name);
                return null;
            }
            UnityEngine.Object load = Resources.Load(name, type);
            if (load == null)
            {
                LCSConsole.WriteError("导入资源不存在！！");
                return null;
            }
            return load as GameObject;
        }

        /// <summary>
        /// 加载 Assets Streaming 文件夹下资源
        /// </summary>
        /// <param name="resName">加载的资源名字</param>
        /// <returns></returns>
        private static LoadSourceEntity LoadStreamingSources(string resName)
        {
            return LoadStreamingSources(resName, "");
        }

        /// <summary>
        /// 加载 Assets Streaming 文件夹下资源
        /// </summary>
        /// <param name="resName">加载的资源名字</param>
        /// <param name="bundPath">导入 AssetBundle 路径</param>
        /// <returns></returns>
        private static LoadSourceEntity LoadStreamingSources(string resName, string bundPath)
        {
            return LoadStreamingSources(resName, bundPath, null);
        }

        /// <summary>
        /// 加载 Assets Streaming 文件夹下资源
        /// </summary>
        /// <param name="resName">加载的资源名字</param>
        /// <param name="type">资源的类型</param>
        /// <returns></returns>
        private static LoadSourceEntity LoadStreamingSources(string resName, Type type)
        {
            return LoadStreamingSources(resName, "", type);
        }

        /// <summary>
        /// 加载 Assets Streaming 文件夹下资源
        /// </summary>
        /// <param name="resName">加载的资源名字</param>
        /// <param name="bundPath">Streaming 下文件夹  如： UI</param>
        /// <param name="type">资源的类型</param>
        /// <returns></returns>
        private static LoadSourceEntity LoadStreamingSources(string resName, string bundPath, Type type)
        {
            if (type == null) type = typeof(GameObject);
            if (string.IsNullOrEmpty(bundPath)) bundPath = resName;
            string path = LCSPathHelper.UnityStreamingSourcePath() + bundPath;
            return LoadBinarySources(path, resName, type);
        }

        /// <summary>
        /// 加载 Assets SourceAssets 文件夹下资源
        /// </summary>
        /// <param name="resName">加载的资源名字</param>
        /// <returns></returns>
        private static LoadSourceEntity LoadBuildSources(string resName)
        {
            return LoadBuildSources(resName, "");
        }

        /// <summary>
        /// 加载 Assets SourceAssets 文件夹下资源
        /// </summary>
        /// <param name="resName">加载的资源名字</param>
        /// <param name="bundPath">导入 AssetBundle 路径</param>
        /// <returns></returns>
        private static LoadSourceEntity LoadBuildSources(string resName, string bundPath)
        {
            return LoadBuildSources(resName, bundPath, null);
        }

        /// <summary>
        /// 加载 Assets SourceAssets 文件夹下资源
        /// </summary>
        /// <param name="resName">加载的资源名字</param>
        /// <param name="type">资源的类型</param>
        /// <returns></returns>
        private static LoadSourceEntity LoadBuildSources(string resName, Type type)
        {
            return LoadBuildSources(resName, "", type);
        }

        /// <summary>
        /// 加载 Assets SourceAssets 文件夹下资源
        /// </summary>
        /// <param name="resName">加载的资源名字</param>
        /// <param name="bundPath">导入 AssetBundle 路径</param>
        /// <param name="type">资源的类型</param>
        /// <returns></returns>
        private static LoadSourceEntity LoadBuildSources(string resName, string bundPath, Type type)
        {
            if (type == null) type = typeof(GameObject);
            if (string.IsNullOrEmpty(bundPath)) bundPath = resName;
            string path = LCSPathHelper.UnityBuildRootPath() + bundPath;
            return LoadBinarySources(path, resName, type);
        }

        /// <summary>
        /// 导入资源
        /// 
        /// 以二进制加载
        /// </summary>
        /// <param name="bundPath">导入 AssetBundle 路径 </param>
        /// <param name="resName"> 从 AssetBundle 中导入的资源名  </param>
        /// <returns></returns>
        private static LoadSourceEntity LoadBinarySources(string bundPath, string resName)
        {
            return LoadBinarySources(bundPath, resName, typeof(GameObject));
        }

        /// <summary>
        /// 导入资源
        /// 
        /// 以二进制加载
        /// </summary>
        /// <param name="bundPath">导入 AssetBundle 路径 </param>
        /// <param name="resName"> 从 AssetBundle 中导入的资源名  </param>
        /// <param name="type">加载资源的类型</param>
        /// <returns></returns>
        private static LoadSourceEntity LoadBinarySources(string bundPath, string resName, Type type)
        {
            if (string.IsNullOrEmpty(resName))
            {
                LCSConsole.WriteError("导入资源名字为空,resName = " + resName);
                return null;
            }

            if (string.IsNullOrEmpty(bundPath))
            {
                LCSConsole.WriteError("导入 AssetBundle 路径为空, bundPath = " + bundPath);
                return null;
            }

            if (!File.Exists(bundPath))
            {
                LCSConsole.WriteError("导入 AssetBundle 路径不存在,bundPath = " + bundPath);
                return null;
            }

            byte[] bytes = File.ReadAllBytes(bundPath);
            AssetBundle bundle = AssetBundle.CreateFromMemoryImmediate(bytes);

            if (bundle == null)
            {
                LCSConsole.WriteError("创建资源 AssetBundle 失败!");
                return null;
            }

            UnityEngine.Object retobj = bundle.Load(resName, type);
            if (retobj == null)
            {
                LCSConsole.WriteError("资源 AssetBundle 中不存在 resName = " + resName);
                return null;
            }

            return new LoadSourceEntity
            {
                LoadObj = retobj as GameObject,
                Bundle = bundle,
                BundlePath = bundPath,
                ResName = resName
            };
        }

        /// <summary>
        /// 直接从 AssetBundle 中加载资源
        /// </summary>
        /// <param name="bundle">AssetBundle</param>
        /// <param name="resName">加载的资源名字</param>
        /// <param name="type">资源类型</param>
        /// <returns></returns>
        private static GameObject LoadBundleSources(AssetBundle bundle, string resName, Type type)
        {
            if (bundle == null)
            {
                LCSConsole.WriteError("资源 AssetBundle 不存在");
                return null;
            }
            if (string.IsNullOrEmpty(resName))
            {
                LCSConsole.WriteError("导入资源名字为空,resName = " + resName);
                return null;
            }
            if (type == null) type = typeof(GameObject);
            UnityEngine.Object retobj = bundle.Load(resName, type);
            if (retobj == null)
            {
                LCSConsole.WriteError("资源 AssetBundle 中不存在 resName = " + resName);
                return null;
            }
            return retobj as GameObject;
        }

        /// <summary>
        /// 同步加载资源
        /// 
        /// 区分 Android， iphone， untiy 
        /// 
        /// 默认 unity
        /// 
        /// </summary>
        /// <param name="resName">资源名字</param>
        /// <param name="bundPath">资源完整路径</param>
        /// <param name="type">加载资源的类型</param>
        /// <returns></returns>
        public static LoadSourceEntity LoadSource(string resName, string bundPath, Type type)
        {
            return LoadBuildSources(resName, bundPath, type);
        }

    }

}