using GroupMonitorApp.Model;
using GroupMonitorApp.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public void UpdateEntry(int studentId, int subjNumber, DateTime day, StudentPresence firstHour, StudentPresence secondHour)
        {
            JournalEntry entry = entries.Where(x => x.Stud.Id == studentId && x.SubjNumber == subjNumber && x.Day == day).First();
            entry.FirstHour = firstHour;
            entry.SecondHour = secondHour;
            DBConnection.AddJournalEntry(entry);
        }
        public void AddStudent(string name)
        {
            students.Add(new Student 
            {
                Name = name
            });
            DBConnection.AddStudent(students.Last());
        }
        public void RemoveStudent(int studentId)
        {
            DBConnection.RemoveStudent(students.Where(x => x.Id == studentId).First());
            students.Remove(students.Where(x => x.Id == studentId).First());
        }
        /// <summary>
        /// Добавление новой записи в журнал
        /// </summary>
        /// <param name="studId">Номер студента в журнале</param>
        /// <param name="subjNumber">Предмет по счету</param>
        /// <param name="day">Дата записи</param>
        /// <param name="daySchedules">Соответствующая запись из расписания</param>
        /// <param name="hourNumber">Первый или второй час</param>
        /// <param name="state">Присутствие студента</param>
        public void AddEntry(int studId, int subjNumber, DateTime day, SchedulesEntry daySchedules, StudentPresence firstHour, StudentPresence secondHour)
        {

            entries.Add(new JournalEntry()
                {
                    Stud = GetStudentById(studId),
                    Day = day,
                    SubjNumber = subjNumber,
                    DaySchedules = daySchedules,
                    FirstHour = firstHour,
                    SecondHour = secondHour
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
            return entries.Where(x => x.Stud.Id == StudentId).ToList();
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
            from = Schedules.SemesterStartedDate.AddDays(-Schedules.DaysFromMonday(Schedules.SemesterStartedDate)).AddDays(7 * weekNumber);
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
        public Student GetStudentById(int Id)
        {
            return students.ElementAt(Id - 1);
        }
        public int NumberOfStudents()
        {
            return students.Count;
        }
        public bool HasEntry(int studentId, int subjNumber, DateTime date)
        {
            if (entries.Where(x => x.Stud.Id == studentId && x.SubjNumber == subjNumber && x.Day == date).Count() != 0)
                return true;
            else
                return false;
        }
        public bool HasEntry(DateTime date)
        {
            if (entries.Where(x => x.Day == date).Count() != 0)
                return true;
            else
                return false;
        }
    }
}
