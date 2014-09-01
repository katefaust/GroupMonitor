using GroupMonitorApp.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GroupMonitorApp.View
{
    public class SubjectView : Grid
    {
        public delegate void DelBtn(SubjectView subject);
        public event DelBtn DelBtnClick;

        public Subject Subject { get; private set; }
        private Label label = new Label();
        private Button del = new Button();

        public SubjectView(Subject subj)
        {
            Subject = subj;
            Margin = new Thickness(15, 5, 0, 0);
            Height = 25;
            Initlabel();
        }

        private void Initlabel()
        {
            Children.Add(label);
            label.Content = Subject.Name;
            label.Margin = new Thickness(0, 0, 15, 0);
            label.BorderBrush = Brushes.LightGray;
            label.BorderThickness = new Thickness(1);
            label.HorizontalContentAlignment = HorizontalAlignment.Center;
            label.VerticalContentAlignment = VerticalAlignment.Center;
            label.AllowDrop = true;
            label.MouseEnter += label_MouseEnter;
            label.MouseLeave += label_MouseLeave;
            label.MouseMove += label_MouseMove;
            
        }

        void label_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Label l = sender as Label;
            if (e.LeftButton == MouseButtonState.Pressed)
                DragDrop.DoDragDrop(l, Subject, DragDropEffects.All);
        }

        void label_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Label l = (Label)sender;
            Thread t = new Thread(method);
            t.Start(l);
        }

        void method(object sender)
        {
            Thread.Sleep(500);
            var l = (Label)sender;
            l.Dispatcher.Invoke((Action)(() => { Children.Remove(del); }));

        }

        void label_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (Children.Contains(del)) return;
            Children.Add(del);
            del.Width = 10;
            del.Height = 10;
            del.Margin = new Thickness(0, 0, 5, 0);
            del.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            del.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            BitmapImage img = new BitmapImage(new Uri("pack://application:,,,/Img/Del.png"));
            Image i = new Image();
            i.Source = img;
            del.Content = i;
            del.Click += DeleteSubject;
        }

        private void DeleteSubject(object sender, RoutedEventArgs e)
        {
            DelBtnClick(this);
        }

    }
}
