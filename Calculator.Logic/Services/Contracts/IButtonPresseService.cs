namespace Calculator.Logic.Services
{
    public interface IButtonPresseService
    {
        void NumberButtonPressed(string input);
        void OperationButtonPressed(string input);
    }
}
