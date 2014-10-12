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
    /// Interaction logic for HireList.xaml
    /// </summary>
    public partial class HireList : Window
    {
        public HireList(List<Employee>l,Company c)
        {
            InitializeComponent();
            for (int i = 0; i < l.Count - 1; i++) {
                this.stack.Children.Add(new ListItem(l[i],c));
            }
        }

        private void stack_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            sender = sender;
        }
    }
}
