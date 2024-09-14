using ITIBlog.Data;
using ITIBlog.Models.Domain;
using ITIBlog.Models.View;
using ITIBlog.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITIBlog.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AdminTagsController : Controller
    {
        private readonly ITagRepository tagRepository;

        public AdminTagsController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }
        [HttpGet]
        public IActionResult Add() // localhost:7242/AddTags/Add
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            ValidateAddTagRequest(addTagRequest);
            if (ModelState.IsValid == false)
            {
                return View();
            }
            //Mapping AddTagRequest to Tag Model (Domain)
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };
            await tagRepository.AddAsync(tag);
            
            return RedirectToAction("List");
        }


        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            //Make The DbContext Read All The Tags
            var tags = await tagRepository.GetAllAsync();
            return View(tags);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            //A way To Get The Tag by Id
            //var tag = db.Tags.Find(id);
            //Another way
            var tag = await tagRepository.GetByIdAsync(id);
            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };

                return View(editTagRequest);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit (EditTagRequest editTagRequest)
        {
            //Mapping EditTagRequest to Tag Model(View To Domain)
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };
            var updatedTag = await tagRepository.UpdateAsync(tag);
            if (updatedTag != null)
            {
                //Show Message Inidicating The Tag Has Been Edited
                return RedirectToAction("List");
            }
            //Show Message Inidicating The Tag Has Not Been Edited
            return RedirectToAction("Edit" , new {id = editTagRequest.Id});
            
        }
        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
            var deletedTag = await tagRepository.DeleteAsync(editTagRequest.Id);
            if (deletedTag != null)
            {
                //Show Message Inidicating The Tag Has Been Deleted
                return RedirectToAction("List");
            }
            //Show Message Inidicating The Tag Has Not Been Deleted
            return View("Edit", new { id = editTagRequest.Id });
        }
        private void ValidateAddTagRequest(AddTagRequest addTagRequest)
        {
            if (addTagRequest.Name != null && addTagRequest.DisplayName != null)
            {
                if (addTagRequest.Name == addTagRequest.DisplayName)
                {
                    ModelState.AddModelError("DisplayName", "Name and Display Name Cannot Be The Same");
                }
            }
        }

    }
}
