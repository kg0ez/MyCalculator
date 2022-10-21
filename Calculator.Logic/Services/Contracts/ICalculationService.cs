using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Logic.Services
{
    public interface ICalculationService
    {
        string GetResultString();
        void GetResult();
        void Calculate(string expression);
        void ClearOutputField();
        void PlusMinus();
    }
}
