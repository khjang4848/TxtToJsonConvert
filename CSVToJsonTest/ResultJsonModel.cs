using System.Collections.Generic;
using Newtonsoft.Json;

namespace CSVToJsonTest
{
    public class ResultJsonModel
    {
        public ResultJsonModel()
            => TextModel = new Dictionary<string, object>();

        [JsonExtensionData]
        public Dictionary<string, object> TextModel { get; set; }


        
        public Dictionary<string, Dictionary<string, object>> Message { get; set; }

    }
}

