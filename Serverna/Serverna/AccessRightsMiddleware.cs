using Microsoft.EntityFrameworkCore;
using Serverna.Model;
using System.Linq;

namespace Serverna
{
    public class AccessRightsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AccessRightsMiddleware> _logger;

        public AccessRightsMiddleware(RequestDelegate next, ILogger<AccessRightsMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context, CursovaDbContext dbContext)
        {
            var path = context.Request.Path.Value.ToLower();
            if (!path.Contains("/auth"))
            {
                if (!context.User.Identity.IsAuthenticated)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Unauthorized");
                    return;
                }

                var userIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == "UserId");
                if (userIdClaim == null)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Unauthorized");
                    return;
                }

                var userId = int.Parse(userIdClaim.Value);
                var user = await dbContext.Users
                    .Include(u => u.Role)
                    .ThenInclude(r => r.AccessRights)
                    .FirstOrDefaultAsync(u => u.UserId == userId);

                if (user == null)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Unauthorized");
                    return;
                }

                var accessRights = user.Role.AccessRights.FirstOrDefault();
                if (accessRights == null)
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("Forbidden");
                    return;
                }


                var method = context.Request.Method.ToLower();

                bool hasAccess = false;

                if (path.Contains("/product"))
                {
                    if (method == "post") hasAccess = accessRights.CanCreateProduct;
                    else if (method == "put") hasAccess = accessRights.CanEditProduct;
                    else if (method == "delete") hasAccess = accessRights.CanDeleteProduct;
                    else hasAccess = true;
                }
                else if (path.Contains("/stockmovements") && (method == "post" || method == "put"))
                {
                    hasAccess = method == "post" ? accessRights.CanCreateStockMovement : accessRights.CanEditStockMovement;
                }
                else if (path.Contains("/stockmovements") && (method == "get"))
                {
                    hasAccess = true;
                }
                else if(path.Contains("/movementypes"))
                {
                    hasAccess = true;
                }
                else if(path.Contains("/buyer"))
                {
                    hasAccess = true;
                }
                else if (path.Contains("/orderstatus"))
                {
                    hasAccess = true;
                }
                else if(path.Contains("/category"))
                {
                    hasAccess = true;
                }
                else if(path.Contains("/suppliers"))
                {
                    hasAccess = true;
                }
                else if (path.Contains("/position"))
                {
                    hasAccess = true;
                }
                else if (path.Contains("/positionproduct"))
                {
                    hasAccess = true;
                }
                else if (path.Contains("/paymentmethod"))
                {
                    hasAccess = true;
                }
                else if (path.Contains("/orderdetail"))
                {
                    hasAccess = true;
                }
                else if (path.Contains("/orderstatus"))
                {
                    hasAccess = true;
                }
                else if (path.Contains("/order") && (method == "post" || method == "put"))
                {
                    hasAccess = method == "post" ? accessRights.CanCreateOrder : accessRights.CanEditOrder;
                }
                else if (path.Contains("/order") && (method == "get"))
                {
                    hasAccess = true;
                }
                else if (path.Contains("/payment") && (method == "post" || method == "put"))
                {
                    hasAccess = method == "post" ? accessRights.CanCreatePayment : accessRights.CanEditPayment;
                }
                else if (path.Contains("/payment") && (method == "get"))
                {
                    hasAccess = true;
                }

                else if (path.Contains("/users"))
                {
                    if (method == "put" || method == "patch" || method =="get")
                    {
                        var segments = path.Split('/');
                        if (segments.Length > 3 && int.TryParse(segments[3], out int targetUserId) && targetUserId == userId)
                        {
                            hasAccess = true;
                        }
                        else
                        {
                            hasAccess = accessRights.CanManageUsers;
                        }
                    }
                    else
                    {
                        hasAccess = accessRights.CanManageUsers;
                    }
                }
                else if (path.Contains("/role"))
                {
                    hasAccess = accessRights.CanManageRoles;
                }
                else if (path.Contains("/accessrights"))
                {
                    hasAccess = accessRights.CanManageAccessRights;
                }

                if (!hasAccess)
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    Console.WriteLine(path);
                    await context.Response.WriteAsync("Forbidden");
                    return;
                }

            }
            await _next(context);
        }
    }

 }
