using System;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Adwaer.Identity.ViewModel;

namespace Adwaer.Identity.Controllers
{
    [AllowAnonymous]
    public class UserControllerBase<T, TViewModel> : ApiController where T : class, IUser<Guid> where TViewModel : IRegistrationModel
    {
        private readonly UserManager<T, Guid> _userManager;
        private readonly IMapper _mapper;

        public UserControllerBase(UserManager<T, Guid> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public virtual async Task<IHttpActionResult> Get(string login, string password)
        {
            var result = await _userManager.FindAsync(login, password);
            if (result == null)
            {
                return BadRequest();
            }
            var identity = new GenericIdentity(login);
            var principal = new GenericPrincipal(identity, null);

            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }

            return Ok();
        }
        
        public virtual async Task<IHttpActionResult> Post(TViewModel id)
        {
            var account = _mapper.Map<T>(id);
            if (await _userManager.FindByEmailAsync(id.Email) != null)
            {
                return BadRequest("username_busy");
            }

            var result = await _userManager
                .CreateAsync(account, id.Password);
            if (result == IdentityResult.Success)
            {
                await RegistrationCompleted(account.Id);
                return Ok();
            }
            IHttpActionResult errorResult = GetErrorResult(result);
            if (errorResult != null)
            {
                return errorResult;
            }

            return BadRequest(result.ToString());
        }

        protected virtual async Task RegistrationCompleted(Guid id)
        {
            
        }
        
        public virtual async Task<IHttpActionResult> Confirm(Guid userId, string token)
        {
            if (await _userManager.FindByIdAsync(userId) == null)
            {
                return BadRequest("user_not_found");
            }

            bool isConfirmed;
            try
            {
                isConfirmed = await _userManager.IsEmailConfirmedAsync(userId);
            }
            catch (InvalidOperationException)
            {
                return BadRequest("user_check_error");
            }

            if (isConfirmed)
            {
                return BadRequest("already_confirmed");
            }

            var result = await _userManager.ConfirmEmailAsync(userId, token);
            if (result == IdentityResult.Success)
            {
                return Ok();
            }
            return BadRequest("cannot_confirm");
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}
