using System.ComponentModel.DataAnnotations;

namespace myvetjob.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}