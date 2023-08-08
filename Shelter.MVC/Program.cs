using Shelter.MVC.Provider;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession();

builder.Services.AddHttpClient<GenussesProvider>(options =>
{
    options.BaseAddress = new Uri("https://localhost:7023/api/");

});

builder.Services.AddHttpClient<AuthProvider>(options =>
{
    options.BaseAddress = new Uri("https://localhost:7023/api/");
});

builder.Services.AddHttpClient<AnimalProvider>(options =>
{
    options.BaseAddress = new Uri("https://localhost:7023/api/");

});

builder.Services.AddHttpClient<AdoptionProvider>(options =>
{
    options.BaseAddress = new Uri("https://localhost:7023/api/");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
