using System.Web;
using Application.DTOs.AuthDTOs;
using Application.Helpers;
using Application.Interfaces;
using Application.IServices.IAuthServices;
using Application.IServices.IEmailService;
using Application.ResultFolder;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services.AuthServices;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;
    private readonly IEmailService _emailService;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly ResetPasswordURL _resetPasswordURL;
    private readonly ApplicationDbContext _context;
    private readonly PasswordHasher<string> _passwordHasher = new PasswordHasher<string>();

    public AuthService(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        IUnitOfWork unitOfWork,
        IEmailService emailService,
        IOptions<ResetPasswordURL> resetPasswordURL,
        ApplicationDbContext context,
        ITokenService tokenService)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
        _unitOfWork = unitOfWork;
        _emailService = emailService;
        _tokenService = tokenService;
        _resetPasswordURL = resetPasswordURL.Value;
    }

    public Task<Result> AskResetPasswordByPhoneNumberAsync(string phoneNumber)
    {
        throw new NotImplementedException();
    }

    public Task<Result> ChangePasswordAsync(string userId, ChangePasswordDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<Result> ForgotPasswordAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task<Result> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Result> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<Result> LoginAsync(LoginDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<Result> RegisterAsync(RegisterDto model)
    {
        throw new NotImplementedException();
    }

    public Task<Result> ResendOtpAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task<Result> ResetPasswordAsync(VerifyOtpDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<Result> ResetPasswordByPhoneNumberAsync(VerifyOtpDto dto)
    {
        throw new NotImplementedException();
    }
}
