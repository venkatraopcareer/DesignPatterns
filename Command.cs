The Command pattern is a behavioral design pattern that we can use to turn a request into an object that contains all the information about the request.
interface ICommand
{
    void execute();
}
class LightCommand : ICommand
{
    Light _light = null;
    internal LightCommand(Light light)
    {
        _light = light;
    }
    public void execute()
    {
        _light.On();
        Console.WriteLine("Execute");
    }
}
class Light
{
    public Light()
    {
    }
    internal bool On()
    { return true; }
    internal bool Off() { return false; }
}
class RemoteController
{
    ICommand _command;
    internal RemoteController(ICommand command)
    {
        _command = command;
    }
    internal string PressButton()
    {
        _command.execute();
        return "On";
    }

}
 Light light= new Light();
 RemoteController remoteController = new RemoteController(new LightCommand(light));
 remoteController.PressButton();


//////////////////////////////////////////////////////////////////////

public interface ICommand1
{
    void ExecuteAction();
}
public class Product
{
    public string Name { get; set; }
    public int Price { get; set; }

    public Product(string name, int price)
    {
        Name = name;
        Price = price;
    }

    public void IncreasePrice(int amount)
    {
        Price += amount;
        Console.WriteLine($"The price for the {Name} has been increased by {amount}$.");
    }

    public void DecreasePrice(int amount)
    {
        if (amount < Price)
        {
            Price -= amount;
            Console.WriteLine($"The price for the {Name} has been decreased by {amount}$.");
        }
    }

    public override string ToString() => $"Current price for the {Name} product is {Price}$.";
}
public class ProductCommand : ICommand1
{
    private readonly Product _product;
    private readonly PriceAction _priceAction;
    private readonly int _amount;

    public ProductCommand(Product product, PriceAction priceAction, int amount)
    {
        _product = product;
        _priceAction = priceAction;
        _amount = amount;
    }

    public void ExecuteAction()
    {
        if (_priceAction == PriceAction.Increase)
        {
            _product.IncreasePrice(_amount);
        }
        else
        {
            _product.DecreasePrice(_amount);
        }
    }
}
public class ModifyPrice
{
    private readonly List<ICommand1> _commands;
    private ICommand1 _command;

    public ModifyPrice()
    {
        _commands = new List<ICommand1>();
    }

    internal void SetCommand(ICommand1 command) => _command = command;

    public void Invoke()
    {
        _commands.Add(_command);
        _command.ExecuteAction();
    }
}
public enum PriceAction
{
    Increase,
    Decrease
}
Client-
        var modifyPrice = new ModifyPrice();
    var product = new Product("Phone", 500);
    Execute(product, modifyPrice, new ProductCommand(product, PriceAction.Increase, 100));

    Execute(product, modifyPrice, new ProductCommand(product, PriceAction.Increase, 50));
    Execute(product, modifyPrice, new ProductCommand(product, PriceAction.Decrease, 25));
    Console.WriteLine(product);

private static void Execute(Product product, ModifyPrice modifyPrice, ICommand1 productCommand)
{
    modifyPrice.SetCommand(productCommand);
    modifyPrice.Invoke();
}
