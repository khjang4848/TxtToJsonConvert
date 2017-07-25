using Newtonsoft.Json;

namespace CSVToJsonTest
{
    public class BaseMessageModel
    {   
        public string SubCode { get; set; }
        public string MainCode { get; set; }
        public string KoreanName { get; set; }
        public string EnglishName { get; set; }

    }


    
    public class SubMessageModel
    {   
        public string MainCode { get; set; }
        public string KoreanName { get; set; }

    }
}
