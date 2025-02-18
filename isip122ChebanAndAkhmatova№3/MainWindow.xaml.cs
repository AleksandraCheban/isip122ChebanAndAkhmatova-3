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

namespace isip122ChebanAndAkhmatova_3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            string height = textBoxHeight.Text.Trim();
            string weight = textBoxweight.Text.Trim();
            double answer = 0, x = Convert.ToInt32(height), y = Convert.ToDouble(weight); ;
            if (x < 130 || x > 220 || y < 40 || y > 170)
            {
                MessageBox.Show("Вы ввели недопустимые значения");
            }
            else
            {
                if (RB_man.IsChecked == true)
                {
                    answer = x - 100 + ((x - 100) * 13 / 100);

                    if (y >= (answer - 3.00) && y <= (answer + 3.00))
                    {
                        MessageBox.Show("С вашим весом все хорошо!");
                    }
                    else if (y < (answer - 3.00))
                    {
                        MessageBox.Show("Ваш вес ниже нормы");
                    }
                    else if (y > (answer + 3.00))
                    {
                        MessageBox.Show("Ваш вес выше нормы");
                    };

                }
                else if (RB_woman.IsChecked == true)
                {
                    answer = x - 100 - ((x - 100) * 10 / 100);
                    if (y >= (answer - 3.00) && y <= (answer + 3.00))
                    {
                        MessageBox.Show("С вашим весом все хорошо!");
                    }
                    else if (y < (answer - 3))
                    {
                        MessageBox.Show("Ваш вес ниже нормы");
                    }
                    else if (y > (answer + 3))
                    {
                        MessageBox.Show("Ваш вес выше нормы");
                    };
                }



            }

        }
    }
}
