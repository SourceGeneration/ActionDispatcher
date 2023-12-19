using Microsoft.Extensions.DependencyInjection;
using SourceGeneration.ActionDispatcher;

var services = new ServiceCollection().AddActionDispatcher().BuildServiceProvider();

var dispatcher = services.GetRequiredService<IActionDispatcher>();
var subscriber = services.GetRequiredService<IActionSubscriber>();

subscriber.Subscribe<Hello>(hello =>
{
    Console.WriteLine("Hello action dispatched.");
});

dispatcher.Dispatch(new Hello { Say = "Hello Source Generation!" });

Console.ReadLine();

public class Hello
{
    public string? Say { get; set; }

    [ActionHandler]
    public static void Handle(Hello hello)
    {
        Console.WriteLine(hello.Say);
    }
}