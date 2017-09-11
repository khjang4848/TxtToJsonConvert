using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace CSVToJsonTest
{
    class Program
    {
        private static string _basePath = @"C:\MyWork\GitHub\TxtToJsonConvert\CSVToJsonTest\";
        static void PrintJsonKorFile(List<BaseMessageModel> baseMessageModels, 
            List<BaseTextModel> baseTextModels )
        {
            string basePath = _basePath;
            ResultJsonModel resultModel = new ResultJsonModel();

            var tt = baseMessageModels.GroupBy(x => x.SubCode);

            var dicc = new Dictionary<string, Dictionary<string, object>>();

            foreach (var row in tt)
            {
                Dictionary<string, object> subModel = new Dictionary<string, object>();
                foreach (var name in row)
                {
                    subModel.Add(name.MainCode, name.KoreanName);
                }

                dicc.Add(row.Key, subModel);
            }

            resultModel.Message = dicc;

            foreach (var row in baseTextModels)
            {
                resultModel.TextModel.Add(row.Key, row.KoreanName);
            }

            File.WriteAllText(basePath + "result_kor.json", JsonConvert.SerializeObject(resultModel, Formatting.Indented));
        }

        static void PrintJsonEngFile(List<BaseMessageModel> baseMessageModels,
            List<BaseTextModel> baseTextModels)
        {
            string basePath = _basePath;
            ResultJsonModel resultModel = new ResultJsonModel();

            var tt = baseMessageModels.GroupBy(x => x.SubCode);

            var dicc = new Dictionary<string, Dictionary<string, object>>();

            foreach (var row in tt)
            {
                Dictionary<string, object> subModel = new Dictionary<string, object>();
                foreach (var name in row)
                {
                    subModel.Add(name.MainCode, name.EnglishName);
                }

                dicc.Add(row.Key, subModel);
            }

            resultModel.Message = dicc;

            foreach (var row in baseTextModels)
            {
                resultModel.TextModel.Add(row.Key, row.EnglishName);
            }

            File.WriteAllText(basePath + "result_eng.json", JsonConvert.SerializeObject(resultModel, Formatting.Indented));

        }

        static void Main(string[] args)
        {
            string basePath = _basePath;
            string strFile = basePath + "message.txt";
            
            List<BaseMessageModel> testMessageList = new List<BaseMessageModel>();

            using (FileStream fs = new FileStream(strFile, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.Default, false))
                {
                    string strLineValue = null;
                    string[] values = null;

                    while ((strLineValue = sr.ReadLine()) != null)
                    {
                        if (string.IsNullOrEmpty(strLineValue)) return;

                        strLineValue = strLineValue.Replace("\"", "");

                        values = strLineValue.Split('\t');

                        if (values[0] == "Sub Code") continue;

                        var textModel = new BaseMessageModel()
                        {   
                            SubCode = values[0],
                            MainCode = values[1],
                            KoreanName = values[2],
                            EnglishName = values[3]
                        };

                        testMessageList.Add(textModel);
                    }

                }
            }


            strFile = basePath + "text.txt";
            List<BaseTextModel> testModelList = new List<BaseTextModel>();
            

            using (FileStream fs = new FileStream(strFile, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.Default, false))
                {
                    string strLineValue = null;
                    string[] values = null;

                    while ((strLineValue = sr.ReadLine()) != null)
                    {
                        if (string.IsNullOrEmpty(strLineValue)) return;

                        strLineValue = strLineValue.Replace("\"", "");

                        values = strLineValue.Split('\t');

                        if (values[0] == "Key") continue;

                        //Console.WriteLine(strLineValue);

                        var textModel = new BaseTextModel()
                        {
                            Key = values[0],
                            EnglishName = values[0],
                            KoreanName = values[1],
                            
                        };

                        testModelList.Add(textModel);
                    }

                }
            }


            PrintJsonKorFile(testMessageList, testModelList);
            PrintJsonEngFile(testMessageList, testModelList);



        }
    }
}
