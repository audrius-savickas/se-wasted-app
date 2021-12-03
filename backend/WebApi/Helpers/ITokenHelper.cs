using Contracts.DTOs;
using Domain.Entities;
using System;
namespace WebApi.Helpers
{
    public interface ITokenHelper
    {
        dynamic GenerateToken(Mail mail);
    }
}
