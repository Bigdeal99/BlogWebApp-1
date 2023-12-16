
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using infrastructure.DataModels;


using infrastructure.Repositories;

namespace service;


public class AdminService
{
    private readonly AdminRepository _adminRepository;

    public AdminService(AdminRepository adminRepository)
    {
        _adminRepository = adminRepository;
    }

    public async Task<Admin> GetAdminByIdAsync(int id)
    {
        return await _adminRepository.GetAdminByIdAsync(id);
    }
    public async Task<bool> AuthenticateAdminAsync(string modelUsername, string modelPassword)
    {
        throw new NotImplementedException();
    }
}
