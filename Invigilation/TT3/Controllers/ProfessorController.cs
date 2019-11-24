using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TT3.Helpers;
using TT3.Models;

namespace TT3.Controllers
{
    public class ProfessorController : ApiController
    {
        Repository r = new Repository();
        [HttpGet]
        public HttpResponseMessage SelectAllProfessor(string UserGroup)
        {
            var response = Request.CreateResponse(HttpStatusCode.OK, r.GetListOfProfessor(UserGroup));
            return response;
        }

        [HttpPost]
        public HttpResponseMessage SaveRoomList([FromBody]InputOfSave data)
        {
            var datastr = System.Uri.UnescapeDataString(data.Data);
            var response = Request.CreateResponse(HttpStatusCode.OK, r.SaveRoomList(datastr,data.UserGroup));
            return response;
        }

        [HttpGet]
        public HttpResponseMessage SelectAllRomList(string UserGroup)
        {
            var response = Request.CreateResponse(HttpStatusCode.OK, r.GetListOfRoom(UserGroup));
            return response;
        }

        [HttpPost]
        public HttpResponseMessage SaveProfessorList([FromBody]InputOfSave data)
        {

            var datastr = System.Uri.UnescapeDataString(data.Data);
            var response = Request.CreateResponse(HttpStatusCode.OK, r.SaveProfessorList(datastr,data.UserGroup));
            return response;
        }
        [HttpGet]
        public HttpResponseMessage SelectAllExamList(string UserGroup)
        {
            var response = Request.CreateResponse(HttpStatusCode.OK, r.GetAllExamList(UserGroup));
            return response;
        }

        [HttpPost]
        public HttpResponseMessage SaveExamList([FromBody]InputOfSave data)
        {
            var datastr = System.Uri.UnescapeDataString(data.Data);
            var response = Request.CreateResponse(HttpStatusCode.OK, r.SaveExamList(datastr,data.UserGroup));
            return response;
        }
        [HttpGet]
        public HttpResponseMessage SelectAllItem(string FileName,string UserGroup)
        {
            var response = Request.CreateResponse(HttpStatusCode.OK, r.GetAllCommonList(FileName, UserGroup));
            return response;
        }

        [HttpPost]
        public HttpResponseMessage SaveAllItem([FromBody]InputOfSave1 data)
        {
            var datastr = System.Uri.UnescapeDataString(data.Data);
            var response = Request.CreateResponse(HttpStatusCode.OK, r.SaveCommonFile(datastr, data.FileName,data.UserGroup));
            return response;
        }
        [HttpPost]
        public HttpResponseMessage CheckLogin(User user)
        {
            var response = Request.CreateResponse(HttpStatusCode.OK,r.CheckLoginUser(user));
            return response;
        }
    }
    public class InputOfSave
    {

        public string Data { get; set; }
        public string   UserGroup { get; set; }
    }
    public class InputOfSave1
    {
        public string FileName { get; set; }
        public string Data { get; set; }
        public string UserGroup { get; set; }
    }
}
