using Autofac;
using Microsoft.AspNetCore.Mvc;
using MiniStackOverflow.Infrastructure;
using MiniStackOverflow.Web.Models;


namespace StackOverflow.Web.Controllers
{
    public class TagController : Controller
    {
        private readonly ILifetimeScope _scope;

        public TagController(ILifetimeScope scope)
        {
            _scope = scope;
        }

		public IActionResult Index()
		{
			return View();
		}

        [HttpPost]
        public async Task<JsonResult> GetTags(TagListModel model)
        {
            var dataTablesModel = new DataTablesAjaxRequestUtility(Request);
            model.Resolve(_scope);
            var data = await model.GetPagedTagsAsync(dataTablesModel);
            return Json(data);
        }
    }
}
