using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Logic.Services
{
    public interface IButtonPresseService
    {
        void NumberButtonPressed(string input);
        void OperationButtonPressed(string input);
    }
}
