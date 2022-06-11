using System.ComponentModel.DataAnnotations;

namespace SelfCore.Hobbies.Services.Dto.RequestDto
{
    /// <summary>
    /// 登录
    /// </summary>
    public class Login
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        public string Code { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        public string Psw { get; set; }
    }
}
