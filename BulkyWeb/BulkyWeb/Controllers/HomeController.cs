using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BulkyWeb.Models;
using System.Data;
using System.Data.SqlClient;

namespace BulkyWeb.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        string connectionString = GetConnectionString();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = connectionString;
        string sql = "SELECT * From Categories;";
        SqlDataAdapter adp = new SqlDataAdapter(sql, conn);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        DataTable dt = ds.Tables[0];
        string str = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            str += $"Name: {dt.Rows[i]["Name"]}<br/>";
            str += $"DisplayOrder: {dt.Rows[i]["DisplayOrder"]}<hr>";
        }
        Console.Write(str);
        return Content(str);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    static private string GetConnectionString()
    {
        // To avoid storing the connection string in your code,
        // you can retrieve it from a configuration file, using the
        // System.Configuration.ConfigurationManager.ConnectionStrings property
        return "server=localhost,1401;user=sa;password=LikeAndSubscribe1!;database=Bulky;TrustServerCertificate=True;Encrypt=False";
    }
}

