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
using WpfVk2019.Models;

namespace WpfVk2019.Views.Clanovi
{
    /// <summary>
    /// Interaction logic for WindowClanPromena.xaml
    /// </summary>
    public partial class WindowClanPromena : Window
    {
       

public Clan Clan
{
    get {
        return                    
            new Clan {
                Ime = TextBoxIme.Text,
                Prezime = TextBoxPrezime.Text,
                LicnaKarta = TextBoxLicnaKarta.Text,
                UlicaBroj = TextBoxUlicaBroj.Text,
                Mesto = TextBoxMesto.Text
            };
    }
    set {
        TextBoxIme.Text = value.Ime;
        TextBoxPrezime.Text = value.Prezime;
        TextBoxLicnaKarta.Text = value.LicnaKarta;
        TextBoxUlicaBroj.Text = value.UlicaBroj;
        TextBoxMesto.Text = value.Mesto;
    }
}


        private bool Validacija()
        {
            if (string.IsNullOrWhiteSpace(TextBoxIme.Text))
            {
                MessageBox.Show("Unesite ime");
                TextBoxIme.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(TextBoxPrezime.Text))
            {
                MessageBox.Show("Unesite prezime");
                TextBoxPrezime.Focus();
                return false;
            }

            string mb = TextBoxLicnaKarta.Text.Trim();

            if (mb.Length != 9)
            {
                MessageBox.Show("Licna karta mora imati 9 karaktera");
                TextBoxLicnaKarta.Focus();
                return false;
            }

            foreach (char c in mb)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }

            if (string.IsNullOrWhiteSpace(TextBoxUlicaBroj.Text))
            {
                MessageBox.Show("Unesite ulicu i broj");
                TextBoxUlicaBroj.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(TextBoxMesto.Text))
            {
                MessageBox.Show("Unesite mesto");
                TextBoxMesto.Focus();
                return false;
            }

            return true;
        }

        public WindowClanPromena()
        {
            InitializeComponent();
        }

        private void ButtonPrihvati_Click(object sender, RoutedEventArgs e)
        {
            if (Validacija())
            {
                DialogResult = true;
            }
        }

        private void ButtonOdustani_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
