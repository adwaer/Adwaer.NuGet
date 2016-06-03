using System;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Adwaer.Identity.Entitites;
using Adwaer.Identity.ViewModel;

namespace Adwaer.Identity.Controllers
{
    public class SimpleCustomerController : ApiController
    {
        private readonly UserManager<SimpleCustomerAccount, Guid> _userManager;
        private readonly IMapper _mapper;

        public SimpleCustomerController(UserManager<SimpleCustomerAccount, Guid> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IHttpActionResult> Get(string login, string password)
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

        public async Task<IHttpActionResult> Post(RegistrationModel id)
        {
            var account = _mapper.Map<SimpleCustomerAccount>(id);
            var result = await _userManager
                .CreateAsync(account, id.Password);
            if (result == IdentityResult.Success)
            {
                return Ok();
            }
            IHttpActionResult errorResult = GetErrorResult(result);
            if (errorResult != null)
            {
                return errorResult;
            }

            return BadRequest(result.ToString());
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
