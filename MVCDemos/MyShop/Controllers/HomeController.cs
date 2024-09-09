using Microsoft.AspNetCore.Mvc; // Model View Controller
// Model = Data and the business logic
// View = Presentation Layer (UI)
// Controller = Handles the user input with the Model to render the appropriate View

namespace MyShop.Controllers // Namespace is the same as the folder name, and we use it to group related classes together.
{
  public class HomeController : Controller // HomeController inherits from Controller
  {
    // GET: /<controller>/
    public IActionResult Index() // Index is an action method
    {
      return View(); // View() is a method that returns a view result
    }
  }
}

// How it works together: 
// 1. Request Handling
// - When the user navigates to the root URL or /home/Index, the routing system directs the request to the Index action method of HomeController. 
// 2. Action Execution
// - The Index action method executes and prepares any necessary data or simply decides which view to render. In this case, it returns a View() without any model data.
// 3. View Selection
// - The MVC framework looks for a view file named Index.cshtml in the Views/Home directory.
// 4. View Rendering
// - The index.cshtml view is rendered into HTML and sent back as the HTTP response to the browser.