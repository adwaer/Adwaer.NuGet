using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Adwaer.ApiContract.Net
{
    public class ContractService
    {
        public static async Task<string> GetFromHttp(string url)
        {
            var response = await new HttpClient()
                .GetStringAsync(url);

            return ParseJson(response);
        }

        public static string ParseJson(string data)
        {
            var jObject = JObject.Parse(data);
            return GenerateObject(jObject, "ResponseObject");
        }

        private static string GenerateObject(JObject jObject, string objectName = null)
        {
            var sb = new StringBuilder();
            List<string> objects = new List<string>();

            sb.AppendLine($"[DataContract(Name = \"{objectName}\")]");
            sb.AppendLine($"internal class {Capitalize(objectName)}");
            sb.AppendLine("{");

            foreach (var kvp in jObject)
            {
                string name = kvp.Key;
                JToken value = kvp.Value;

                sb.AppendLine($"\t[DataMember(Name = \"{name}\")]");
                if (value is JObject)
                {
                    sb.Append($"\tpublic {Capitalize(name)} {Capitalize(name)}");
                    sb.AppendLine(" { get; set; }");

                    objects.Add(GenerateScheme(value, name));
                }
                else if (value is JValue)
                {
                    sb.Append($"\tpublic string {Capitalize(name)}");
                    sb.AppendLine(" { get; set; }");
                }
                else if (value is JArray)
                {
                    var firstObject = value.FirstOrDefault();
                    if (firstObject == null)
                    {
                        sb.Append($"\tpublic string[] {Capitalize(name)}");
                    }
                    else
                    {
                        sb.Append($"\tpublic {Capitalize(name)}[] {Capitalize(name)}");
                        objects.Add(GenerateScheme(value[0], name));
                    }
                    sb.AppendLine(" { get; set; }");
                }
                //sb.AppendLine();

                //sb.AppendLine($"[DataContract(Name = \"{name}\"]");
                //sb.AppendLine($"internal class {Capitalize(name)}");
                //sb.AppendLine("{");

                //foreach (var valueToken in value)
                //{
                //    var prop = valueToken as JProperty;
                //    if (prop != null)
                //    {
                //        objects.Add(GenerateScheme(prop.Value, prop.Name));
                //    }
                //}

                //sb.AppendLine("}");
                //sb.AppendLine();
            }

            sb.AppendLine("}");
            sb.AppendLine();

            foreach (var o in objects)
            {
                sb.AppendLine(o);
            }

            return sb.ToString();
        }

        private static string GenerateValue(JValue jvalue, string name)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"\t[DataMember(Name = \"{name}\")]");
            sb.Append($"\tpublic string {Capitalize(name)}");
            sb.AppendLine(" { get; set; }");

            return sb.ToString();
        }

        private static string GenerateArray(JArray jarray, string property)
        {
            return "";
        }

        private static string GenerateScheme(JToken jtoken, string objectName = null)
        {
            if (jtoken is JObject)
            {
                return GenerateObject((JObject)jtoken, objectName);
            }
            if (jtoken is JValue)
            {
                return GenerateValue((JValue)jtoken, objectName);
            }
            if (jtoken is JArray)
            {
                return GenerateArray((JArray)jtoken, objectName);
            }
            return null;
        }

        private static string Capitalize(string text)
        {
            return $"{text[0].ToString().ToUpperInvariant()}{text.Substring(1)}";
        }
    }
}
