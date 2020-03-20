using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace TeduCoreApp.Models
{
    public class GooglereCaptchaService
    {
        private GoogleSettings _setting;
        public GooglereCaptchaService(IOptions<GoogleSettings> setting)
        {
            _setting = setting.Value;
        }
        public virtual async Task<GooglereCaptchaRespo> VerifyCaptcha(string _Token)
        {
            GooglereCaptchaData data = new GooglereCaptchaData()
            {
                response = _Token,
                secret = _setting.reCaptcha_Secret_Key
            };
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync($"https://www.google.com/recaptcha/api/siteverify?=secret{data.secret}&response={data.response}");
            var capresp = JsonConvert.DeserializeObject<GooglereCaptchaRespo>(response);
            return capresp;
        }

    }
    public class GooglereCaptchaData
    {
        public string response { get; set; }
        public string secret { get; set; }
    }
    public class GooglereCaptchaRespo
    {
        public bool success { get; set; }
        public double score { get; set; }
        public string action { get; set; }
        public DateTime challenge_ts { get; set; }
        public string hostname { get; set; }
    }
}
