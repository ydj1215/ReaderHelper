using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Login
{

    public partial class MainWindow : Window
    {
        static public List<Users> UsersList = new List<Users>(); //사용자 목록
        private int LoginedIndex;
        private bool Logined;
        public MainWindow()
        {
            InitializeComponent();
            ReadAllLoginsFromBase();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        //닫기
        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            SolidColorBrush b = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            buttonClose.Background = b;
            Close();
        }

        //회원가입
        private void ButtonRegClick1(object sender, RoutedEventArgs e)
        {
            Registration a = new Registration();
            Close();
            a.ShowDialog();

        }

        //로그인
        private void buttonEnter_Click_2(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBoxLogin.Text) || String.IsNullOrWhiteSpace(passwordBoxPass.Password))
            {
                MessageBox.Show("아이디와 비밀번호를 입력해주세요!");
                return;
            }
            for (int i = 0; i < UsersList.Count(); i++)
            {
                if(string.Compare(textBoxLogin.Text, UsersList[i].Login, StringComparison.OrdinalIgnoreCase) == 0 &&
                    string.Compare(passwordBoxPass.Password, UsersList[i].Password, StringComparison.CurrentCulture) ==0)
                {
                    MessageBox.Show("로그인 성공!");
                    Logined = true;
                    break;
                }
                
            }
            if(!Logined)
                MessageBox.Show("아이디 혹은 비밀번호가 존재하지 않습니다!");
        }

        public static void WriteAllLoginsToBase()
        {
            if (!Directory.Exists(@"files"))//폴더가 없다면 새로 만들기
                Directory.CreateDirectory(@"files");

            BinaryWriter writer = new BinaryWriter(File.Open(@"files\base.dat", FileMode.Create), Encoding.BigEndianUnicode);
            foreach (Users login in UsersList)
            {
                writer.Write(login.Login);
                writer.Write(login.Password);
            }
            writer.Close();
        }
        public static void ReadAllLoginsFromBase()
        {
            UsersList.Clear(); //중복을 피하기 위해 목록을 제거
            try
            {
                BinaryReader reader = new BinaryReader(File.Open(@"files\base.dat", FileMode.Open), Encoding.BigEndianUnicode);
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    UsersList.Add(new Users(reader.ReadString(), reader.ReadString()));
                }
                reader.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("프로필 데이터 파일에 오류가 발생했습니다!");
            }
        }
    }
}
