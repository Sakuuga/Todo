﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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

namespace TodoApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Task> tasksList = new ObservableCollection<Task>();

        public MainWindow()
        {
            InitializeComponent();

            //Task t1 = new Task();
            //t1.Name = "Купити молоко";
            //t1.Description = "Копити молоко 3.2%";
            //t1.IsCompleted = false;

            //Task t2 = new Task();
            //t2.Name = "Вивчити C#";
            //t2.Description = "Вивчити у CyberBionic Systenatics";
            //t2.IsCompleted = false;

            //Task t3 = new Task();
            //t3.Name = "Створити резюме";
            //t3.Description = "Не забути зберегти у PDF";
            //t3.IsCompleted = false;

            //tasksList.Add(t1);
            //tasksList.Add(t2);
            //tasksList.Add(t3);

            ToDoListBox.ItemsSource = tasksList;
            ToDoListBox.DisplayMemberPath = "Name";
        }

        private void ToDoListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Task selected = ToDoListBox.SelectedItem as Task;
            if (selected != null)
            {
                MessageBox.Show(selected.Description);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NewTaskWindow window = new NewTaskWindow();
            window.Owner = this;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            if(window.ShowDialog() == true)
            {
                Task newTask = window.Result;
                tasksList.Add(newTask);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            int index = ToDoListBox.SelectedIndex;
            if (index != -1)
            {
                tasksList.RemoveAt(index);
            }
        }

        private void CompleteButton_Click(object sender, RoutedEventArgs e)
        {
            int index = ToDoListBox.SelectedIndex;
            if (index != -1)
            {
                tasksList[index].IsCompleted = true;
            }
        }

        private void AllRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            ToDoListBox.ItemsSource = tasksList;
        }

        private void NotCompleteRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Task> filtered = new ObservableCollection<Task>();
            for (int i = 0; i < tasksList.Count; i++)
            {
                Task current = tasksList[i];
                if (current.IsCompleted == false) 
                {
                    filtered.Add(current);
                }
            }
            ToDoListBox.ItemsSource = filtered;
        }

        private void CompleteRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Task> filtered = new ObservableCollection<Task>();
            for (int i = 0; i < tasksList.Count; i++)
            {
                Task current = tasksList[i];
                if (current.IsCompleted == true)
                {
                    filtered.Add(current);
                }
            }
            ToDoListBox.ItemsSource = filtered;
        }

        string fileName = "data.bin";

        private void Window_Closed(object sender, EventArgs e)
        {
            string json = JsonConvert.SerializeObject(tasksList);
            File.WriteAllText(fileName, json);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (File.Exists(fileName))
            {
                string json = File.ReadAllText(fileName);
                tasksList = JsonConvert.DeserializeObject<ObservableCollection<Task>>(json);
                ToDoListBox.ItemsSource = tasksList;
            }
        }
    }
}
