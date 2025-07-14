using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace _20250714_4.Model
{
    internal class MainModel : INotifyPropertyChanged
    {
        private string name;
        private string group;
        private string rank;
        private string num;
        private string email;

        public string Name
        {
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
            get { return name; }
        }

        public string Group
        {
            set
            {
                group = value;
                OnPropertyChanged("Group");
            }
            get { return group; }
        }

        public string Rank
        {
            set
            {
                rank = value;
                OnPropertyChanged("Rank");
            }
            get { return rank; }
        }

        public string Num
        {
            set
            {
                num = value;
                OnPropertyChanged("Num");
            }
            get { return num; }
        }

        public string Email
        {
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
            get { return email; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
