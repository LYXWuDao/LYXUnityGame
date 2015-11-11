using System;
using Game.LCommon;
using Game.LDebug;
using UnityEngine;

namespace Game.LProfiler
{

    /***
     * 
     * 
     * 添加性能观察, 使用C#内置
     * 
     */

    public class LCRecordWatch : LAWatch, LIWatch
    {

        /// <summary>
        /// 开始观察
        /// </summary>
        public void StartWatch()
        {
            StartWatch(string.Empty);
        }

        /// <summary>
        /// 开始观察
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public void StartWatch(string key)
        {
            RecordWatchEntity entity = CurrentWatch;
            if (entity != null && entity.IsWatch)
            {
                LCSLogConsole.WriteError("已经开始观察性能......");
                return;
            }

            if (entity != null)
            {
                entity.IsWatch = true;
                entity.StartTime = Time.realtimeSinceStartup;
                entity.BeginWatch();
                return;
            }

            entity = new RecordWatchEntity();
            entity.WatchKey = string.IsNullOrEmpty(key) ? Guid.NewGuid().ToString() : key;
            // 记录 自游戏开始的实时时间
            entity.StartTime = Time.realtimeSinceStartup;
            entity.BeginWatch();
            AddWatch(entity);
            CurrentWatch = entity;
        }

        public void EndWatch()
        {
            RecordWatchEntity entity = CurrentWatch;
            if (entity == null) return;
            entity.EndTime = Time.realtimeSinceStartup;
            entity.EndWatch();
            double watchTime = entity.GetWatchTime();
            LCSLogConsole.Write(string.Format(" [RecordTime] {0} use {1}s.", entity.WatchKey, entity.DiffTime));
            // 7位精度
            LCSLogConsole.WriteWarning("[Watch] {0} use time : {1}s.", entity.WatchKey, watchTime.ToString("F7"));
            RemoveWatch(entity);
        }

    }

}
