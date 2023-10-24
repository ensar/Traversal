using Microsoft.AspNetCore.Mvc;

namespace Traversal.ViewComponents.MemberLayout
{
    public class _MemberLayoutNavbar : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
