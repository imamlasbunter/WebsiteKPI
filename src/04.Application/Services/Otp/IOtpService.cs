using Pertamina.Website_KPI.Application.Services.Otp.Models.CreateOtp;

namespace Pertamina.Website_KPI.Application.Services.Otp;
public interface IOtpService
{
    CreateOtpResponse CreateOtp(string username);
    string GetCode(string secret, bool isTotp = true);
    byte[] GetGraphic(string otpUrl);
    bool VerifyCode(string secret, string code);
}
