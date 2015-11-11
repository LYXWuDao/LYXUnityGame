using System;
using System.Collections.Generic;

/***
 * 
 * 
 *  ui 界面响应事件行为组件
 * 
 * 
 */

namespace Game.LBehaviour
{

    public abstract class LAUIBehaviour : LABehaviour
    {

        /// <summary>
        /// 打开界面
        /// 
        /// 打开并创建界面
        /// </summary>
        public virtual void OnOpen() { }

        /// <summary>
        /// 打开界面
        /// </summary>
        /// <param name="depth">界面深度</param>
        public virtual void OnOpen(int depth) { }

        /// <summary>
        /// 关闭界面
        /// 
        /// 关闭并销毁该界面
        /// </summary>
        public virtual void OnClose() { }

        /// <summary>
        ///  界面获得焦点
        /// </summary>
        public virtual void OnFocus() { }

        /// <summary>
        /// 界面失去焦点
        /// </summary>
        public virtual void OnLostFocus() { }

        /// <summary>
        /// 单击事件
        /// </summary>
        public virtual void OnClick() { }

        /// <summary>
        /// 双击事件
        /// </summary>
        public virtual void OnDoubleClick() { }

        /// <summary>
        /// 按下一定时间
        /// </summary>
        public virtual void OnPress() { }

        /// <summary>
        /// 鼠标按下一定时间后抬起
        /// </summary>
        public virtual void OnRelease() { }

        /// <summary>
        /// 显示
        /// </summary>
        public virtual void OnShow() { }

        /// <summary>
        /// 隐藏
        /// </summary>
        public virtual void OnHide() { }

    }

}

