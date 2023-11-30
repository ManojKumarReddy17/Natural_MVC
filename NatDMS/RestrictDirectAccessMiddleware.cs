using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


//public class RestrictDirectAccessAccessMiddleware
//{
//    private readonly RequestDelegate _next;

//    public RestrictDirectAccessAccessMiddleware(RequestDelegate next)
//    {
//        _next = next;
//    }

//    public async Task Invoke(HttpContext context)
//    {
//        if (!context.User.ide)

//            await _next(context);
//    }

//}
