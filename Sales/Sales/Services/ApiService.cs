

namespace Sales.Services
{
    using Newtonsoft.Json;
    using Plugin.Connectivity;
    using Sales.Common.Models;
    using Sales.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    public class ApiService
    {

        /*Prueba la conexion a internet*/
        public async Task<Response> CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Response
                {

                    IsSucces = false,
                    Message = Languages.TurnOnInternet,
                };
            }

            var isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");

            if (!isReachable)
            {

                return new Response
                {
                    IsSucces = false,
                    Message = Languages.CheckInternet,

                };
            }

            return new Response
            {
                IsSucces = true,
                Message = "ok",

            };




        }

        #region Normal api
        /*Metodo Get List*/

        public async Task<Response> GetList<T>(string urlBase,
            string prefix, string controller)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = $"{prefix}{controller}";//string.Format("{0}{1}", prefix, controller);
                var response = await client.GetAsync(url);
                var answer = await response.Content.ReadAsStringAsync();
                //si falla
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {

                        IsSucces = false,
                        Message = answer,

                    };
                }
                //si es exitoso
                var list = JsonConvert.DeserializeObject<List<T>>(answer);
                return new Response
                {

                    IsSucces = true,
                    Message = "Ok",
                    Result = list,

                };

            }
            catch (Exception ex)
            {

                return new Response
                {

                    IsSucces = false,
                    Message = ex.Message,

                };
            }


        }



        /*Metodo Add Object*/
        public async Task<Response> Post<T>(string urlBase,
            string prefix, string controller, T model)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(request, Encoding.UTF8, "application/json");



                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = $"{prefix}{controller}";//string.Format("{0}{1}", prefix, controller);
                var response = await client.PostAsync(url, content);
                var answer = await response.Content.ReadAsStringAsync();
                //si falla
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {

                        IsSucces = false,
                        Message = answer,

                    };
                }
                //si es exitoso
                var obj = JsonConvert.DeserializeObject<T>(answer);
                return new Response
                {

                    IsSucces = true,
                    Message = "Ok",
                    Result = obj,

                };

            }
            catch (Exception ex)
            {

                return new Response
                {

                    IsSucces = false,
                    Message = ex.Message,

                };
            }


        }


        /*Metodo Delete Object*/
        public async Task<Response> Delete(string urlBase,
            string prefix, string controller, int id)
        {
            try
            {


                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = $"{prefix}{controller}/{id}";//string.Format("{0}{1}", prefix, controller);
                var response = await client.DeleteAsync(url);
                var answer = await response.Content.ReadAsStringAsync();
                //si falla
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {

                        IsSucces = false,
                        Message = answer,

                    };
                }
                //si es exitoso

                return new Response
                {

                    IsSucces = true,

                };

            }
            catch (Exception ex)
            {

                return new Response
                {

                    IsSucces = false,
                    Message = ex.Message,

                };
            }


        }


        /*Metodo Put Object*/
        public async Task<Response> Put<T>(string urlBase,
            string prefix, string controller, T model, int id)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(request, Encoding.UTF8, "application/json");



                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = $"{prefix}{controller}/{id}";//string.Format("{0}{1}", prefix, controller);
                var response = await client.PutAsync(url, content);
                var answer = await response.Content.ReadAsStringAsync();
                //si falla
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {

                        IsSucces = false,
                        Message = answer,

                    };
                }
                //si es exitoso
                var obj = JsonConvert.DeserializeObject<T>(answer);
                return new Response
                {

                    IsSucces = true,
                    Message = "Ok",
                    Result = obj,

                };

            }
            catch (Exception ex)
            {

                return new Response
                {

                    IsSucces = false,
                    Message = ex.Message,

                };
            }


        }


        #endregion


        #region Con Token

        /*con token*/
        public async Task<Response> GetList<T>(string urlBase,
           string prefix, string controller, string tokenType, string accessToken)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);


                var url = $"{prefix}{controller}";//string.Format("{0}{1}", prefix, controller);
                var response = await client.GetAsync(url);
                var answer = await response.Content.ReadAsStringAsync();
                //si falla
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {

                        IsSucces = false,
                        Message = answer,

                    };
                }
                //si es exitoso
                var list = JsonConvert.DeserializeObject<List<T>>(answer);
                return new Response
                {

                    IsSucces = true,
                    Message = "Ok",
                    Result = list,

                };

            }
            catch (Exception ex)
            {

                return new Response
                {

                    IsSucces = false,
                    Message = ex.Message,

                };
            }


        }

        /*Metodo Add Object*/
        public async Task<Response> Post<T>(string urlBase,
            string prefix, string controller, T model, string tokenType, string accessToken)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(request, Encoding.UTF8, "application/json");



                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                var url = $"{prefix}{controller}";//string.Format("{0}{1}", prefix, controller);
                var response = await client.PostAsync(url, content);
                var answer = await response.Content.ReadAsStringAsync();
                //si falla
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {

                        IsSucces = false,
                        Message = answer,

                    };
                }
                //si es exitoso
                var obj = JsonConvert.DeserializeObject<T>(answer);
                return new Response
                {

                    IsSucces = true,
                    Message = "Ok",
                    Result = obj,

                };

            }
            catch (Exception ex)
            {

                return new Response
                {

                    IsSucces = false,
                    Message = ex.Message,

                };
            }


        }


        /*Metodo Delete Object*/
        public async Task<Response> Delete(string urlBase,
            string prefix, string controller, int id, string tokenType, string accessToken)
        {
            try
            {


                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                var url = $"{prefix}{controller}/{id}";//string.Format("{0}{1}", prefix, controller);
                var response = await client.DeleteAsync(url);
                var answer = await response.Content.ReadAsStringAsync();
                //si falla
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {

                        IsSucces = false,
                        Message = answer,

                    };
                }
                //si es exitoso

                return new Response
                {

                    IsSucces = true,

                };

            }
            catch (Exception ex)
            {

                return new Response
                {

                    IsSucces = false,
                    Message = ex.Message,

                };
            }


        }


        /*Metodo Put Object*/
        public async Task<Response> Put<T>(string urlBase,
            string prefix, string controller, T model, int id, string tokenType, string accessToken)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
               

                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);

                var url = $"{prefix}{controller}/{id}";//string.Format("{0}{1}", prefix, controller);
                var response = await client.PutAsync(url, content);
                var answer = await response.Content.ReadAsStringAsync();
                //si falla
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {

                        IsSucces = false,
                        Message = answer,

                    };
                }
                //si es exitoso
                var obj = JsonConvert.DeserializeObject<T>(answer);
                return new Response
                {

                    IsSucces = true,
                    Message = "Ok",
                    Result = obj,

                };

            }
            catch (Exception ex)
            {

                return new Response
                {

                    IsSucces = false,
                    Message = ex.Message,

                };
            }


        }


        #endregion
        /*Login with token*/

        public async Task<TokenResponse> GetToken(
    string urlBase,
    string username,
    string password)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var response = await client.PostAsync("Token",
                    new StringContent(string.Format(
                    "grant_type=password&username={0}&password={1}",
                    username, password),
                    Encoding.UTF8, "application/x-www-form-urlencoded"));
                var resultJSON = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TokenResponse>(
                    resultJSON);
                return result;
            }
            catch
            {
                return null;
            }
        }


        /*saludo al usuario*/
        public async Task<Response> GetUser(string urlBase, string prefix, string controller, string email, string tokenType, string accessToken)
        {
            try
            {
                var getUserRequest = new GetUserRequest
                {
                    Email = email,
                };

                var request = JsonConvert.SerializeObject(getUserRequest);
                var content = new StringContent(request, Encoding.UTF8, "application/json");

                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                var url = $"{prefix}{controller}";
                var response = await client.PostAsync(url, content);
                var answer = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSucces = false,
                        Message = answer,
                    };
                }

                var user = JsonConvert.DeserializeObject<MyUserASP>(answer);
                return new Response
                {
                    IsSucces = true,
                    Result = user,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSucces = false,
                    Message = ex.Message,
                };
            }
        }



        /*integracion con redes sociales*/

        public async Task<FacebookResponse> GetFacebook(string accessToken)
        {
            var requestUrl = "https://graph.facebook.com/v2.8/me/?fields=name," +
                "picture.width(999),cover,age_range,devices,email,gender," +
                "is_verified,birthday,languages,work,website,religion," +
                "location,locale,link,first_name,last_name," +
                "hometown&access_token=" + accessToken;
            var httpClient = new HttpClient();
            var userJson = await httpClient.GetStringAsync(requestUrl);
            var facebookResponse =
                JsonConvert.DeserializeObject<FacebookResponse>(userJson);
            return facebookResponse;
        }



        public async Task<TokenResponse> LoginFacebook(string urlBase, string servicePrefix, string controller, FacebookResponse profile)
        {
            try
            {
                var request = JsonConvert.SerializeObject(profile);
                var content = new StringContent(
                    request,
                    Encoding.UTF8,
                    "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = $"{servicePrefix}{controller}";
                var response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var tokenResponse = await GetToken(
                    urlBase,
                    profile.Id,
                    profile.Id);
                return tokenResponse;
            }
            catch
            {
                return null;
            }
        }



        //public async Task<InstagramResponse> GetInstagram(string accessToken)
        //{
        //    var client = new HttpClient();
        //    var userJson = await client.GetStringAsync(accessToken);
        //    var InstagramJson = JsonConvert.DeserializeObject<InstagramResponse>(userJson);
        //    return InstagramJson;
        //}


        //public async Task<TokenResponse> LoginTwitter(string urlBase, string servicePrefix, string controller, TwitterResponse profile)
        //{
        //    try
        //    {
        //        var request = JsonConvert.SerializeObject(profile);
        //        var content = new StringContent(
        //            request,
        //            Encoding.UTF8,
        //            "application/json");
        //        var client = new HttpClient();
        //        client.BaseAddress = new Uri(urlBase);
        //        var url = $"{servicePrefix}{controller}";
        //        var response = await client.PostAsync(url, content);

        //        if (!response.IsSuccessStatusCode)
        //        {
        //            return null;
        //        }

        //        var tokenResponse = await GetToken(
        //            urlBase,
        //            profile.IdStr,
        //            profile.IdStr);
        //        return tokenResponse;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        //public async Task<TokenResponse> LoginInstagram(string urlBase, string servicePrefix, string controller, InstagramResponse profile)
        //{
        //    try
        //    {
        //        var request = JsonConvert.SerializeObject(profile);
        //        var content = new StringContent(
        //            request,
        //            Encoding.UTF8,
        //            "application/json");
        //        var client = new HttpClient();
        //        client.BaseAddress = new Uri(urlBase);
        //        var url = $"{servicePrefix}{controller}";
        //        var response = await client.PostAsync(url, content);

        //        if (!response.IsSuccessStatusCode)
        //        {
        //            return null;
        //        }

        //        var tokenResponse = await GetToken(
        //            urlBase,
        //            profile.UserData.Id,
        //            profile.UserData.Id);
        //        return tokenResponse;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}






















    }
}
