﻿//using System.Text;

//using MassTransit;
//using MassTransit.Introspection;

//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;

//using Newtonsoft.Json;

//using SFC.Player.Infrastructure.Consumers.Data;
//using SFC.Player.Infrastructure.Extensions;

//namespace SFC.Player.Infrastructure.UnitTests.Extensions;
//public class MassTransitExtensionsTests
//{
//    [Fact]
//    [Trait("Extension", "MassTransit")]
//    public void Extension_MassTransit_ShouldHaveDefinedConfiguration()
//    {
//        // Arrange
//        Dictionary<string, string> initialData = new()
//        {
//            {"ConnectionStrings:RabbitMq", "rabbitmq://127.0.0.1:5672"},
//            {"RabbitMq:Username", "guest"},
//            {"RabbitMq:Password", "guest"},
//            {"RabbitMq:Name", "SFC.Player"},
//            {"RabbitMq:Retry:Limit", "5"},
//            {"RabbitMq:Retry:Intervals:0", "15"},
//            {"RabbitMq:Exchanges:Data:Key", "data"},
//            {"RabbitMq:Exchanges:Data:Value:Init:Name", "sfc.data.init"},
//            {"RabbitMq:Exchanges:Data:Value:Init:Type", "direct"},
//            {"RabbitMq:Exchanges:Data:Value:Require:Name", "sfc.data.require"},
//            {"RabbitMq:Exchanges:Data:Value:Require:Type", "direct"},
//            {"RabbitMq:Exchanges:Data:Value:Require:RoutingKey", "DATA_REQUIRE"}
//        };

//        IConfigurationRoot configuration = new ConfigurationBuilder()
//            .AddInMemoryCollection(initialData!)
//            .Build();
//        IServiceCollection services = new ServiceCollection();
//        services.AddTransient<IConfiguration>(sp => configuration);

//        // Act
//        IServiceCollection assertedServices = services.AddMassTransit(configuration);

//        using IServiceScope scope = assertedServices.BuildServiceProvider().CreateScope();

//        IBusControl busControl = scope.ServiceProvider.GetRequiredService<IBusControl>();

//        string assertConfiguration = ToJsonString(busControl.GetProbeResult());

//        dynamic configurationObject = JsonConvert.DeserializeObject<dynamic>(assertConfiguration)!;

//        // Assert
//        Assert.Equal("RabbitMQ", configurationObject.Results.bus.host.type.ToString());
//        Assert.Equal("127.0.0.1", configurationObject.Results.bus.host.host.ToString());
//        Assert.Equal("5672", configurationObject.Results.bus.host.port.ToString());
//        Assert.Equal("/", configurationObject.Results.bus.host.virtualHost.ToString());
//        Assert.Equal("guest", configurationObject.Results.bus.host.username.ToString());
//    }

//    [Fact]
//    [Trait("Extension", "MassTransit")]
//    public void Extension_MassTransit_ShouldRegisterConsumerWithDefinition()
//    {
//        // Arrange
//        IConfigurationRoot configuration = new ConfigurationBuilder().Build();
//        IServiceCollection services = new ServiceCollection();

//        // Act
//        IServiceCollection assertedServices = services.AddMassTransit(configuration);

//        // Assert
//        Assert.NotNull(assertedServices.FirstOrDefault(s => s.ImplementationType == typeof(DataInitializedDefinition)));
//        Assert.NotNull(assertedServices.FirstOrDefault(s => s.ImplementationType == typeof(DataInitializedConsumer)));
//    }

//    //[Fact]
//    //[Trait("Extension", "MassTransit")]
//    //public void Extension_MassTransit_ShouldBuildExchangeRoutingKey()
//    //{
//    //    // Arrange
//    //    DataInitiator initiator = DataInitiator.Player;
//    //    string key = "data";

//    //    // Act
//    //    string result = initiator.BuildExchangeRoutingKey(key);

//    //    // Assert
//    //    Assert.Equal($"{key}.player", result);
//    //}

//    public static string ToJsonString(ProbeResult result)
//    {
//        UTF8Encoding encoding = new(false, true);

//        using MemoryStream stream = new();
//        using StreamWriter writer = new(stream, encoding, 1024, true);
//        using JsonTextWriter jsonWriter = new(writer);
//        jsonWriter.Formatting = Formatting.Indented;

//        new JsonSerializer().Serialize(jsonWriter, result, typeof(ProbeResult));

//        jsonWriter.Flush();
//        writer.Flush();

//        return encoding.GetString(stream.ToArray());
//    }
//}
