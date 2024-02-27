using MassTransitMultiProject.Subscriber1;

var builder = Host.CreateApplicationBuilder(args);
// builder.Services.AddHostedService<Worker>();
builder.Services.AddMassTransitConsumers(builder.Configuration);

var host = builder.Build();
host.Run();