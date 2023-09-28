namespace FDT.DataAquisition.Messaging.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishReceivedData(DataReceived dataReceived);
    }


    public interface IBackgroundDataSender
    {
        void PublishReceivedData(DataReceived dataReceived);
    }

}
