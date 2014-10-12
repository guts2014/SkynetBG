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
using Game.Buisness;

namespace game_gui
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MsgBox : Window
    {
        Employee em;
        Company c;
        public MsgBox(Employee e,Company c)
        {
            InitializeComponent();
            this.em = e;
            this.c = c;
            this.price.Text = e.DesiredWage().ToString() ;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double offer=Double.Parse(price.Text);
            c.Hire(em,offer);
        }
    }
}
