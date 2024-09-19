using EnforceCodingConventionsEDU.LIB.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace EnforceCodingConventionsEDU.Services;

public class SomeService : ISomeService
{
    private readonly IConfiguration _config;
    private readonly ILogger<SomeService> _logger;

    public SomeService(IConfiguration config, ILogger<SomeService>? logger = null)
    {
        _config = config;
        _logger = logger ?? NullLogger<SomeService>.Instance;
    }

    public async Task Run()
    {
        var waitTime = _config.GetSection(AppSettingsHelper.AppSettingsSectionName).GetValue<int>("WaitingInMs");
        _logger.LogInformation("The current environment is {DOTNET_ENVIRONMENT}", Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production");
        await Task.Delay(waitTime);
    }
}