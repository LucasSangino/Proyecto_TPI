﻿using Dsw2025Ej15.Domain;
using Dsw2025Ej15.Domain.Entities;
using System.Linq.Expressions;
using System.Text.Json;

namespace Dsw2025Ej15.Data;

public class InMemory : IRepository
{
    private List<Product>? _products;
    private List<Category>? _categories;

    public InMemory()
    {
        LoadProducts();
    }

    private void LoadProducts()
    {
        var json = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "Sources\\products.json"));
        _products = JsonSerializer.Deserialize<List<Product>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        });
    }

    private List<T>? GetList<T>() where T : EntityBase
    {
        return typeof(T).Name switch
        {
            nameof(Product) => _products as List<T>,
            nameof(Category) => _categories as List<T>,
            _ => throw new NotSupportedException(),
        };
    }

    public async Task<T?> GetById<T>(Guid id, params string[] include) where T : EntityBase
    {
        return await Task.FromResult(GetList<T>()?.FirstOrDefault(e => e.Id == id));
    }

    public async Task<IEnumerable<T>?> GetAll<T>(params string[] include) where T : EntityBase
    {
        return await Task.FromResult(GetList<T>());
    }

    public async Task<T> Add<T>(T entity) where T : EntityBase
    {
        GetList<T>()?.Add(entity);
        return await Task.FromResult(entity);
    }

    public Task<T> Update<T>(T entity) where T : EntityBase
    {
        throw new NotImplementedException();
    }

    public Task<T> Delete<T>(T entity) where T : EntityBase
    {
        throw new NotImplementedException();
    }

    public async Task<T?> First<T>(Expression<Func<T, bool>> predicate, params string[] include) where T : EntityBase
    {
        var product = GetList<T>()?.FirstOrDefault(predicate.Compile());
        return await Task.FromResult(product);
    }

    public async Task<IEnumerable<T>?> GetFiltered<T>(Expression<Func<T, bool>> predicate, params string[] include) where T : EntityBase
    {
        var products = GetList<T>()?.Where(predicate.Compile());
        return await Task.FromResult(products);
    }
}
