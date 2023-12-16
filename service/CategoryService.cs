
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using infrastructure.DataModels;

using infrastructure.Repositories;

namespace service;

public class CategoryService
{
    private readonly CategoryRepository _categoryRepository;

    public CategoryService(CategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    
    public async Task<Category> GetCategoryByIdAsync(int id)
    {
        return await _categoryRepository.GetCategoryByIdAsync(id);
    }
}