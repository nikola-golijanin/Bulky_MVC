using Domain.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BulkyWeb.ViewModels;

public class ProductVM
{
	public int Id { get; set; }

	[Required]
	public string Title { get; set; }

	[Required]
	public string Description { get; set; }

	[Required]
	public string ISBN { get; set; }

	[Required]
	public string Author { get; set; }

	[Required]
	[Display(Name = "List price")]
	[Range(1, 1000)]
	public double ListPrice { get; set; }

	[Required]
	[Display(Name = "Price for 1-50")]
	[Range(1, 1000)]
	public double Price { get; set; }

	[Required]
	[Display(Name = "Price for 50+")]
	[Range(1, 1000)]
	public double PriceFor50 { get; set; }

	[Required]
	[Display(Name = "Price for 100+")]
	[Range(1, 1000)]
	public double PriceFor100 { get; set; }

	[Display(Name = "Category")]
	public int CategoryId { get; set; }

	[ValidateNever]
	public string ImageUrl { get; set; }

	[ValidateNever]
	public IEnumerable<SelectListItem> CategoryList { get; set; }

	#region Mapping
	public static explicit operator Product(ProductVM productVm)
		=> new()
		{
			Title = productVm.Title,
			Description = productVm.Description,
			ISBN = productVm.ISBN,
			Author = productVm.Author,
			ListPrice = productVm.ListPrice,
			Price = productVm.Price,
			PriceFor50 = productVm.PriceFor50,
			PriceFor100 = productVm.PriceFor100,
			CategoryId = productVm.CategoryId,
			ImageUrl = productVm.ImageUrl
		};
	#endregion
}
