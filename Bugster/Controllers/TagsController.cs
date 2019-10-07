using Bugster.Business.Models;
using Bugster.Business.TagsApplicationService;
using Bugster.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace Bugster.Controllers
{
    [Route("[controller]/[action]")]
    public class TagsController : 
        Controller
    {
        private readonly ITagApplicationService _tagApplicationService;

        public TagsController(ITagApplicationService tagApplicationService)
        {
            _tagApplicationService = tagApplicationService;
        }

        public async Task<IActionResult> Index()
        {
            var readTagsRequest = new ReadTagsRequest();
            var response = await _tagApplicationService.Handle(readTagsRequest);

            return View(response);
        }

        public IActionResult Create()
        {
            var viewModel = new CreateTagViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm, Bind("Name,Bound")] CreateTagViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var createTagRequest = new CreateTagRequest
            {
                Name = model.Name,
                Bound = model.Bound
            };
            var response = await _tagApplicationService.Handle(createTagRequest);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update([BindRequired]long? tagId)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var readTagRequest = new ReadTagRequest
            {
                TagId = tagId.Value
            };
            var response = await _tagApplicationService.Handle(readTagRequest);
            var viewModel = response.Adapt<UpdateTagViewModel>();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromForm, Bind("TagId,Name,Bound")] UpdateTagViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var updateTagRequest = new UpdateTagRequest
            {
                TagId = model.TagId.Value,
                Name = model.Name,
                Bound = model.Bound
            };
            var response = await _tagApplicationService.Handle(updateTagRequest);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete([BindRequired]long? tagId)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            var deleteTagRequest = new DeleteTagRequest
            {
                TagId = tagId.Value
            };
            var response = await _tagApplicationService.Handle(deleteTagRequest);

            return RedirectToAction(nameof(Index));
        }
    }
}
