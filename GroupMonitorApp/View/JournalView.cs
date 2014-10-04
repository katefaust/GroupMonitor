using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using GroupMonitorApp.Control;
using GroupMonitorApp.Model;

namespace GroupMonitorApp.View
{
    class JournalView
    {
        private int SubjectCellWidth = 162;
        private int SubjectCellHeight = 35;
        private int StudentCellWidth = 179;
        private int StudentCellHeight = 20;
        private int BorderLeft = 10;
        private int BorderTop = 10;

        private Grid grid;
        private DateTime date;
        private int nOfStudents, nOfSubjects;
        private Journal journal;
        private Schedules schedules;
        private Cell[,] cells;

        public delegate void CellClickHandler(PresenceOnLesson info);
        public event CellClickHandler OnCellClick;

        public JournalView(Grid grid, DateTime date, Journal journal, Schedules schedules)
        {
            this.grid = grid;
            this.date = date;
            this.journal = journal;
            this.schedules = schedules;
            nOfStudents = journal.NumberOfStudents();
            nOfSubjects = schedules.NumberOfSubjects(date);
        }
        private void DrawCells()
        {
            int width = (SubjectCellHeight + 1) / 2;
            int height = StudentCellHeight;

            for (int i = 0; i < nOfStudents; i++)
            {
                for (int j = 0; j < nOfSubjects * 2; j++)
                {
                    cells[i, j] = new Cell(i + 1, (int)Math.Round((double)((j + 2) / 2)), (((j + 1) % 2 != 0) ? 1 : 2), BorderLeft - 1 + StudentCellWidth + j * (width - 1), BorderTop + SubjectCellWidth - 1 + i * (height - 1), width, height);
                    cells[i, j].MouseLeftButtonDown += new MouseButtonEventHandler(CellClick);
                    cells[i, j].MouseRightButtonDown += new MouseButtonEventHandler(CellClear);
                    grid.Children.Add(cells[i, j]);
                }
            }

        }
        private void DrawLabel(Label l, int width, int height, string content)
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
        private void TransferLabel(Label l, int degree)
        {
            l.RenderTransform = new RotateTransform(degree);
        }
        private void DrawSubjects()
        {
            int n = schedules.NumberOfSubjects(date);
            Label[] l = new Label[n];
            int width = SubjectCellWidth;
            int height = SubjectCellHeight;
            int ofset = 1;
            for (int i = 0; i < n; i++)
            {                
                while (!schedules.HasEntry(date, i + ofset))
                {
                    ofset++;                    
                }
                l[i] = new Label();
                l[i].Name = "LabelSubject" + (i + 1).ToString();
                DrawLabel(l[i], width, height, schedules.GetSchedulesEntry(date, i + ofset).Subject.Name);
                l[i].Margin = new Thickness(BorderLeft - 1 + StudentCellWidth + i * (height - 1), BorderTop + width, 0, 0);
                TransferLabel(l[i], -90);
                grid.Children.Add(l[i]);
            }


        }
        private void DrawStudents()
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
                grid.Children.Add(l[i]);
            }
        }
        private void DrawCalendar(Grid mainGrid)
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
        private void calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            this.date = ((Calendar)sender).SelectedDate.Value;
            DrawScene();
        }
        public void DrawScene()
        {
            nOfSubjects = schedules.NumberOfSubjects(date);
            cells = new Cell[nOfStudents, nOfSubjects * 2];
            grid.Children.Clear();
            DrawSubjects();
            DrawStudents();
            DrawCells();
            if (journal.HasEntry(date))
                FillCells();
            DrawCalendar(grid);
        }
        private void FillCells()
        {
            if (nOfStudents == 0 || nOfSubjects == 0) return;
            foreach (var entry in journal.GetEntries(date, date))
            {
                cells[entry.Stud.Id - 1, (entry.SubjNumber - 1) * 2].State = entry.FirstHour;
                cells[entry.Stud.Id - 1, (entry.SubjNumber - 1) * 2 + 1].State = entry.SecondHour;
            }
        }
        private void CellClick(object sender, EventArgs e)
        {
            Cell cell = (Cell)sender;
            cell.ChangeState();
            CallJournal(cell.StudentId, cell.SubjectNumber);

        }
        private void CellClear(object sender, EventArgs e)
        {
            Cell cell = (Cell)sender;
            cell.State = StudentPresence.Present;
            CallJournal(cell.StudentId, cell.SubjectNumber);
        }
        private void CallJournal(int studentId, int subjectNumber)
        {
            PresenceOnLesson info = new PresenceOnLesson();
            info.Date = date;
            info.StudentId = studentId;
            info.SubjectNumber = subjectNumber;
            info.FirstHour = cells[studentId-1, (subjectNumber - 1) * 2].State;
            info.SecondHour = cells[studentId-1, (subjectNumber - 1) * 2 + 1].State;
            OnCellClick(info);
        }
    }

    public class PresenceOnLesson
    {
        public DateTime Date { get; set; }
        public int StudentId { get; set; }
        public int SubjectNumber { get; set; }
        public StudentPresence FirstHour { get; set; }
        public StudentPresence SecondHour { get; set; }
    }
}
