using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Net.Sockets;
using _20250714_4.Model;
using _20250714_4;
using System.IO;
using System.Windows;

namespace _20250714_4.ViewModel
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<MainModel> Addressing { get; set; } = new ObservableCollection<MainModel>();
        private Model.MainModel model;
        public string Search_screen {  get; set; } 
        public Command Search_btn {  get; set; }
        public Command Add_btn { get; set; }
        public Command Retouch_btn { get; set; }
        public Command Delete_btn { get; set; }

        public MainViewModel()
        {
            model = new Model.MainModel();
            Addressing = new ObservableCollection<MainModel>();

            // Search_btn = new Command(ExeCute_search, _=> true);
            Add_btn = new Command(ExeCute_add, _=> true);
            // Retouch_btn = new Command(ExeCute_retouch, _ => true);
            Delete_btn = new Command(ExeCute_del, _=> true);
        }


        public void ExeCute_search()
        {
            
        }

        public void ExeCute_add(Object obj)
        {
            var newModel = new MainModel();
            var window = new AddWindow();
            window.DataContext = newModel;

            if(window.ShowDialog() == true)
            {
                Addressing.Add(newModel);

                string filePath = @"D:\사번\Address3.txt";
                using (StreamWriter sw  = new StreamWriter(filePath, true))
                {
                    sw.WriteLine($"{newModel.Name}, {newModel.Group}, {newModel.Rank}, {newModel.Num}, {newModel.Email} ");
                }
                MessageBox.Show("파일저장완료");
            }
        }

        public void ExeCute_del(Object obj)
        {
            if 
        }






        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnpropertyChanged(String  propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
