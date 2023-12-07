using infrastructure.Repositories;

namespace service;

using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using infrastructure.DataModels;

public class AdminService
{
    private readonly BlogRepository _blogRepository;

    public AdminService(BlogRepository blogRepository)
    {
        _blogRepository = blogRepository;
    }

    public async Task<bool> AuthenticateAdminAsync(string username, string password)
    {
        var admin = await _blogRepository.GetAdminByUsernameAsync(username);

        if (admin != null)
        {
            if (VerifyPasswordHash(password, admin.PasswordHash))
            {
                // Password is correct, reset failed login attempts
                admin.FailedLoginAttempts = 0;
                await _blogRepository.UpdateAdminAsync(admin);

                return true;
            }
            else
            {
                // Incorrect password, increment failed login attempts
                admin.FailedLoginAttempts++;
                await _blogRepository.UpdateAdminAsync(admin);

                return false;
            }
        }

        // Admin not found
        return false;
    }

    private bool VerifyPasswordHash(string password, string storedHash)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            var hashedPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

            return hashedPassword == storedHash;
        }
    }
}
