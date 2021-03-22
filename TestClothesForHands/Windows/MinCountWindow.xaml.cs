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
    /// Логика взаимодействия для MinCountWindow.xaml
    /// </summary>
    public partial class MinCountWindow : Window
    {
        public MinCountWindow()
        {
            InitializeComponent();
            txtMinCount.Text = ClassHelper.MinCountMaterial.getMinCount.MinCount.ToString();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var resultMsg = MessageBox.Show("Нажмите Да для подтверждения", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultMsg == MessageBoxResult.Yes)
            {
                ClassHelper.MinCountMaterial.getMinCount.MinCount = Int32.Parse(txtMinCount.Text);
                Close();
            }
        }
    }
}
