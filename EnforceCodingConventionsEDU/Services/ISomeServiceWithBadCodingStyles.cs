
namespace EnforceCodingConventionsEDU.Services
{
    public interface ISomeServiceWithBadCodingStyles
    {
        void lowerCaseAndWeirdParameters(string __WhyDoYouDoThis, bool __IsThisSomeKindOfJoke = false);
        void lowerCaseMethodNamesAreBad();
        Task Run();
        float WowUpperCaseButParamsAreStillBad(int a, uint b, double c, thisClassShouldntBeHere t);
    }
}