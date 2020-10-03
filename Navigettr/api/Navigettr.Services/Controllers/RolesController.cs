using Navigettr.Core;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Navigettr.Services.Controllers
{
    public class RolesController : ApiController
    {
        private static readonly RoleRepository _repository = new RoleRepository();
        private static readonly IPartnerRepository _partnerRepository = new PartnerRepository();

        public class Role
        {
            public Int32 RoleId { get; set; }
            public string RoleName { get; set; }

        }

        [HttpPost]
        [HttpOptions]
        [Route("api/GetRoles")]

        public HttpResponseMessage GetRoles([FromBody]Role jsondata)
        {
            try
            {
                var data = _repository.GetRoles(jsondata.RoleId);

                if (data.Count > 0 && data != null)
                {

                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No Data found ");
                }


            }
            catch (Exception ex)
            {


                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "");
            }

        }
    }
}

