using Microsoft.AspNetCore.Mvc;
using SelfCore.Hobbies.Domains;
using SelfCore.Hobbies.Domains.Models;
using SelfCore.Hobbies.Services;
using SelfCore.Hobbies.Services.Dto.RequestDto;
using SelfCore.Hobbies.Services.Helpers;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace SelfCore.Hobbies.WebApi.Controllers
{
    public class UserController : BaseApiController<User>
    {
        public UserController(HobbyContext context) : base(context)
        {
        }
        /// <summary>
        /// 条件检索
        /// </summary>
        /// <param name="query"></param>
        /// <param name="param"></param>
        protected override void QueryBefore(IQueryable<User> query, QueryParam param)
        {
            if (!string.IsNullOrWhiteSpace(param.Keyword))
            {
                query = query.Where(t => t.Name.Contains(param.Keyword) || t.Code.Contains(param.Keyword));
            }
        }

        protected override void AddBeforAsync(User entity)
        {
            var user = _context.Users.FirstOrDefault(t => t.Code == entity.Code);
            if (user != null)
                throw new Exception($"已存在名为 {entity.Code} 的用户！");
            entity.Psd = Encrypt.MD5Encrypt(entity.Psd.Trim(), System.Text.Encoding.Default);
        }

        protected override void UpdateBeforeAsync(User oldEntity, User entity)
        {
            var user = _context.Users.FirstOrDefault(t => t.Code == entity.Code && t.Id != entity.Id);
            if (user != null)
                throw new Exception($"已存在名为 {entity.Code} 的用户！");
            if (oldEntity.Psd != entity.Psd)
            {
                entity.Psd = Encrypt.MD5Encrypt(entity.Psd.Trim(), System.Text.Encoding.Default);
            }
        }

        /// <summary>
        /// 用户账号登录
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public IActionResult Login([FromBody][NotNull] Login login)
        {
            var user = _context.Users
                .FirstOrDefault(t => t.Code == login.Code.Trim() && t.Psd == Encrypt.MD5Encrypt(login.Psw, System.Text.Encoding.UTF8));

            return user == null ? Fail("用户名/密码错误！") : Success(user);
        }
    }
}