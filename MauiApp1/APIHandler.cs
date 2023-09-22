using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MauiApp1
{
    public static class APIHandler
    {
        public static async Task<string> Request()
        {
            string output;


            using(HttpClient client = new HttpClient())
            {
                //!!! /armies NOT working. !!!
                string apiUrl = "https://aos-api.com/units";

                try
                {

                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();

                        output = responseBody;
                    }
                    else
                    {
                        HttpStatusCode statusCode = response.StatusCode;

                        output = "Error: "+statusCode;
                    }
                }
                catch (HttpRequestException ex)
                {
                    output = "Error: " + ex.Message;
                }
                catch(Exception ex)
                {
                    output = ex.Message;
                }
            }

            return output;
        }
    }
}
