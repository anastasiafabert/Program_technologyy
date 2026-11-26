namespace GameLibrary.Interfaces
{
    public interface IUserInterface
    {
        void ShowMessage(string message);

        string ReadInput(string message);
    }
}