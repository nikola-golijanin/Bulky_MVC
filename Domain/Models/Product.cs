using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;
public class Product
{
	[Key]
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
	[Range(1, 1000)]
	public double ListPrice { get; set; }

	[Required]
	[Range(1, 1000)]
	public double Price { get; set; }

	[Required]
	[Range(1, 1000)]
	public double PriceFor50 { get; set; }

	[Required]
	[Range(1, 1000)]
	public double PriceFor100 { get; set; }

	public int CategoryId { get; set; }

	[ForeignKey("CategoryId")]
	[ValidateNever]
	public Category Category { get; set; }

	[ValidateNever]
	public string ImageUrl { get; set; }
}
