using System.ComponentModel.DataAnnotations;

namespace OdontoPrev.ViewModels
{
    public class PostViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O título é obrigatório")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "O título deve ter entre 5 e 200 caracteres")]
        [Display(Name = "Título")]
        public string Title { get; set; }

        [Required(ErrorMessage = "O conteúdo é obrigatório")]
        [MinLength(10, ErrorMessage = "O conteúdo deve ter no mínimo 10 caracteres")]
        [Display(Name = "Conteúdo")]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
    }
}