using System;

namespace TaskManagementService.Application.Interfaces;

public interface ICurrentUserService
{
    Guid UserId { get; }
}