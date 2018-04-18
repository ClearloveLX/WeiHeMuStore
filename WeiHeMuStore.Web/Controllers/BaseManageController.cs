using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WeiHeMuStore.Model;
using WeiHeMuStore.Model.BaseModel;
using WeiHeMuStore.Web.Models;

namespace WeiHeMuStore.Web.Controllers
{
    public class BaseManageController : Controller
    {
        private readonly WeiHeMuStoreDBContext _context;
        private readonly PySelfSetting _pySelfSetting;
        private readonly IHostingEnvironment _hostingEnvironment;
        public BaseManageController(WeiHeMuStoreDBContext context, IOptions<PySelfSetting> pySelfSetting, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _pySelfSetting = pySelfSetting.Value;
            _hostingEnvironment = hostingEnvironment;
        }
        #region 菜品类别

        public IActionResult ProductCate()
        {
            return View();
        }

        public JsonResult ProductCateData(int limit, int offset, string name)
        {
            var list = _context.ProductCates.ToList();
            if (!string.IsNullOrWhiteSpace(name))
            {
                list = list.Where(e => e.ProductCateName.Contains(name)).ToList();
            }
            var total = list.Count;
            if (total > offset)
            {
                list = list.Skip(offset).Take(limit).ToList();
            }

            return Json(new { total, rows = list });
        }

        public IActionResult ProductCateAdd()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ProductCateAdd(string name)
        {
            if (_context.ProductCates.Any(b => b.ProductCateName.ToUpper() == name.Trim().ToUpper()))//验证名称是否重复
            {
                return Json(new { state = 2, msg = "此菜品已存在！" });
            }

            ProductCate _productCate = new ProductCate
            {
                ProductCateName = name
            };
            _context.Add(_productCate);
            var num = _context.SaveChanges();
            if (num > 0)
            {
                return Json(new { state = 1, msg = "保存成功！" });
            }
            return Json(new { state = 0, msg = "保存失败！" });
        }

        [HttpPost]
        public JsonResult ProductCateEdit(ProductCate productCate)
        {
            var list = _context.ProductCates.Where(b => b.ProductCateId.Equals(productCate.ProductCateId)).FirstOrDefault();
            if (list != null)
            {
                list.ProductCateName = productCate.ProductCateName;
            }
            var num = _context.SaveChanges();
            return Json(new { state = num });
        }

        [HttpPost]
        public JsonResult ProductCateRemove(int id)
        {
            var list = _context.ProductCates.Where(b => b.ProductCateId.Equals(id)).FirstOrDefault();
            if (list != null)
            {
                _context.ProductCates.Remove(list);
            }
            var num = _context.SaveChanges();
            return Json(new { start = num });
        }

        #endregion

        #region 菜品管理

        public IActionResult Product()
        {
            return View();
        }

        public JsonResult ProductData(int limit, int offset, string name)
        {
            var list = _context.Products.ToList();
            if (!string.IsNullOrWhiteSpace(name))
            {
                list = list.Where(e => e.ProductName.Contains(name)).ToList();
            }
            var total = list.Count;
            if (total > offset)
            {
                list = list.Skip(offset).Take(limit).ToList();
            }

            return Json(new { total, rows = list });
        }

        public IActionResult ProductAdd()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ProductAdd(string name)
        {
            if (_context.Products.Any(b => b.ProductName.ToUpper() == name.Trim().ToUpper()))
            {
                return Json(new { state = 2, msg = "此菜品已存在！" });
            }

            Product _product = new Product
            {
                ProductName = name
            };
            _context.Add(_product);
            var num = _context.SaveChanges();
            if (num > 0)
            {
                return Json(new { state = 1, msg = "保存成功！" });
            }
            return Json(new { state = 0, msg = "保存失败！" });
        }

        public IActionResult ProductImage()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ProductImages()
        {
            var file = Request.Form.Files.Where(b => b.Name == "myHeadPhoto" && b.ContentType.Contains("image")).SingleOrDefault();
            if (file == null)
            {
                return Json(new { state = 0, msg = "无法找到图片！" });
            }
            var fileExtend = file.FileName.Substring(file.FileName.LastIndexOf('.'));
            var fileNewName = $"{DateTime.Now.ToString("yyyyMMddhhmmssfff")}{fileExtend}";
            var path = Path.Combine($"{_hostingEnvironment.WebRootPath}/{_pySelfSetting.ContentPhotoPath}", fileNewName);
            using (var stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                file.CopyTo(stream);
            }
            var viewPath = $"{_pySelfSetting.ContentPhotoPath}/{fileNewName}";


            return Json(new { state = 1, msg = "上传成功！", path = viewPath });
        }

        #endregion
    }
}