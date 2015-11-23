using System;
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
        /// 获得资源实体类
        /// 
        /// 区分 Android， iphone， untiy 
        /// 
        /// 默认 unity
        /// </summary>
        /// <param name="resName">资源名字</param>
        /// <param name="bundPath">加载AssetBundle完整路径 </param>
        /// <param name="type">资源类型</param>
        /// <returns></returns>
        private static LoadSourceEntity LoadEnitySource(string resName, string bundPath, Type type)
        {
            return LCSLoadSource.LoadBuildSources(resName, bundPath, type);
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
            entity.Callback(entity.LoadObj);
        }

        /// <summary>
        /// 移出单个资源
        /// </summary>
        /// <param name="resName">资源名字</param>
        /// <returns></returns>
        private static LoadSourceEntity RemoveSource(string resName)
        {
            if (string.IsNullOrEmpty(resName))
            {
                LCSConsole.WriteError("移出的资源名字为空！,resName = " + resName);
                return null;
            }
            LoadSourceEntity entity;
            if (!TryFind<LCSManageSource>(resName, out entity))
            {
                LCSConsole.WriteError("移出的资源不存在！,resName = " + resName);
                return null;
            }
            LCSUnloadSource.UnLoadSource(entity.Bundle);
            Remove<LCSManageSource>(resName);
            return entity;
        }

        /// <summary>
        /// 加载资源
        /// </summary>
        /// <param name="resName">资源名字，不带后缀, 资源名字唯一</param>
        /// <param name="bundPath">前置路径</param>
        /// <param name="type">资源类型</param>
        /// <returns></returns>
        public static GameObject LoadSource(string resName, string bundPath, Type type)
        {
            LoadSourceEntity entity = null;
            if (!TryFind<LCSManageSource>(resName, out entity)) return entity.LoadObj;
            entity = LoadEnitySource(resName, bundPath, type);
            if (entity == null) return null;
            Add<LCSManageSource>(resName, entity);
            return entity.LoadObj;
        }

        /// <summary>
        /// 异步导入数据
        /// </summary>
        /// <param name="resName"></param>
        /// <param name="bundPath"></param>
        /// <param name="type"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public static void AsyncLoadSource(string resName, string bundPath, Type type, Action<GameObject> callback)
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