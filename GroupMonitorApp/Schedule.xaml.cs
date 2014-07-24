using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
        private List<Button> Subjects= new List<Button>();

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
            t.MaxLength = 25;
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
            b.Content = content;
            b.Name = name;
            b.IsDefault = true;
            schGrid.Children.Add(b);
        }
        public void DrawLabel(Label l, int left, int top, int width, int height, string content, string name, bool allowDrop = false)
        {
            l.Content = content;
            l.Name = name;
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
            l.AllowDrop = allowDrop;
            l.Drop += SubjectCopy;
            schGrid.Children.Add(l);
        }
      
        public void DrawList()
        {
            TextBox t = new TextBox();
            DrawTextBox(t, BorderLeft, BorderTop, "TextBoxSubject" + TextBoxCount);

            Button b = new Button();
            DrawButton(b, "mainButton", "Добавить");
            b.Click += new RoutedEventHandler(ButtonClick);
        }
        public void ButtonClick(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            var replaceable = schGrid.Children.OfType<TextBox>().FirstOrDefault(block => block.Name == "TextBoxSubject" + (TextBoxCount - 1));
            if (replaceable.Text != "")
            {
                TextBox t = new TextBox();
                DrawTextBox(t, (int)b.Margin.Left, (int)b.Margin.Top, "TextBoxSubject" + TextBoxCount);

                b.Margin = new Thickness(t.Margin.Left, t.Margin.Top + SubjectHeight + 3, 0, 0);

                Button replacement = new Button();
                Subjects.Add(replacement);
                DrawButton(replacement, "LabelSubject" + (TextBoxCount - 1), replaceable.Text);
                replacement.Margin = new Thickness((int)replaceable.Margin.Left, (int)replaceable.Margin.Top, 0, 0);
                replacement.MouseEnter += ShowDel;
                replacement.MouseLeave += DelDel;
                schGrid.Children.Remove(replaceable);
                replacement.MouseMove += SubjectMouseMove;
            }
        }

        public void DrawCells(Label l, int i, int j)
        {
            l = new Label();

            DrawLabel(l, SchLeft + SchDayWidth + SchWeekWidth - 2, SchTop, SchSubjectWidth, SchSubjectHeight, "", "Day" + (i + 1) + "Subj" + (j + 1), true);
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
            l.Margin = new Thickness(SchLeft, SchTop + i * (SchSubjectHeight - 1), 0, 0);
            l.FontWeight = FontWeights.SemiBold;
            l.BorderBrush = Brushes.Black;
        }
        public void DrawSchedule()
        {
            Label[,] SchCells = new Label[5, 5];
            for (int i = 0; i < 5; i++) //Ячейки предметов
                for (int j = 0; j < 5; j++)
                {
                    DrawCells(SchCells[i, j], i, j);
                }
            

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
            DrawLabel(l1, (int)clicked.Margin.Left, (int)clicked.Margin.Top, (int)clicked.Width, (int)(clicked.Height / 2 + 0.5), "", clicked.Name.Substring(5) + "Week1", true);
            DrawLabel(l2, (int)clicked.Margin.Left, (int)(clicked.Margin.Top + clicked.Height / 2), (int)clicked.Width, (int)(clicked.Height / 2 + 0.5), "", clicked.Name.Substring(5) + "Week2", true);
            l1.BorderBrush = Brushes.Black;
            l2.BorderBrush = Brushes.Black;
            string name = clicked.Name;
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
                DrawLabel(l, (int)clicked.Margin.Left, (int)clicked.Margin.Top, (int)SchSubjectWidth, (int)SchSubjectHeight, "", name.Substring(5, 14), true);
            else
                DrawLabel(l, (int)clicked.Margin.Left, (int)(clicked.Margin.Top - clicked.Height + 1), (int)SchSubjectWidth, (int)SchSubjectHeight, "", name.Substring(5, 14), true);
            l.BorderBrush = Brushes.Black;
            l.MouseDoubleClick += BigCellDoubleClick;
        }

        public void SubjectMouseMove(object sender, MouseEventArgs e)
        {
            Label l = sender as Label;
            if (e.LeftButton == MouseButtonState.Pressed)
                DragDrop.DoDragDrop(l, l.Content.ToString(), DragDropEffects.Copy);
        }
        public void SubjectCopy(object sender, DragEventArgs e)
        {
            Label l = sender as Label;
            string dataString = (string)e.Data.GetData(DataFormats.Text);
            TextBlock tb = new TextBlock();
            tb.Text = dataString;
            tb.TextWrapping = TextWrapping.Wrap;
            l.Content = tb;
            if (l.Height == SchSubjectHeight)
                tb.FontSize = 14;
            else
                tb.FontSize = 12;
        }

        public void ShowDel(object sender, MouseEventArgs e)
        {
            Button l = (Button)sender;
            Button x = new Button();
            DrawButton(x, "ButtonX"+l.Name, "");
            x.Margin = new Thickness((int)(l.Margin.Left + l.Width), (int)l.Margin.Top, 0, 0);
            x.Width = 10;
            x.Height = 10;
            x.Padding = new Thickness(0,0,0,0);
            BitmapImage img = new BitmapImage(new Uri("pack://application:,,,/Del.png"));
            Image i = new Image();
            i.Source = img;
            x.Content = i;
            x.Click += DeleteSubject;
        }

        private void DeleteSubject(object sender, RoutedEventArgs e)
        {
            Button X = (Button)sender;
            string name = X.Name.Substring(7);
            Button l = schGrid.Children.OfType<Button>().FirstOrDefault(x => x.Name == name);

            TextBox tb = schGrid.Children.OfType<TextBox>().FirstOrDefault(x => x.Name == "TextBoxSubject" + (TextBoxCount-1));
            Button b = schGrid.Children.OfType<Button>().FirstOrDefault(x => x.Name == "mainButton");
            b.Margin = tb.Margin;
            tb.Margin = Subjects.Last().Margin;
            
            for (int i = Subjects.Count - 1; i > Subjects.IndexOf(l); i--)
                Subjects[i].Margin = Subjects[i - 1].Margin;

            Subjects.Remove(l);
            schGrid.Children.Remove(l);
        }
        public void DelDel(object sender, MouseEventArgs e)
        {
            Button l = (Button)sender;
            Thread t = new Thread(method);
            t.Start(l);
        }

        void method(object sender)
        {
            Thread.Sleep(500);
            var l = (Button)sender;
            l.Dispatcher.Invoke((Action)(() => { schGrid.Children.Remove(schGrid.Children.OfType<Button>().FirstOrDefault(x => x.Name == ("ButtonX" + l.Name))); }));
            
        }
    }
}