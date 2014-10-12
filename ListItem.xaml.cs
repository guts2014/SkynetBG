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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Game.Buisness;
using Game.Utils;

namespace game_gui
{
    /// <summary>
    /// Interaction logic for ListItem.xaml
    /// </summary>
    public partial class ListItem : UserControl
    {
        Employee e;
        Company c;
        public ListItem(Employee e,Company c)
        {           
            //skill.Text = ;
            InitializeComponent();
            this.e = e;
            this.c = c;
            name.Text = e.name;
            age.Text = e.age.ToString();
            exp.Text = e.exp.ToString();
            gen.Text = e.g.ToString();
            int v = e.skill.BestSkill();
            pic.Source = new BitmapImage(new Uri(e.g == Enums.Gender.MALE ? @"malegold.png" : @"femalegold.png", UriKind.Relative));
        }

        private void hire_Click(object sender, RoutedEventArgs e)
        {
            MsgBox m = new MsgBox(this.e,c);

        }
    }
}
