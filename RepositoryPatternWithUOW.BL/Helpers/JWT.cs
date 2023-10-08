namespace RepositoryPatternWithUOW.BL.Interfaces
{
    public class JWT
    {
        public string Key { get; set; }
        public string issuer { get; set; }
        public string Audience { get; set; }
        public double DurationInDays { get; set; }
    }
}
