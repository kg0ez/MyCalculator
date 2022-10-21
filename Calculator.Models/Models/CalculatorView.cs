using System.Text;

namespace Calculator.Model.Models
{
    public class CalculatorView
    {
        private StringBuilder? _expression = new StringBuilder();
        private StringBuilder? _result = new StringBuilder(10);

        public delegate void EventHandler();
        public static event EventHandler? ResultChanged;

        public string Expression
        {
            get => _expression!.ToString();
            set
            {
                if (value != string.Empty)
                    _expression!.Append(value);
                else 
                    _expression = new StringBuilder(value);
            }
        }

        public string Result
        {
            get => _result!.ToString();
            set
            {
                if (value == string.Empty)
                {
                    _result.Clear();

                    ResultChanged!();

                    return;
                }

                if (_result.ToString() == CalculatorSymbols.ZERO && char.IsDigit(char.Parse(value)))
                {
                    return;
                }

                if (value[1..] == _result!.ToString() && value[0] == CalculatorSymbols.MINUS)
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
