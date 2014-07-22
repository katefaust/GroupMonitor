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
using System.Windows.Shapes;

namespace GroupMonitorApp
{
    /// <summary>
    /// Логика взаимодействия для Schedule.xaml
    /// </summary>
    public partial class Schedule : Window
    {

        private int BorderLeft = 25;
        private int BorderTop = 50;
        private int SubjectWidth = 120;
        private int SubjectHeight = 25;
        private int SchSubjectWidth = 120;
        private int SchSubjectHeight = 75;
        private int TextBoxCount = 0;
        //public List<string> Subjects = new List<string>();


        public Schedule()
        {
            InitializeComponent();
            DrawList();
            DrawSchedule();
        }

        //<Border BorderBrush="Black" BorderThickness="1" HorizontalAlignment="Left" Height="300" Margin="10,10,0,0" VerticalAlignment="Top" Width="153"/>
        public void DrawTextBox(TextBox t, int borderLeft, int borderTop, string name)
        {
            t.Text = "";
            t.HorizontalAlignment = 0;
            t.VerticalAlignment = 0;
            t.Margin = new Thickness(borderLeft, borderTop, 0, 0);
            t.Padding = new Thickness(4, 0, 4, 0);
            t.Width = SubjectWidth;
            t.Height = SubjectHeight;
            t.Name = name;
            schGrid.Children.Add(t);
            TextBoxCount++;
        }
        public void DrawButton(Button b, string name, string  content)
        {
            b.HorizontalAlignment = 0;
            b.VerticalAlignment = 0;
            b.Width = SubjectWidth;
            b.Height = SubjectHeight;
            b.Margin = new Thickness(BorderLeft, BorderTop + b.Height + 3, 0, 0);
            b.Padding = new Thickness(4, 0, 4, 0);
            b.Click += new RoutedEventHandler(ButtonClick);
            b.Content = content;
            b.Name = name;
            schGrid.Children.Add(b);
        }
        public void DrawLabel(Label l, int left, int top, int width, int height, string content)
        {
            TextBlock tb = new TextBlock();
            tb.Text = content;
            tb.TextWrapping = TextWrapping.Wrap;
            l.Content = tb;
            l.HorizontalAlignment = 0;
            l.VerticalAlignment = 0;
            l.Margin = new Thickness(left, top, 0, 0);
            l.Padding = new Thickness(4, 0, 4, 0);
            l.Width = width;
            l.Height = height;
            l.HorizontalContentAlignment = HorizontalAlignment.Center;
            l.VerticalContentAlignment = VerticalAlignment.Center;
            l.BorderBrush = Brushes.LightGray;
            l.Background = Brushes.White;
            l.BorderThickness = new Thickness(1);
            schGrid.Children.Add(l);
            var a = l.GetValue(Content);
        }
        public void DrawList()
        {
            TextBox t = new TextBox();
            DrawTextBox(t, BorderLeft, BorderTop, "TextBoxSubject" + TextBoxCount);

            Button b = new Button();
            DrawButton(b, "mainButton","Добавить");
        }
        public void ButtonClick(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;

            TextBox t = new TextBox();
            DrawTextBox(t, (int)b.Margin.Left, (int)b.Margin.Top, "TextBoxSubject" + TextBoxCount);
            
            b.Margin = new Thickness(t.Margin.Left, t.Margin.Top + SubjectHeight + 3, 0, 0);

            var replaceable = schGrid.Children.OfType<TextBox>().FirstOrDefault(block => block.Name == "TextBoxSubject" + (TextBoxCount-2));
            Label replacement = new Label();
            DrawLabel(replacement, (int)replaceable.Margin.Left, (int)replaceable.Margin.Top, SubjectWidth, SubjectHeight, replaceable.Text);
            //Subjects.Add(replaceable.Text);
            schGrid.Children.Remove(replaceable);

        }


        //<Label HorizontalAlignment="Left" Margin="190,10,0,0" VerticalAlignment="Top" Height="75" Width="120"/>
        public void DrawSchedule()
        {
            Label[,] SchCells = new Label[5,5];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    SchCells[i, j] = new Label();
                    DrawLabel(SchCells[i, j], 200, 10, SchSubjectWidth, SchSubjectHeight, "");
                    SchCells[i, j].Name = "LabelDay" + (0 + 1) + "Subj" + (0 + 1);
                    //l.Margin = new Thickness(200, 10, 0, 0);
                    SchCells[i, j].BorderBrush = Brushes.Black;
                }
            }

            for (int i = 0; i < 10; i++)
            {

            }

            for (int i = 0; i < 5; i++)
            {

            }


        }
    }
}