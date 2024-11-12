﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Customer.Controllers;

[Area("Customer")]
[Authorize]
public class CartController : Controller
{

    public IActionResult Index()
    {

        return View();
    }
}
