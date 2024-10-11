using Domain.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyWeb.ViewModels;

public class CategoryVM
{
	public int Id { get; set; }

	[Required]
	[DisplayName("Category name")]
	[MaxLength(30)]
	public string Name { get; set; }

	[Required]
	[DisplayName("Display Order")]
	[Range(1, 100, ErrorMessage = "Display order must be between 1 and 100")]
	public int DisplayOrder { get; set; }

	#region Mapping
	public static explicit operator Category(CategoryVM categoryVm)
		=> new()
		{
			Name = categoryVm.Name,
			DisplayOrder = categoryVm.DisplayOrder
		};
	#endregion
}
