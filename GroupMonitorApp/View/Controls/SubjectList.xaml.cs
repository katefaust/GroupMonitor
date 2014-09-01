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
    /// Interaction logic for SubjectList.xaml
    /// </summary>
    public partial class SubjectList : UserControl
    {
        public delegate void ContainerChanged(Subject subject);
        public event ContainerChanged OnSubjectAdd;
        public event ContainerChanged OnSubjectRemove;

        private List<Subject> subjects = new List<Subject>();

        public void Add(Subject subject)
        {
            subjects.Add(subject);
            OnSubjectAdd(subject);
            SubjectView sView = new SubjectView(subject);
            sView.DelBtnClick += Remove;
            ListPanel.Children.Insert(ListPanel.Children.Count - 2, sView);
        }

        public void Add(List<Subject> subjects)
        {
            this.subjects = subjects;
            foreach (var sub in subjects)
            {
                SubjectView sView = new SubjectView(sub);
                sView.DelBtnClick += Remove;
                ListPanel.Children.Insert(ListPanel.Children.Count - 2, sView);
            }
        }

        public void Remove(SubjectView subject)
        {
            subjects.Remove(subject.Subject);
            OnSubjectRemove(subject.Subject);
            ListPanel.Children.Remove(subject);
        }
        public SubjectList()
        {
            InitializeComponent();
            Add(DBConnection.GetSubjectsList());
        }

        private void AddSubjBtn_Click(object sender, RoutedEventArgs e)
        {
            Add(new Subject() { Name = addTB.Text });
            addTB.Text = "";
        }

    }
}
