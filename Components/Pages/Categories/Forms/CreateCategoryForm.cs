using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Components.Pages.Categories.Forms
{
    public class CreateCategoryForm
    {
        [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
        [MinLength(3 , ErrorMessage = "O nome deve ter no mínimo 3 caracters.")]
        [MaxLength(120, ErrorMessage = "O nome deve ter no máximo 120 caracteres.")]
        public string Name { get; set; } = string.Empty;
    }
}
