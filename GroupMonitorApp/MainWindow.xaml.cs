﻿using GroupMonitorApp.Model;
using GroupMonitorApp.Model.Entities;
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
using GroupMonitorApp.Control;

namespace GroupMonitorApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int SubjectCellWidth = 162;
        private int SubjectCellHeight = 35;
        private int StudentCellWidth = 179;
        private int StudentCellHeight = 20;
        private int BorderLeft = 10;
        private int BorderTop = 10;
        //private Journal journal;
        //private Schedules schedules;
        public MainWindow()
        {
            InitializeComponent();
            Object[] students = new Object[15];
            for (int i = 0; i < students.Count(); i++)
                students[i] = "Student #" + (i + 1);
            DrawScene(students, "02/09/2014");
            //journal = new Journal();
            //schedules = new Schedules();
                        
        }


        public int GetNumberOfSubj(Object date) //Возвращает количество предметов в определенный день
        {
            int NumberOfSubj = 4;
            return NumberOfSubj;
        }
        public int GetNumberOfStud(Object[] stud) //Возвращает количество студентов
        {
            return stud.Count();
        }


        public void DrawLabel(Label l, int width, int height, string content)
        {
            TextBlock tb = new TextBlock();
            tb.Text = content;
            tb.TextWrapping = TextWrapping.Wrap;
            l.Content = tb;
            l.HorizontalAlignment = 0;
            l.VerticalAlignment = 0;
            l.VerticalContentAlignment = VerticalAlignment.Center;
            l.Padding = new Thickness(4, 0, 4, 0);
            l.Width = width;
            l.Height = height;
            l.BorderBrush = Brushes.Black;
            l.BorderThickness = new Thickness(1);
        }
        public void TransferLabel(Label l, int degree)
        {
            l.RenderTransform = new RotateTransform(degree);
        }


        public void DrawSubjects(Object date)
        {
            int n = GetNumberOfSubj(date);
            Label[] l = new Label[n];
            int width = SubjectCellWidth;
            int height = SubjectCellHeight;

            for (int i = 0; i < n; i++)
            {
                l[i] = new Label();
                l[i].Name = "LabelSubject" + (i + 1).ToString();
                DrawLabel(l[i], width, height, "Subject #" + (i + 1));
                l[i].Margin = new Thickness(BorderLeft - 1 + StudentCellWidth + i * (height - 1), BorderTop + width, 0, 0);
                TransferLabel(l[i], -90);
                mainGrid.Children.Add(l[i]);
            }


        }
        public void DrawStudents(Object[] stud)
        {
            int n = GetNumberOfStud(stud);
            Label[] l = new Label[n];
            int width = StudentCellWidth;
            int height = StudentCellHeight;

            for (int i = 0; i < n; i++)
            {
                l[i] = new Label();
                l[i].Name = "LabelStudent" + (i + 1).ToString();
                DrawLabel(l[i], width, height, "Student #" + (i + 1));
                l[i].Margin = new Thickness(BorderLeft, BorderTop + SubjectCellWidth + (i * (height - 1)) - 1, 0, 0);
                mainGrid.Children.Add(l[i]);
                var a = mainGrid.Children.OfType<TextBox>().FirstOrDefault(block => block.Name == "");
            }
        }
        public void DrawCells(Object date, Object[] stud)
        {
            int n = GetNumberOfStud(stud);
            int m = GetNumberOfSubj(date);
            Label[,] l = new Label[n, m * 2];
            int width = (SubjectCellHeight + 1) / 2;
            int height = StudentCellHeight;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m * 2; j++)
                {
                    l[i, j] = new Label();
                    DrawLabel(l[i, j], width, height, "");
                    l[i, j].Name = "LabelSt" + (i + 1).ToString() + "Subj" + (j / 2 + 1).ToString() + "Cell" + (((j + 1) % 2 != 0) ? 1 : 2).ToString();
                    l[i, j].Margin = new Thickness(BorderLeft - 1 + StudentCellWidth + j * (width - 1), BorderTop + SubjectCellWidth - 1 + i * (height - 1), 0, 0);
                    l[i, j].Background = Brushes.White;
                    l[i, j].MouseLeftButtonDown += new MouseButtonEventHandler(CellClick);
                    l[i, j].MouseRightButtonDown += new MouseButtonEventHandler(CellClear);
                    mainGrid.Children.Add(l[i, j]);
                }
            }

        }

        public void DrawCalendar(Grid mainGrid)
        {
            Calendar calendar = new Calendar();
            calendar.Name = "MainCalendar";
            calendar.DisplayMode = CalendarMode.Year;
            calendar.BorderBrush = Brushes.Black;
            calendar.Margin = new Thickness(BorderLeft, BorderTop - 3, 0, 0);
            calendar.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            calendar.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            mainGrid.Children.Add(calendar);
        }

        public void DrawScene(Object[] students, Object date)
        {
            DrawSubjects(date);
            DrawStudents(students);
            DrawCells(date, students);
            DrawCalendar(mainGrid);

        }


        public void CellClick(object sender, EventArgs e)
        {
            Label l = (Label)sender;
            if (l.Background == Brushes.White)
                l.Background = Brushes.Red;
            else
            {
                if (l.Background == Brushes.Red)
                    l.Background = Brushes.LimeGreen;
                else
                {
                    if (l.Background == Brushes.LimeGreen)
                        l.Background = Brushes.White;
                }
            }
            string[] s = l.Name.Split(new string[] { "LabelSt", "Subj", "Cell" }, StringSplitOptions.RemoveEmptyEntries);
            //journal.AddEntry(int.Parse(s[0]), int.Parse(s[1]), new DateTime(2014,9,1), schedules.entries.Where(x => x.DayOfWeek == DayOfWeek.Monday && x.WeekType == 1 && x.SubjNumber == 1).First(), 1, false);
        }
        public void CellClear(object sender, EventArgs e)
        {
            Label l = (Label)sender;
            l.Background = Brushes.White;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Schedule sch = new Schedule();
            sch.Show();
        }
    }
}
