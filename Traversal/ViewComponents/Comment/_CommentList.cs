using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Traversal.ViewComponents.Comment
{
    public class _CommentList:ViewComponent
    {
        CommentManager commentManager = new CommentManager(new EfCommentDal());
        Context context = new Context();
        public IViewComponentResult Invoke(int id)
        {
            ViewBag.commentCount=context.Comments.Where(c=>c.DestinationID==id).Count();
            var values=commentManager.TGetListCommentWithDestinationAndUser(id);
            return View(values);
        }
    }
}
