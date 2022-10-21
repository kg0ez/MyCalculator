using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Model.Models
{
    public class CalculatorView
    {
        private const string _zero = "0";
        private const char _minus = '-';

        private StringBuilder _expression = new StringBuilder();
        private StringBuilder _result = new StringBuilder(10);

        public delegate void EventHandler();
        public static event EventHandler? ResultChanged;

        public string Expression
        {
            get => _expression.ToString();
            set
            {
                if (value != string.Empty)
                    _expression.Append(value);
                else 
                    _expression = new StringBuilder(value);
            }
        }

        public string Result
        {
            get => _result.ToString();
            set
            {
                if (value == string.Empty)
                {
                    _result.Clear();

                    ResultChanged!();

                    return;
                }

                if (_result.ToString() == _zero && char.IsDigit(char.Parse(value)))
                {
                    return;
                }

                if (value[1..] == _result!.ToString() && value[0] == _minus)
                {
                    _result.Clear();
                    _result.Append(value);

                    ResultChanged!();
                }
                _result = _result.Append(value);
                ResultChanged!();
            }
        }
    }
}
