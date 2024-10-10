using DataAccess.Repository.Products;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    private readonly IProductRepository _productRepository;

    public ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public IActionResult Index()
    {
        var products = _productRepository.GetAll();
        return View(products);
    }

    public IActionResult Create() => View();

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
