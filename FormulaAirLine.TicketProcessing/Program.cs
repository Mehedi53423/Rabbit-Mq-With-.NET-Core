using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Runtime.InteropServices.JavaScript;
using System.Text;

Console.WriteLine("Welcome To The Ticketing Service");

var factory = new ConnectionFactory()
{
    HostName = "localhost",
    UserName = "guest",
    Password = "guest",
    VirtualHost = "/"
};

var connection = factory.CreateConnection();

using var channel = connection.CreateModel();

channel.QueueDeclare("bookings", durable: true, exclusive: false);

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (sender, args) =>
{
    var body = args.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    var messageJson = JObject.Parse(message);

    JObject jsonObject = JObject.Parse(messageJson.ToString());

    int id = (int)jsonObject["Id"];
    string passengerName = (string)jsonObject["PassengerName"];
    string passportNumber = (string)jsonObject["PassportNumber"];
    string from = (string)jsonObject["From"];
    string to = (string)jsonObject["To"];
    int status = (int)jsonObject["Status"];

    Console.WriteLine("New Ticket Processing Is Initiated For : ");
    Console.WriteLine("Id: " + id);
    Console.WriteLine("Passenger Name: " + passengerName);
    Console.WriteLine("Passport Number: " + passportNumber);
    Console.WriteLine("From: " + from);
    Console.WriteLine("To: " + to);
    Console.WriteLine("Status: " + status);
};

channel.BasicConsume("bookings", true, consumer);

Console.ReadKey();