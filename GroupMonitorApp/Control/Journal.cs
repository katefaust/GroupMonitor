using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroupMonitorApp.Model.Entities;
using GroupMonitorApp.Model;
using System.Resources;
using NUnit.Framework;

namespace GroupMonitorApp.Control
{
    public class Journal
    {
        private List<Student> students;
        private List<JournalEntry> entries;
        
        public Journal()
        {
            students = DBConnection.GetStudentsList();
            entries = DBConnection.GetJournalEntries();
        }
        /// <summary>
        /// Обновить записиь в журнале
        /// </summary>
        /// <param name="studentId">Номер студента в журнале</param>
        /// <param name="subjNumber">Номер предмета в расписании</param>
        /// <param name="day">Дата записи</param>
        /// <param name="absent">Новое количество пропусков</param>
        /// <param name="valid">Уважительная причина</param>
        public void UpdateEntry(int studentId, int subjNumber, DateTime day, int absent, bool valid)
        {
            JournalEntry entry = entries.Where(x => x.Stud == GetStudentById(studentId) && x.SubjNumber == subjNumber && x.Day == day).First();
            entry.Absent = absent;
            entry.Valid = valid;
            DBConnection.AddJournalEntry(entry);
        }
        public void AddStudent(string name)
        {
            students.Add(new Student 
            {
                Name = name
            });
        }
        /// <summary>
        /// Добавление новой записи в журнал
        /// </summary>
        /// <param name="studId">Номер студента в журнале</param>
        /// <param name="subjNumber">Предмет по счету</param>
        /// <param name="day">Дата записи</param>
        /// <param name="daySchedules">Соответствующая запись из расписания</param>
        /// <param name="absent">Количество пропусков</param>
        /// <param name="valid">Уважительная причина</param>
        public void AddEntry(int studId, int subjNumber, DateTime day, SchedulesEntry daySchedules, int absent, bool valid )
        {
            entries.Add(new JournalEntry()
            {
                Stud = GetStudentById(studId),
                Day = day,
                SubjNumber = subjNumber,
                DaySchedules = daySchedules,
                Absent = absent,
                Valid = valid
            });
            DBConnection.AddJournalEntry(entries.Last());
        }
        /// <summary>
        /// Все записи для одного студента
        /// </summary>
        /// <param name="StudentId">Номер студента в журнале</param>
        /// <returns>Записи журнала</returns>
        public List<JournalEntry> GetEntriesForStudent(int StudentId)
        {
            return entries.Where(x => x.Stud == GetStudentById(StudentId)).ToList();
        }
        /// <summary>
        /// Все записи для одного студента за определенную неделю
        /// </summary>
        /// <param name="StudentId">Номер студента в журнале</param>
        /// <param name="weekNumber">Номер учебной недели</param>
        /// <returns>Записи журнала</returns>
        public List<JournalEntry> GetEntriesForStudent(int StudentId, int weekNumber)
        {
            return GetEntries(weekNumber).Where(x => x.Stud == GetStudentById(StudentId)).ToList();
        }
        /// <summary>
        /// Все записи для одного студента за заданное время
        /// </summary>
        /// <param name="StudentId">Номер студента в журнале</param>
        /// <param name="from">Дата начала диапазона</param>
        /// <param name="to">Дата конца диапазона</param>
        /// <returns>Записи журнала</returns>
        public List<JournalEntry> GetEntriesForStudent(int StudentId, DateTime from, DateTime to)
        {
            return GetEntries(from, to).Where(x => x.Stud == GetStudentById(StudentId)).ToList();
        }
        /// <summary>
        /// Получить все записи за неделю
        /// </summary>
        /// <param name="weekNumber">Номер учебной недели</param>
        /// <returns>Записи журнала</returns>
        public List<JournalEntry> GetEntries(int weekNumber)
        {
            DateTime from = new DateTime(), to = new DateTime();
            from = Schedules.SemesterStartedDate.AddDays(-DaysFromMonday(Schedules.SemesterStartedDate)).AddDays(7 * weekNumber);
            to = from.AddDays(7);
            return GetEntries(from,to);
        }
        /// <summary>
        /// Получить все записи за заданное время
        /// </summary>
        /// <param name="from">Дата начала диапазона</param>
        /// <param name="to">Дата конца диапазона</param>
        /// <returns>Записи журнала</returns>
        public List<JournalEntry> GetEntries(DateTime from, DateTime to)
        {
            return entries.Where(x => x.Day >= from && x.Day <= to).ToList();
        }
        public int GetWeekNumberСontainsDate(DateTime date)
        {
            DateTime SemesterStart = Schedules.SemesterStartedDate;
            return (date.Day - SemesterStart.Day + DaysFromMonday(date)) / 7;
        }
        private int DaysFromMonday(DateTime date)
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
        private Student GetStudentById(int Id)
        {
            return students.Where(x => x.Id == Id).Select(x => x).First();
        }
    }
}
