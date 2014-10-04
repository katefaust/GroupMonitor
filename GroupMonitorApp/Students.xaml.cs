using GroupMonitorApp.Model;
using GroupMonitorApp.Model.Entities;
using GroupMonitorApp.View;
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
    /// Interaction logic for Students.xaml
    /// </summary>
    public partial class Students : Window
    {
    public delegate void ContainerChanged(Student student);
        public event ContainerChanged OnStudentAdd;
        public event ContainerChanged OnStudentRemove;

        private List<Student> students = new List<Student>();

        public void Add(Student student)
        {
            students.Add(student);
            OnStudentAdd(student);
            StudentView sView = new StudentView(student);
            sView.DelBtnClick += Remove;
            ListPanel.Children.Insert(ListPanel.Children.Count - 2, sView);
        }

        public void Add(List<Student> students)
        {
            this.students = students;
            foreach (var stud in students)
            {
                StudentView sView = new StudentView(stud);
                sView.DelBtnClick += Remove;
                ListPanel.Children.Insert(ListPanel.Children.Count - 2, sView);
            }
        }

        public void Remove(StudentView student)
        {
            students.Remove(student.Student);
            OnStudentRemove(student.Student);
            ListPanel.Children.Remove(student);
        }
        public Students()
        {
            InitializeComponent();
            Add(DBConnection.GetStudentsList());
            this.OnStudentAdd += Students_OnStudentAdd;
            this.OnStudentRemove += Students_OnStudentRemove;
        }

        void Students_OnStudentRemove(Student student)
        {
            DBConnection.RemoveStudent(student);
        }

        void Students_OnStudentAdd(Student student)
        {
            DBConnection.AddStudent(student);
        }

        private void AddStudBtn_Click(object sender, RoutedEventArgs e)
        {
            Add(new Student() { Name = addTB.Text });
            addTB.Text = "";
        }

}
}
