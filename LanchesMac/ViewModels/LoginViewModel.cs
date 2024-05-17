using System.ComponentModel.DataAnnotations;

namespace LanchesMac.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Por favor, insira um usuário válido")]
        [Display(Name ="Usuário")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Por favor, insira uma senha")]
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string Password { get; set; } 

        public string ReturnUrl { get; set; }
    }
}
