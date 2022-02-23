using MeterReader.Data;
using MeterReader.MeterReading;
using MeterReader.MeterReading.Model;
using MeterReader.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MeterReaderContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("MeterReadContext")));
builder.Services.AddScoped<IMeterReadUploadBO, MeterReadUploadBO>();
builder.Services.AddScoped<IMeterReadUploadDAO, MeterReadUploadDAO>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapPost("/meter-reading-uploads", async (HttpRequest request, IMeterReadUploadDAO meterReadUploadDAO, IMeterReadUploadBO meterReadUploadBO, bool firstLineHeaders) =>
{
    //do do a bunch of file uploading
    if (!request.HasFormContentType)
        return Results.BadRequest();

    var form = await request.ReadFormAsync();
    var formFile = form.Files[0];

    if (formFile is null || formFile.Length == 0)
        return Results.BadRequest();

    await using var stream = formFile.OpenReadStream();

    var reader = new StreamReader(stream);
    var text = await reader.ReadToEndAsync();

    //send the data to read the content of the file
    List<MeterReadingWithError> meterReadings = meterReadUploadBO.UploadMeterReading(text, firstLineHeaders);

    //send the results back to the controller
    return Results.Ok(meterReadings);
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
