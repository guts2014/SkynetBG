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
using Game.Buisness;

namespace game_gui
{
    /// <summary>
    /// Interaction logic for create.xaml
    /// </summary>
    public partial class create : Window
    {
        Main res;
        public create(Main g)
        {
            InitializeComponent();
            res = g;
        }

        private void create1_Click(object sender, RoutedEventArgs e)
        {
            res.c = new Game.Buisness.Company(name.Text, res);            
            MainGameWindow m = new MainGameWindow(res);
            m.Show();
            this.Close();
        }
    }
}
