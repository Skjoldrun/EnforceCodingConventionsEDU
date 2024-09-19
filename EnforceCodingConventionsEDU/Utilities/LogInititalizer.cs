using Microsoft.Extensions.Configuration;
using Serilog;
using System.Diagnostics;
using System.Reflection;

namespace EnforceCodingConventionsEDU.Utilities;

public static class LogInitializer
{
    private const string CompanyName = "Skjoldrun";
    private const string FileLogFolderName = "Log";
    private const string FileLogName = "Serilog_SelfLog_";
    private static IConfiguration? _appconfig;

    /// <summary>
    /// Creates the logger with settings from app.config and enrichments from code.
    /// Logger configuration is for logging to a mssql database with transforming configs for connection strings.
    /// </summary>
    /// <param name="selfLog">enables the serilog selflog for internal exception logging</param>
    /// <returns>Logger with inline and app.config settings</returns>
    public static ILogger CreateLogger(IConfiguration appconfig, bool selfLog = false)
    {
        _appconfig = appconfig;

        if (selfLog)
            StartSerilogSelfLog();

        return new LoggerConfiguration()
            .ReadFrom.Configuration(_appconfig)
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithProcessName()
            .Enrich.WithThreadId()
            .Enrich.WithAssemblyVersion()
            .CreateLogger();
    }

    /// <summary>
    /// Starts the Serilog self logging for internal log exceptions.
    /// Creates a log file in the LocalAppDataCompanyFolderPath.
    /// Deletes old selflog files to keep the folder clean.
    /// </summary>
    private static void StartSerilogSelfLog()
    {
        var applicationName = ThisAssembly.AssemblyName;
        var appSettingsPath = GetCompanyFolderPath(applicationName);
        var fileLogFolderPath = Path.Combine(appSettingsPath, FileLogFolderName);
        var fileLogFilePath = Path.Combine(fileLogFolderPath, FileLogName.Replace(FileLogName, $"{FileLogName}{DateTime.Now:yyyyMMdd-hhmmss.fff}.txt"));

        Directory.CreateDirectory(fileLogFolderPath);
        var serilogSelfLogFile = File.CreateText(fileLogFilePath);
        Serilog.Debugging.SelfLog.Enable(TextWriter.Synchronized(serilogSelfLogFile));
        Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));

        ClearSerilogSelfLog(fileLogFolderPath);
    }

    /// <summary>
    /// Gets a common path for settings and files for the application in the LocalAppData folder, combined with company name and application name.
    /// Creates the directories of the path unless they don't exist.
    /// </summary>
    /// <param name="applicationName">A subfolder with the given application string gets created if provided.</param>
    /// <returns>Path to appsettings folder</returns>
    public static string GetCompanyFolderPath(string applicationName = "")
    {
        string company = ((AssemblyCompanyAttribute)Attribute.GetCustomAttribute(Assembly.GetEntryAssembly()!, typeof(AssemblyCompanyAttribute), false)!).Company;
        company = string.IsNullOrEmpty(company) ? CompanyName : company.Replace(' ', '_');

        string companyPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), company, applicationName);
        Directory.CreateDirectory(companyPath);
        return companyPath;
    }

    /// <summary>
    /// Clears the old selflog files from directory.
    /// </summary>
    /// <param name="fileLogFolderPath">path to be cleared</param>
    /// <param name="ageInDays">number of days to hold old selflog files</param>
    private static void ClearSerilogSelfLog(string fileLogFolderPath, int ageInDays = 5)
    {
        string[] selfLogFiles = Directory.GetFiles(fileLogFolderPath);

        foreach (var file in selfLogFiles)
        {
            FileInfo fi = new FileInfo(file);
            if (fi.LastWriteTime < DateTime.Now.AddDays(-ageInDays) && fi.Name.Contains(FileLogName))
            {
                fi.Delete();
            }
        }
    }
}