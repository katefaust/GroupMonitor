using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroupMonitorApp.Model.Entities;

namespace GroupMonitorApp.Control
{
    public class Statistics
    {
        private Journal journal;

        public Statistics(Journal journal)
        {
            this.journal = journal;
        }
        /// <summary>
        /// Получить количество пропусков для студента за весь семестр
        /// </summary>
        /// <param name="studId">Номер студента в журнале</param>
        /// <returns></returns>
        public Pass StudentMissed(int studId)
        {
            Pass pass = new Pass();
            foreach (var entry in journal.GetEntriesForStudent(studId))
            {
                if(entry.Absent > 0)
                    if (entry.Valid)
                    {
                        pass.ValidHours += entry.Absent;
                        pass.ValidLessons++;
                    }
                    else
                    {
                        pass.NotValidHours += entry.Absent;
                        pass.NotValidLessons++;
                    }
            }
            return pass;
        }
        /// <summary>
        /// Получить количество пропусков для студента за неделю
        /// </summary>
        /// <param name="studId">Номер студента в журнале</param>
        /// <param name="weekNumber">Номер учебной недели</param>
        /// <returns></returns>
        public Pass StudentMissed(int studId, int weekNumber)
        {
            Pass pass = new Pass();
            foreach (var entry in journal.GetEntriesForStudent(studId,weekNumber))
            {
                if (entry.Absent > 0)
                    if (entry.Valid)
                    {
                        pass.ValidHours += entry.Absent;
                        pass.ValidLessons++;
                    }
                    else
                    {
                        pass.NotValidHours += entry.Absent;
                        pass.NotValidLessons++;
                    }
            }
            return pass;
        }
        /// <summary>
        /// Получить количество пропусков для студента за период
        /// </summary>
        /// <param name="studId">Номер студента в журнале</param>
        /// <param name="from">Начало диапазона</param>
        /// <param name="to">Конец диапазона</param>
        /// <returns></returns>
        public Pass StudentMissed(int studId, DateTime from, DateTime to)
        {
            Pass pass = new Pass();
            foreach (var entry in journal.GetEntriesForStudent(studId, from, to))
            {
                if (entry.Absent > 0)
                    if (entry.Valid)
                    {
                        pass.ValidHours += entry.Absent;
                        pass.ValidLessons++;
                    }
                    else
                    {
                        pass.NotValidHours += entry.Absent;
                        pass.NotValidLessons++;
                    }
            }
            return pass;
        }
        /// <summary>
        /// Получить количество пропусков для студента за весь семестр по одному предмету
        /// </summary>
        /// <param name="studId">Номер студента в журнале</param>
        /// <param name="subject">Предмет</param>
        /// <returns></returns>
        public Pass StudentMissed(int studId, Subject subject)
        {
            Pass pass = new Pass();
            foreach (var entry in journal.GetEntriesForStudent(studId).Where(x=>x.DaySchedules.Subject == subject))
            {
                if (entry.Absent > 0)
                    if (entry.Valid)
                    {
                        pass.ValidHours += entry.Absent;
                        pass.ValidLessons++;
                    }
                    else
                    {
                        pass.NotValidHours += entry.Absent;
                        pass.NotValidLessons++;
                    }
            }
            return pass;
        }
        /// <summary>
        /// Получить количество пропусков для студента за неделю по одному предмету
        /// </summary>
        /// <param name="studId">Номер студента в журнале</param>
        /// <param name="subject">Предмет</param>
        /// <param name="weekNumber">Номер учебной недели</param>
        /// <returns></returns>
        public Pass StudentMissed(int studId, Subject subject, int weekNumber)
        {
            Pass pass = new Pass();
            foreach (var entry in journal.GetEntriesForStudent(studId, weekNumber).Where(x => x.DaySchedules.Subject == subject))
            {
                if (entry.Absent > 0)
                    if (entry.Valid)
                    {
                        pass.ValidHours += entry.Absent;
                        pass.ValidLessons++;
                    }
                    else
                    {
                        pass.NotValidHours += entry.Absent;
                        pass.NotValidLessons++;
                    }
            }
            return pass;
        }
        /// <summary>
        /// Получить количество пропусков для студента за период по одному предмету
        /// </summary>
        /// <param name="studId">Номер студента в журнале</param>
        /// <param name="subject">Предмет</param>
        /// <param name="from">Начало диапазона</param>
        /// <param name="to">Конец диапазона</param>
        /// <returns></returns>
        public Pass StudentMissed(int studId, Subject subject, DateTime from, DateTime to)
        {
            Pass pass = new Pass();
            foreach (var entry in journal.GetEntriesForStudent(studId, from, to).Where(x => x.DaySchedules.Subject == subject))
            {
                if (entry.Absent > 0)
                    if (entry.Valid)
                    {
                        pass.ValidHours += entry.Absent;
                        pass.ValidLessons++;
                    }
                    else
                    {
                        pass.NotValidHours += entry.Absent;
                        pass.NotValidLessons++;
                    }
            }
            return pass;
        }
        /// <summary>
        /// Количество пропусков по всей группе за неделю
        /// </summary>
        /// <param name="weekNumber">Номер учебной недели</param>
        /// <returns></returns>
        public Pass GroupMissed(int weekNumber)
        {
            Pass pass = new Pass();
            foreach (var entry in journal.GetEntries(weekNumber))
            {
                if (entry.Absent > 0)
                    if (entry.Valid)
                    {
                        pass.ValidHours += entry.Absent;
                        pass.ValidLessons++;
                    }
                    else
                    {
                        pass.NotValidHours += entry.Absent;
                        pass.NotValidLessons++;
                    }
            }
            return pass;
        }
    }
}
