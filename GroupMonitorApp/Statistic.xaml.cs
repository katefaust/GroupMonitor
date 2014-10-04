using GroupMonitorApp.Control;
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
using System.Windows.Shapes;

namespace GroupMonitorApp
{
    /// <summary>
    /// Interaction logic for Statistic.xaml
    /// </summary>
    public partial class Statistic : Window
    {
        private Journal journal;
        public Statistic(Journal journal, Schedules schedule)
        {
            InitializeComponent();
            this.journal = journal;
            cBSubject.ItemsSource = schedule.GetSubjectsList();
            cBSubject.DisplayMemberPath = "Name";
            cBSubject.SelectedIndex = 0;
        }
        private void FillGrid()
        {
            List<StatisticEntry> entries = new List<StatisticEntry>();
            for (int i = 1; i <= journal.NumberOfStudents(); i++)
            {
                entries.Add(new StatisticEntry(journal.GetStudentById(i), (cBSubject.SelectedItem as Subject), journal));
            }
            dGStat.ItemsSource = entries;
            dGStat.EnableColumnVirtualization = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FillGrid();            
        }

        private class StatisticEntry
        {
            private Statistics statistic;
            public StatisticEntry(Student student, Subject subject, Journal journal)
            {
                statistic = new Statistics(journal);
                var a = statistic.StudentMissed(student.Id, subject);
                Name = student.Name;
                //validHours = a.ValidHours;
                //validLessons = a.ValidLessons;
                notValidHours = a.NotValidHours;
                notValidLessons = a.NotValidLessons;                
            }

            public string Name { get; set; }
            //public int validHours { get; set; }
            public int notValidHours { get; set; }
            //public int validLessons { get; set; }
            public int notValidLessons { get; set; }
            
        }
        
    }
}
