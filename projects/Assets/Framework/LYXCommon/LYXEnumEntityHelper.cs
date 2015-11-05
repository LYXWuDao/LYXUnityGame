using UnityEngine;

/****
*
* 
* 公用 枚举和实体 
* 
* 
*/

namespace Game.LYX.Common
{

    #region 框架内 公用枚举


    #endregion

    #region 框架内 公用实体

    /***
     * 
     * 导入的二进制文件实体
     * 
     */

    public class LoadSourceEntity
    {

        /// <summary>
        /// 资源的id
        /// </summary>
        public string SourceId = string.Empty;

        /// <summary>
        ///  资源 AssetBundle
        /// </summary>
        public AssetBundle Bundle = null;

        /// <summary>
        /// 资源的名字
        /// </summary>
        public string ResName = string.Empty;

        /// <summary>
        /// 加载 资源 AssetBundle 的路径
        /// </summary>
        public string BundlePath = string.Empty;

        /// <summary>
        /// 加载的资源
        /// </summary>
        public GameObject LoadObj = null;

    }

    #endregion

}