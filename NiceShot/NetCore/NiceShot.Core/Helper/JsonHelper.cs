using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace NiceShot.Core.Helper
{
    public class JsonHelper
    {
        public static string JsonSerializer<T>(T t)
        {
            string jsonString;

            using (var ms = new MemoryStream())
            {
                var ser = new DataContractJsonSerializer(typeof(T));
                ser.WriteObject(ms, t);
                jsonString = Encoding.UTF8.GetString(ms.ToArray());
            }

            return jsonString;
        }

        public static T JsonDeserialize<T>(string jsonString)
        {
            try
            {
                var dser = new DataContractJsonSerializer(typeof(T));
                using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
                {
                    var obj = (T)dser.ReadObject(ms);
                    return obj;
                }
            }
            catch (SerializationException ex)
            {
                return default(T);
            }
        }

    }
}
