﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CloudTrader.Api.Service.Helpers
{
    static class HttpResponseMessageExtensions
    {
        public static async Task<T> ReadAsJson<T>(this HttpResponseMessage message)
        {
            return JsonConvert.DeserializeObject<T>(
                await message.Content.ReadAsStringAsync()
            );
        }
    }
}
