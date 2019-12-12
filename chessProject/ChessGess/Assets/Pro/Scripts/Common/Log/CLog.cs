using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Asvo.Common
{
    public enum SysLogType
    {
        ALL = 0, //默认全部
        InitGame,
        Battle,
        Battle_Role_State,  //战斗-角色状态
        Animation,
        Table,
        ECS_DEBUG,
        FSM_DEBUG,

        Unity_Native
    }

    public static class CLog
    {
        public static bool ENABLE_LOG = true;
        public static bool ENABLE_WARNING = true;
        public static bool ENABLE_ERROR = true;

        public delegate void OnLogMessage(SysLogType sysLogType, LogType logType, string log);

        public static List<SysLogType> IgnoreTypeList = new List<SysLogType> {};
        
        private static OnLogMessage _logMessage;
        public static event OnLogMessage LogMessageEvent
        {
            add { _logMessage += value; }
            remove { _logMessage -= value; }
        }

        //区分Unity_Native log标记
        public static bool isInCustomLog = false;

        private static void InvokeLogMsg(string message, SysLogType sysLogType, LogType logType)
        {
            if (null == _logMessage)
            {
                return;
            }
            _logMessage.Invoke(sysLogType, logType, message);
        }

        [System.Diagnostics.Conditional("ASVO_ENABLE_LOG_LOG")]
        public static void Log(SysLogType sysLogType, object format, params object[] args)
        {
            if (ENABLE_LOG)
            {
                if (IgnoreTypeList.Contains(sysLogType))
                    return;

                if (args.Length > 0) format = string.Format(format.ToString(), args);
                string msg = format.ToString();
                InvokeLogMsg(msg, sysLogType, LogType.Log);
                isInCustomLog = true;
                Debug.Log(msg);
                isInCustomLog = false;
            }
        }

        [System.Diagnostics.Conditional("ASVO_ENABLE_LOG_LOG")]
        public static void Warning(SysLogType sysLogType, object format, params object[] args)
        {
            if (ENABLE_WARNING)
            {
                if (args.Length > 0) format = string.Format(format.ToString(), args);
                string msg = format.ToString();
                InvokeLogMsg(msg, sysLogType, LogType.Warning);
                isInCustomLog = true;
                Debug.LogWarning(msg);
                isInCustomLog = false;
            }
        }

        public static void Error(SysLogType sysLogType, object format, params object[] args)
        {
            if (ENABLE_ERROR)
            {
                if (args.Length > 0) format = string.Format(format.ToString(), args);
                string msg = format.ToString();
                InvokeLogMsg(msg, sysLogType, LogType.Error);
                isInCustomLog = true;
                Debug.LogError(msg);
                isInCustomLog = false;
            }
        }
    }
}