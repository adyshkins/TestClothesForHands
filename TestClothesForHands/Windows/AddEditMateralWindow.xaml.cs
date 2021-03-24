using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
            if ((bool)openFileDialog.ShowDialog())
            {
                pathPhoto = openFileDialog.FileName;
                imgMaterial.Source = new BitmapImage(new Uri(pathPhoto));
            }           
            
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            var resultClick = MessageBox.Show("Добавить?","Добавление нового материала",MessageBoxButton.YesNo,MessageBoxImage.Question);
            if (resultClick == MessageBoxResult.Yes)
            {
                Material addMaterial = new Material();
                if (pathPhoto != null)
                {
                    var format = pathPhoto.Split('.')[pathPhoto.Split('.').Length - 1];

                    string namePhoto = $@"\materials\{random.Next()}.{format}";

                    File.Copy(pathPhoto, $@"..\..\{namePhoto}");
                    addMaterial.Image = namePhoto;
                }
                addMaterial.Name = txtName.Text;
                addMaterial.Size = "1*1";
                addMaterial.TypeId = cmbTypeMAterial.SelectedIndex + 1;
                addMaterial.Price = Convert.ToDecimal(txtPrice.Text);
                addMaterial.Count = Convert.ToInt32(txtCount.Text);
                addMaterial.MinCount = Convert.ToInt32(txtMinCount.Text);
                addMaterial.CountInBox = Convert.ToInt32(txtCountInBox.Text);
                addMaterial.TypeDimension = cmbUnitMaterial.SelectedIndex + 1;

                Context.Material.Add(addMaterial);
                Context.SaveChanges();
                Close();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
