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
    /// Interaction logic for EmployeeItem.xaml
    /// </summary>
    public partial class EmployeeItem : UserControl
    {
        public EmployeeItem(Employee e)
        {
            InitializeComponent();
            name.Text = e.name;
            stat.Text = e.IsFree?"Free":"Working";
            pic.Source = new BitmapImage(new Uri(e.g==Enums.Gender.MALE?@"male.png":@"female.png", UriKind.Relative));
        }
    }
}
