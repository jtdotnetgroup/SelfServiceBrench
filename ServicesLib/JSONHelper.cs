using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ServicesLib
{
    public class JSONHelper
    {
        public static DataTable JsonToDataTable(string jsonStr)
        {
            if (string.IsNullOrEmpty(jsonStr))
            {
                return null;
            }

            return null;

        }

        /// <summary>
        /// json字符串转换为实体类
        /// </summary>
        /// <typeparam name="T">返回的类型</typeparam>
        /// <param name="jsonStr">json字符串</param>
        /// <returns></returns>
        public static T JsonToObject<T>(string jsonStr) where T : class, new()
        {
            if (string.IsNullOrEmpty(jsonStr))
            {
                return null;
            }

           return JsonConvert.DeserializeObject<T>(jsonStr);
        }
    }
}
