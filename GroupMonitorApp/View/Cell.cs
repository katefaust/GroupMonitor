using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GroupMonitorApp.Model;

namespace GroupMonitorApp.View
{
    public class Cell: Label
    {
        public int StudentId { get; set; }
        public int SubjectNumber { get; set; }
        public int HourNumber { get; set; }
        public StudentPresence State
        {
            get
            {
                if (this.Background == Brushes.Red) return StudentPresence.AbsentNoReason;
                if(this.Background == Brushes.LimeGreen) return StudentPresence.AbsentWithReason;
                return StudentPresence.Present;
            }
            set
            {
                switch (value)
                {
                    case StudentPresence.AbsentNoReason: this.Background = Brushes.Red; break;
                    case StudentPresence.AbsentWithReason: this.Background = Brushes.LimeGreen; break;
                    case StudentPresence.Present: this.Background = Brushes.White; break;
                }
            }
        }

        public Cell(int studentId, int subjectNumber, int hourNumber, int xPos, int yPos, int width, int height)
        {
            StudentId = studentId;
            SubjectNumber = subjectNumber;
            HourNumber = hourNumber;
            this.Margin = new Thickness(xPos, yPos, 0, 0);
            this.Background = Brushes.White;
            this.HorizontalAlignment = 0;
            this.VerticalAlignment = 0;
            this.VerticalContentAlignment = VerticalAlignment.Center;
            this.Padding = new Thickness(4, 0, 4, 0);
            this.Width = width;
            this.Height = height;
            this.BorderBrush = Brushes.Black;
            this.BorderThickness = new Thickness(1);
        }
        public void ChangeState()
        {
            if (State == StudentPresence.Present) { State = StudentPresence.AbsentNoReason; return; }
            if (State == StudentPresence.AbsentNoReason) { State = StudentPresence.AbsentWithReason; return; }
            if (State == StudentPresence.AbsentWithReason) { State = StudentPresence.Present; return; }
        }
    }
}
