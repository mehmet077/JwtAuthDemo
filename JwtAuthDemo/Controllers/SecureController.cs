// Bu controller'ın bir API controller olduğunu belirtir
// Model binding, otomatik validation gibi özellikleri aktif eder
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]

// Route tanımı:
// Controller adı otomatik olarak route'a eklenir
// Örn: SecureController → api/secure
[Route("api/[controller]")]
public class SecureController : ControllerBase
{
    // Bu endpoint'e sadece JWT ile giriş yapmış kullanıcılar erişebilir
    [Authorize]

    // GET: api/secure/data
    [HttpGet("data")]
    public IActionResult GetSecureData()
    {
        // JWT içindeki Name claim'inden kullanıcının adını alır
        // Token doğrulandıysa User.Identity.Name dolu gelir
        var userName = User.Identity?.Name;

        // JWT içindeki Role claim'ini alıyoruz
        // ClaimTypes.Role, rol bazlı yetkilendirme için kullanılır
        var role = User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.Role)
            ?.Value;

        // Kullanıcıya özel bilgileri response olarak döner
        return Ok(new
        {
            message = "JWT ile giriş başarılı 🔐",
            user = userName,
            role = role
        });
    }

    // Bu endpoint'e sadece Role = "Admin" olan kullanıcılar erişebilir
    // JWT içindeki ClaimTypes.Role kontrol edilir
    [Authorize(Roles = "Admin")]

    // GET: api/secure/admin
    [HttpGet("admin")]
    public IActionResult AdminOnly()
    {
        // Eğer buraya geliyorsa:
        // - Token geçerli
        // - Kullanıcının rolü Admin
        return Ok("Admin yetkisi var 👑");
    }
}
