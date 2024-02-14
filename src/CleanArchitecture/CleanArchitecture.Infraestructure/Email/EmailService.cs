using CleanArchitecture.Application.Abstractions.Email;
using CleanArchitecture.Domain.Users;

namespace CleanArchitecture.Infraestructure;

internal sealed class EmailService : IEmailService
{
    public Task SendAsync(Email recipient, string subject, string body)
    {
        // throw new NotImplementedException();  // Aqui se debe añadir la logica para enviar en email sendGrid, Gmail,Etc
        return Task.CompletedTask;
    }
}