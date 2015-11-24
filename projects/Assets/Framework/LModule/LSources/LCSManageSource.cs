﻿using System;
using System.Collections.Generic;
using LGame.LBase;
using LGame.LCommon;
using LGame.LDebug;
using UnityEngine;

namespace LGame.LSource
{

    /***
     * 
     * 
     * 所有资源加载卸载管理类型
     * 
     * 包括图片，界面，模型，声音等
     * 
     * 
     */

    public sealed class LCSManageSource : LATManager<LoadSourceEntity>
    {

        /// <summary>
        /// 
        /// 私有话 
        /// 
        /// 不允许实例化
        /// 
        /// </summary>
        private LCSManageSource()
        {
        }

        /// <summary>
        /// 异步加载资源回调
        /// </summary>
        /// <param name="entity"></param>
        private static void AsyncLoadCallback(LoadSourceEntity entity)
        {
            if (entity == null)
            {
                LCSConsole.WriteError("资源加载回调数据为空！ entity = null");
                return;
            }
            if (entity.Callback == null) return;
            entity.Callback(entity.ResName, entity.LoadObj);
            Add<LCSManageSource>(entity.ResName, entity);
        }

        /// <summary>
        /// 同步加载资源
        /// </summary>
        /// <param name="resName">资源名字，不带后缀, 资源名字唯一</param>
        /// <param name="bundPath">资源完成路径(打包后的路径)</param>
        /// <returns></returns>
        public static GameObject LoadSource(string resName, string bundPath)
        {
            return LoadSource(resName, bundPath, null);
        }

        /// <summary>
        /// 同步加载资源
        /// </summary>
        /// <param name="resName">资源名字，不带后缀, 资源名字唯一</param>
        /// <param name="bundPath">资源完成路径(打包后的路径)</param>
        /// <param name="type">资源类型</param>
        /// <returns></returns>
        public static GameObject LoadSource(string resName, string bundPath, Type type)
        {
            LoadSourceEntity entity = null;
            if (!TryFind<LCSManageSource>(resName, out entity)) return entity.LoadObj;
            entity = LCSLoadSource.LoadSource(resName, bundPath, type);
            if (entity == null) return null;
            Add<LCSManageSource>(resName, entity);
            return entity.LoadObj;
        }

        /// <summary>
        /// 异步加载资源
        /// </summary>
        /// <param name="resName">资源名字，不带后缀, 资源名字唯一</param>
        /// <param name="bundPath">资源完成路径(打包后的路径)</param>
        /// <param name="callback"></param>
        public static void AsyncLoadSource(string resName, string bundPath, Action<string, GameObject> callback)
        {
            AsyncLoadSource(resName, bundPath, null, callback);
        }

        /// <summary>
        /// 异步导入数据
        /// </summary>
        /// <param name="resName">资源名字，不带后缀, 资源名字唯一</param>
        /// <param name="bundPath">资源完成路径(打包后的路径)</param>
        /// <param name="type">资源加载类型</param>
        /// <param name="callback">加载完成回调</param>
        /// <returns></returns>
        public static void AsyncLoadSource(string resName, string bundPath, Type type, Action<string, GameObject> callback)
        {
            LoadSourceEntity entity = new LoadSourceEntity()
            {
                ResName = resName,
                BundlePath = bundPath,
                BundleType = type,
                Callback = callback,
            };
            LCAsyncLoadSource.Instance.LoadSource(entity, AsyncLoadCallback);
        }

        /// <summary>
        /// 移出单个资源
        /// </summary>
        /// <param name="resName">资源名字</param>
        /// <returns></returns>
        public static bool RemoveSource(string resName)
        {
            if (string.IsNullOrEmpty(resName))
            {
                LCSConsole.WriteError("移出的资源名字为空！,resName = " + resName);
                return false;
            }
            LoadSourceEntity entity;
            if (!TryFind<LCSManageSource>(resName, out entity))
            {
                LCSConsole.WriteError("移出的资源不存在！,resName = " + resName);
                return false;
            }
            LCSUnloadSource.UnLoadSource(entity.Bundle);
            Remove<LCSManageSource>(resName);
            return true;
        }

        /// <summary>
        /// 移出所有资源
        /// </summary>
        /// <returns></returns>
        public static void RemoveAllSource()
        {
            foreach (LoadSourceEntity entity in FindValues<LCSManageSource>())
                LCSUnloadSource.UnLoadSource(entity.Bundle);
            Remove<LCSManageSource>();
        }

    }

}