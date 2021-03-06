using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SelfCore.Hobbies.Domains;
using SelfCore.Hobbies.Domains.Models;
using SelfCore.Hobbies.Services;
using SelfCore.Hobbies.Services.Dto.RequestDto;
using SelfCore.Hobbies.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace SelfCore.Hobbies.WebApi.Controllers
{
    public class UserController : BaseApiController<User>
    {
        private IConfiguration _configuration { get; }
        private IWebHostEnvironment _web { get; }
        public UserController(HobbyContext context, IConfiguration configuration,IWebHostEnvironment web) : base(context)
        {
            _configuration = configuration;
            _web = web;
        }
        /// <summary>
        /// 条件检索
        /// </summary>
        /// <param name="query"></param>
        /// <param name="param"></param>
        protected override void QueryBefore(ref IQueryable<User> query, QueryParam param)
        {
            if (!string.IsNullOrWhiteSpace(param.Keyword))
                query = query.Where(t => t.Name.Contains(param.Keyword) || t.Code.Contains(param.Keyword));
        }

        protected override void AddBeforAsync(User entity)
        {
            var user = _context.Users.FirstOrDefault(t => t.Code == entity.Code && !t.IsDeleted);
            if (user != null)
                throw new Exception($"已存在名为 {entity.Code} 的用户！");
            entity.Psd = Encrypt.MD5Encrypt(entity.Psd.Trim(), System.Text.Encoding.Default);
            entity.IsAdmin = false;
        }

        protected override void UpdateBeforeAsync(User entity)
        {
            var user = _context.Users.FirstOrDefault(t => t.Code == entity.Code && t.Id != entity.Id && t.IsDeleted);
            if (user != null)
                throw new Exception($"已存在名为 {entity.Code} 的用户！");
            // 重置密码另外实现
            //if (oldEntity.Psd != entity.Psd)
            //{
            //    entity.Psd = Encrypt.MD5Encrypt(entity.Psd.Trim(), System.Text.Encoding.Default);
            //}
        }

        /// <summary>
        /// 用户账号登录
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody][NotNull] Login login)
        {
            var user = _context.Users
                .FirstOrDefault(t => t.Code == login.Code.Trim() && t.Psd == Encrypt.MD5Encrypt(login.Psw, System.Text.Encoding.UTF8));
            if (user == null)
                return Fail("用户名/密码错误！");
            return GetToken(user);
        }

        /// <summary>
        /// 生成token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private IActionResult GetToken(User user)
        {
            Claim[] claims = new[] {
                new Claim("userId",user.Id.ToString()),
                new Claim("userName" , user.Code)
            };
            var secretByte = Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]);
            //使用非对称算法对私钥进行加密
            var signingKey = new SymmetricSecurityKey(secretByte);
            //使用HmacSha256来验证加密后的私钥生成数字签名
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            //生成Token
            var Token = new JwtSecurityToken(
                    issuer: _configuration["Authentication:Issuer"],        //发布者
                    audience: _configuration["Authentication:Audience"],    //接收者
                    claims: claims,                                         //存放的用户信息
                    notBefore: DateTime.UtcNow,                             //发布时间
                    expires: DateTime.UtcNow.AddDays(1),                    //有效期设置为1天
                    signingCredentials                                      //数字签名
                );
            //生成字符串token
            var TokenStr = new JwtSecurityTokenHandler().WriteToken(Token);
            return Success(new { user, Token = TokenStr });
        }

        /// <summary>
        /// 上传资源
        /// </summary>
        /// <param name="type">资源类型</param>
        /// <param name="file">文件</param>
        /// <returns></returns>
        [HttpPost("upload")]
        public IActionResult Upload() { 
            var files = Request?.Form?.Files;
            if (files == null || files.Count == 0)
                return Fail("请选择资源文件！");
            Dictionary<string, string> paths = new();
            files.ToList().GroupBy(t => t.Name).ToList().ForEach(t =>
            {
                string basePath = Path.Combine(_web.WebRootPath, "api", t.Key.ToLower());
                if (!Directory.Exists(basePath))
                    Directory.CreateDirectory(basePath);
            });
            foreach (var file in files)
            {
                string path = Path.GetFileNameWithoutExtension(file.FileName) + "_" + new Random().Next().ToString() + Path.GetExtension(file.FileName);
                FileStream fileStream = new FileStream(Path.Combine(Path.Combine(_web.WebRootPath, "api",file.Name.ToLower()), path), FileMode.CreateNew);
                file.CopyTo(fileStream);
                fileStream.Close();
                fileStream.Dispose();
                paths.Add(file.Name ,$"api/{file.Name.ToLower()}/{path}");
            }
            return Success(paths);
        }

    }
}