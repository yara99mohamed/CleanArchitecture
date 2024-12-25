using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Data.ViewData;

namespace SchoolProject.Core.Feature.Authorization.Commands.Models
{
    public class EditRoleCommand : EditRoleViewData, IRequest<Response<string>>
    {

    }
}
