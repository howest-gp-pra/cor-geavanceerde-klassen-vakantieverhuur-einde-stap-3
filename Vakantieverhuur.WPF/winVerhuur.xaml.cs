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
    /// Interaction logic for winVerhuur.xaml
    /// </summary>
    public partial class winVerhuur : Window
    {
        public winVerhuur()
        {
            InitializeComponent();
        }

        public string situatie = "";
        public Verblijf verblijf;
        public Verhuur verhuur;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            VulHuurders();
            VulWoningGegevens();
            if(situatie == "new")
            {
                dtpDatumVan.SelectedDate = DateTime.Today;
                dtpDatumTot.SelectedDate = DateTime.Today.AddDays(7);
                VerwerkData();
                txtBetaald.Text = "0";
        
            }
            else
            {
                cmbHuurder.SelectedItem = verhuur.DeHuurder;
                dtpDatumVan.SelectedDate = verhuur.DatumVan;
                dtpDatumTot.SelectedDate = verhuur.DatumTot;
                txtBetaald.Text = verhuur.Betaald.ToString();
                chkWaarborgGestort.IsChecked = verhuur.WaarborgGestort;
                VerwerkData();
            }
        }
        private void VulHuurders()
        {
            List<Huurder> huurders = Huurders.AlleHuurders;
            foreach (Huurder huurder in huurders)
            {
                if(!huurder.IsBlackListed)
                {
                    cmbHuurder.Items.Add(huurder);
                }
            }
        }
        private void VulWoningGegevens()
        {
            if (verblijf is Vakantiehuis)
            {
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
            chkPersoonlijkSanitair.IsChecked = false;

        }
        private void DtpDatumVan_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            VerwerkData();
        }
        private void DtpDatumTot_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            VerwerkData();

        }
        private void TxtBetaald_TextChanged(object sender, TextChangedEventArgs e)
        {
            VerwerkData();
        }
        private void VerwerkData()
        {
            if (!this.IsLoaded) return;
            if (dtpDatumVan.SelectedDate == null) return;
            if (dtpDatumTot.SelectedDate == null) return;
            DateTime DatumVan = (DateTime)dtpDatumVan.SelectedDate;
            DateTime DatumTot = (DateTime)dtpDatumTot.SelectedDate;
            if(DatumTot < DatumVan)
            {
                DatumTot = DatumVan.AddDays(1);
                dtpDatumTot.SelectedDate = DatumTot;
            }

            lblOverboeking.Content = "";
            btnBewaren.Visibility = Visibility.Visible;
            foreach(Verhuur verhuring in Verhuringen.AlleVerhuringen)
            {
                if(verhuring.Vakantieverblijf == verblijf && (verhuring != verhuur))
                {
                    if(DatumVan >= verhuring.DatumVan && DatumVan < verhuring.DatumTot )
                    {
                        lblOverboeking.Content = "OVERBOEKING";
                        btnBewaren.Visibility = Visibility.Hidden;
                    }
                    if (DatumTot > verhuring.DatumVan && DatumTot <= verhuring.DatumTot)
                    {
                        lblOverboeking.Content = "OVERBOEKING";
                        btnBewaren.Visibility = Visibility.Hidden;
                    }
                }
            }


            TimeSpan ts = (TimeSpan)(dtpDatumTot.SelectedDate - dtpDatumVan.SelectedDate);
            int aantalOvernachtingen = (int)ts.TotalDays;
            lblAantalOvernachtingen.Content = aantalOvernachtingen.ToString();

            int dagenvoorvermindering = verblijf.DagenVoorVermindering;
            decimal tebetalen = TeBetalen();
            if (dagenvoorvermindering > aantalOvernachtingen)
            {
                lblTeBetalen.Content = $"{aantalOvernachtingen} x {verblijf.BasisPrijs} = {tebetalen}";
            }
            else
            {
                lblTeBetalen.Content = $"{aantalOvernachtingen} x {verblijf.VerminderdePrijs} = {tebetalen}";
            }

            decimal.TryParse(txtBetaald.Text, out decimal betaald);
            txtBetaald.Text = betaald.ToString();

            decimal nogtebetalen = tebetalen - betaald;
            lblNogTeBetalen.Content = nogtebetalen.ToString();




        }

        private void BtnAnnuleren_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnBewaren_Click(object sender, RoutedEventArgs e)
        {
            if(cmbHuurder.SelectedIndex == -1)
            {
                cmbHuurder.Focus();
                return;
            }
            if(situatie == "new")
                 verhuur = new Verhuur();
            verhuur.DeHuurder = (Huurder)cmbHuurder.SelectedItem;
            verhuur.Vakantieverblijf = verblijf;
            verhuur.DatumVan = (DateTime)dtpDatumVan.SelectedDate;
            verhuur.DatumTot = (DateTime)dtpDatumTot.SelectedDate;
            verhuur.WaarborgGestort = (bool)chkWaarborgGestort.IsChecked;


            TimeSpan ts = (TimeSpan)(dtpDatumTot.SelectedDate - dtpDatumVan.SelectedDate);
            int aantalOvernachtingen = (int)ts.TotalDays;
            lblAantalOvernachtingen.Content = aantalOvernachtingen.ToString();

            verhuur.Tebetalen = TeBetalen();
            decimal.TryParse(txtBetaald.Text, out decimal betaald);
            verhuur.Betaald = betaald;
            if (situatie == "new")
                Verhuringen.AlleVerhuringen.Add(verhuur);
            this.Close();

        }
        private decimal TeBetalen()
        {
            TimeSpan ts = (TimeSpan)(dtpDatumTot.SelectedDate - dtpDatumVan.SelectedDate);
            int aantalOvernachtingen = (int)ts.TotalDays;
            int dagenvoorvermindering = verblijf.DagenVoorVermindering;
            decimal tebetalen;
            if (dagenvoorvermindering > aantalOvernachtingen)
            {
                tebetalen = aantalOvernachtingen * verblijf.BasisPrijs;
            }
            else
            {
                tebetalen = aantalOvernachtingen * verblijf.VerminderdePrijs;
            }
            return tebetalen;
        }

    }
}
