using GroupMonitorApp.View;
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
using GroupMonitorApp.Model;
using GroupMonitorApp.Control;

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
        private Schedules schedules = new Schedules();


        public Schedule()
        {
            InitializeComponent();
            //DrawList();
            SubjectList.OnSubjectAdd += SubjectList_OnSubjectAdd;
            SubjectList.OnSubjectRemove += SubjectList_OnSubjectRemove;
            ScheduleTable.OnAddEntry += ScheduleTable_OnAddEntry;
            ScheduleTable.AddCells(schedules);
            //DrawSchedule();
        }

        void ScheduleTable_OnAddEntry(View.Controls.ScheduleCell sender, WeekType week)
        {
            if (sender.IsOneRow)
            {
                schedules.AddEntry(sender.DayOfWeek, WeekType.First, sender.FirstSubj, sender.SubjNumber);
                schedules.AddEntry(sender.DayOfWeek, WeekType.Second, sender.FirstSubj, sender.SubjNumber);
            }
            else
            {
                switch (week)
                {
                    case WeekType.First: schedules.AddEntry(sender.DayOfWeek, WeekType.First, sender.FirstSubj, sender.SubjNumber); return;
                    case WeekType.Second: schedules.AddEntry(sender.DayOfWeek, WeekType.Second, sender.SecondSubj, sender.SubjNumber); return;
                }
            }
        }

        void SubjectList_OnSubjectRemove(Model.Entities.Subject subject)
        {
            schedules.RemoveSubject(subject);
        }

        void SubjectList_OnSubjectAdd(Model.Entities.Subject subject)
        {
            schedules.AddSubject(subject.Name);
        }

        
    }
}