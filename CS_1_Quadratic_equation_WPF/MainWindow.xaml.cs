using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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

            textBox_a.GotMouseCapture += TextBox_a_GotMouseCapture;
            textBox_b.GotMouseCapture += TextBox_b_GotMouseCapture;
            textBox_c.GotMouseCapture += TextBox_c_GotMouseCapture;
        }

        private void TextBox_c_GotMouseCapture(object sender, MouseEventArgs e)
        {
            textBox_c.Background = Brushes.White;
        }

        private void TextBox_b_GotMouseCapture(object sender, MouseEventArgs e)
        {
            textBox_b.Background = Brushes.White;
        }

        private void TextBox_a_GotMouseCapture(object sender, MouseEventArgs e)
        {
            textBox_a.Background = Brushes.White;
        }

        private void Button_solve_Click(object sender, RoutedEventArgs e)
        {
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
                    label_answer.Content = "Корень равен " + equation.X1.ToString() + ".";
                }

                else if (equation.NoX)
                {
                    label_answer.Content = "Корней нет.";
                }

                else
                {
                    label_answer.Content = "1 корень равен " + equation.X1 + ". 2 корень равен " + equation.X2 + ".";
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
        /// Разрешение ввода лишь цифр, '-' в начале и 1 запятой после цифры.
        /// </summary>
        /// <param name="textBox">Обрабатываемый текстбокс.</param>
        /// <param name="e">Нажатая клавиша.</param>
        private void IsValidKey(TextBox textBox, TextCompositionEventArgs e)
        {
            char number = e.Text;
            char charBeforeCursor; //Символ до курсора.

            try
            {
                charBeforeCursor = textBox.Text[textBox.SelectionStart - 1];
            }

            catch
            {
                charBeforeCursor = ' ';
            }

            //Игнорирование неподходящих символов при вводе коэффициентов.
            if (!Char.IsDigit(number))
            {
                //Разрешаем печатать '-' в начале.
                if (number == '-' && textBox.SelectionStart == 0)
                {

                }

                //Разрешаем печатать лишь 1 запятую и лишь после цифры.
                else if (number == ',' && (Char.IsDigit(charBeforeCursor) && !textBox.Text.Contains(',')))
                {

                }

                //Разрешаем пользоваться Backspace.
                else if (number == 8)
                {

                }

                //Игнорируем все непредусмотренные символы.
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void TextBox_a_KeyDown(object sender, KeyEventArgs e)
        {
            IsValidKey(textBox_a, e);
        }

        private void TextBox_b_KeyDown(object sender, KeyEventArgs e)
        {
            IsValidKey(textBox_b, e);
        }

        private void TextBox_c_KeyDown(object sender, KeyEventArgs e)
        {
            IsValidKey(textBox_c, e);
        }
    }
}
