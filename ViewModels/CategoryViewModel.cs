using BlazorApp.Components.Pages.Categories.Forms;
using BlazorApp.Database.Models;
using BlazorApp.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.ObjectModel;

public partial class CategoryViewModel : ObservableObject
{
    private readonly ICategoryService _categoryService;
    private readonly IFlashMessage _flashMessage;

    [ObservableProperty]
    private ObservableCollection<Category> categories = new();

    [ObservableProperty]
    private bool isBusy;

    [ObservableProperty]
    private int currentPage = 1;

    [ObservableProperty]
    private int totalPages = 1;

    private const int PageSize = 10;
    private void NotifyStateChanged() => OnChange?.Invoke();

    public event Action? OnChange;
    public bool CanGoPrev => CurrentPage > 1;
    public bool CanGoNext => CurrentPage < TotalPages;

    public CategoryViewModel(ICategoryService categoryService, IFlashMessage flashMessage)
    {
        _categoryService = categoryService;
        _flashMessage = flashMessage;
    }

    public async Task CreateAsync(CreateCategoryForm createCategoryForm)
    {
        if (await _categoryService.GetByNameAsync(createCategoryForm.Name))
        {
            _flashMessage.ShowError("Nome da categoria já está cadastrado!");
            return;
        }

        try
        {
            IsBusy = true;

            Category category = new Category
            {
                Name = createCategoryForm.Name
            };

            await _categoryService.CreateAsync(category);
            await LoadAsync();
            NotifyStateChanged();

            _flashMessage.ShowSuccess("Categoria adicionada com sucesso!");
        }
        catch (Exception ex)
        {
            _flashMessage.ShowError($"Erro ao adicionar categoria: {ex.Message}");
        }
        finally
        {
            IsBusy = false;
        }
    }

    public async Task LoadAsync(int page = 1, string? search = null)
    {
        try
        {
            IsBusy = true;

            var (items, total) = await _categoryService.GetAllAsync(page, PageSize, search);

            Categories = new(items);
            CurrentPage = page;
            TotalPages = (int)Math.Ceiling(total / (double)PageSize);
        }
        finally
        {
            IsBusy = false;
            NotifyStateChanged();
        }
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        try
        {
            IsBusy = true;
            return await _categoryService.GetByIdAsync(id);
        }
        catch (Exception ex)
        {
            _flashMessage.ShowError($"Erro ao buscar categoria: {ex.Message}");
            return null;
        }
        finally
        {
            IsBusy = false;
        }
    }

    public async Task UpdateAsync(int id, UpdateCategoryForm updateCategoryForm)
    {
        try
        {
            IsBusy = true;

            if (await _categoryService.GetByNameAsync(updateCategoryForm.Name))
            {
                _flashMessage.ShowError("Nome da categoria já está cadastrado!");
                return;
            }

            Category? category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                _flashMessage.ShowError("Categoria não encontrada!");
                return;
            }

            await _categoryService.UpdateAsync(id, updateCategoryForm);
            await LoadAsync(CurrentPage);
            NotifyStateChanged();

            _flashMessage.ShowSuccess("Categoria atualizada com sucesso!");
        }
        catch (Exception ex)
        {
            _flashMessage.ShowError($"Erro ao atualizar categoria: {ex.Message}");
        }
        finally
        {
            IsBusy = false;
        }
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            IsBusy = true;

            await _categoryService.DeleteAsync(id);
            await LoadAsync(CurrentPage);
            NotifyStateChanged();

            _flashMessage.ShowSuccess("Categoria removida com sucesso!");
        }
        catch (Exception ex)
        {
            _flashMessage.ShowError($"Erro ao tentar remover categoria: {ex.Message}.");
        }
        finally
        {
            IsBusy = false;
        }
    }

    public IEnumerable<int> GetPageNumbers()
    {
        for (int i = 1; i <= TotalPages; i++)
            yield return i;
    }
}
