using Game.LBehaviour;
using Game.LCommon;

namespace Game.LDebug
{

    /***
     * 
     * 
     * 输出控制台日志
     * 
     * 
     */

    public class LCLogConsole : LABehaviour, LILog
    {

        private static LCLogConsole _instance = null;

        public static LCLogConsole Instance
        {
            get
            {
                if (_instance != null) return _instance;
                UnityEngine.GameObject create = LCSCompHelper.Create("_Console Debug");
                return (_instance = LCSCompHelper.FindComponet<LCLogConsole>(create));
            }
        }

        /// <summary>
        /// 输出普通日志
        /// </summary>
        public void Write(object msg)
        {
            LCSLogConsole.Write(msg);
        }

        /// <summary>
        /// 输出普通日志
        /// </summary>
        public void Write(string msg, params object[] args)
        {
            LCSLogConsole.Write(msg, args);
        }

        /// <summary>
        /// 输出普通日志
        /// </summary>
        public void Write(params object[] args)
        {
            LCSLogConsole.Write(args);
        }

        /// <summary>
        /// 输出错误日志
        /// </summary>
        public void WriteError(object msg)
        {
            LCSLogConsole.WriteError(msg);
        }

        /// <summary>
        /// 输出错误日志
        /// </summary>
        public void WriteError(string msg, params object[] args)
        {
            LCSLogConsole.WriteError(msg, args);
        }

        /// <summary>
        /// 输出错误日志
        /// </summary>
        public void WriteError(params object[] args)
        {
            LCSLogConsole.WriteError(args);
        }

        /// <summary>
        /// 输出警告日志
        /// </summary>
        public void WriteWarning(object msg)
        {
            LCSLogConsole.WriteWarning(msg);
        }

        /// <summary>
        /// 输出警告日志
        /// </summary>
        public void WriteWarning(string msg, params object[] args)
        {
            LCSLogConsole.WriteWarning(msg, args);
        }

        /// <summary>
        /// 输出警告日志
        /// </summary>
        public void WriteWarning(params object[] args)
        {
            LCSLogConsole.WriteWarning(args);
        }

        public override void OnClear()
        {
            _instance = null;
        }

        public override void OnDestroy()
        {
            OnClear();
        }

    }

}

