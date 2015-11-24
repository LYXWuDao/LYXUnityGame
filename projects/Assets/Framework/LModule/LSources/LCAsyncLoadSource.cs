using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LGame.LBehaviour;
using LGame.LCommon;
using LGame.LDebug;
using UnityEngine;

namespace LGame.LSource
{

    /****
     * 
     * 
     * 异步加载资源  
     * 
     * 使用文件流，二进制方式进行加载
     * 
     * 例如： 模型，界面，图片，声音，特效 等.
     * 
     * 使用单例模式
     * 
     */

    public sealed class LCAsyncLoadSource
    {

        /// <summary>
        /// 构造函数私有化
        /// 
        /// 单例模式需要
        /// </summary>
        private LCAsyncLoadSource()
        {

        }

        private static LCAsyncLoadSource _instance;

        /// <summary>
        /// 用于 lock 
        /// </summary>
        private static object _lock = new object();

        /// <summary>
        /// 单例模式实例话
        /// </summary>
        public static LCAsyncLoadSource Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new LCAsyncLoadSource();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// 开始异步加载
        /// </summary>
        /// <param name="bundleRequest">异步AssetBundle </param>
        /// <param name="entity">加载资源后实体</param>
        /// <param name="callback">资源加载完成回调</param>
        /// <returns></returns>
        private IEnumerator Load(AssetBundleCreateRequest bundleRequest, LoadSourceEntity entity, Action<LoadSourceEntity> callback)
        {
            if (entity == null) yield return 0;
            if (bundleRequest == null)
            {
                LCSConsole.WriteError("异步加载 AssetBundleCreateRequest 不存在!, bundleRequest = null");
                yield return 0;
            }
            yield return bundleRequest;
            AssetBundle assetBundle = bundleRequest.assetBundle;
            if (assetBundle == null)
            {
                LCSConsole.WriteError("创建资源 AssetBundle 失败!");
                yield return 0;
            }
            UnityEngine.Object retobj = assetBundle.Load(entity.ResName, entity.BundleType);
            if (retobj == null)
            {
                LCSConsole.WriteError("资源 AssetBundle 中不存在 resName = " + entity.ResName);
                yield return 0;
            }
            if (callback == null) yield return 0;
            entity.LoadObj = retobj as GameObject;
            entity.Bundle = assetBundle;
            callback(entity);
        }

        /// <summary>
        /// 异步加载资源
        /// 
        /// 默认加载路径和 resName 相同
        /// 默认加载 gameObject
        /// </summary>
        /// <param name="resName">资源名字</param>
        /// <param name="finishCall">加载完成后回调创建类(用户自定义创建位置)</param>
        /// <param name="callback">异步加载完成回调资源管理类</param>
        public void LoadSource(string resName, Action<string, GameObject> finishCall,
            Action<LoadSourceEntity> callback)
        {
            LoadSource(resName, resName, null, finishCall, callback);
        }

        /// <summary>
        /// 异步加载资源
        /// 
        /// 默认加载 gameObject
        /// </summary>
        /// <param name="resName">资源名字</param>
        /// <param name="bundPath">资源完整路径</param>
        /// <param name="finishCall">加载完成后回调创建类(用户自定义创建位置)</param>
        /// <param name="callback">异步加载完成回调资源管理类</param>
        public void LoadSource(string resName, string bundPath, Action<string, GameObject> finishCall,
            Action<LoadSourceEntity> callback)
        {
            LoadSource(resName, bundPath, null, finishCall, callback);
        }

        /// <summary>
        /// 异步加载资源
        /// </summary>
        /// <param name="resName">资源名字</param>
        /// <param name="bundPath">资源完整路径</param>
        /// <param name="type">资源的类型</param>
        /// <param name="finishCall">加载完成后回调创建类(用户自定义创建位置)</param>
        /// <param name="callback">异步加载完成回调资源管理类</param>
        public void LoadSource(string resName, string bundPath, Type type, Action<string, GameObject> finishCall,
            Action<LoadSourceEntity> callback)
        {
            LoadSourceEntity entity = new LoadSourceEntity()
            {
                ResName = resName,
                BundlePath = bundPath,
                BundleType = type,
                Callback = finishCall,
            };
            LoadSource(entity, callback);
        }

        /// <summary>
        /// 导入资源
        /// </summary>
        /// <param name="entity">加载资源后实体</param>
        /// <param name="callback">加载完成后回调</param>
        public void LoadSource(LoadSourceEntity entity, Action<LoadSourceEntity> callback)
        {
            if (entity == null) return;
            if (string.IsNullOrEmpty(entity.ResName))
            {
                LCSConsole.WriteError("导入资源名字为空, resName = string.Empty");
                return;
            }

            if (string.IsNullOrEmpty(entity.BundlePath))
            {
                LCSConsole.WriteError("导入 AssetBundle 路径为空, bundPath = string.Empty");
                return;
            }

            if (!File.Exists(entity.BundlePath))
            {
                LCSConsole.WriteError("导入 AssetBundle 路径不存在, bundPath = " + entity.BundlePath);
                return;
            }
            if (entity.BundleType == null) entity.BundleType = typeof(GameObject);
            byte[] bytes = File.ReadAllBytes(entity.BundlePath);
            AssetBundleCreateRequest request = AssetBundle.CreateFromMemory(bytes);
            LCCoroutine.Instance.StartCoroutine(Load(request, entity, callback));
        }

    }

}