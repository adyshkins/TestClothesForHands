using Microsoft.Win32;
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
using TestClothesForHands.EF;
using TestClothesForHands.Windows;
using static TestClothesForHands.EF.AppData;

namespace TestClothesForHands.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddEditMateralWindow.xaml
    /// </summary>
    public partial class AddEditMateralWindow : Window
    {
        string pathPhoto = null;
        public AddEditMateralWindow()
        {
            InitializeComponent();

            cmbTypeMAterial.ItemsSource = Context.TypeMaterial.ToList();
            cmbTypeMAterial.DisplayMemberPath = "Name";
            cmbTypeMAterial.SelectedIndex = 0;

            cmbUnitMaterial.ItemsSource = Context.UntType.ToList();
            cmbUnitMaterial.DisplayMemberPath = "Unit";
            cmbUnitMaterial.SelectedIndex = 0;
        }

        private void btnChooseImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            pathPhoto = openFileDialog.FileName;
            imgMaterial.Source = new BitmapImage(new Uri(pathPhoto));
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
