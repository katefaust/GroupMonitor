using GroupMonitorApp.Control;
using GroupMonitorApp.Model;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using GroupMonitorApp.View;

namespace GroupMonitorApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DateTime date = Schedules.SemesterStartedDate;
        private Journal journal;
        private Schedules schedules;
        private JournalView cells;
        public MainWindow()
        {
            InitializeComponent();
            journal = new Journal();
            schedules = new Schedules();
            cells = new JournalView(mainGrid, date, journal, schedules);
            cells.OnCellClick +=cells_OnCellClick;
            cells.DrawScene();  
        }

        void cells_OnCellClick(PresenceOnLesson info)
        {
            if (journal.HasEntry(info.StudentId, info.SubjectNumber, info.Date))
                journal.UpdateEntry(info.StudentId, info.SubjectNumber, date, info.FirstHour, info.SecondHour);
            else
                journal.AddEntry(info.StudentId, info.SubjectNumber, date, schedules.GetSchedulesEntry(info.Date, info.SubjectNumber), info.FirstHour, info.SecondHour);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Schedule sch = new Schedule();
            sch.Show();
        }
    }
}
