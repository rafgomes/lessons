using System.Net;

namespace Monitor.Helpers{
    public class Requesters {
        public static async Task<HttpStatusCode> GetStatusFromUrl(string url){
            
            try{
                HttpClient client  = new HttpClient(); //faz as requisições
                HttpResponseMessage response = await client.GetAsync(url); //armazena as requisições 
                return response.StatusCode;
            }
            catch(HttpRequestException){
                return HttpStatusCode.NotFound;
            }




        }

    }
}