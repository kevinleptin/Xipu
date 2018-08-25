using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using Xipu.Models;
using System.IO;

namespace Xipu.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public FileResult Data(string token)
        {
            if (string.IsNullOrEmpty(token) || token != "meoexport")
            {
                return File(Encoding.UTF8.GetBytes("未授权"), "plain/text");
            }
            // https://blog.csdn.net/sxf359/article/details/72729870 
            var list = new List<Chef>();
            using (XipuDbContext _context = new XipuDbContext())
            {
                list = _context.Chefs.ToList();
            }
           
            var fileName = "DataInfo" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff");
            var strRows = new StringBuilder();
            strRows.Append("<table border=\"1\"><tr>");
            strRows.Append("<td style=\"mso-number-format:\\@;\">编号</td>");
            strRows.Append("<td style=\"mso-number-format:\\@;\">时间</td>");
            strRows.Append("<td style=\"mso-number-format:\\@;\">IP</td>");
            strRows.Append("<td style=\"mso-number-format:\\@;\">姓名</td>");
            strRows.Append("<td style=\"mso-number-format:\\@;\">电话</td>");
            strRows.Append("<td style=\"mso-number-format:\\@;\">省份</td>");
            strRows.Append("<td style=\"mso-number-format:\\@;\">菜系</td>");
            strRows.Append("<td style=\"mso-number-format:\\@;\">入口标记</td>");
            strRows.Append("</tr>");

            foreach (var item in list)
            {
                if (string.IsNullOrEmpty(item.ClientIp) ||
                    string.IsNullOrEmpty(item.PhoneNumber) ||
                    string.IsNullOrEmpty(item.Province) ||
                    string.IsNullOrEmpty(item.Cuisine) ||
                    string.IsNullOrEmpty(item.Entrypoint))
                {
                    continue;
                }

                strRows.Append("<tr>");
                strRows.Append("<td style=\"mso-number-format:\\@;\">" + item.Id + "</td>");
                strRows.Append("<td style=\"mso-number-format:\\@;\">" + item.CreateOn.ToString("yyyy-MM-dd HH:mm:ss") + "</td>");
                strRows.Append("<td style=\"mso-number-format:\\@;\">" + item.ClientIp + "</td>");
                strRows.Append("<td style=\"mso-number-format:\\@;\">" + (item.UserName ?? " ") + "</td>");
                strRows.Append("<td style=\"mso-number-format:\\@;\">" + item.PhoneNumber + "</td>");
                strRows.Append("<td style=\"mso-number-format:\\@;\">" + item.Province + "</td>");
                strRows.Append("<td style=\"mso-number-format:\\@;\">" + item.Cuisine + "</td>");
                strRows.Append("<td style=\"mso-number-format:\\@;\">" + item.Entrypoint + "</td>");
                strRows.Append("</tr>");


            }
            strRows.Append("</table>");
            var result = strRows.ToString();

            //生成字节数组
            var fileContents = Encoding.Default.GetBytes(result);
            //设置excel保存到服务器的路径 
            var filePath = Server.MapPath("~/excel/" + fileName + ".xls");
            //保存excel到指定路径
            System.IO.File.WriteAllBytes(filePath, fileContents);
            // FileManager.WriteBuffToFile(fileContents, filePath);
            //读取已有的excel文件输出到客户端供客户下载该excel文件
            return File(filePath, "application/ms-excel", fileName + ".xls");
        }
    }
}
