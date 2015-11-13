using System;
using System.Collections.Generic;
using LGame.LCommon;

namespace LGame.LBase
{

    /*****
     * 
     * 
     * 所有管理的基类
     *  
     * 包括，资源管理，界面管理等
     * 
     * 
     */

    public static class LCSBaseManage
    {

        /// <summary>
        /// 缓存当前所有数据
        /// </summary>
        private static Dictionary<Type, LCManageEntity> _dicManage = new Dictionary<Type, LCManageEntity>();

        /// <summary>
        /// 增加数据
        /// </summary>
        /// <param name="type">数据类型,不能重复</param>
        /// <param name="key">数据key</param>
        /// <param name="value">数据实体</param>
        public static void Add(Type type, string key, object value)
        {
            if (type == null) return;
            LCManageEntity entity = null;
            if (!_dicManage.TryGetValue(type, out entity))
            {
                entity = new LCManageEntity { Guid = LCSGuid.NewUpperGuid() };
                _dicManage.Add(type, entity);
            }
            if (entity == null) return;
            if (entity.DicEntitys.ContainsKey(key))
                entity.DicEntitys[key] = value;
            else
                entity.DicEntitys.Add(key, value);
        }

        /// <summary>
        /// 查找某一类型的数据
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static LCManageEntity Find(Type type)
        {
            if (type == null) return null;
            LCManageEntity entity = null;
            _dicManage.TryGetValue(type, out entity);
            return entity;
        }

        /// <summary>
        /// 查找某一类型数据的所有值
        /// </summary>
        /// <typeparam name="TValue">数据值的类型</typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static TValue[] Find<TValue>(Type type)
        {
            if (type == null) return null;
            LCManageEntity entity = null;
            if (!_dicManage.TryGetValue(type, out entity)) return null;
            TValue[] results = new TValue[entity.DicEntitys.Count];
            int index = 0;
            foreach (object value in entity.DicEntitys.Values)
            {
                results[index] = (TValue)value;
                index++;
            }
            return results;
        }

        /// <summary>
        /// 查找数据
        /// </summary>
        /// <param name="type">数据类型,不能重复</param>
        /// <param name="key">数据key, 不能重复</param>
        /// <returns>返回查找到的数据</returns>
        public static object Find(Type type, string key)
        {
            if (type == null || string.IsNullOrEmpty(key)) return null;
            LCManageEntity entity = Find(type);
            object result = null;
            entity.DicEntitys.TryGetValue(key, out result);
            return result;
        }

        /// <summary>
        /// 查找数据
        /// </summary>
        /// <typeparam name="TValue">数据值的类型</typeparam>
        /// <param name="type"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TValue Find<TValue>(Type type, string key)
        {
            object result = Find(type, key);
            return result != null ? (TValue)result : default(TValue);
        }

        /// <summary>
        /// 清理该类型的所有数据
        /// </summary>
        /// <param name="type"></param>
        /// <returns>返回是否清理成功</returns>
        public static bool Remove(Type type)
        {
            if (type == null || !_dicManage.ContainsKey(type)) return true;
            return _dicManage.Remove(type);
        }

        /// <summary>
        /// 移出数据
        /// </summary>
        /// <param name="type">数据类型,不能重复</param>
        /// <param name="key">数据key, 不能重复</param>
        /// <returns>返回移出的数据,如果为空表示没有找到移出的数据</returns>
        public static object Remove(Type type, string key)
        {
            if (type == null || string.IsNullOrEmpty(key)) return null;
            LCManageEntity entity = null;
            if (!_dicManage.TryGetValue(type, out entity)) return null;
            object result = null;
            entity.DicEntitys.TryGetValue(key, out result);
            if (result != null) entity.DicEntitys.Remove(key);
            return result;
        }

        /// <summary>
        /// 清除保存的所有数据
        /// </summary>
        public static void Clear()
        {
            _dicManage.Clear();
        }

    }

}

