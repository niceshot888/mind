using CefSharp;
using CefSharp.WinForms;
using System;
using System.IO;
namespace Sas.WfClient.Core
{
    public static class CefBootstrap
    {

        public static void Init(bool osr, bool multiTheadedMessageLoop)
        {
            //Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cache")
            var settings = new CefSettings
            {
                RemoteDebuggingPort = 8088,
                CachePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cache"), 
                LogSeverity = LogSeverity.Disable,
                MultiThreadedMessageLoop = multiTheadedMessageLoop
            };

            //允许跨域
            settings.CefCommandLineArgs.Add("disable-web-security", "1");

            settings.CefCommandLineArgs.Add("enable-media-stream", "1");

            var pepflashPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"plugins\pepflashplayer.dll");
            settings.CefCommandLineArgs.Add("ppapi-flash-path", pepflashPath);
            settings.CefCommandLineArgs.Add("ppapi-flash-version", "32.0.0.465");

            settings.CefCommandLineArgs.Add("disable-pinch", "1");
            settings.CefCommandLineArgs.Add("persist_session_cookies", "1");

            if (osr)
            {
                settings.WindowlessRenderingEnabled = true;
                settings.CefCommandLineArgs.Add("enable-begin-frame-scheduling", "1");
            }

            settings.PersistUserPreferences = true;
            settings.PersistSessionCookies = true;

            Cef.Initialize(settings);
        }


        
    }
}
