using GroupMonitorApp.Model;
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

namespace GroupMonitorApp.View.Controls
{
    /// <summary>
    /// Interaction logic for ScheduleCell.xaml
    /// </summary>
    public partial class ScheduleCell : UserControl
    {
        

        public bool IsOneRow { get; private set; }
        public Subject FirstSubj {get; private set;}
        public Subject SecondSubj { get; private set; }
        public DayOfWeek DayOfWeek { get; set; }
        public int SubjNumber { get; set; }

        public ScheduleCell()
        {
            InitializeComponent();
            IsOneRow = true;
            FrameSubj1.Visibility = System.Windows.Visibility.Hidden;
            FrameSubj2.Visibility = System.Windows.Visibility.Hidden;
        }

        public void Add(Subject subject)
        {
            FirstSubj = subject;
            DrawCell();
        }
        public void Add(Subject subject, WeekType week)
        {
            if (week == WeekType.First) FirstSubj = subject;
            else SecondSubj = subject;
            DrawCell();
        }

        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ChangeState();
        }

        public void ChangeState()
        {
            if (IsOneRow) 
            { 
                IsOneRow = false;
                FrameSubjSingle.Visibility = System.Windows.Visibility.Hidden;
                FrameSubj1.Visibility = System.Windows.Visibility.Visible;
                FrameSubj2.Visibility = System.Windows.Visibility.Visible;
            }
            else 
            { 
                IsOneRow = true;
                FrameSubjSingle.Visibility = System.Windows.Visibility.Visible;
                FrameSubj1.Visibility = System.Windows.Visibility.Hidden;
                FrameSubj2.Visibility = System.Windows.Visibility.Hidden;
            }
            FirstSubj = null;
            SecondSubj = null;

        }
        private void DrawCell()
        {
            if (IsOneRow)
            {
                Label cell = new Label();
                FrameSubjSingle.Content = cell;
                cell.Content = FirstSubj.Name;
                cell.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                cell.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            }
            else
            {
                if (FirstSubj != null)
                {
                    Label cell = new Label();
                    FrameSubj1.Content = cell;
                    cell.Content = FirstSubj.Name;
                    cell.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                    cell.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                }
                if (SecondSubj != null)
                {
                    Label cell = new Label();
                    FrameSubj2.Content = cell;
                    cell.Content = SecondSubj.Name;
                    cell.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                    cell.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                }
            }
        }

    }
}
