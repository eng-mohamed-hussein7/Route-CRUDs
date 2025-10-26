﻿using Application.IRepositories;

namespace Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<T> Repository<T>() where T : class;

    Task<int> SaveChangesAsync();
    int SaveChanges();
}
