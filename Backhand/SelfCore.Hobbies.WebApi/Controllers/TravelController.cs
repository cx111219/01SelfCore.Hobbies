using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SelfCore.Hobbies.Domains;
using SelfCore.Hobbies.Domains.Models;
using SelfCore.Hobbies.Services;
using System.IO;
using System.Linq;
using System.Text;

namespace SelfCore.Hobbies.WebApi.Controllers
{
    public class TravelController : BaseApiController<Travel>
    {
        private IWebHostEnvironment _web { get; }
        public TravelController(HobbyContext context, IWebHostEnvironment web) : base(context)
        {
            _web = web;
        }
        /// <summary>
        /// 条件检索
        /// </summary>
        /// <param name="query"></param>
        /// <param name="param"></param>
        protected override void QueryBefore(ref IQueryable<Travel> query, QueryParam param)
        {
            if (!string.IsNullOrWhiteSpace(param.Keyword))
                query = query.Where(t => t.Remark.Contains(param.Keyword));
        }

        /// <summary>
        /// 上传更新附图集合
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("pics/{id}")]
        public IActionResult UpdatePics(int id) {
            var files = Request?.Form?.Files;
            if (files == null || files.Count == 0)
                return Fail("请选择资源文件！");

            var travel = _context.Travels.Find(id);
            if(travel == null)
                return Fail("未找到实体对象！");
            StringBuilder paths = new();
            string basePath = Path.Combine(_web.WebRootPath, "api", files[0].Name);
            if (!Directory.Exists(basePath))
                Directory.CreateDirectory(basePath);
            foreach (var file in files)
            {
                string path = Path.GetFileNameWithoutExtension(file.FileName) + "_" + new System.Random().Next().ToString() + Path.GetExtension(file.FileName);
                FileStream fileStream = new FileStream(Path.Combine(Path.Combine(_web.WebRootPath, "api", file.Name.ToLower()), path), FileMode.CreateNew);
                file.CopyTo(fileStream);
                fileStream.Close();
                fileStream.Dispose();
                paths.Append($"api/{file.Name.ToLower()}/{path};");
            }
            string subPics = Request?.Form["subPics"].FirstOrDefault();
            travel.SubPics = $"{(string.IsNullOrWhiteSpace(subPics) ? "" : subPics)};{paths}".Trim(';');
            _context.Travels.Update(travel);
            _context.SaveChanges();
            return Success(travel.SubPics);
        }

    }
}
