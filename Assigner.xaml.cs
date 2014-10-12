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
using System.Collections.ObjectModel;
using Game;
using Game.Buisness;

namespace game_gui
{
    /// <summary>
    /// Interaction logic for Assigner.xaml
    /// </summary>
    public partial class Assigner : Window
    {
        Main game;
        List<Customer> c=new List<Customer>();
        List<Employee> em = new List<Employee>();
        public Assigner(Main game)
        {
            InitializeComponent();
            this.game = game;
            ObservableCollection<string> clients = new ObservableCollection<string>();
            ObservableCollection<string> employees = new ObservableCollection<string>();
            c=game.c.UpForGrabs().Take(game.c.UpForGrabs().Count).ToList();
            em = game.c.workers.Take(game.c.workers.Count).ToList();
            c.Sort();
            for (int i = 0; i < c.Count; i++) {
                clients.Add(c[i].name+" value="+c[i].Payment);
            }
            customers.ItemsSource = clients;
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void asg_Click(object sender, RoutedEventArgs e)
        {
            this.game.c.clients.Find(item=>item==c[customers.SelectedIndex]).AssignTo(em[employee.SelectedIndex]);
            this.Close();
        }

        private void customers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int sk=(int)c[customers.SelectedIndex].pref;
            em.Sort((a, b)=> a.skill.values[sk].CompareTo(b.skill.values[sk]));
            ObservableCollection<string> employees = new ObservableCollection<string>();
            for (int i = 0; i < em.Count; i++)
            {
                employees.Add(em[i].name + " " + c[customers.SelectedIndex].pref + "=" + em[i].skill.values[sk]);
            }
            employee.ItemsSource = employees;
        }
    }
}
