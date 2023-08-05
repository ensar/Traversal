using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Traversal.ViewComponents.Default
{
    public class _Feature:ViewComponent
    {
        FeatureManager featureManager = new FeatureManager(new EfFeatureDal()); 
        public IViewComponentResult Invoke()
        {
            List<Feature> list=featureManager.TGetList();
            return View(list);
        }
    }
}
