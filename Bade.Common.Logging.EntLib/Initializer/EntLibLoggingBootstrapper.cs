using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace Bade.Common.Logging.EntLib.Initializer
{
    public class EntLibLoggingBootstrapper
    {
        private static DictionaryConfigurationSource ConfigureEntLib(string logFilePath, int minPriority = 0)
        {
            //var builder = new ConfigurationSourceBuilder();

            //builder.ConfigureData().ForDatabaseNamed("EntLibLoggingDbConnection")
            //    .ThatIs.ASqlDatabase()
            //    .WithConnectionString(ConfigurationManager.ConnectionStrings["EntLibConnectionString"].ConnectionString)
            //    .AsDefault();

            //#region custom db tracer

            ////builder.ConfigureLogging()
            ////    .WithOptions
            ////    .DoNotRevertImpersonation()
            ////    .LogToCategoryNamed("All")
            ////    .SendTo
            ////    .Custom<EntLibCustomDatabaseLogger>("DatabaseLogger"); 
            //#endregion


            //builder.ConfigureLogging()
            //    .WithOptions
            //    .DoNotRevertImpersonation()
            //    .FilterOnPriority("EntLibLoggingPriorityFilter")
            //        .StartingWithPriority(minPriority)
            //    .LogToCategoryNamed(LoggingCategory.General)
            //        .WithOptions
            //        .SetAsDefaultCategory()
            //        .SendTo
            //            .Database("EntLibLoggingDatabaseLogger")
            //            .UseDatabase("EntLibLoggingDbConnection")
            //            .WithWriteLogStoredProcedure("Services.WriteLog")
            //            .WithAddCategoryStoredProcedure("Services.AddCategory")
            //            .WithTraceOptions(System.Diagnostics.TraceOptions.Callstack
            //                | System.Diagnostics.TraceOptions.DateTime
            //                | System.Diagnostics.TraceOptions.LogicalOperationStack
            //                | System.Diagnostics.TraceOptions.ProcessId
            //                | System.Diagnostics.TraceOptions.ThreadId
            //                | System.Diagnostics.TraceOptions.Timestamp)
            //            .FormatWith(new FormatterBuilder()
            //                .TextFormatterNamed("EntLibLoggingTextFormatter")
            //                .UsingTemplate(
            //                    "Timestamp: {timestamp}{newline}"
            //                    + "Message: {message}{newline}"
            //                    + "Category: {category}{newline}"
            //                    + "Priority: {priority}{newline}"
            //                    + "EventId: {eventid}{newline}"
            //                    + "Severity: {severity}{newline}"
            //                    + "Title: {title}{newline}"
            //                    + "Machine: {localMachine}{newline}"
            //                    + "App Domain: {localAppDomain}{newline}"
            //                    + "ProcessId: {localProcessId}{newline}"
            //                    + "Process Name: {localProcessName}{newline}"
            //                    + "Thread Name: {threadName}{newline}"
            //                    + "Win32 ThreadId: {win32ThreadId}{newline}"
            //                    + "Extended Properties: {dictionary({newline}{tab}{key} = {value})}"))
            //        .SendTo
            //            .RollingFile("EntLibRollingFileLogger")
            //            .Filter(System.Diagnostics.SourceLevels.Error)
            //            .RollAfterSize(1024)
            //            .WhenRollFileExists(RollFileExistsBehavior.Increment)
            //            .UseTimeStampPattern("{yyyy-MM-dd-HH-mm-ss}")
            //            .RollEvery(RollInterval.Midnight)
            //            .ToFile(logFilePath)
            //            .FormatWithSharedFormatter("EntLibLoggingTextFormatter")
            //    .LogToCategoryNamed(LoggingCategory.Exception)
            //        .SendTo
            //            .SharedListenerNamed("EntLibLoggingDatabaseLogger")
            //        .SendTo
            //            .SharedListenerNamed("EntLibRollingFileLogger")
            //    .LogToCategoryNamed(LoggingCategory.Security)
            //        .SendTo
            //            .SharedListenerNamed("EntLibLoggingDatabaseLogger")
            //        .SendTo
            //            .SharedListenerNamed("EntLibRollingFileLogger")
            //            ;

#warning enterprise library using unity container as default, you can write your ServiceLocator for structure map when you have extra time.

            var configSource = new DictionaryConfigurationSource();
           // builder.UpdateConfigurationWithReplace(configSource);

            return configSource;
        }

        public static void Bootstrap(/*ConstantMembershipUserEnum user*/)
        {
            //string logFilePath = ConfigResolver.LoggingFileFullPath;
            //string logMinPriority = ConfigResolver.LoggingMinimumPriority;

            //if (user == ConstantMembershipUserEnum.ADMIN_APPLICATION_USER)
            //{
            //    logFilePath = ConfigResolver.AdminLoggingFileFullPath;
            //    logMinPriority = ConfigResolver.AdminLoggingMinimumPriority;
            //}
            //else if (user == ConstantMembershipUserEnum.NEXUM_WEBSERVICE_USER)
            //{
            //    logFilePath = ConfigResolver.VehicleWebServiceLoggingFileFullPath;
            //    logMinPriority = ConfigResolver.VehicleWebServiceLoggingMinimumPriority;
            //}
            //else if (user == ConstantMembershipUserEnum.WEBUI_APPLICATION_USER)
            //{
            //    logFilePath = ConfigResolver.WebUILoggingFileFullPath;
            //    logMinPriority = ConfigResolver.WebUILoggingMinimumPriority;
            //}
            //else if (user == ConstantMembershipUserEnum.NEXUM_IMAGEWEBSERVICE_USER)
            //{
            //    logFilePath = ConfigResolver.ImageWebServiceLoggingFileFullPath;
            //    logMinPriority = ConfigResolver.ImageWebServiceLoggingMinimumPriority;
            //}
            //else
            //{
            //    logFilePath = ConfigResolver.LoggingFileFullPath;
            //    logMinPriority = ConfigResolver.LoggingMinimumPriority;
            //}

            //int minPriority = 0;
            //int.TryParse(logMinPriority, out minPriority);

            //var configSource = ConfigureEntLib(logFilePath, minPriority);
            //EnterpriseLibraryContainer.Current = EnterpriseLibraryContainer.CreateDefaultContainer(configSource);
        }



    }
}