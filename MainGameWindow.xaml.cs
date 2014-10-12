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
using Game;

namespace game_gui
{
    /// <summary>
    /// Interaction logic for MainGameWindow.xaml
    /// </summary>
    public partial class MainGameWindow : Window
    {
        Main game;
        public MainGameWindow(Main g)
        {
            game = g;
            InitializeComponent();
            for (int i = 0; i < g.c.workers.Count;i++ )
            {
                stack.Children.Add(new EmployeeItem(g.c.workers[i]));
            }
            g.Update +=new Main.UpdateUI( UpdateUI);
            this.cl.Content = "Clients:"+game.c.clients.Count;
            this.cname.Content = "Name:"+game.c.name;
            this.bal.Content = "Funds:" + game.c.Balance;
            g.time.Tick += new Game.Utils.Clock.TickHandler(ClockUpdate);
            g.time.Start();
         }

        private void bank_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Bank b = new Bank(game);
            b.Show();
        }
        private void recruit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            HireList h = new HireList(game.jobseekers,game.c);
            h.Show();
        }

        private void recruit_MouseEnter(object sender, MouseEventArgs e)
        {
            recruit.Source = new BitmapImage(new Uri(@"recruitmentH.png", UriKind.Relative));
        }
        private void recruit_MouseLeave(object sender, MouseEventArgs e)
        {
            bank.Source = new BitmapImage(new Uri(@"recruit.png", UriKind.Relative));
        }
        private void bank_MouseEnter(object sender, MouseEventArgs e)
        {
            bank.Source = new BitmapImage(new Uri(@"bankH.png", UriKind.Relative));
        }

        private void bank_MouseLeave(object sender, MouseEventArgs e)
        {
            bank.Source = new BitmapImage(new Uri(@"banki.png", UriKind.Relative));
        }
           
        private void ClockUpdate() {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(() => time.Content = game.time.ToString());
            }
            else
            {
                time.Content = game.time.ToString();
            }            
        }
        private void UpdateUI() {
            stack.Children.Clear();
            for (int i = 0; i < game.c.workers.Count; i++)
            {
                stack.Children.Add(new EmployeeItem(game.c.workers[i]));
            }
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(() => bal.Content = game.c.Balance);
                Dispatcher.Invoke(() => cl.Content = game.c.clients.Count);
            }
            else
            {
                time.Content = game.time.ToString();
            } 
        
        }

        private void assign_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Assigner a= new Assigner(game);           
            a.Show();
        }
    }
}
