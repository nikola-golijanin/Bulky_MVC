using DataAccess.Repository.Categories;
using DataAccess.Repository.Products;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
	private readonly IProductRepository _productRepository;
	private readonly ICategoryRepository _categoryRepository;

	public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
	{
		_productRepository = productRepository;
		_categoryRepository = categoryRepository;
	}

	public IActionResult Index()
	{
		var products = _productRepository.GetAllIncluding(p => p.Category);
		return View(products);
	}

	public IActionResult Create()
	{
		var categoryList = _categoryRepository.GetAllQueryable(c => new SelectListItem
		{
			Value = c.Id.ToString(),
			Text = c.Name
		});

		ViewBag.CategoryList = categoryList;
		return View();
	}

	[HttpPost]
	public IActionResult Create(Product product)
	{
		if (!ModelState.IsValid) return View();

		_productRepository.Add(product);
		_productRepository.SaveChanges();
		TempData["success"] = "Product created successfully";
		return RedirectToAction("Index");
	}

	public IActionResult Edit(int? id)
	{
		if (id is null) return NotFound();

		var product = _productRepository.GetFirstOrDefault(c => c.Id == id);
		if (product is null) return NotFound();

		return View(product);
	}

	[HttpPost]
	public IActionResult Edit(Product Product)
	{
		if (!ModelState.IsValid) return View();

		_productRepository.Update(Product);
		_productRepository.SaveChanges();
		TempData["success"] = "Product updated successfully";
		return RedirectToAction("Index");
	}

	public IActionResult Delete(int? id)
	{
		if (id is null) return NotFound();

		var product = _productRepository.GetFirstOrDefault(c => c.Id == id);
		if (product is null) return NotFound();

		return View(product);
	}

	[HttpPost, ActionName("Delete")]
	public IActionResult DeleteProduct(int? id)
	{

		var product = _productRepository.GetFirstOrDefault(c => c.Id == id);
		if (product is null) return NotFound();

		_productRepository.Remove(product);
		_productRepository.SaveChanges();
		TempData["success"] = "Product deleted successfully";
		return RedirectToAction("Index");
	}
}
