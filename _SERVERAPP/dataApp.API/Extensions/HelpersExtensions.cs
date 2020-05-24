using Microsoft.AspNetCore.Http;

namespace dataApp.API.Extensions
{
    //!une classe statique ne peut pas être instanciée
    public static class HelpersExtensions
    {
         public static void AddApplicationError(this HttpResponse response , string message){
                        response.Headers.Add("Application-error", message);
                        response.Headers.Add("Access-Control-Expose-Headers", "Application-error");
                        response.Headers.Add("Access-Control-Allow-Origin", "*");

        }
    }
}