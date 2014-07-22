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

        private int SchWeekWidth = 30;
        private int SchWeekHeight = 38;

        private int SchDayWidth = 50;

        private int SchLeft = 195;
        private int SchTop = 35;

        private int TextBoxCount = 0;


        public Schedule()
        {
            InitializeComponent();
            DrawList();
            DrawSchedule();
        }

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
        public void DrawButton(Button b, string name, string content)
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
        public void DrawLabel(Label l, int left, int top, int width, int height, string content, string name)
        {
            TextBlock tb = new TextBlock();
            tb.Text = content;
            tb.TextWrapping = TextWrapping.Wrap;
            tb.Name = "TextBox" + name;
            l.Content = tb;
            l.Name = "Label" + name;
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
            l.AllowDrop = true;
            l.Drop += SubjectCopy;
            schGrid.Children.Add(l);
        }
        public void DrawList()
        {
            TextBox t = new TextBox();
            DrawTextBox(t, BorderLeft, BorderTop, "TextBoxSubject" + TextBoxCount);

            Button b = new Button();
            DrawButton(b, "mainButton", "Добавить");
        }
        public void ButtonClick(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;

            TextBox t = new TextBox();
            DrawTextBox(t, (int)b.Margin.Left, (int)b.Margin.Top, "TextBoxSubject" + TextBoxCount);

            b.Margin = new Thickness(t.Margin.Left, t.Margin.Top + SubjectHeight + 3, 0, 0);

            var replaceable = schGrid.Children.OfType<TextBox>().FirstOrDefault(block => block.Name == "TextBoxSubject" + (TextBoxCount - 2));
            Label replacement = new Label();
            DrawLabel(replacement, (int)replaceable.Margin.Left, (int)replaceable.Margin.Top, SubjectWidth, SubjectHeight, replaceable.Text, "LabelSubject" + (TextBoxCount - 2));
            schGrid.Children.Remove(replaceable);
            replacement.MouseMove += SubjectMouseMove;
        }

        public void DrawCells(Label l, int i, int j)
        {
            l = new Label();
            DrawLabel(l, SchLeft + SchDayWidth + SchWeekWidth - 2, SchTop, SchSubjectWidth, SchSubjectHeight, "", "Day" + (i + 1) + "Subj" + (j + 1));
            //l.Name = "LabelDay" + (i + 1) + "Subj" + (j + 1);
            l.Margin = new Thickness(SchLeft + SchDayWidth + SchWeekWidth - 2 + i * (SchSubjectWidth - 1), SchTop + j * (SchSubjectHeight - 1), 0, 0);
            l.BorderBrush = Brushes.Black;
            l.MouseDoubleClick += BigCellDoubleClick;
        }
        public void DrawWeeks(Label l, int i)
        {
            l = new Label();
            string name = "";
            switch (i % 2)
            {
                case 0: name = "I"; break;
                default: name = "II"; break;
            }
            DrawLabel(l, SchLeft + SchDayWidth - 1, SchTop, SchWeekWidth, SchWeekHeight, name, "Day" + (i + 1) + "Week" + (i % 2 == 0 ? 1 : 2));
            //l.Name = "LabelDay" + (i + 1) + "Week" + (i % 2 == 0 ? 1 : 2);
            l.Margin = new Thickness(SchLeft + SchDayWidth - 1, SchTop + i * (SchWeekHeight - 1), 0, 0);
            l.BorderBrush = Brushes.Black;
        }
        public void DrawDays(Label l, int i)
        {
            l = new Label();
            string name = "";
            switch (i)
            {
                case 0: name = "ПН"; break;
                case 1: name = "ВТ"; break;
                case 2: name = "СР"; break;
                case 3: name = "ЧТ"; break;
                case 4: name = "ПТ"; break;
            }
            DrawLabel(l, SchLeft, SchTop, SchDayWidth, SchSubjectHeight, name, "Day" + (i + 1));
            //l.Name = "LabelDay" + (i + 1);
            l.Margin = new Thickness(SchLeft, SchTop + i * (SchSubjectHeight - 1), 0, 0);
            l.FontWeight = FontWeights.SemiBold;
            l.BorderBrush = Brushes.Black;
        }
        public void DrawSchedule()
        {
            Label[,] SchCells = new Label[5, 5];
            for (int i = 0; i < 5; i++) //Ячейки предметов
                for (int j = 0; j < 5; j++)
                    DrawCells(SchCells[i, j], i, j);

            Label[] SchWeek = new Label[10];
            for (int i = 0; i < 10; i++)
            {
                DrawWeeks(SchWeek[i], i);
            }

            Label[] SchDay = new Label[5];
            for (int i = 0; i < 5; i++)
                DrawDays(SchDay[i], i);
        }

        public void BigCellDoubleClick(object sender, RoutedEventArgs e)
        {
            Label clicked = (Label)sender;
            Label l1 = new Label();
            Label l2 = new Label();
            DrawLabel(l1, (int)clicked.Margin.Left, (int)clicked.Margin.Top, (int)clicked.Width, (int)(clicked.Height / 2 + 0.5), "", clicked.Name.Substring(5) + "Week1");
            DrawLabel(l2, (int)clicked.Margin.Left, (int)(clicked.Margin.Top + clicked.Height / 2), (int)clicked.Width, (int)(clicked.Height / 2 + 0.5), "", clicked.Name.Substring(5) + "Week2");
            l1.BorderBrush = Brushes.Black;
            l2.BorderBrush = Brushes.Black;
            string name = clicked.Name;
            //l1.Name = name + "Week1";
            //l2.Name = name + "Week2";
            l1.MouseDoubleClick += SmallCellDoubleClick;
            l2.MouseDoubleClick += SmallCellDoubleClick;
            schGrid.Children.Remove(clicked);
        }
        public void SmallCellDoubleClick(object sender, RoutedEventArgs e)
        {
            Label clicked = (Label)sender;
            string name = clicked.Name;
            int weekNumber = Convert.ToInt32(name.Substring(18));
            Label l = new Label();
            if (weekNumber == 1)
                DrawLabel(l, (int)clicked.Margin.Left, (int)clicked.Margin.Top, (int)SchSubjectWidth, (int)SchSubjectHeight, "", name.Substring(5, 14));
            else
                DrawLabel(l, (int)clicked.Margin.Left, (int)(clicked.Margin.Top - clicked.Height + 1), (int)SchSubjectWidth, (int)SchSubjectHeight, "", name.Substring(5, 14));
            l.BorderBrush = Brushes.Black;
            l.MouseDoubleClick += BigCellDoubleClick;
            //l.Name = name.Substring(0, 14);
        }

        public void SubjectMouseMove(object sender, MouseEventArgs e)
        {
            Label l = sender as Label;
            if (e.LeftButton == MouseButtonState.Pressed)
                DragDrop.DoDragDrop(l, l.Content, DragDropEffects.Copy);
        }
        public void SubjectCopy(object sender, DragEventArgs e)
        {
            Label l = sender as Label;
            string dataString = (string)e.Data.GetData(DataFormats.Text);
            l.Content = dataString.Substring(25);
        }
    }
}