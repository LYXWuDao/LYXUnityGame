using System;
using System.Collections.Generic;

namespace Game.LProfiler
{

    /***
     * 
     * 
     * 性能观察基础类
     * 
     * 
     */

    public interface LIWatch
    {

        /// <summary>
        /// 添加性能观察
        /// </summary>
        void StartWatch();

        /// <summary>
        /// 添加性能观察
        /// </summary>
        void StartWatch(string key);

        /// <summary>
        /// 添加性能观察
        /// </summary>
        void EndWatch();

    }

}

