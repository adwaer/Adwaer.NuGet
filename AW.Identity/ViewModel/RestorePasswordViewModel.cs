using System.Runtime.Serialization;

namespace Adwaer.Identity.ViewModel
{
    [DataContract]
    public class RestorePasswordViewModel
    {
        [DataMember(Name = "token")]
        public string Token { get; set; }
        [DataMember(Name = "pwd")]
        public string Pwd { get; set; }
    }
}
