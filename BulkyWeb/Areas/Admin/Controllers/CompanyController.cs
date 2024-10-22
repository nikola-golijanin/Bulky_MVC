using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Companies;
using Utility;

namespace BulkyWeb.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = Roles.Admin)]
public class CompanyController : Controller
{

    private readonly ICompanyService _companyService;

    public CompanyController(ICompanyService companyService)
    {
        _companyService = companyService;
    }

    #region Views
    public async Task<IActionResult> Index()
    {
        var companies = await _companyService.GetAllAsync();
        return View(companies);
    }

    public IActionResult Create() => View(new Company());

    [HttpPost]
    public IActionResult Create(Company company)
    {
        if (!ModelState.IsValid)
            return View(company);

        _companyService.Create(company);
        TempData["success"] = "Company created successfully";
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(int id)
    {
        //if (id is null) return NotFound();

        var company = await _companyService.GetByIdAsync(id);
        return View(company);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Company company)
    {
        if (!ModelState.IsValid)
            return View();

        await _companyService.UpdateAsync(company);

        TempData["success"] = "Company updated successfully";
        return RedirectToAction("Index");
    }
    #endregion

    #region API
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var companies = await _companyService.GetAllAsync();
        return Json(new { data = companies });
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        await _companyService.DeleteAsync(id);
        return Ok("Company deleted successfully");
    }
    #endregion
}
