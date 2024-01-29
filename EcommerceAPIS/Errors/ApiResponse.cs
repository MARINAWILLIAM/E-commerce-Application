using Microsoft.AspNetCore.Http;

namespace EcommerceAPIS.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public ApiResponse(int statuscode ,string? message=null)
        {
            StatusCode = statuscode;
            Message = message ?? GetDefaultMessForStatusCode(statuscode);
        }

        private string? GetDefaultMessForStatusCode(int statuscode)
        {
            return statuscode switch
            {
                
                400 => "BadRequest , you have made ",
                401 => "UnAuthorized, sorry you are Not",
                404 => "Resource Was Not Found",
                500 => "Errors are the path to the dark side. errors lead to anger . anger lead to hate. Hate leads to career change  ",
                _ => null
            };
        }
    }
}
