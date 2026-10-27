using LanchesProj.Areas.Admin.Services;
using LanchesProj.Context;
using LanchesProj.Models;
using LanchesProj.Repositories.Interfaces;
using LanchesProj.Repositories;
using LanchesProj.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options =>
			options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();//para usar login

builder.Services.Configure<ConfigurationImages>(builder.Configuration.GetSection("ConfigurationPastaImagens"));
//Importante, Usar valores mais rigidos em prod
builder.Services.Configure<IdentityOptions>(options =>
{
	options.Password.RequireDigit = true;
	options.Password.RequireLowercase = true;
	options.Password.RequireUppercase = false;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequiredLength = 6;
	options.Password.RequiredUniqueChars = 1;
});

builder.Services.AddTransient<ILancheRepository, LancheRepository>();
builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddTransient<IPedidoRepository, PedidoRepository>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); //Singleton tem o livetime em toda aplicação. Usa o httpcontext 

builder.Services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();
builder.Services.AddScoped<RelatorioVendasService>();
builder.Services.AddScoped<GraficoVendasService>();

builder.Services.AddAuthorization(options => {
	options.AddPolicy("Admin", politica => {
		politica.RequireRole("Admin");
	});
});

builder.Services.AddScoped(sp => CarrinhoCompra.GetCarrinho(sp)); //Scoped tem o livetime em toda requisição. Usa o carrinho de compras

builder.Services.AddControllersWithViews();

builder.Services.AddPaging(options => {
	options.ViewName = "Bootstrap4";
	options.PageParameterName = "pageindex";
});

builder.Services.AddMemoryCache();// Adiciona suporte a cache em memória
builder.Services.AddSession();// Adiciona suporte a sessão

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
}
else
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles(); //para armazenar os arquivos com acesso de todos users

app.UseRouting();

//precisa usar o ServiceProvider
//seedUserRoleInitial.SeedRoles();//cria perfis, tipos de users
//seedUserRoleInitial.SeedUsers();//cria dois users default

CriaPerfisUsuario(app);

app.UseSession();  // Adiciona suporte a sessão

app.UseAuthentication();
app.UseAuthorization();

#pragma warning disable ASP0014 // Suggest using top level route registrations
app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
		name: "categoriaFiltro",
		pattern: "Lanche/{action}/{categoria}",
		defaults: new { Controller = "Lanche", action = "List" });

	endpoints.MapControllerRoute(
		name: "areas",
		pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

	endpoints.MapControllerRoute(
		name: "default",
		pattern: "{controller=Home}/{action=Index}/{id?}");

});
#pragma warning restore ASP0014 // Suggest using top level route registrations


app.Run();


void CriaPerfisUsuario(WebApplication app) {
	var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
	using (var scope = scopedFactory.CreateScope())
	{
		var service = scope.ServiceProvider.GetService<ISeedUserRoleInitial>();
		service.SeedUsers();
		service.SeedRoles();
	}
}