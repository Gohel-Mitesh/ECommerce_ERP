using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ECommerce_ERP.Models;

namespace ECommerce_ERP.Controllers;

public class DashboardsController : Controller
{
  public IActionResult Index() => View();
}
