//using TestAPI.Services;

//public class TokenValidationMiddleware
//{
//    private readonly RequestDelegate _next;
//    private readonly ITokenService _tokenService;

//    public TokenValidationMiddleware(RequestDelegate next, ITokenService tokenService)
//    {
//        _next = next;
//        _tokenService = tokenService;
//    }

//    public async Task InvokeAsync(HttpContext context)
//    {
//        // Extract the token from the cookie
//        var refreshToken = context.Request.Cookies["refreshToken"];

//        if (!string.IsNullOrEmpty(refreshToken))
//        {
//            // Validate the token
//            var principal = _tokenService.GetPrincipalFromExpiredToken(refreshToken);
//            if (principal != null)
//            {
//                // Token is valid, attach the user to the HttpContext
//                context.User = principal;
//            }
//        }

//        // Continue processing the request
//        await _next(context);
//    }
//}
