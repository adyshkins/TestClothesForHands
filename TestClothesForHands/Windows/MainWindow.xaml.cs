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
using TestClothesForHands.EF;
using TestClothesForHands.Windows;
using static TestClothesForHands.EF.AppData;

namespace TestClothesForHands
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> listSort = new List<string>()
        {
            "Наименование (по возрастанию)",
            "Наименование (по убыванию)",
            "Остаток на складе(по возрастанию)",
            "Остаток на складе(по убыванию)",
            "Стоимость(по возрастанию)",
            "Стоимость(по убыванию)"
        };
        List<string> listFiltr = new List<string>();

        List<Material> listMaterial = new List<Material>();

        private int numPage = 0;

        private Material selectMaterial;

        void Filtr()
        {

            listMaterial = Context.Material.ToList();
            // Сотрировка
            var selectSort = cmbSort.SelectedIndex;
            switch (selectSort)
            {
                case 0:
                    listMaterial = listMaterial.OrderBy(i => i.Name).ToList(); // по возрастанию
                    break;

                case 1:
                    listMaterial = listMaterial.OrderByDescending(i => i.Name).ToList(); // по убыванию
                    break;

                case 2:
                    listMaterial = listMaterial.OrderBy(i => i.Count).ToList();
                    break;

                case 3:
                    listMaterial = listMaterial.OrderByDescending(i => i.Count).ToList();
                    break;

                case 4:
                    listMaterial = listMaterial.OrderBy(i => i.Price).ToList();
                    break;

                case 5:
                    listMaterial = listMaterial.OrderByDescending(i => i.Price).ToList();
                    break;

                default:
                    listMaterial = listMaterial.OrderBy(i => i.ID).ToList();
                    break;
            }

            // Фильтрация

            var selectFiltr = cmbFiltr.SelectedIndex;

            if (selectFiltr != 0)
            {
                listMaterial = listMaterial.Where(i => i.TypeId == selectFiltr).ToList();
            }

            // Поиск

            listMaterial = listMaterial.
                Where(i => i.Name.ToLower().Contains(txtSearch.Text.ToLower())).
                ToList();

            // Вывод количества записей 

            tbStartCount.Text = "15";
            tbAllCount.Text = listMaterial.Count.ToString();
           
            // Вывод по страницам

            listMaterial = listMaterial.
                Skip(15 * numPage).
                Take(15).
                ToList();

            // переопределение источника данных для ListView

            lvMaterial.ItemsSource = listMaterial;
        }

        public MainWindow()
        {
            InitializeComponent();

            btnEditMinCount.Visibility = Visibility.Collapsed;

            var typeMaterial = Context.TypeMaterial.ToList();
            foreach (var i in typeMaterial)
            {
                listFiltr.Add(i.Name);
            }

            listFiltr.Insert(0, "Все типы");
            cmbFiltr.ItemsSource = listFiltr;
            cmbFiltr.SelectedIndex = 0;

            cmbSort.ItemsSource = listSort;
            cmbSort.SelectedIndex = 0;

        }

        private void cmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filtr();
        }

        private void cmbFiltr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filtr();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filtr();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (numPage > 0)
            {
                numPage--;
                btn1.Content = (numPage + 1).ToString();
                btn2.Content = (numPage + 2).ToString();
                btn3.Content = (numPage + 3).ToString();
            }
            Filtr();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (listMaterial.Count > 0)
            {
                numPage++;
                btn1.Content = (numPage + 1).ToString();
                btn2.Content = (numPage + 2).ToString();
                btn3.Content = (numPage + 3).ToString();
            }
            Filtr();
        }

        private void lvMaterial_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnEditMinCount.Visibility = Visibility.Visible; // стала видимой кнопка изменения минимального количества

            if (lvMaterial.SelectedItem is Material material)
            {
                ClassHelper.MinCountMaterial.getMinCount = material;
            }
        }

        private void btnEditMinCount_Click(object sender, RoutedEventArgs e)
        {
            MinCountWindow minCountWindow = new MinCountWindow();
            minCountWindow.ShowDialog();

            selectMaterial.MinCount = ClassHelper.MinCountMaterial.getMinCount.MinCount;
            Context.SaveChanges();
            Filtr();
        }
    }
}
