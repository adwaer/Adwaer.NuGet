using System.Runtime.Serialization;

namespace Adwaer.Identity.ViewModel
{
    [DataContract]
    public class RestorePasswordViewModel : ConfirmByTokenViewModel
    {
        [DataMember(Name = "pwd")]
        public string Pwd { get; set; }
    }
}
