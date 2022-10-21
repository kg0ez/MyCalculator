using Calculator.Logic.Services;
using Calculator.Model.Models;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Calculator.App
{
    public partial class MainWindow : Window
    {
        private static readonly CalculatorView _model = new CalculatorView();
        private readonly CalculationService  _calculationService = new CalculationService(_model);
        private readonly ButtonPresseService _buttonPresseService = new ButtonPresseService(_model);
        public MainWindow()
        {
            InitializeComponent();
            CalculatorView.ResultChanged += RefreshREsult;
        }
        private void RefreshREsult() 
        { 
            textDisplay.Text = _calculationService.GetResultString(); 
        }
        private void ActionButton_Click(object sender, RoutedEventArgs e)
        {
            _buttonPresseService.ActionButtonPressed((string)((Button)sender).Content);

            textDisplay.Text = string.Empty;
        }

        private void Number_Click(object sender, RoutedEventArgs e)
        {
            _buttonPresseService.NumberButtonPressed((string)((Button)sender).Content);
        }

        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
            _calculationService.GetResult();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            _calculationService.Clear();
        }
        private void PlusMinus_Click(object sender, EventArgs e)
        {
            _calculationService.PlusMinus();
        }
        public void OutputBox_TextChanged(object sender, EventArgs e)
        {
            string output = textDisplay.Text;

            if (output.Length == 0)
            {
                ButtonResult.IsEnabled = false;
                return;
            }

            if (char.IsPunctuation(output[^1]) || char.IsSymbol(output[^1]))
            {
                ButtonResult.IsEnabled = false;
                return;
            }

            ButtonResult.IsEnabled = true;
        }
    }
}
