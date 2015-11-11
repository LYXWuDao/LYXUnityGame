using System;
using Game.LCommon;
using Game.LDebug;
using UnityEngine;

namespace Game.LProfiler
{


    /****
     * 
     * 
     * 使用 untiy 运行时间分析性能
     * 
     * 
     */

    public class LCRecordTime : LAWatch, LIWatch
    {
        public void StartWatch()
        {
            StartWatch(string.Empty);
        }

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
                return;
            }

            entity = new RecordWatchEntity();
            entity.WatchKey = string.IsNullOrEmpty(key) ? Guid.NewGuid().ToString() : key;
            // 记录 自游戏开始的实时时间
            entity.StartTime = Time.realtimeSinceStartup;
            AddWatch(entity);
            CurrentWatch = entity;
        }

        public void EndWatch()
        {
            RecordWatchEntity entity = CurrentWatch;
            if (entity == null) return;
            entity.EndTime = Time.realtimeSinceStartup;
            LCSLogConsole.Write(string.Format(" [RecordTime] {0} use {1}s.", entity.WatchKey, entity.DiffTime));
            RemoveWatch(entity);
        }
    }

}

