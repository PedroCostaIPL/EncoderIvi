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

        public static async Task<PerEncDec.IVI.IVIMPDUDescriptions.IVIM> MakeRequest(string jsonID, Form1 form)
        {
            using HttpClient client = new();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            String jsonResponse = await client.GetStringAsync(form.getURL() + jsonID);

            Root deserializedJson = JsonConvert.DeserializeObject<Root>(jsonResponse);

            var f = JsonConvert.SerializeObject(deserializedJson, Formatting.Indented);
            form.setRichTextBox(f);

            //Call
            return Json2PerBitAdapter.Json2Bit(deserializedJson, form);
        }
    }
}
