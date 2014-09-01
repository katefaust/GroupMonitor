using GroupMonitorApp.Control;
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
    /// Interaction logic for ScheduleTable.xaml
    /// </summary>
    public partial class ScheduleTable : UserControl
    {
        public delegate void AddSchedule(ScheduleCell sender, WeekType week);
        public event AddSchedule OnAddEntry;

        List<ScheduleCell> cellList = new List<ScheduleCell>();

        public ScheduleTable()
        {
            InitializeComponent();
            
            cellList.Add(day1_1);
            cellList.Add(day1_2);
            cellList.Add(day1_3);
            cellList.Add(day1_4);
            cellList.Add(day1_5);
            cellList.Add(day2_1);
            cellList.Add(day2_2);
            cellList.Add(day2_3);
            cellList.Add(day2_4);
            cellList.Add(day2_5);
            cellList.Add(day3_1);
            cellList.Add(day3_2);
            cellList.Add(day3_3);
            cellList.Add(day3_4);
            cellList.Add(day3_5);
            cellList.Add(day4_1);
            cellList.Add(day4_2);
            cellList.Add(day4_3);
            cellList.Add(day4_4);
            cellList.Add(day4_5);
            cellList.Add(day5_1);
            cellList.Add(day5_2);
            cellList.Add(day5_3);
            cellList.Add(day5_4);
            cellList.Add(day5_5);
        }

        public void AddCells(Schedules schedules)
        {
            for (int subjNumber = 1; subjNumber <= 5; subjNumber++)
            {
                foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
                    if (day != DayOfWeek.Sunday && day != DayOfWeek.Saturday)
                    {
                        if (schedules.SubjInWeek(day, subjNumber) == 1)
                            cellList.Where(x => x.DayOfWeek == day && x.SubjNumber == subjNumber).First().Add(schedules.GetSchedulesEntry(day, WeekType.First, subjNumber).Subject);
                        if (schedules.SubjInWeek(day, subjNumber) == 2)
                        {
                            var cell = cellList.Where(x => x.DayOfWeek == day && x.SubjNumber == subjNumber).First();
                            cell.ChangeState();
                            if (schedules.HasEntry(day, WeekType.First, subjNumber)) 
                                cell.Add(schedules.GetSchedulesEntry(day, WeekType.First, subjNumber).Subject, WeekType.First);
                            if (schedules.HasEntry(day, WeekType.Second, subjNumber)) 
                                cell.Add(schedules.GetSchedulesEntry(day, WeekType.Second, subjNumber).Subject, WeekType.Second);
                        }
                    }

            }
        }

        private void ScheduleCell_Drop(object sender, DragEventArgs e)
        {
            var cell = sender as ScheduleCell;
            if (cell.IsOneRow)
            {
                cell.Add((Subject)e.Data.GetData(typeof(Subject)));
                OnAddEntry(cell, WeekType.First);
            }
            else
                if (e.GetPosition(cell).Y < cell.ActualHeight / 2)
                {
                    cell.Add((Subject)e.Data.GetData(typeof(Subject)), Model.WeekType.First);
                    OnAddEntry(cell, WeekType.First);
                }
                else
                {
                    cell.Add((Subject)e.Data.GetData(typeof(Subject)), Model.WeekType.Second);
                    OnAddEntry(cell, WeekType.Second);
                }
            
        }
    }
}
