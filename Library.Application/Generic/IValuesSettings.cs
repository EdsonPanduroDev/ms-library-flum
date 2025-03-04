namespace Library.Application.Generic
{
    public interface IValuesSettings
    {
        string GetTimeZone();
    }

    public class ValuesSettings : IValuesSettings
    {
        public string _timeZone { get; set; }
        public ValuesSettings(string timeZone)
        {
            _timeZone = timeZone;
        }

        public string GetTimeZone()
        {
            return _timeZone;
        }
    }
}
