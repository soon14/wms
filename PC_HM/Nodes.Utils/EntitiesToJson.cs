using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Nodes.Utils
{
    public class EntitiesToJson
    {
        /// <summary>   
        /// 对象转换为Json字符串   
        /// </summary>   
        /// <param name="jsonObject">对象</param>   
        /// <returns>Json字符串</returns>   
        public static string ToJson(object jsonObject)
        {
            string _json = string.Empty;
            // 序列化为JSON字串
            if(jsonObject != null)
                _json = JsonConvert.SerializeObject(jsonObject);

            return _json;
        }
    }
}
