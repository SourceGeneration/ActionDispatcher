using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SourceGeneration.ActionDispatcher;

var services = new ServiceCollection()
    .AddLogging(builder => builder.AddConsole())
    .AddActionDispatcher()
    //.AddScoped<ActionInjectServiceHandler>()
    .BuildServiceProvider();

var dispatcher = services.GetRequiredService<IActionDispatcher>();
var subscriber = services.GetRequiredService<IActionSubscriber>();

subscriber.Subscribe<Hello>(ActionDispatchStatus.WaitingToDispatch, hello =>
{
    Console.WriteLine("Subscriber: Hello action dispatching.");
});

subscriber.Subscribe<Hello>(hello =>
{
    Console.WriteLine("Subscriber: Hello action dispatched.");
});

dispatcher.Dispatch(new Hello { Say = "Hello World" });

Console.ReadLine();

public class Hello
{
    public string? Say { get; set; }

    [ActionHandler]
    public static void Handle(Hello hello, ILogger<Hello> logger)
    {
        logger.LogInformation("Handled : " + hello.Say);
    }
}

public class ActionHandler
{
    [ActionHandler]
    public async Task Handle(Hello hello, ILogger<ActionHandler> logger, CancellationToken cancellationToken)
    {
        await Task.Delay(1000, cancellationToken);
        logger.LogInformation("Handled : " + hello.Say);
    }
}

//public class ActionInjectServiceHandler(ILogger<ActionInjectServiceHandler> logger)
//{
//    [ActionHandler]
//    public async Task Handle(Hello hello, CancellationToken cancellationToken)
//    {
//        await Task.Delay(1000, cancellationToken);
//        logger.LogInformation("Handled : " + hello.Say);
//    }
//}