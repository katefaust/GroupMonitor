using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroupMonitorApp.Model.Entities;
using GroupMonitorApp.Model;
using Microsoft.Win32;

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
        public void UpdateEntry(DayOfWeek dayOfWeek, int weekType, int subjNumber, Subject subject)
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
        public void AddEntry(DayOfWeek dayOfWeek, int weekType, Subject subject, int subjectNumber)
        {
            entries.Add(new SchedulesEntry()
                {
                    DayOfWeek = dayOfWeek,
                    Subject = subject,
                    WeekType = weekType,
                    SubjNumber = subjectNumber
                });
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
    }
}
