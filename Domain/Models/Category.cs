using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class Category
{
	[Key]
	public int Id { get; set; }

	[Required]
	[MaxLength(30)]
	public string Name { get; set; }

	[Required]
	public int DisplayOrder { get; set; }
}
