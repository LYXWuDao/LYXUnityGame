using Game.LYX.Common;
using UnityEngine;
using System.Collections;


/*****
 * 
 * 
 * 销毁资源
 * 
 */

namespace Game.LYX.Source
{

    public class LYXUnloadSource
    {

        /// <summary>
        /// 卸载资源
        /// </summary>
        /// <param name="bundle">卸载的资源</param>
        public static void UnLoadSource(AssetBundle bundle)
        {
            UnLoadSource(bundle, true);
        }

        /// <summary>
        /// 卸载资源
        /// </summary>
        /// <param name="bundle">卸载的资源</param>
        /// <param name="unload"></param>
        public static void UnLoadSource(AssetBundle bundle, bool unload)
        {
            if (bundle == null)
            {
                LYXLogHelper.Error("卸载资源为空！");
                return;
            }
            bundle.Unload(unload);
        }

    }

}