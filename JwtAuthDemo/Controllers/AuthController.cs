using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// Bu attribute controller'ın bir API controller olduğunu belirtir.
// Otomatik model validation, binding vs. sağlar.
[ApiController]

// Controller'ın route tanımı:
// Bu controller altındaki tüm endpoint'ler "api/auth" ile başlar.
[Route("api/auth")]
public class AuthController : ControllerBase
{
    // appsettings.json içindeki ayarlara erişmek için IConfiguration
    private readonly IConfiguration _configuration;

    // Constructor üzerinden IConfiguration dependency injection ile alınır
    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // POST: api/auth/login
    // Kullanıcı giriş endpoint'i
    [HttpPost("login")]
    public IActionResult Login(LoginModel mdl)
    {
        // JWT içine gömülecek kullanıcı bilgileri (Claims)
        // Bu bilgiler token decode edildiğinde okunabilir olur
        var claims = new[]
        {
            // Kullanıcının adı (User.Identity.Name olarak erişilir)
            new Claim(ClaimTypes.Name, mdl.Name),

            // Kullanıcının rolü (Authorize(Roles="Admin") gibi yerlerde kullanılır)
            new Claim(ClaimTypes.Role, mdl.Role)
        };

        // appsettings.json içindeki "Jwt" section'ını alıyoruz
        var jwt = _configuration.GetSection("Jwt");

        // JWT imzalamak için gizli anahtar (Secret Key)
        // UTF8 byte dizisine çevrilir
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwt["Key"]!)
        );

        // HS256 algoritması ile imzalama yapılacağını söylüyoruz
        var creds = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256
        );

        // JWT token oluşturuluyor
        var token = new JwtSecurityToken(
            issuer: jwt["Issuer"],        // Token'ı kim üretti
            audience: jwt["Audience"],    // Token kimler için geçerli
            claims: claims,               // Kullanıcıya ait bilgiler
            expires: DateTime.Now.AddMinutes(
                int.Parse(jwt["ExpireMinutes"]!) // Token süresi (dakika)
            ),
            signingCredentials: creds     // İmzalama bilgileri
        );

        // Token string (Bearer token) haline getirilir
        var tokenString = new JwtSecurityTokenHandler()
            .WriteToken(token);

        // Client'a token döndürülür
        return Ok(new
        {
            token = tokenString
        });
    }
}


public class LoginModel
{
    //Swager input alanında bulunmasını istemiyorsanız [SwaggerIgnore] yazmanız yeterli
    //Id propertysi içeriğine atadığımız  "Random.Shared.Next(1, int.MaxValue)" kodumuzun amacı random bir int değeri oluşturup otomatik atama yapması.

    //Not: Apinizde eğer model bazlı çalışma yapıyorsanız modeliniz içerisinde zorunlu olmayan alanları belirlemek için "?" kullanrak o modelin null olsa dahi çalışmsını sağlar.
    [SwaggerIgnore]
    public int Id { get; set; } = Random.Shared.Next(1, int.MaxValue);
    [SwaggerIgnore]
    public string? UserName { get; set; }
    public string? Name { get; set; }
    [SwaggerIgnore]
    public int? RoleId { get; set; }
    public string? Role { get; set; }
}
