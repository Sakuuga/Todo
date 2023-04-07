using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace TodoApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Task> tasksList = new List<Task>();
        public MainWindow()
        {
            InitializeComponent();

            Task t1 = new Task();
            t1.Name = "Купити молоко";
            t1.Description = "Копити молоко 3.2%";
            t1.IsCompleted = false;

            Task t2 = new Task();
            t2.Name = "Вивчити C#";
            t2.Description = "Вивчити у CyberBionic Systenatics";
            t2.IsCompleted = false;

            Task t3 = new Task();
            t3.Name = "Створити резюме";
            t3.Description = "Не забути зберегти у PDF";
            t3.IsCompleted = false;

            tasksList.Add(t1);
            tasksList.Add(t2);
            tasksList.Add(t3);

            ToDoListBox.ItemsSource = tasksList.ToArray();
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
    }
}
