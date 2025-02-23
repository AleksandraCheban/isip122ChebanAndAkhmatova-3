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
using System.ComponentModel;

namespace isip122ChebanAndAkhmatova_3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Regex onlyNumbers = new Regex(@"^-?(\d+(,\d+)?)$");

        public MainWindow()
        {
            InitializeComponent();
        }

        //      Исключить ошибки ввода роста (диапазон 130-220 см) и веса (диапазон 40 – 170 кг).
        //      Выводимая характеристика оценки веса(ниже нормы, норма, выше нормы) ± 3 кг от нормы.
        //      Нормальный вес: мужчины = [рост]–100+13%; женщины = [рост]–100–10%.

        private void WrongInput()
        {
            // Очищает поля, если возникает какая-либо ошибка
            textBox_optimal_weight.Text = "";
            textBox_comparison.Text = "";
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            string height = textBoxHeight.Text.Trim(), weight = textBoxweight.Text.Trim();

            #region ошибки ввода
            if (textBoxHeight.Text == "" && textBoxweight.Text == "")
            {
                MessageBox.Show("Заполните все поля!", "Ошибочка", MessageBoxButton.OK, MessageBoxImage.Error);
                WrongInput();
            }
            else if (textBoxHeight.Text == "")
            {
                MessageBox.Show("Заполните поле с РОСТОМ", "Ошибочка", MessageBoxButton.OK, MessageBoxImage.Error);
                WrongInput();
            }
            else if (textBoxweight.Text == "")
            {
                MessageBox.Show("Заполните поле с ВЕСОМ", "Ошибочка", MessageBoxButton.OK, MessageBoxImage.Error);
                WrongInput();
            }
            else if (!onlyNumbers.IsMatch(textBoxHeight.Text) && !onlyNumbers.IsMatch(textBoxweight.Text))
            {
                MessageBox.Show("Вы ввели недопустимые символы в полях", "Ошибочка");
                WrongInput();
            }
            else if (!onlyNumbers.IsMatch(textBoxHeight.Text))
            {
                MessageBox.Show("Вы ввели недопустимые символы в поле с РОСТОМ", "Ошибочка", MessageBoxButton.OK, MessageBoxImage.Error);
                WrongInput();
            }
            else if (!onlyNumbers.IsMatch(textBoxweight.Text))
            {
                MessageBox.Show("Вы ввели недопустимые символы в поле с ВЕСОМ", "Ошибочка", MessageBoxButton.OK, MessageBoxImage.Error);
                WrongInput();
            }
            else if (RB_man.IsChecked == false && RB_woman.IsChecked == false)
            {
                MessageBox.Show("Вы не выбрали пол!", "Ошибочка", MessageBoxButton.OK, MessageBoxImage.Error);
                WrongInput();
            }
            #endregion

            else
            {
                double answer = 0, x = Convert.ToDouble(height), y = Convert.ToDouble(weight);
                bool flag = true;

                #region ошибки ввода (диапазон)
                if (x < 130 || x > 220 && y < 40 || y > 170)
                {
                    MessageBox.Show("Вы ввели недопустимые значения в полях", "Ошибочка", MessageBoxButton.OK, MessageBoxImage.Error);
                    flag = false;
                    WrongInput();
                }
                else if (x < 130 || x > 220)
                {
                    MessageBox.Show("Вы ввели недопустимые значения в поле с РОСТОМ (вне диапазона рассчета)", "Ошибочка", MessageBoxButton.OK, MessageBoxImage.Error);
                    flag = false;
                    WrongInput();
                }
                else if (y < 40 || y > 170)
                {
                    MessageBox.Show("Вы ввели недопустимые значения в поле с ВЕСОМ (вне диапазона рассчета)", "Ошибочка", MessageBoxButton.OK, MessageBoxImage.Error);
                    flag = false;
                    WrongInput();
                }
                #endregion

                #region рассчет веса + характеристика
                else
                {
                    if (flag == true)
                    {
                        if (RB_man.IsChecked == true)
                        {
                            answer = x - 100 + ((x - 100) * 13 / 100);
                            textBox_optimal_weight.Text = $"{answer - 3.00} - {answer + 3.00} ";

                            if (y >= (answer - 3.00) && y <= (answer + 3.00)) textBox_comparison.Text = "С вашим весом все хорошо"; // MessageBox.Show("С вашим весом все хорошо!");
                            else if (y < (answer - 3.00)) textBox_comparison.Text = "Ваш вес ниже нормы"; // MessageBox.Show("Ваш вес ниже нормы");
                            else if (y > (answer + 3.00)) textBox_comparison.Text = "Ваш вес выше нормы"; // MessageBox.Show("Ваш вес выше нормы");
                        }
                        else if (RB_woman.IsChecked == true)
                        {
                            answer = x - 100 - ((x - 100) * 10 / 100);
                            textBox_optimal_weight.Text = $"{answer - 3.00} - {answer + 3.00} ";

                            if (y >= (answer - 3.00) && y <= (answer + 3.00)) textBox_comparison.Text = "С вашим весом все хорошо"; // MessageBox.Show("С вашим весом все хорошо!");
                            else if (y < (answer - 3)) textBox_comparison.Text = "Ваш вес ниже нормы"; // MessageBox.Show("Ваш вес ниже нормы");
                            else if (y > (answer + 3)) textBox_comparison.Text = "Ваш вес выше нормы"; // MessageBox.Show("Ваш вес выше нормы");
                        }
                    }
                }
                #endregion
            }
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            var exitConfirmation = MessageBox.Show("Вы действительно хотите выйти?  :(", "Пока-пока", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (exitConfirmation == MessageBoxResult.No) e.Cancel = true; // Отменяем закрытие окна
        }
    }
}
