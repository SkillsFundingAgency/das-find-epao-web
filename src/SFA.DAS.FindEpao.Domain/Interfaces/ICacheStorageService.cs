﻿using System.Threading.Tasks;

namespace SFA.DAS.FindEpao.Domain.Interfaces
{
    public interface ICacheStorageService
    {
        Task<T> RetrieveFromCache<T>(string key);
        Task SaveToCache<T>(string key, T item, int expirationInHours);
        Task DeleteFromCache(string key);
    }
}
