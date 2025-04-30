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
