using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using TG.Exam.WebMVC.Models;

namespace TG.Exam.WebMVC.Controllers
{
    public class UsersApiController : ApiController
    {
        public JsonResult<List<User>> Get() => 
            Json(TG.Exam.WebMVC.Models.User.GetAll());
    }
}