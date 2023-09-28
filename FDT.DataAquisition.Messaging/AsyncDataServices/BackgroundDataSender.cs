using MathNet.Numerics.Distributions;

namespace FDT.DataAquisition.Messaging.AsyncDataServices
{
    public class BackgroundDataSender : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public BackgroundDataSender(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var normalDist = new Normal(55, 15); 

            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var messageBusClient = scope.ServiceProvider.GetRequiredService<IMessageBusClient>();

                    double randomValue = normalDist.Sample();
                    randomValue = Math.Max(10, Math.Min(100, randomValue)); 

                    messageBusClient.PublishReceivedData(new FDT.DataAquisition.Messaging.DataReceived()
                    {
                        DataValue = randomValue,
                        DigitalTwinId = 1,
                        ReceivedTime = DateTime.UtcNow,
                        Event = "DataReceived"
                    });
                }

                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
            }
        }
    }

}
