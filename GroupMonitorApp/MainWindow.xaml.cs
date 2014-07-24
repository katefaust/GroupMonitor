using GroupMonitorApp.Control;
using GroupMonitorApp.Model;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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
        DateTime date = Schedules.SemesterStartedDate;
        private Journal journal;
        private Schedules schedules;
        public MainWindow()
        {
            InitializeComponent();
            journal = new Journal();
            schedules = new Schedules();

            DrawScene();  
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


        public void DrawSubjects()
        {
            int n = schedules.NumberOfSubjects(date);
            Label[] l = new Label[n];
            int width = SubjectCellWidth;
            int height = SubjectCellHeight;

            for (int i = 0; i < n; i++)
            {
                l[i] = new Label();
                l[i].Name = "LabelSubject" + (i + 1).ToString();
                DrawLabel(l[i], width, height, schedules.GetSchedulesEntry(date, i + 1).Subject.Name);
                l[i].Margin = new Thickness(BorderLeft - 1 + StudentCellWidth + i * (height - 1), BorderTop + width, 0, 0);
                TransferLabel(l[i], -90);
                mainGrid.Children.Add(l[i]);
            }


        }
        public void DrawStudents()
        {
            int n = journal.NumberOfStudents();
            Label[] l = new Label[n];
            int width = StudentCellWidth;
            int height = StudentCellHeight;

            for (int i = 0; i < n; i++)
            {
                l[i] = new Label();
                l[i].Name = "LabelStudent" + (i + 1).ToString();
                DrawLabel(l[i], width, height, journal.GetStudentById(i + 1).Name);
                l[i].Margin = new Thickness(BorderLeft, BorderTop + SubjectCellWidth + (i * (height - 1)) - 1, 0, 0);
                mainGrid.Children.Add(l[i]);
            }
        }
        public void DrawCells()
        {
            int n = journal.NumberOfStudents();
            int m = schedules.NumberOfSubjects(date);
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
            calendar.DisplayMode = CalendarMode.Month;
            calendar.BorderBrush = Brushes.Black;
            calendar.Margin = new Thickness(BorderLeft, BorderTop - 3, 0, 0);
            calendar.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            calendar.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            calendar.DisplayDateStart = Schedules.SemesterStartedDate;
            calendar.DisplayDate = DateTime.Now;
            calendar.SelectedDate = date;
            calendar.SelectedDatesChanged += calendar_SelectedDatesChanged;
            mainGrid.Children.Add(calendar);
        }

        void calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            this.date = ((Calendar)sender).SelectedDate.Value;
            DrawScene();
        }

        private void FillCells()
        {
            foreach (var entry in journal.GetEntries(date, date))
            {
                mainGrid.Children.OfType<Label>().FirstOrDefault(label => label.Name == "LabelSt" + entry.Stud.Id + "Subj" + entry.SubjNumber + "Cell1").Background = GetcolourForState(entry.FirstHour);
                mainGrid.Children.OfType<Label>().FirstOrDefault(label => label.Name == "LabelSt" + entry.Stud.Id + "Subj" + entry.SubjNumber + "Cell2").Background = GetcolourForState(entry.SecondHour); 
            }
        }
        private SolidColorBrush GetcolourForState(StudentPresence state)
        {
            switch (state)
            {
                case StudentPresence.AbsentNoReason: return Brushes.Red;
                case StudentPresence.AbsentWithReason: return Brushes.LimeGreen;
            }
            return Brushes.White;
        }

        public void DrawScene()
        {
            mainGrid.Children.Clear();
            DrawSubjects();
            DrawStudents();
            DrawCells();
            if (journal.HasEntry(date))
                FillCells();
            DrawCalendar(mainGrid);

        }


        public void CellClick(object sender, EventArgs e)
        {
            Label l = (Label)sender;
            StudentPresence state = StudentPresence.Present;
            if (l.Background == Brushes.White)
            {
                l.Background = Brushes.Red;
                state = StudentPresence.AbsentNoReason;
            }
            else
                if (l.Background == Brushes.Red)
                {
                    l.Background = Brushes.LimeGreen;
                    state = StudentPresence.AbsentWithReason;
                }
                else
                    if (l.Background == Brushes.LimeGreen)
                        l.Background = Brushes.White;
            string[] s = l.Name.Split(new string[] { "LabelSt", "Subj", "Cell" }, StringSplitOptions.RemoveEmptyEntries);
            SaveEntry(int.Parse(s[0]), int.Parse(s[1]), int.Parse(s[2]), state);
        }
        public void CellClear(object sender, EventArgs e)
        {
            Label l = (Label)sender;
            l.Background = Brushes.White;
            string[] s = l.Name.Split(new string[] { "LabelSt", "Subj", "Cell" }, StringSplitOptions.RemoveEmptyEntries);
            SaveEntry(int.Parse(s[0]), int.Parse(s[1]), int.Parse(s[2]), StudentPresence.Present);
        }
        public void SaveEntry(int studentId, int subjNumber, int cellNumber, StudentPresence state)
        {
            if (journal.HasEntry(studentId, subjNumber, date))
                journal.UpdateEntry(studentId, subjNumber, date, cellNumber, state);
            else
                journal.AddEntry(studentId, subjNumber, date, schedules.GetSchedulesEntry(date, subjNumber), cellNumber, state);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Schedule sch = new Schedule();
            sch.Show();
        }
    }
}
