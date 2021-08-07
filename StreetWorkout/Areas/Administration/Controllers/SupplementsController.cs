namespace StreetWorkout.Areas.Administration.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using Services.Supplements;
    using ViewModels.Supplements;

    using static WebConstants;
    using static WebConstants.TempDataMessageKeys;

    [Authorize(Roles = AdministratorRoleName)]
    public class SupplementsController : AdministrationController
    {
        private readonly ISupplementService supplements;
        private readonly IMapper mapper;

        public SupplementsController(ISupplementService supplements, IMapper mapper)
        {
            this.supplements = supplements;
            this.mapper = mapper;
        }

        public IActionResult Create()
            => this.View(new SupplementFormModel
            {
                Categories = this.supplements.GetSupplementCategories(),
            });

        [HttpPost]
        public IActionResult Create(SupplementFormModel model)
        {
            if (!this.supplements.IsValidCategoryId(model.CategoryId))
            {
                this.ModelState.AddModelError(nameof(model.CategoryId), "Invalid category.");
            }

            if (!this.ModelState.IsValid)
            {
                model.Categories = this.supplements.GetSupplementCategories();
                return this.View(model);
            }

            this.supplements.Create(model.Name, model.CategoryId, model.ImageUrl, model.Content, model.Price, model.Quantity);

            return this.RedirectToAction("All");
        }

        public IActionResult Edit(int id)
        {
            const string SupplementIdKey = "SupplementIdKey";

            var supplementFormModel = this.supplements.EditForModel(id);

            if (supplementFormModel == null)
            {
                return this.BadRequest();
            }

            supplementFormModel.Categories = this.supplements.GetSupplementCategories();

            this.TempData[SupplementIdKey] = id;

            return this.View(supplementFormModel);
        }

        [HttpPost]
        public IActionResult Edit(int id, SupplementFormModel model)
        {
            if (!this.supplements.IsValidCategoryId(model.CategoryId))
            {
                this.ModelState.AddModelError(nameof(model.CategoryId), "Invalid category.");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var isEdited = this.supplements.Edit(id, model.Name, model.CategoryId, model.ImageUrl, model.Content, model.Price, model.Quantity);

            if (!isEdited)
            {
                return this.BadRequest();
            }

            this.TempData[EditedSuccessfully] = "You Edited " + model.Name + " Successfully.";

            return this.RedirectToAction("Details", "Supplements", new { area = "", Id = id });
        }
    }
}
