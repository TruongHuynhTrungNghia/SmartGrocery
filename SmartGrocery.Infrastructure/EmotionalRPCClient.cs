using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Concurrent;
using System.Text;

namespace SmartGrocery.Infrastructure
{
    public class EmotionalRPCClient
    {
        private readonly IConnection connection;
        private readonly IModel channel;
        private readonly string replyQueueName;
        private readonly EventingBasicConsumer consumer;
        private readonly BlockingCollection<string> respQueue = new BlockingCollection<string>();
        private readonly IBasicProperties props;

        public EmotionalRPCClient()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            replyQueueName = channel.QueueDeclare().QueueName;
            consumer = new EventingBasicConsumer(channel);

            props = channel.CreateBasicProperties();
            var correlationId = Guid.NewGuid().ToString();
            props.CorrelationId = correlationId;
            props.ReplyTo = replyQueueName;

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var response = Encoding.UTF8.GetString(body.ToArray());
                if (ea.BasicProperties.CorrelationId == correlationId)
                {
                    respQueue.Add(response);
                }
            };
        }

        public EmotionalData SendEmotionDataToServer(byte[] data)
        {
            var messageBytes = data;

            channel.BasicPublish(
                exchange: "",
                routingKey: "rpc_queue",
                basicProperties: props,
                body: messageBytes);

            channel.BasicConsume(
                consumer: consumer,
                queue: replyQueueName,
                autoAck: true);

            var result = respQueue.Take();

            return ConverttoEmotionData(result);
        }

        public EmotionalData EmptyEmotionData => new EmotionalData();

        private EmotionalData ConverttoEmotionData(string result)
        {
            string[] data = result.Split(new char[] { ' ' }, 2);

            return new EmotionalData
            {
                Emotion = data[1] == "Null" ? string.Empty : data[1],
                Probability = Decimal.TryParse(data[0], out decimal percentage) == true ? percentage : 0M
            };
        }
    }

    public class EmotionalData
    {
        public string Emotion { get; set; }

        public decimal Probability { get; set; }

        public EmotionalData CheckAndAssert(EmotionalData emotion)
        {
            return this.Probability >= emotion.Probability ? this : emotion;
        }
    }
}