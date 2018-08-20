using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Xipu.Models;
using Xipu.Models.dto;

namespace Xipu.Controllers
{
    public class ActivityController : ApiController
    {
        private XipuDbContext _context = null;

        public ActivityController()
        {
            _context = new XipuDbContext();
        }

        [HttpGet]
        [Route("activity/static")]
        public IHttpActionResult Statics()
        {
            int chefCount = _context.Chefs.Count();
            dynamic x = new {totalUser = chefCount};
            return Ok(x);
        }

        [HttpPost]
        [Route("activity/join")]
        public IHttpActionResult Join(Chef chef)
        {
            ErrorMessage dto = new ErrorMessage();
            if (string.IsNullOrEmpty(chef.PhoneNumber))
            {
                dto.Status = 1;
                dto.Msg = "电话号码不能为空";
                return Ok(dto);
            }

            if (_context.Chefs.FirstOrDefault(c => c.PhoneNumber == chef.PhoneNumber) != null)
            {
                dto.Status = 1;
                dto.Msg = "该电话号码已参与成功";
                return Ok(dto);
            }

            chef.UserName = System.Web.HttpUtility.UrlDecode(chef.UserName);
            chef.Cuisine = System.Web.HttpUtility.UrlDecode(chef.Cuisine);
            chef.Entrypoint = System.Web.HttpUtility.UrlDecode(chef.Entrypoint);
            chef.Province = System.Web.HttpUtility.UrlDecode(chef.Province);
            chef.ClientIp = System.Web.HttpContext.Current.Request.UserHostAddress;

            _context.Chefs.Add(chef);
            _context.SaveChanges();

            dto.Msg = "参与成功";
            dto.Status = 0;
            return Ok(dto);

        }
    }
}
