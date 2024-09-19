using EnforceCodingConventionsEDU.LIB.ServiceExtensions;
using EnforceCodingConventionsEDU.LIB.Utilities;
using EnforceCodingConventionsEDU.Services;
using EnforceCodingConventionsEDU.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace EnforceCodingConventionsEDU;

public class Program
{
    private static bool _keepRunning = true;

    private static async Task Main(string[] args)

    {
        Console.CancelKeyPress += delegate (object? sender, ConsoleCancelEventArgs e)
        {
            e.Cancel = true;
            _keepRunning = false;
        };

        var appConfig = AppSettingsHelper.GetAppConfigBuilder().Build();
        Log.Logger = LogInitializer.CreateLogger(appConfig);

        Log.Information($"{ThisAssembly.AssemblyName} start");

        var host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                // Add DI registration here ...
                services.AddTransient<ISomeService, SomeService>();
                services.AddTransient<ISomeSecondService, SomeSecondService>();
                services.AddTransient<ISomeServiceWithBadCodingStyles, SomeServiceWithBadCodingStyles>();

                services.AddLibServices();
            })
            .UseSerilog()
            .Build();

        var service = host.Services.GetRequiredService<ISomeService>();
        var secondService = ActivatorUtilities.GetServiceOrCreateInstance<SomeSecondService>(host.Services);
        var someServiceWithBadCodingStyles = host.Services.GetService<ISomeServiceWithBadCodingStyles>()!; // this could be null
        var someUtilityClass = host.Services.GetRequiredService<SomeUtilityClassInterface>();

        try
        {
            await service.Run();
            await secondService.Run();
            await someServiceWithBadCodingStyles.Run();
            await someUtilityClass.DoSomething();
            lowerCasePrivateMethod();
            lowerCasePublicMethod();

            string _badNameVar = "uff...";
            string Result = _badNameVar;

            // This is still possible ...
            int ba = 1;
            int c = ba;

            lowerCasePublicMethodWithUglyParameterNames(_text: "Important text", __Number: c, Switch: true, out Result);
            Log.Information(Result);

            await Console.Out.WriteLineAsync("Press [Ctrl]+[C] to exit the application ...");
            while (_keepRunning)
                Thread.Sleep(1000);
        }
        catch (Exception ex)
        {
            Log.Error(ex, $"Exception: {ex.Message}");
        }

        Log.Information($"{ThisAssembly.AssemblyName} stop");
        Log.CloseAndFlush();
    }

    private static void lowerCasePrivateMethod()
    {
        Log.Information("This name is bad! {methodName}", nameof(lowerCasePrivateMethod));
    }

    public static void lowerCasePublicMethod()
    {
        Log.Information("This name is bad! {methodName}", nameof(lowerCasePublicMethod));
    }

    public static void lowerCasePublicMethodWithUglyParameterNames(string _text, int __Number, bool Switch, out string Result)
    {
        Log.Information("These parameters are bad! {stringParam}, {intParam}, {boolParam}, {outStringParam}",
            nameof(_text), nameof(__Number), nameof(Switch), nameof(Result));

        if (Switch)
            _text = $"{_text} with {__Number} of something ?";

        Result = _text;
    }
}