var builder = WebApplication.CreateBuilder(args);
//Add services to container
var app = builder.Build();
// configure the http requests pipeline

app.Run();