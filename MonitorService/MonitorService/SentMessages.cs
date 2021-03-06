﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace MonitorService
{
    class SentMessages
    {
        private static readonly HttpClient httpClient;
        string host;
        static SentMessages()
        {
            httpClient = new HttpClient();
        }

        public SentMessages(string host)
        {
            this.host = host;
        }
        public async Task SendMetrics ( Messages.MetricsModel model)
        {
            await SendMessages("/api/metrics", model);
        }
        public async Task SendJobs(List<Messages.JobModel> model)
        {
            await SendMessages("/api/jobs", model);
        }
        public async Task SendLogs(Messages.LogModel model)
        {
            await SendMessages("/api/logs", model);
        }
        private async Task SendMessages<T>(string url, T model)
        {
            string body = JsonConvert.SerializeObject(model);
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await httpClient.PostAsync($"{this.host}{url}", content);
            await this.CheckResponce(responseMessage);
        }
        private async Task CheckResponce(HttpResponseMessage responseMessage)
        {
            if (responseMessage.IsSuccessStatusCode == false)
            {
                string responceBody = await responseMessage.Content.ReadAsStringAsync();
                Console.WriteLine(responseMessage.StatusCode);
                throw new Exception($"Can't send data. Status:{responseMessage.StatusCode}. Body:{responceBody}");
            }
        }
    }
}
