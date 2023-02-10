using System;
using Newtonsoft.Json;
using RestSharp;

namespace Ventas_Tostatronic.Services
{
	public class WebService
	{
        public static Response GetDataNodeNoAsync(string url, string criterialSearch)
        {
            url += criterialSearch;
            try
            {
                var client = new RestClient(URLData.baseURLNET);
                var request = new RestRequest($"{url}{criterialSearch}", Method.Get);
                var response = client.Execute(request);
                Response result;
                if (response.IsSuccessful)
                    result = JsonConvert.DeserializeObject<Response>(response.Content);
                else
                    result = new Response() { succes = false, message = response.ErrorMessage, statusCode = 404 };
                return result;
            }
            catch (TimeoutException e)
            {
                Response result = new Response();
                result.succes = false;
                result.message = "Tiempo de espera agotado... " + e.Message;
                return result;
            }
            catch (Exception e)
            {
                Response result = new Response();
                result.succes = false;
                result.message = "Error... " + e.Message;
                return result;
            }
        }
        public static async Task<Response> GetDataNode(string url, string criterialSearch)
        {
            url += criterialSearch;
            try
            {
                var client = new RestClient(URLData.baseURLNET);
                var request = new RestRequest($"{url}{criterialSearch}", Method.Get);
                var response = await client.ExecuteAsync(request);
                Response result;
                if (response.IsSuccessful)
                    result = JsonConvert.DeserializeObject<Response>(response.Content);
                else
                    result = new Response() { succes = false, message = response.ErrorMessage, statusCode = 404 };
                return result;
            }
            catch (TimeoutException e)
            {
                Response result = new Response();
                result.succes = false;
                result.message = "Tiempo de espera agotado... " + e.Message;
                return result;
            }
            catch (Exception e)
            {
                Response result = new Response();
                result.succes = false;
                result.message = "Error... " + e.Message;
                return result;
            }
        }
        public static async Task<Response> InsertData(object newPI, string url)
        {
            try
            {
                var client = new RestClient(URLData.baseURLNET);
                var request = new RestRequest($"{url}", Method.Post);
                string data = JsonConvert.SerializeObject(newPI);
                request.AddParameter("application/json", data, ParameterType.RequestBody);
                var response = await client.ExecuteAsync(request);
                Response result;
                if (response.IsSuccessful)
                    result = JsonConvert.DeserializeObject<Response>(response.Content);
                else
                    result = JsonConvert.DeserializeObject<Response>(response.Content);
                return result;
            }
            catch (TimeoutException e)
            {
                Response result = new Response();
                result.succes = false;
                result.message = "Tiempo de espera agotado... " + e.Message;
                return result;
            }
            catch (Exception e)
            {
                Response result = new Response();
                result.succes = false;
                result.message = "Error... " + e.Message;
                return result;
            }
        }
    }
}

