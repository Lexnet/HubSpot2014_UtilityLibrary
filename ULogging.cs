using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Layout;
using log4net.Plugin;
using log4net.Repository;
using log4net.Util;

namespace com.lexnetcrm.common
{
    /// <summary>
    /// Logging functions - Most of these functions use log4net. There were written and tested with log4net 1.2.10
    /// </summary>
    public class ULogging
    {
        /// <summary>
        /// If layout is null, the layout with default to a SimpleLayout
        /// If file path is null or empty, the path will be defaulted to the current executing directory
        /// If file name is null or empty, the path will be defaulted to "Log.txt"
        /// </summary>
        /// <param name="layout"></param>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        /// <param name="appendToLog"></param>
        /// <returns></returns>
        public static FileAppender getFileAppender(ILayout layout, string filePath, string fileName, bool appendToFile)
        {
            if (layout == null)
            {
                layout = new SimpleLayout();
            }

            if (String.IsNullOrEmpty(filePath) == true)
            {
                filePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\Logs";
                if (System.IO.Directory.Exists(filePath) == false)
                {
                    System.IO.Directory.CreateDirectory(filePath);
                }
            }

            // DateTime dt = Convert.ToDateTime(date); 
            // Since we know the \Logs folder and we are here dealing with new log files, we might as well delete old log files here as well.
            int logDaysToKeep = 0;
            if (Int32.TryParse(UConfig.getAppConfigValue("LogDaysToKeep", false), out logDaysToKeep) == false)
            {
                logDaysToKeep = 10;
            }
            string fileNameOnly = "";
            DateTime dt;
            string[] formats = "yyyyMMdd".Split(' ');
            string [] logFiles = System.IO.Directory.GetFiles(filePath);
            foreach (string logFile in logFiles)
            {
                // grab date section out of log file name, check to see if date is > LogDaysToKeep
                fileNameOnly = System.IO.Path.GetFileName(logFile);
                if (fileNameOnly.Length == 16)
                {
                    if (DateTime.TryParseExact(fileNameOnly.Substring(4, 8), formats, new System.Globalization.CultureInfo("en-US"), System.Globalization.DateTimeStyles.None, out dt) == true)
                    {
                        if ((DateTime.Now - dt).TotalDays > logDaysToKeep)
                        {
                            System.IO.File.Delete(logFile);
                        }
                    }
                }
            }

            if (String.IsNullOrEmpty(fileName) == true)
            {
                string logFile = "Log_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                fileName = logFile;
                //fileName = "Log.txt";
            }

            FileAppender fa = new FileAppender();

            fa.File = UFileSystem.ValidateDirectoryPath(filePath) + fileName;
            fa.Layout = layout;
            fa.AppendToFile = appendToFile;

            return fa;
        }

        /// <summary>
        /// If layout is null, the layout with default to a SimpleLayout        
        /// </summary>
        /// <param name="layout"></param>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        /// <param name="appendToLog"></param>
        /// <returns></returns>
        public static ConsoleAppender getConsoleAppender(ILayout layout)
        {
            if (layout == null)
            {
                layout = new SimpleLayout();
            }

            ConsoleAppender ca = new ConsoleAppender();
            
            ca.Layout = layout;

            return ca;
        }

        /// <summary>
        /// Requires an element in app.config with a key of "LogLevel" to function
        /// </summary>
        /// <returns></returns>
        public static string getLogLevel()
        {
            string logLevel = UConfig.getAppConfigValue("LogLevel", false);

            logLevel = logLevel.Trim().ToUpper();

            return logLevel;
        }

        public static void writeToInfoLog(ILog logger, object message)
        {
            writeToInfoLog(logger, message, null);
        }

        public static void writeToInfoLog(ILog logger, object message, Exception e)
        {            
            if (logger.IsInfoEnabled == true)
            {
                if (e != null)
                {
                    logger.Info(message, e);
                }
                else
                {
                    logger.Info(message);
                }
            }            
            else
            {
                //Not enabled
            }
        }

        public static void writeToWarnLog(ILog logger, object message)
        {
            writeToWarnLog(logger, message, null);
        }

        public static void writeToWarnLog(ILog logger, object message, Exception e)
        {
            if (logger.IsWarnEnabled == true)
            {
                if (e != null)
                {
                    logger.Warn(message, e);
                }
                else
                {
                    logger.Warn(message);
                }
            }
            else
            {
                //Not enabled
            }
        }

        public static void writeToDebugLog(ILog logger, object message)
        {
            writeToDebugLog(logger, message, null);
        }

        public static void writeToDebugLog(ILog logger, object message, Exception e)
        {
            if (logger.IsDebugEnabled == true)
            {
                if (e != null)
                {
                    logger.Debug(message, e);
                }
                else
                {
                    logger.Debug(message);
                }
            }
            else
            {
                //Not enabled
            }
        }

        public static void writeToErrorLog(ILog logger, object message)
        {
            writeToErrorLog(logger, message, null);
        }

        public static void writeToErrorLog(ILog logger, object message, Exception e)
        {
            if (logger.IsErrorEnabled == true)
            {
                if (e != null)
                {
                    logger.Error(message, e);
                }
                else
                {
                    logger.Error(message);
                }
            }
            else
            {
                //Not enabled
            }
        }

        public static void writeToFatalLog(ILog logger, object message)
        {
            writeToFatalLog(logger, message, null);
        }

        public static void writeToFatalLog(ILog logger, object message, Exception e)
        {
            if (logger.IsFatalEnabled == true)
            {
                if (e != null)
                {
                    logger.Fatal(message, e);
                }
                else
                {
                    logger.Fatal(message);
                }
            }
            else
            {
                //Not enabled
            }
        }
    }
}
