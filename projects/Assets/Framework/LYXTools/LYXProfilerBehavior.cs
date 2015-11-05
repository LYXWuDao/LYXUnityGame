using Game.LYX.Behaviour;
using Game.LYX.Common;
using UnityEngine;
using System;
using System.Collections.Generic;


/***
 * 
 * 
 * 系统内存分析
 * 
 * 
 */

namespace Game.LYX.Tools
{

    public class LYXProfilerBehavior : LYXBaseBehaviour
    {

        /// <summary>
        /// 转换成 mb
        /// </summary>
        private float kBSize = 1024.0f * 1024.0f;

        /// <summary>
        /// 是否分析内存
        /// </summary>
        public bool IsProfiler = true;

        /// <summary>
        /// 开始分析
        /// </summary>
        /// <returns></returns>
        public static LYXProfilerBehavior BeginProfiler(GameObject go)
        {
            if (go == null) return null;
            LYXProfilerBehavior profiler = LYXCompHelper.FindComponet<LYXProfilerBehavior>(go);
            profiler.IsProfiler = true;
            return profiler;
        }

        /// <summary>
        /// 绘制分析结果
        /// </summary>
        protected override void OnGUI()
        {
            if (!IsProfiler) return;

            GUI.Label(new Rect(0, 0, 200, 25), string.Format("MonoUsedSize:{0}", Profiler.GetMonoUsedSize() / kBSize));
            GUI.Label(new Rect(0, 15, 200, 25),
                string.Format("Allocated:{0}", Profiler.GetTotalAllocatedMemory() / kBSize));
            GUI.Label(new Rect(0, 30, 200, 25), string.Format("Reserved:{0}", Profiler.GetTotalReservedMemory() / kBSize));
        }

    }
}
