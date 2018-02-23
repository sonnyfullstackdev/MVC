using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProjectName.Controllers.Api
{
    public class BaseApiController : ApiController
    {
        protected bool IsValidRequest(object model)
        {
            bool isValid = true;

            if (model == null)
            {
                isValid = false;
            }
            else if (!ModelState.IsValid)
            {
                isValid = false;
            }

            return isValid;
        }
    }
}
