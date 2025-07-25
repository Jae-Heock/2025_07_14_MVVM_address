﻿using _20250714_4;
using _20250714_4.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace _20250714_4.ViewModel
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<MainModel> Addressing { get; set; } = new ObservableCollection<MainModel>();
        public ObservableCollection<MainModel> OriginalList { get; set; } = new ObservableCollection<MainModel>();

        private Model.MainModel model;

        public Command Search_btn { get; set; }
        public Command Add_btn { get; set; }
        public Command Retouch_btn { get; set; }
        public Command Delete_btn { get; set; }

        private string searchTextBox;
        private int searchIndex;

        public string SearchTextBox
        {
            set
            {
                searchTextBox = value;
                OnpropertyChanged("SearchTextBox");
            }
            get { return searchTextBox; }
        }

        public int SearchIndex
        {
            set
            {
                searchIndex = value;
                OnpropertyChanged("SearchIndex");
            }
            get { return searchIndex; }
        }

        public MainViewModel()
        {
            model = new Model.MainModel();
            Addressing = new ObservableCollection<MainModel>();
            FileLoad();

            Search_btn = new Command(ExeCute_search, _=> true);
            Add_btn = new Command(ExeCute_add, _ => true);
            Retouch_btn = new Command(ExeCute_retouch, _ => true);
            Delete_btn = new Command(ExeCute_del, _ => true);
        }


        // 검색버튼 
        public void ExeCute_search(Object obj)
        {
            string key = SearchTextBox?.Trim() ?? "";
            var filtered = new ObservableCollection<MainModel>();

            foreach (var item in OriginalList)
            {
                if (SearchIndex == 0) // 전체
                {
                    filtered.Add(item);
                }
                else if (SearchIndex == 1 && item.Name.Contains(key))
                {
                    filtered.Add(item);
                }
                else if (SearchIndex == 2 && item.Group.Contains(key))
                {
                    filtered.Add(item);
                }
                else if (SearchIndex == 3 && item.Rank.Contains(key))
                {
                    filtered.Add(item);
                }
            }

            // 검색 결과를 Addressing에 반영
            Addressing.Clear();
            foreach (var i in filtered)
                Addressing.Add(i);
        }


        // 추가버튼 
        public void ExeCute_add(Object obj)
        {
            var newModel = new MainModel();
            var window = new AddWindow();
            window.DataContext = newModel;

            if (window.ShowDialog() == true)
            {
                Addressing.Add(newModel);

                string filePath = @"D:\사번\Address3.txt";
                using (StreamWriter sw = new StreamWriter(filePath, true))
                {
                    sw.WriteLine($"{newModel.Name}, {newModel.Group}, {newModel.Rank}, {newModel.Num}, {newModel.Email} ");
                }
                MessageBox.Show("파일저장완료");
            }
        }

        // 파일 로드 
        public void FileLoad()
        {
            string filePath = @"D:\사번\Address3.txt";

            if(File.Exists(filePath))
            {
                Addressing.Clear();
                OriginalList.Clear();

                var lines = File.ReadLines(filePath);
                foreach(var line in lines)
                {
                    var parts = line.Split(',');

                    if (parts.Length > 0)
                    {
                        var newItem = new MainModel
                        {
                            Name = parts[0].Trim(),
                            Group = parts[1].Trim(),
                            Rank = parts[2].Trim(),
                            Num = parts[3].Trim(),
                            Email = parts[4].Trim(),
                        };

                        Addressing.Add(newItem);
                        OriginalList.Add(newItem);
                    }
                }
            }
        }

        // 수정 버튼 
        public void ExeCute_retouch(object obj)
        {
            MainModel selectedAddress = (MainModel)obj;
            
            if(selectedAddress != null)
            {
                var newModel = new MainModel()
                {
                    Name = selectedAddress.Name,
                    Group = selectedAddress.Group,
                    Rank = selectedAddress.Rank,
                    Num = selectedAddress.Num,
                    Email = selectedAddress.Email,
                };

                var window = new AddWindow();
                window.DataContext = newModel;

                if (window.ShowDialog() == true)
                {
                    selectedAddress.Name = newModel.Name;
                    selectedAddress.Group = newModel.Group;
                    selectedAddress.Rank = newModel.Rank;
                    selectedAddress.Num = newModel.Num;
                    selectedAddress.Email = newModel.Email;

                    string filePath = @"D:\사번\Address3.txt";
                    using (StreamWriter sw = new StreamWriter(filePath, true))
                    {
                        foreach (var address in Addressing)
                        {
                            sw.WriteLine($"{address.Name}, {address.Group}, {address.Rank}, {address.Num}, {address.Email}");
                        }
                    }
                    MessageBox.Show("수정완료");
                }
            }
        }

        // 삭제버튼 
        public void ExeCute_del(object obj)
        {
            MainModel selectedAddress = obj as MainModel;
            if (selectedAddress != null)
            {
                Addressing.Remove(selectedAddress);

                string filePath = @"D:\사번\Address3.txt";
                using (StreamWriter sw = new StreamWriter(filePath, false))
                {
                    foreach (var address in Addressing)
                    {
                        sw.WriteLine($"{address.Name}, {address.Group}, {address.Rank}, {address.Num}, {address.Email}");
                    }
                }
                MessageBox.Show("삭제 완료~!!");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnpropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
