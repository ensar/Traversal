using BusinessLayer.Abstract.AbstractUow;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Traversal.Areas.Admin.Models;

namespace Traversal.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        public readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult Index(AccountVM model)
        {
            var valueSender = _accountService.TGetByID(model.SenderID);
            var valueReceiver = _accountService.TGetByID(model.ReceiverID);

            valueSender.Balance -= model.Amount;
            valueReceiver.Balance += model.Amount;

            List<Account> modifiedAccounts = new List<Account>()
            {
                valueSender,valueReceiver
            };
            _accountService.TMultiUpdate(modifiedAccounts);
            return View();
        }
    }
}
