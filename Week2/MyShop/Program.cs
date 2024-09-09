var builder = WebApplication.CreateBuilder(args);

// This line adds services required for handling controllers and views to the dependency injection container.
// In laymans terms, it adds the services required to handle HTTP requests and responses
builder.Services.AddControllersWithViews();

var app = builder.Build();

// This checks if the environment is development and if so it adds a developer exception page which is useful for debugging
if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();


// This line tells the app to use the routing middleware
// Routing middleware is used to match incoming HTTP requests and dispatch those requests to the app's endpoints
// In simple terms: It tells the app to use the routing middleware to match incoming requests to the correct controller and action 
app.MapDefaultControllerRoute();

app.Run();

