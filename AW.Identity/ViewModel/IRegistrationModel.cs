using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Adwaer.Identity.ViewModel
{
    public interface IRegistrationModel
    {
        [Required]
        [DataMember(Name = "password")]
        [DataType(DataType.Password)]
        string Password { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [DataMember(Name = "email")]
        string Email { get; set; }
    }
}
