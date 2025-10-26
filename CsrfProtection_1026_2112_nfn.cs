// 代码生成时间: 2025-10-26 21:12:46
using System;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace CsrfProtectionDemo
{
    public class CsrfProtection
    {
        // 通过ASP.NET Cache存储CSRF Token
        private const string CacheKey = "CSRFToken";

        // 生成CSRF Token
        public static string GenerateToken()
        {
            // 使用SHA256生成Token
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()));
                return Convert.ToBase64String(bytes);
            }
        }

        // 验证CSRF Token
        public static bool ValidateToken(string providedToken)
        {
            try
            {
                // 从Cache中获取保存的Token
                var savedToken = (string)HttpContext.Current.Cache[CacheKey];

                // 验证提供的Token是否与Cache中的Token匹配
                return string.Equals(savedToken, providedToken, StringComparison.Ordinal);
            }
            catch (Exception ex)
            {
                // 错误处理
                Console.WriteLine($"Error validating CSRF token: {ex.Message}.");
                return false;
            }
        }

        // 设置CSRF Token到Cache
        public static void SetToken()
        {
            var token = GenerateToken();
            HttpContext.Current.Cache.Insert(CacheKey, token, null,
                DateTime.Now.AddMinutes(30), System.Web.Caching.Cache.NoSlidingExpiration);
        }
    }
}
