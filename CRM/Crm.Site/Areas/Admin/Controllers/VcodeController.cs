using Crm.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Crm.Site.Areas.Admin.Controllers
{
    using Crm.WebHelper;

    [SkipCheckLogin]
    public class VcodeController : Controller
    {
        //
        // GET: /Admin/Vcode/

        public ActionResult GetVcode()
        {
            //1.0产生一个验证码字符串
            string vcode = Vcode(1);
            //2.0字符串存入session
            Session[Keys.vcode] = vcode;
            byte[] imagebuff;
            //3.0字符串画到画布上
            using (Image img = new Bitmap(65, 25))
            {
                using (Graphics g = Graphics.FromImage(img))
                {
                    //1.0清除位图背景
                    g.Clear(Color.White);
                    //2.0画矩形边框
                    g.DrawRectangle(Pens.Blue, 0, 0, img.Width - 1, img.Height - 1);

                    //3.0将字符串画到画布上
                    g.DrawString(vcode, new Font("黑体", 14, FontStyle.Bold | FontStyle.Strikeout), new SolidBrush(Color.Red), 0, 0);

                    //将图片以jpg的格式写入到内存流中 
                    using (MemoryStream ms = new MemoryStream())
                    {
                        img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        //图片流转换成byte数组
                        imagebuff = ms.ToArray();
                    }

                }
            }
            return File(imagebuff, "image/jpeg");
            //4.0将图片装换成byte[]
            return View();
        }
        Random r = new Random();
        private string Vcode(int num)
        {
            string[] arr = { "1", "2", "3", "4", "5", "6", "8", "9", "a", "b", "c" };
            string res = string.Empty;
            for (int i = 0; i < num; i++)
            {
                res += arr[r.Next(arr.Length)];
            }
            return res;
        }
    }
}
