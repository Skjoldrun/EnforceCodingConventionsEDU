using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace EnforceCodingConventionsEDU.LIB.Utilities;

public class SomeUtilityClass : SomeUtilityClassInterface
{
    private readonly IConfiguration _config;
    private readonly ILogger _logger;

    public SomeUtilityClass(IConfiguration config, ILogger<SomeUtilityClass>? logger = null)
    {
        _config = config;
        _logger = logger ?? NullLogger<SomeUtilityClass>.Instance;
    }

    public async Task DoSomething(bool _isThisAGoodIdea = false, bool __shouldIBeNamedLikeThis = false)
    {
        var wt = _config.GetSection(AppSettingsHelper.AppSettingsSectionName).GetValue<int>("WaitingInMs");
        _logger.LogInformation("Read {waitTime} from settings.", wt);
        await Task.Delay(wt);
        _logger.LogInformation("Done waiting!");
    }
}