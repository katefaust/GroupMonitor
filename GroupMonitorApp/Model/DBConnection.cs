using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;
using System.IO;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using GroupMonitorApp.Model.Entities;
using NHibernate.Tool.hbm2ddl;

namespace GroupMonitorApp.Model
{
    public class DBConnection
    {
        public static ISessionFactory SessionFactory = CreateSessionFactory();
        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012
                    .ConnectionString(x => x.Server("ADMIN-PC\\SQLEXPRESS")
                                            .Database("new")
                                            .TrustedConnection()))
                .Mappings(m =>
                    m.FluentMappings.AddFromAssemblyOf<DBConnection>())
                //.ExposeConfiguration(BuildSchema)
                .BuildSessionFactory();
        }

        private static void BuildSchema(Configuration config)
        {
            new SchemaExport(config)
                .Create(false, true);
        }

        public static void AddStudent(Student stud)
        {
            using (var session = SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(stud);
                    transaction.Commit();
                }
            }
        }
        public static void RemoveStudent(Student stud)
        {
            using (var session = SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Delete(stud);
                    transaction.Commit();
                }
            }
        }

        public static List<Student> GetStudentsList()
        {
            using (var session = SessionFactory.OpenSession())
            {
                // retreive all stores and display them
                using (session.BeginTransaction())
                {
                    return session.CreateCriteria(typeof(Student))
                        .List<Student>().ToList<Student>();
                }
            }
        }

        public static void AddScheduleEntry(SchedulesEntry entry)
        {
            using (var session = SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(entry);
                    transaction.Commit();
                }
            } 
        }
        public static void RemoveScheduleEntry(SchedulesEntry entry)
        {
            using (var session = SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Delete(entry);
                    transaction.Commit();
                }
            }
        }
        public static List<SchedulesEntry> GetSchedules()
        {
            using (var session = SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    return session.CreateCriteria(typeof(SchedulesEntry))
                        .List<SchedulesEntry>().ToList<SchedulesEntry>();
                }
            }
        }

        public static void AddSubject(Subject subj)
        {
            using (var session = SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(subj);
                    transaction.Commit();
                }
            }
        }
        public static void RemoveSubject(Subject subj)
        {
            using (var session = SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Delete(subj);
                    transaction.Commit();
                }
            }
        }

        public static List<Subject> GetSubjectsList()
        {
            using (var session = SessionFactory.OpenSession())
            {
                // retreive all stores and display them
                using (session.BeginTransaction())
                {
                    return session.CreateCriteria(typeof(Subject))
                        .List<Subject>().ToList<Subject>();
                }
            }
        }

        public static void AddJournalEntry(JournalEntry entry)
        {
            using (var session = SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(entry);
                    transaction.Commit();
                }
            }
        }

        public static List<JournalEntry> GetJournalEntries()
        {
            using (var session = SessionFactory.OpenSession())
            {
                // retreive all stores and display them
                using (session.BeginTransaction())
                {
                    return session.CreateCriteria(typeof(JournalEntry))
                        .List<JournalEntry>().Where(x => x.Day >= Control.Schedules.SemesterStartedDate).ToList<JournalEntry>();
                }
            }
        }

    }
}
