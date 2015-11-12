﻿using System;
using System.Collections.Generic;
using Game.LCommon;
using Game.LDebug;
using UnityEngine;

/***
 * 
 * 
 * 资源管理
 * 
 * 
 */

namespace Game.LSource
{

    public static class LCSManageSource
    {

        /// <summary>
        /// 缓存资源数据
        /// </summary>
        private static Dictionary<string, LoadSourceEntity> CacheSource = new Dictionary<string, LoadSourceEntity>();

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
            if (CacheSource.ContainsKey(resName)) entity = CacheSource[resName];
            else entity = LoadEnitySource(resName, bundPath, type);
            if (entity == null) return null;
            if (!CacheSource.ContainsKey(resName))
            {
                entity.SourceId = Guid.NewGuid().ToString();
                CacheSource.Add(resName, entity);
            }
            return entity.LoadObj;
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
        public static LoadSourceEntity LoadEnitySource(string resName, string bundPath, Type type)
        {
            return LCSLoadSource.LoadBuildSources(resName, bundPath, type);
        }

        /// <summary>
        /// 移出单个资源
        /// </summary>
        /// <param name="resName">资源名字</param>
        /// <returns></returns>
        public static LoadSourceEntity RemoveSource(string resName)
        {
            if (string.IsNullOrEmpty(resName))
            {
                LCSConsole.WriteError("移出的资源名字为空！,resName = " + resName);
                return null;
            }
            if (!CacheSource.ContainsKey(resName))
            {
                LCSConsole.WriteError("移出的资源不存在！,resName = " + resName);
                return null;
            }
            LoadSourceEntity entity = CacheSource[resName];
            LCSUnloadSource.UnLoadSource(entity.Bundle);
            CacheSource.Remove(resName);
            return entity;
        }

        /// <summary>
        /// 移出所有资源
        /// </summary>
        /// <returns></returns>
        public static void RemoveAllSource()
        {
            foreach (LoadSourceEntity entity in CacheSource.Values)
                LCSUnloadSource.UnLoadSource(entity.Bundle);
            CacheSource.Clear();
        }

    }

}