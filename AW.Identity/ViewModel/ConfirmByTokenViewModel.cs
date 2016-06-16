using System.Runtime.Serialization;

namespace Adwaer.Identity.ViewModel
{
    [DataContract]
    public class ConfirmByTokenViewModel 
    {
        [DataMember(Name = "token")]
        public string Token { get; set; }
    }
}
