using System;

namespace abc_bank
{
    public class DateProvider
    {
        private static DateProvider _instance = null;

        public static DateProvider GetInstance()
        {
            if (_instance == null)
                _instance = new DateProvider();
            return _instance;
        }

        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}
