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
using System.Text.RegularExpressions;
using System.Data;

namespace calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string number = "";
        bool test = false;

        public MainWindow()
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            Button btn = sender as Button;
            number = btn.Content.ToString();

            if (Regex.IsMatch(number, "[0-9]"))
            {
                test = true;


            }
            else if (Regex.IsMatch(number, "[-*/+]") && test == false)
            {
                number = "";
                textBox.Text = textBox.Text + number;
                test = false;
                return;
            }
            else if (Regex.IsMatch(number, "[-*/+]"))
            {
                test = false;


            }

            textBox.Text = textBox.Text+number;

        }

        private void Button_Count(object sender, RoutedEventArgs e)
        {
            if (textBox.Text != "")
            {
                double finalResult = 0;
                var text = textBox.Text;
                text = Regex.Replace(text, ",", ".");
                DataTable dt = new DataTable();
                try
                {
                    string lastChar = "";
                    lastChar = textBox.Text.ElementAt(textBox.Text.Length - 1).ToString();
                    if (Regex.IsMatch(lastChar, "[-*/+]"))
                    {
                        text = textBox.Text.Remove(textBox.Text.Length - 1, 1);


                    }
                    var result = dt.Compute(text, null);
                    finalResult = Convert.ToDouble(result);

                    if (double.IsInfinity(finalResult))
                    {

                        textBox.Text = "Ошибка!";

                        return;
                    }

                }
                catch
                {

                    textBox.Text = "Ошибка!";
                    return;

                }

                textBox.Text = finalResult.ToString();
            }
        }

        private void Button_Clear(object sender, RoutedEventArgs e)
        {
            textBox.Text = "";
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonStretch_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void ButtonCollapse_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
