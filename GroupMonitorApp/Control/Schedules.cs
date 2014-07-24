using GroupMonitorApp.Model;
using GroupMonitorApp.Model.Entities;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GroupMonitorApp.Control
{
    public class Schedules
    {
        private List<Subject> subjects;
        public List<SchedulesEntry> entries;
     
        public static DateTime SemesterStartedDate
        {
            get 
            {
                RegistryKey SaveDate = Registry.CurrentUser.OpenSubKey("software\\GroupMonitor");
                DateTime date = DateTime.Parse((String)SaveDate.GetValue("SemesterStarted"));
                SaveDate.Close();
                return date;
            }
            set 
            {
                RegistryKey SaveDate = Registry.CurrentUser.CreateSubKey("software\\GroupMonitor");
                SaveDate.SetValue("SemesterStarted", value.ToString("dd.MM.yyyy"));
                SaveDate.Close();
            }
        }
        public Schedules()
        {
            entries = DBConnection.GetSchedules();
            subjects = DBConnection.GetSubjectsList();
        }
        /// <summary>
        /// Изменить запись в расписании
        /// </summary>
        /// <param name="dayOfWeek">День недели</param>
        /// <param name="weekType">Первая или вторая неделя</param>
        /// <param name="subjNumber">Номер предмета в расписании</param>
        /// <param name="subject">Новый предмет, null - удалить запись</param>
        public void UpdateEntry(DayOfWeek dayOfWeek, WeekType weekType, int subjNumber, Subject subject)
        {
            var entry = entries.Where(x => x.DayOfWeek == dayOfWeek && x.WeekType == weekType && x.SubjNumber == subjNumber).First();
            if (subjects != null)
            {
                entry.Subject = subject;
                DBConnection.AddScheduleEntry(entry);
            }
            else
                DBConnection.RemoveScheduleEntry(entry);

        }
        /// <summary>
        /// Добавление новой записи в расписание
        /// </summary>
        /// <param name="dayOfWeek">День недели</param>
        /// <param name="weekType">Первая или вторая неделя</param>
        /// <param name="subject">Предмет</param>
        /// <param name="subjectNumber">Номер предмета в расписании</param>
        public void AddEntry(DayOfWeek dayOfWeek, WeekType weekType, Subject subject, int subjectNumber)
        {
            entries.Add(new SchedulesEntry()
                {
                    DayOfWeek = dayOfWeek,
                    Subject = subject,
                    WeekType = weekType,
                    SubjNumber = subjectNumber
                });
            DBConnection.AddScheduleEntry(entries.Last());
        }
        public void AddSubject(String name)
        {
            subjects.Add(new Subject() { Name = name });
            DBConnection.AddSubject(subjects.Last());
        }
        public void RemoveSubject(Subject subj)
        {
            subjects.Remove(subj);
            DBConnection.RemoveSubject(subj);
        }
        public List<Subject> GetSubjectsList() 
        {
            return subjects;
        }
        public SchedulesEntry GetSchedulesEntry(DateTime date, int subjectNumber)
        {
            SchedulesEntry entry = entries.Where(x => x.DayOfWeek == date.DayOfWeek && x.SubjNumber == subjectNumber &&
                x.WeekType == ((GetWeekNumberСontainsDate(date) % 2 == 0) ? WeekType.First : WeekType.Second)).First();
            return entry;
 
        }
        public List<SchedulesEntry> GetSchedulesEntry(DateTime date)
        {
            return entries.Where(x => x.DayOfWeek == date.DayOfWeek && x.WeekType == ((GetWeekNumberСontainsDate(date) % 2 == 0) ? WeekType.First : WeekType.Second)).ToList();

        }
        public static int GetWeekNumberСontainsDate(DateTime date)
        {
            DateTime SemesterStart = SemesterStartedDate;
            return (date.Day - SemesterStart.Day + DaysFromMonday(date)) / 7;
        }
        public static int DaysFromMonday(DateTime date)
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday: return 0;
                case DayOfWeek.Tuesday: return 1;
                case DayOfWeek.Wednesday: return 2;
                case DayOfWeek.Thursday: return 3;
                case DayOfWeek.Friday: return 4;
                case DayOfWeek.Saturday: return 5;
                case DayOfWeek.Sunday: return 6;
            }
            return 0;
        }
        public int NumberOfSubjects(DateTime date)
        {
            return GetSchedulesEntry(date).Count;
        }
    }
}
