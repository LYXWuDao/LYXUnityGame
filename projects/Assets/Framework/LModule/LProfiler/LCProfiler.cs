using Game.LBehaviour;
using Game.LCommon;
using UnityEngine;


/***
 * 
 * 
 * 系统内存分析
 * 
 * 
 */

namespace Game.LProfiler
{

    /****
     * 
     * 
     * 内存使用分析
     * 
     * 
     */

    public class LCProfiler : LABehaviour
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
        public static LCProfiler BeginProfiler(GameObject go)
        {
            if (go == null) return null;
            LCProfiler profiler = LCSCompHelper.FindComponet<LCProfiler>(go);
            profiler.IsProfiler = true;
            return profiler;
        }

        /// <summary>
        /// 绘制分析结果
        /// </summary>
        public override void OnGUI()
        {
            if (!IsProfiler) return;

            GUI.Label(new Rect(0, 0, 200, 25), string.Format("MonoUsedSize:{0}", Profiler.GetMonoUsedSize() / kBSize));
            GUI.Label(new Rect(0, 15, 200, 25),
                string.Format("Allocated:{0}", Profiler.GetTotalAllocatedMemory() / kBSize));
            GUI.Label(new Rect(0, 30, 200, 25), string.Format("Reserved:{0}", Profiler.GetTotalReservedMemory() / kBSize));
        }

    }
}
