using MediatR;

namespace TaskManagementService.Application.Features.Tasks.Commands.CreateTask;

// Bu komut işlendikten sonra, oluşturulan yeni görevin ID'sini geri dönmek
// kullanışlıdır. Bu yüzden IRequest<Guid> kullanıyoruz.
public class CreateTaskCommand : IRequest<Guid>
{
    public string Name { get; set; }
}