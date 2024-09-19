using EnforceCodingConventionsEDU.LIB.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace EnforceCodingConventionsEDU.Services;

public class SomeSecondService : ISomeSecondService
{
    private readonly IConfiguration _config;
    private readonly ILogger<SomeService> _logger;

    public SomeSecondService(IConfiguration config, ILogger<SomeService>? logger = null)
    {
        _config = config;
        _logger = logger ?? NullLogger<SomeService>.Instance;
    }

    public async Task Run()
    {
        var waitTime = _config.GetSection(AppSettingsHelper.AppSettingsSectionName).GetValue<int>("WaitingInMs");
        _logger.LogInformation("This does some awesome work for {waitTime} seconds!", waitTime);
        await Task.Delay(waitTime);
    }
}