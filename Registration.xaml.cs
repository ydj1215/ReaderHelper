using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Login
{
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void WindowMouseLeftButtonDown1(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ButtonCloseClick(object sender, RoutedEventArgs e)
        {
            SolidColorBrush b = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            buttonClose.Background = b;
            Close();
        }

        private void ButtonAcceptClick1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLogin.Text) ||
                string.IsNullOrWhiteSpace(passwordBoxPass.Password) ||
                string.IsNullOrWhiteSpace(passwordBoxPassAgain.Password))
                MessageBox.Show("빈칸을 입력해주세요!");
            else
            {
                if (passwordBoxPass.Password != passwordBoxPassAgain.Password)
                {
                    MessageBox.Show("비밀번호가 틀립니다! =(");
                    passwordBoxPassAgain.Focus();
                    passwordBoxPassAgain.SelectAll();
                }
                else
                {
                    Users tmp = new Users(textBoxLogin.Text, passwordBoxPass.Password);
                    if (MainWindow.UsersList.Contains(tmp))
                    {
                        MessageBox.Show(string.Format("회원정보가 " + textBoxLogin.Text + " 이미 존재합니다" +
                                                  "\n다시 입력해주세요!"));
                        textBoxLogin.Clear();
                        passwordBoxPass.Clear();
                        passwordBoxPassAgain.Clear();
                    }
                    else
                    {
                        MainWindow.UsersList.Add(tmp);
                        MainWindow.WriteAllLoginsToBase();
                        // MessageBox.Show(string.Format("회원가입: {0} ", textBoxLogin.Text),
                        //            "이 완료되었습니다!");
                        MessageBox.Show("회원가입이 완료되었습니다!");
                        MainWindow window = new MainWindow();
                        Close();
                        window.ShowDialog();
                    }
                }
            }

        }

        private void ButtonCancelClick(object sender, RoutedEventArgs e)
        {
            MainWindow tmp = new MainWindow();
            Close();
            tmp.ShowDialog();
        }
    }
}
