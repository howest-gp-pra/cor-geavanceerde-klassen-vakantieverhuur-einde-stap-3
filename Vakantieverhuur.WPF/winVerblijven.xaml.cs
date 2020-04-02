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
using Vakantieverhuur.LIB.Entities;
using Vakantieverhuur.LIB.Services;

namespace Vakantieverhuur.WPF
{
    /// <summary>
    /// Interaction logic for winVerblijven.xaml
    /// </summary>
    public partial class winVerblijven : Window
    {
        public winVerblijven()
        {
            InitializeComponent();
        }

        public string situatie="";
        public Verblijf verblijf = null;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(situatie=="view")
            {
                grpGegevens.IsEnabled = false;
                lblTitel.Content = "Verblijf bekijken";
                btnBewaren.Visibility = Visibility.Hidden;
                btnAnnuleren.Content = "Sluiten";
            }
            else
            {
                grpGegevens.IsEnabled = true;
                btnBewaren.Visibility = Visibility.Visible;
                btnAnnuleren.Content = "Annuleren";
                if(situatie == "new")
                {
                    lblTitel.Content = "Verblijf toevoegen";
                }
                else
                {
                    lblTitel.Content = "Verblijf wijzigen";
                }
            }
            if(verblijf == null)
            {
                cmbSoorten.SelectedIndex = 0;
                txtHuisNaam.Text = "";
                txtStraatEnNummer.Text = "";
                txtPostnummer.Text = "";
                txtGemeente.Text = "";
                txtBasisPrijs.Text = "";
                txtVerminderdePrijs.Text = "";
                txtDagenVoorVermindering.Text = "";
                txtWaarborg.Text = "";
                txtMaxPersonen.Text = "";
                chkMicrogolf.IsChecked = false;
                chkTV.IsChecked = false;
                chkVaatwas.IsChecked = false;
                chkWasmachine.IsChecked = false;
                chkHoutkachel.IsChecked = false;
                chkPersoonlijkSanitair.IsChecked = false;
                chkPersoonlijkSanitair.Visibility = Visibility.Hidden;
                chkVerhuurbaar.IsChecked = false;
                cmbSoorten.IsEnabled = true;
            }
            else
            {
                if (verblijf is Vakantiehuis)
                {
                    cmbSoorten.SelectedIndex = 0;
                    chkPersoonlijkSanitair.Visibility = Visibility.Hidden;
                    chkVaatwas.Visibility = Visibility.Visible;
                    chkWasmachine.Visibility = Visibility.Visible;
                    chkHoutkachel.Visibility = Visibility.Visible;

                    chkVaatwas.IsChecked = ((Vakantiehuis)verblijf).Vaatwas;
                    chkWasmachine.IsChecked = ((Vakantiehuis)verblijf).Wasmachine;
                    chkHoutkachel.IsChecked = ((Vakantiehuis)verblijf).Houtkachel;
                    chkPersoonlijkSanitair.IsChecked = false;

                }
                else
                {
                    cmbSoorten.SelectedIndex = 1;
                    chkPersoonlijkSanitair.Visibility = Visibility.Visible;
                    chkVaatwas.Visibility = Visibility.Hidden;
                    chkWasmachine.Visibility = Visibility.Hidden;
                    chkHoutkachel.Visibility = Visibility.Hidden;

                    chkVaatwas.IsChecked = false;
                    chkWasmachine.IsChecked = false;
                    chkHoutkachel.IsChecked = false;
                    chkPersoonlijkSanitair.IsChecked = ((Caravan)verblijf).PersoonlijkSanitair;

                }

                txtHuisNaam.Text = verblijf.HuisNaam;
                txtStraatEnNummer.Text = verblijf.StraatEnNummer;
                txtPostnummer.Text = verblijf.Postnummer.ToString();
                txtGemeente.Text = verblijf.Gemeente;
                txtBasisPrijs.Text = verblijf.BasisPrijs.ToString();
                txtVerminderdePrijs.Text = verblijf.VerminderdePrijs.ToString();
                txtDagenVoorVermindering.Text = verblijf.DagenVoorVermindering.ToString();
                txtWaarborg.Text = verblijf.Waarborg.ToString();
                txtMaxPersonen.Text = verblijf.MaxPersonen.ToString();
                chkMicrogolf.IsChecked = verblijf.Microgolf;
                chkTV.IsChecked = verblijf.TV;
                chkVerhuurbaar.IsChecked = verblijf.Verhuurbaar;

                cmbSoorten.IsEnabled = false;
            }
        }
        private void cmbSoorten_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!this.IsLoaded) return;

            if(cmbSoorten.SelectedIndex == 0)
            {
                chkPersoonlijkSanitair.Visibility = Visibility.Hidden;
                chkVaatwas.Visibility = Visibility.Visible;
                chkWasmachine.Visibility = Visibility.Visible;
                chkHoutkachel.Visibility = Visibility.Visible;
            }
            else
            {
                chkPersoonlijkSanitair.Visibility = Visibility.Visible;
                chkVaatwas.Visibility = Visibility.Hidden;
                chkWasmachine.Visibility = Visibility.Hidden;
                chkHoutkachel.Visibility = Visibility.Hidden;
            }
        }

        private void BtnAnnuleren_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnBewaren_Click(object sender, RoutedEventArgs e)
        {
            KleurWit(txtHuisNaam);
            KleurWit(txtStraatEnNummer);
            KleurWit(txtPostnummer);
            KleurWit(txtGemeente);
            KleurWit(txtBasisPrijs);
            KleurWit(txtVerminderdePrijs);


            string huisnaam = txtHuisNaam.Text.Trim();
            string straatennummer  = txtStraatEnNummer.Text.Trim();
            int.TryParse(txtPostnummer.Text, out int postnummer);
            string gemeente = txtGemeente.Text.Trim();
            decimal.TryParse(txtBasisPrijs.Text, out decimal basisprijs);
            decimal.TryParse(txtVerminderdePrijs.Text, out decimal verminderdeprijs);
            byte.TryParse(txtDagenVoorVermindering.Text, out byte dagenvoorvermindering);
            decimal.TryParse(txtWaarborg.Text, out decimal waarborg);
            int.TryParse(txtMaxPersonen.Text, out int maxpersonen);
            bool verhuurbaar = (bool)chkVerhuurbaar.IsChecked;
            bool? microgolf = chkMicrogolf.IsChecked;
            bool? tv = chkTV.IsChecked;
            bool? persoonlijksanitair =  chkPersoonlijkSanitair.IsChecked;
            bool? vaatwas = chkVaatwas.IsChecked;
            bool? wasmachine = chkWasmachine.IsChecked;
            bool? houtkachel = chkHoutkachel.IsChecked;

            bool fouten = false;
            if(huisnaam.Length == 0)
            {
                fouten = true;
                KleurRood(txtHuisNaam);
            }
            if(straatennummer.Length == 0)
            {
                fouten = true;
                KleurRood(txtStraatEnNummer);
            }
            if(postnummer == 0)
            {
                fouten = true;
                KleurRood(txtPostnummer);
            }
            if(gemeente.Length == 0)
            {
                fouten = true;
                KleurRood(txtGemeente);
            }
            if(basisprijs == 0)
            {
                fouten = true;
                KleurRood(txtBasisPrijs);
            }
            if(verminderdeprijs == 0)
            {
                fouten = true;
                KleurRood(txtVerminderdePrijs);
            }
            if(dagenvoorvermindering == 0)
            {
                fouten = true;
                KleurRood(txtDagenVoorVermindering);
            }
            if(waarborg == 0)
            {
                fouten = true;
                KleurRood(txtWaarborg);
            }
            if(maxpersonen == 0)
            {
                fouten = true;
                KleurRood(txtMaxPersonen);
            }

            if (fouten)
                return;

            if (situatie == "new")
            {
                if (cmbSoorten.SelectedIndex == 0)
                    verblijf = new Vakantiehuis();
                else
                    verblijf = new Caravan();
            }
            verblijf.HuisNaam = huisnaam;
            verblijf.StraatEnNummer = straatennummer;
            verblijf.Postnummer = postnummer;
            verblijf.Gemeente = gemeente;
            verblijf.BasisPrijs = basisprijs;
            verblijf.VerminderdePrijs = verminderdeprijs;
            verblijf.DagenVoorVermindering = dagenvoorvermindering;
            verblijf.Waarborg = waarborg;
            verblijf.MaxPersonen = maxpersonen;
            verblijf.Verhuurbaar = verhuurbaar;
            verblijf.Microgolf = microgolf;
            verblijf.TV = tv;
            if(verblijf is Vakantiehuis)
            {
                ((Vakantiehuis)verblijf).Vaatwas = vaatwas;
                ((Vakantiehuis)verblijf).Wasmachine = wasmachine;
                ((Vakantiehuis)verblijf).Houtkachel = houtkachel;
            }
            else
            {
                ((Caravan)verblijf).PersoonlijkSanitair = persoonlijksanitair;
            }
            if (situatie == "new")
            {
                Verblijven.AlleVerblijven.Add(verblijf);
            }
            this.Close();
        }
        private void KleurRood(Control ctrl)
        {
            ctrl.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFBEDED"));
            ctrl.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF0000"));
            ctrl.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF0000"));
        }
        private void KleurWit(Control ctrl)
        {
            ctrl.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString( "#FFFFFFFF"));
            ctrl.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF000000"));
            ctrl.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString( "#FF000000"));
        }


    }
}
