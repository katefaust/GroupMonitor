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
        private int TextBoxCount = 0;


        public Schedule()
        {
            InitializeComponent();
            DrawList();
        }

        //<Border BorderBrush="Black" BorderThickness="1" HorizontalAlignment="Left" Height="300" Margin="10,10,0,0" VerticalAlignment="Top" Width="153"/>
        public void DrawTextBox(TextBox t, double borderLeft, double borderTop, string name)
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

        public void DrawList()
        {
            TextBox t = new TextBox();
            DrawTextBox(t, BorderLeft, BorderTop, "TextBoxSubject" + TextBoxCount);

            Button b = new Button();
            DrawButton(b, "mainButton","Добавить");
            
            
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;

            TextBox t = new TextBox();
            DrawTextBox(t, b.Margin.Left, b.Margin.Top, "TextBoxSubject" + TextBoxCount);
            
            b.Margin = new Thickness(t.Margin.Left, t.Margin.Top + SubjectHeight + 3, 0, 0);

            /*var T = schGrid.FindName("mainButton");//("TextBoxSubject" + (TextBoxCount-2));
            Button q = (Button)T;
            q.Background = Brushes.Aquamarine;*/

            var replaceable = schGrid.Children.OfType<TextBox>().FirstOrDefault(block => block.Name == "TextBoxSubject" + (TextBoxCount-2));
            
        }
    }
}