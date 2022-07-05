namespace VirtualFreezer.Shared.Infrastructure.Security.Random;

public interface IRng
{
    string Generate(int length = 50, bool removeSpecialChars = true);
}