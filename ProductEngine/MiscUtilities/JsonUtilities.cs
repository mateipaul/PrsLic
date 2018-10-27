using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiscUtilities
{
    public class JsonUtilities
    {
        private static JsonUtilities _instance;

        private JsonUtilities()
        {

        }

        public static JsonUtilities Instance
        {
            get
            {
                return _instance ?? (_instance = new JsonUtilities());
            }
        }


        public T DeserializeJson<T>(string textToParse)
        {
            try
            {
                if (string.IsNullOrEmpty(textToParse)) return default(T);

                JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings { MissingMemberHandling = MissingMemberHandling.Ignore };

                T jsonModel = JsonConvert.DeserializeObject<T>(textToParse, jsonSerializerSettings);

                return jsonModel;
            }
            catch (Exception Ex)
            {
                return default(T);
            }
        }
        public string SerializeObject(object obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            return json;
        }
    }
}
