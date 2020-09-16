using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ClassLibrary;

namespace CS_1_Quadratic_equation_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Кнопка "Решить".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_solve_Click(object sender, RoutedEventArgs e)
        {
            label_answer.Content = "";

            try
            {
                QuadraticEquation equation = new QuadraticEquation(textBox_a.Text, textBox_b.Text, textBox_c.Text);

                if (equation.IsXAnyNumber)
                {
                    label_answer.Content = "x - любое число.";
                }

                else if (equation.IsEquationWrong)
                {
                    label_answer.Content = equation.C + " = 0 - неверное равенство.";
                }

                else if (equation.IsXOne)
                {
                    label_answer.Content = "x = " + equation.X1.ToString();
                }

                else if (equation.NoX)
                {
                    label_answer.Content = "Корней нет.";
                }

                else
                {
                    label_answer.Content = "x1 = " + equation.X1 + "\nx2 = " + equation.X2;
                }
            }

            catch (NotParsedException exc)
            {
                if (!exc.IsAParsed)
                {
                    textBox_a.Background = Brushes.LightPink;
                }

                if (!exc.IsBParsed)
                {
                    textBox_b.Background = Brushes.LightPink;
                }

                if (!exc.IsCParsed)
                {
                    textBox_c.Background = Brushes.LightPink;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Значения коэффициентов по умолчанию.
            textBox_a.Text = "1";
            textBox_b.Text = "1";
            textBox_c.Text = "0";
        }

        /// <summary>
        /// При изменении текста внутри любого текстбокса.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            int caretIndex = textbox.CaretIndex;
            int maxSymbolsQty = 15;
            int extraSymbolsQty = textbox.Text.Length - maxSymbolsQty;

            //Если превышено макс. количество символов в текстбоксе, то лишнее просто стирается.
            if (textbox.Text.Length > maxSymbolsQty)
            {
                textbox.Text = textbox.Text.Remove(caretIndex - extraSymbolsQty, extraSymbolsQty);
                textbox.CaretIndex = caretIndex - extraSymbolsQty;
            }

            else
            {
                if (double.TryParse(textbox.Text, out _))
                {
                    textbox.Background = Brushes.White;
                }

                else
                {
                    textbox.Background = Brushes.LightPink;
                }
            }
        }
    }
}
