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
using Vakantieverhuur.LIB.Entities;
using Vakantieverhuur.LIB.Services;

namespace Vakantieverhuur.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        List<Verblijf> verblijven;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Verblijven.Initialiseer();
            Huurders.Initialiseer();
            verblijven = Verblijven.AlleVerblijven;
            lstVerblijven.Items.Clear();
            foreach (Verblijf verblijf in verblijven)
            {
                lstVerblijven.Items.Add(verblijf);
            }
        }
        private void cmbSoorten_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!this.IsLoaded) return;

            lstVerblijven.Items.Clear();
            if (cmbSoorten.SelectedIndex == 0)
            {
                foreach (Verblijf verblijf in verblijven)
                {
                    lstVerblijven.Items.Add(verblijf);
                }
            }
            else if (cmbSoorten.SelectedIndex == 1)
            {
                foreach(Verblijf verblijf in verblijven)
                {
                    if(verblijf is Vakantiehuis)
                    {
                        lstVerblijven.Items.Add(verblijf);
                    }
                }
            }
            else
            {
                foreach (Verblijf verblijf in verblijven)
                {
                    if (verblijf is Caravan)
                    {
                        lstVerblijven.Items.Add(verblijf);
                    }
                }
            }

        }

        private void BtnView_Click(object sender, RoutedEventArgs e)
        {
            if (lstVerblijven.SelectedIndex == -1) return;
            winVerblijven venster = new winVerblijven();
            venster.situatie = "view";
            venster.verblijf = (Verblijf)lstVerblijven.SelectedItem;
            venster.ShowDialog();
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            winVerblijven venster = new winVerblijven();
            venster.situatie = "new";
            venster.verblijf = null;
            venster.ShowDialog();

            cmbSoorten_SelectionChanged(null, null);
            lstVerblijven.SelectedItem = venster.verblijf;

        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lstVerblijven.SelectedIndex == -1) return;

            winVerblijven venster = new winVerblijven();
            venster.situatie = "edit";
            venster.verblijf = (Verblijf)lstVerblijven.SelectedItem;
            venster.ShowDialog();

            cmbSoorten_SelectionChanged(null, null);
            lstVerblijven.SelectedItem = venster.verblijf;

        }

        private void LstVerblijven_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dgrVerhuur.Items.Clear();
            if (lstVerblijven.SelectedItem == null) return;

            Verblijf verblijf = (Verblijf)lstVerblijven.SelectedItem;
            foreach (Verhuur verhuur in Verhuringen.AlleVerhuringen)
            {
                if (verhuur.Vakantieverblijf == verblijf)
                {
                    dgrVerhuur.Items.Add(verhuur);
                }
            }
        }
        private void BtnVerhuurNew_Click(object sender, RoutedEventArgs e)
        {
            if (lstVerblijven.SelectedIndex == -1) return;

            if (!((Verblijf)lstVerblijven.SelectedItem).Verhuurbaar)
            {
                MessageBox.Show("Dit verblijf kan momenteel niet verhuurd worden", "Niet toegelaten", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            winVerhuur venster = new winVerhuur();
            venster.situatie = "new";
            venster.verblijf = (Verblijf)lstVerblijven.SelectedItem;
            venster.ShowDialog();

            LstVerblijven_SelectionChanged(null, null);
        }


        private void BtnVerhuurEdit_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Verhuur verhuur =(Verhuur)btn.DataContext;

            winVerhuur venster = new winVerhuur();
            venster.situatie = "edit";
            venster.verblijf = verhuur.Vakantieverblijf;
            venster.verhuur = verhuur;
            venster.ShowDialog();

            LstVerblijven_SelectionChanged(null, null);

        }
        private void BtnVerhuurDelete(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Verhuur verhuur = (Verhuur)btn.DataContext;

            if(MessageBox.Show("Deze verhuur annuleren?","Verhuur verwijderen",MessageBoxButton.YesNo, MessageBoxImage.Question,MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                Verhuringen.AlleVerhuringen.Remove(verhuur);
            }

            LstVerblijven_SelectionChanged(null, null);

        }
    }
}
