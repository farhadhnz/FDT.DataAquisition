using FDT.DataAquisition.Messaging.AsyncDataServices;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();
builder.Services.AddHostedService<BackgroundDataSender>();

var app = builder.Build();

// Create a ManualResetEvent to signal application exit
var exitEvent = new ManualResetEvent(false);

// Run the application in a separate thread
var applicationThread = new Thread(() =>
{
    app.Run();
    exitEvent.Set(); // Set the event when the application is done running
});

applicationThread.Start();

// Wait for the exit signal
exitEvent.WaitOne();

// Ensure that the background thread (BackgroundDataSender) is stopped before exiting
app.Services.GetRequiredService<IHostApplicationLifetime>().StopApplication();

// Wait for the application thread to complete
applicationThread.Join();