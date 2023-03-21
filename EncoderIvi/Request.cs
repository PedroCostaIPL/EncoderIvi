using Newtonsoft.Json;
using PerEncDec.IVI;
using System.Collections;
using System.Net.Http.Headers;
using EncoderIvi.Message;
using PerEncDec.IVI.IVIModule;
using System.ComponentModel;

namespace EncoderIvi
{
    public static class Request
    {

        public static async void MakeRequest(string jsonID)
        {
            using HttpClient client = new();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            async Task JsonHttpRequest(HttpClient client)
            {
                String myJsonResponse = await client.GetStringAsync(
                    "http://projeto-informatico2.test/api/ivim/json/" + jsonID);

                Root deserializedJson = JsonConvert.DeserializeObject<Root>(myJsonResponse);

                Json2PerBitAdapter.Json2Bit(deserializedJson);
            }
            await JsonHttpRequest(client);

            
        }
    }
}
