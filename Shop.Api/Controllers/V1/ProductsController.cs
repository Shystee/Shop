using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Contracts.V1;

namespace Shop.Api.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        [HttpGet(ApiRoutes.Products.GetAll)]
        public IActionResult GetAll()
        {
            return Ok("test");
        }
    }
}
