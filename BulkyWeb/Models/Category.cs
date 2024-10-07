﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyWeb.Models;

public class Category
{
    [Key]
    public int Id { get; set; }

    [Required]
    [DisplayName("Category name")]
    [MaxLength(30)]
    public string Name { get; set; }

    [Required]
    [DisplayName("Display Order")]
    [Range(1,100, ErrorMessage="Display order must be between 1 and 100")]
    public int DisplayOrder { get; set; }
}