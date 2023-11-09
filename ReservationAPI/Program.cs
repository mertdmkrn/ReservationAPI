using ReservationAPI.Handler.Abstract;
using ReservationAPI.Handler.Concrete;
using ReservationAPI.Handler.Model;
using ReservationAPI.Repository.Abstract;
using ReservationAPI.Repository.Concrete;
using ReservationAPI.Service.Abstract;
using ReservationAPI.Service.Concrete;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        string projectPath = AppDomain.CurrentDomain.BaseDirectory.Split(new string[] { @"bin\" }, StringSplitOptions.None)[0];
        builder.Services.AddOptions();
        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
        {
            builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
        }));

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        builder.Services.AddSwaggerGen(c =>
        {
            var filePath = Path.Combine(projectPath + "ReservationApiV1.xml");
            c.IncludeXmlComments(filePath);
        });

        builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

        configureInjection(builder);

        var app = builder.Build();

        app.UseSwagger();
        app.UseCors("corsapp");
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Resarvation API V1");
            c.RoutePrefix = string.Empty;
        });

        app.MapControllers();

        app.Run();
    }

    private static void configureInjection(WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IMailHandler, MailHandler>();

        builder.Services.AddSingleton<IReservationRepository, ReservationRepository>();
        builder.Services.AddSingleton<ITableRepository, TableRepository>();

        builder.Services.AddSingleton<IReservationService, ReservationService>();
        builder.Services.AddSingleton<ITableService, TableService>();
    }
}