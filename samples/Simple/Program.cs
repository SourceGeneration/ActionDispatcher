// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SourceGeneration.ActionDispatcher;

var services = new ServiceCollection()
    .AddLogging(builder => builder.AddConsole())
    .AddActionDispatcher()
    .AddScoped<InjectServiceHandler>()
    .BuildServiceProvider();

var dispatcher = services.GetRequiredService<IActionDispatcher>();
var subscriber = services.GetRequiredService<IActionSubscriber>();

subscriber.Subscribe<Say>(action =>
{
    Console.WriteLine("Subscriber: Say action dispatched.");
});

subscriber.Subscribe<Say>(ActionDispatchStatus.WaitingToDispatch, action =>
{
    Console.WriteLine("Subscriber: Say action dispatching.");
});

subscriber.Subscribe<Say>(ActionDispatchStatus.Faulted, (action, exception) =>
{
    Console.WriteLine("Subscriber: Say action dispatch faulted.");
});

//Subscribe all types action
subscriber.Subscribe<object>(action =>
{
    Console.WriteLine($"Subscriber: {action.GetType()} action dispatched");
});

dispatcher.Dispatch(new Say { Text = "Hello World" });

Console.ReadLine();

public class Say
{
    public string? Text { get; set; }
}

public class ActionHandler
{
    [ActionHandler]
    public static void StaticHandle(Say say, ILogger<ActionHandler> logger, CancellationToken cancellationToken)
    {
        logger.LogInformation("StaticHandle : " + say.Text);
    }

    [ActionHandler]
    public void Handle(Say say, ILogger<ActionHandler> logger, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handled : " + say.Text);
    }

    [ActionHandler]
    public async Task AsyncHandle(Say say, ILogger<ActionHandler> logger, CancellationToken cancellationToken)
    {
        await Task.Delay(1000, cancellationToken);
        logger.LogInformation("AsyncHandled : " + say.Text);
    }
}

public class InjectServiceHandler(ILogger<InjectServiceHandler> logger)
{
    [ActionHandler]
    public void Handle(Say say)
    {
        logger.LogInformation("InjectServiceHandled : " + say.Text);
    }
}