namespace VirtualFreezer.Shared.Infrastructure.Contexts;

public interface IContextProvider
{
    IContext Current();
}