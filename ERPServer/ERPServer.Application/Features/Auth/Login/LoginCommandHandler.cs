using ERPServer.Application.Services;
using ERPServer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TS.Result;

namespace ERPServer.Application.Features.Auth.Login;

internal sealed class LoginCommandHandler(
    UserManager<AppUser> userManager,
    SignInManager<AppUser> signInManager,
    IJwtProvider jwtProvider) : IRequestHandler<LoginCommand, Result<LoginCommandResponse>>
{
    public async Task<Result<LoginCommandResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        AppUser? user = await userManager.FindByNameAsync(request.EmailOrUserName) ?? await userManager.FindByEmailAsync(request.EmailOrUserName);

        if (user == null)
        {
            return (500, "Kullanıcı bulunamadı!");
        }

        SignInResult signInResult = await signInManager.CheckPasswordSignInAsync(user, request.Password, true);

        if (signInResult.IsLockedOut)
        {
            TimeSpan? timeSpan = user.LockoutEnd - DateTime.UtcNow;
            if (timeSpan is not null)
            {
                return (500, $"Şifrenizi 3 defa yanlış girdiğiniz için kullanıcı girişi {Math.Ceiling(timeSpan.Value.TotalMinutes)} dakika süreyle bloke edilmiştir.");
            }
            else
            {
                return (500, "Kullanıcı girişinde 3 kez yanlış şifre girişi yaptığınız için giriş istekleriniz 5 dakika süreyle bloke edilmiştir!");
            }
        }

        if (signInResult.IsNotAllowed)
        {
            return (500, "Mail adresiniz onaylanmamış durumdadır! Lütfen devam edebilmek için mail adresinizi onaylayınız.");
        }

        if (!signInResult.Succeeded)
        {
            return (500, "Kullanıcı şifresi yanlış!");
        }

        var loginResponse = await jwtProvider.CreateToken(user);

        return loginResponse;
    }
}
