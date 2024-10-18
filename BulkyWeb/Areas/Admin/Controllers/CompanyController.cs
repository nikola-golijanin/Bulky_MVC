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
    public IActionResult Index()
    {
        var companies = _companyService.GetAll();
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

    public IActionResult Edit(int id)
    {
        //if (id is null) return NotFound();

        var company = _companyService.GetById(id);

        return View(company);
    }

    [HttpPost]
    public IActionResult Edit(Company company)
    {
        if (!ModelState.IsValid)
            return View();

        _companyService.Update(company);

        TempData["success"] = "Company updated successfully";
        return RedirectToAction("Index");
    }
    #endregion

    #region API
    [HttpGet]
    public IActionResult GetAll()
    {
        var companies = _companyService.GetAll();
        return Json(new { data = companies });
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
        _companyService.Delete(id);
        return Ok("Company deleted successfully");
    }
    #endregion
}
