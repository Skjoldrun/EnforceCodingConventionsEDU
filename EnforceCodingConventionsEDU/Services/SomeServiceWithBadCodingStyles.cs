using EnforceCodingConventionsEDU.LIB.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace EnforceCodingConventionsEDU.Services;

/// <summary>
/// Be careful not to get angry here. This class shows some strange and horrible examples that I have encountered in real life.
/// </summary>
public class SomeServiceWithBadCodingStyles : ISomeServiceWithBadCodingStyles
{
    private const string camelCaseConst = "This should be PascalCase!";
    private const string alllowerconst = "This also should be PascalCase!";
    private const string _fieldlikeConst = "This also should be PascalCase!";
    private const string _FieldlikeConst = "This is PascalCase but has a wrong underscore!";

    private readonly IConfiguration _config;
    private readonly ILogger<SomeService> _logger;

    private readonly string whyIsThereNoUnderscore = "whyIsThereNoUnderscore??";
    private readonly bool _IsItReallyThatHard = false;
    private readonly bool UpperAndStillWrong = true;

    public string _wrongPropName { get; set; } = "This should be PascalCase!";
    public string _StillWrongPropName { get; set; } = "This should be PascalCase without _ prefix!";

    public SomeServiceWithBadCodingStyles(IConfiguration config, ILogger<SomeService>? logger = null)
    {
        _config = config;
        _logger = logger ?? NullLogger<SomeService>.Instance;
    }

    public async Task Run()
    {
        bool _isThisStubid = true;

        //What the hell does 'g' stand for?
        decimal g;
        _logger.LogInformation("Executing some code with bad naming ...");

        lowerCaseMethodNamesAreBad();
        lowerCaseAndWeirdParameters("I have no clue.", false);

        if (_isThisStubid)
        {
            g = (decimal)WowUpperCaseButParamsAreStillBad(1, (uint)new Random().Next(1, 11), 3.0, new thisClassShouldntBeHere());
            _logger.LogInformation("soo after some really well written calculating method I found out that {g} is {value}!", nameof(g), g);
        }

        var waitTime = _config.GetSection(AppSettingsHelper.AppSettingsSectionName).GetValue<int>("WaitingInMs");
        await Task.Delay(waitTime);
    }

    public void lowerCaseMethodNamesAreBad()
    {
        _logger.LogInformation("The Methodname {methodname} is so bad, I should be ashamed ...", nameof(lowerCaseMethodNamesAreBad));
        _logger.LogInformation("And the fieldname {fieldname} is also bad, I should be more ashamed ...", whyIsThereNoUnderscore);
    }

    public void lowerCaseAndWeirdParameters(string __WhyDoYouDoThis, bool __IsThisSomeKindOfJoke = false)
    {
        _logger.LogInformation("{WhyDoYouDoThisN}? => {WhyDoYouDoThis} ", nameof(__WhyDoYouDoThis), __WhyDoYouDoThis);
        _logger.LogInformation("Is this a joke? => {IsThisAJoke}", __IsThisSomeKindOfJoke ? "It better is!" : "Nope... this is some fucked up reality!");
    }

    // I don't even tell you what this is about, [Summary] comments are overrated anyway, right?
    public float WowUpperCaseButParamsAreStillBad(int a, uint _b, double __C, thisClassShouldntBeHere t)
    {
        float f = 1;
        try
        {
            if (a == _b)
            {
                f = 2;
            }
            else
            {
                if (t.omgThisFieldIsPublicAndWithoutAUnderscore == "")
                {
                    _logger.LogInformation("Why not a proper string validation here? -> t.omgThisFieldIsPublicAndWithoutAUnderscore == \"\" ??");
                    f = 3;
                }
                else
                {
                    _logger.LogInformation("Nobody knows what t stands for anyway ...");
                    f = 4;
                }
            }

            if ((uint)(int)__C != (int)_b)
            {
                _logger.LogInformation("What the hell am I doing? ...");
                f = (float)__C;
            }
            else
            {
                // Do nothing

                // What?
                // Why is this block even here then???
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Something went wrong but I don't tell you what, figure it out by yourself ... hehe");
        }

        return f;
    }
}

public class thisClassShouldntBeHere()
{
    public string omgThisFieldIsPublicAndWithoutAUnderscore = "Srsly???";
    public string whyTheHellIsThisLowerCase { get; set; } = "😑";

    private class thisClassShouldBeUpperCose()
    {
        public int omgThisFieldIsPublicAndWithoutAUnderscore = 0;
        public string whyTheHellIsThisStillLowerCase { get; set; } = "😑";
    }

    private enum thisEnumShouldBeSomewhereElseAndUpperCase
    {
        zero,
        one,
        two,
        three
    }
}

public enum thisEnumShouldBeSomewhereElse
{
    zero,
    one,
    two,
    three
}