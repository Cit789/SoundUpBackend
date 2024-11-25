﻿using SoundUp.Contracts;
using SoundUp.Models;

namespace SoundUp.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<BaseUser?> GetUserById(Guid Id);
        Task<T?> GetUserById<T>(Guid Id) where T : BaseUser;
        
        Task<BaseUser?> GetUserByUserName(string UserName);
        Task<string> CreateUser<T>(CreateAuthorOrUserRequest createUserRequest) where T : BaseUser, new();
    }
}
