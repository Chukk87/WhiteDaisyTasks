namespace WhiteDaisyLibrary.Dictionaries
{
    public static class NameAffix
    {
        public static readonly Dictionary<string, List<string>> NameAffixDictionary = new Dictionary<string, List<string>>
        {
            { "Title", new List<string> {  "MR", "MRS", "MS", "DR", "MISS", "MASTER", "Prof", "Sister", "Major", "Lady", "Doctor" } },
            { "Suffix", new List<string> { "JR", "SR", "III", "II", "SNR", "PhD", "MD" } }
        };
    }
}