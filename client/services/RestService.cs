using Services.Attr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;

namespace Services
{
    public delegate void Callback<T>(bool success, T result);
    public delegate void Callback(bool success);

    [ServiceUrl("http://localhost:50346/api")]
    public abstract class RestService
    {



        protected void Request<T>(Callback<T> callback, HttpMethod type, Dictionary<string, string> parameters = null, object data = null) where T:class
        {
            HttpClient http;
            Task<HttpResponseMessage> requestResponse;
            CreateResponse(type, parameters, data, out http, out requestResponse);


                requestResponse.ContinueWith(
                    responseTask =>
                    {

                        try
                        {
                            var content = responseTask.Result.Content;
                            content.ReadAsStringAsync().ContinueWith((task) =>
                            {
                                try
                                {
                                    var result = Deserialize<T>(task.Result);
                                    callback(true, result);
                                    http.Dispose();
                                }
                                catch (Exception ex)
                                {
                                    callback(false,null);
                                }
                            });
                        }
                        catch (Exception ex)
                        {
                            callback(false, null);
                        }

                    }
                );

    
        }




        protected void Request(Callback callback, HttpMethod type, Dictionary<string, string> parameters = null, object data = null)
        {
            HttpClient http;
            Task<HttpResponseMessage> requestResponse;
            CreateResponse(type, parameters, data, out http, out requestResponse);


            requestResponse.ContinueWith(
                responseTask =>
                {

                    try
                    {
                        var content = responseTask.Result.Content;
                        content.ReadAsStringAsync().ContinueWith((task) =>
                        {
                            try
                            {
                                callback(true);
                                http.Dispose();
                            }catch(Exception ex){
                                callback(false);
                            }
                        });
                    }
                    catch (Exception ex)
                    {
                        callback(false);
                    }

                }
            );


        }

        private void CreateResponse(HttpMethod type, Dictionary<string, string> parameters, object data, out HttpClient http, out Task<HttpResponseMessage> requestResponse)
        {
            var url = GetFullUrl(parameters);

            http = new System.Net.Http.HttpClient();

     
            
            http.Timeout = new TimeSpan(0, 0, 30);


            HttpRequestMessage request = new HttpRequestMessage(type, url);

            if (data != null)
            {
                request.Content = new StringContent(Serialize(data),Encoding.UTF8,"application/json");
            }

            requestResponse = http.SendAsync(request);
        }




        public static T Deserialize<T>(string json)
        {
            var _Bytes = Encoding.Unicode.GetBytes(json);
            using (MemoryStream _Stream = new MemoryStream(_Bytes))
            {
                var _Serializer = new DataContractJsonSerializer(typeof(T));
                return (T)_Serializer.ReadObject(_Stream);
            }
        }

        public static string Serialize(object instance)
        {
            using (MemoryStream _Stream = new MemoryStream())
            {
                var _Serializer = new DataContractJsonSerializer(instance.GetType());
                _Serializer.WriteObject(_Stream, instance);
                _Stream.Position = 0;
                using (StreamReader _Reader = new StreamReader(_Stream))
                { return _Reader.ReadToEnd(); }
            }
        }




        private string GetFullUrl()
        {
            var type = this.GetType();
            return type.GetTypeInfo().GetCustomAttribute<ServiceUrlAttribute>().Url + "/" + type.GetTypeInfo().GetCustomAttribute<ServiceControllerAttribute>().Controller;

        }

        private string GetFullUrl(Dictionary<string, string> parameters)
        {
            var url = GetFullUrl();
            if (parameters!=null&&parameters.ContainsKey("Id"))
            {
                url += "/" + parameters["Id"];
                parameters.Remove("Id");
            }

            var i = 0;
            if (parameters != null)
            {
                foreach (var kv in parameters)
                {
                    if (i == 0)
                    {
                        url += "?";
                    }
                    else
                    {
                        url += "&";
                    }

                    url += kv.Key + "=" + kv.Value;
                    i++;
                }
            }
            return url;
        }







    }
}
