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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Bank : Window
    {
        Main game;
        public Bank(Main g)
        {
           InitializeComponent();
           sumloan.Text = g.c.loans[0].ToString();
           income.Content = g.c.Income.ToString();
           expense.Content = g.c.Expensess.ToString();
           total.Content = g.c.Balance.ToString();
           inflation.Content = g.economy.Inflation.ToString();
           game=g;
            
        }

        private void getloan_Click(object sender, RoutedEventArgs e)
        {
            double size = Double.Parse(sumloan.Text);
            game.c.TakeLoan(size);

        }

        private void bankclose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void payloan_Click(object sender, RoutedEventArgs e)
        {
            double size = Double.Parse(sumloan.Text);
            game.c.PayLoan(size);
        }
    }
}
