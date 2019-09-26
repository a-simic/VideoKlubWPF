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
using WpfVk2019.DAL;
using WpfVk2019.Models;

namespace WpfVk2019.Views.Clanovi
{
    /// <summary>
    /// Interaction logic for WindowClan.xaml
    /// </summary>
    public partial class WindowClan : Window
    {
        private List<Clan> listaClanova = null;
        private ClanDal cDal = null;
        public WindowClan()
        {
            InitializeComponent();
            VideoKlub2019 db = new VideoKlub2019();
            cDal = new ClanDal(db);
        }
        private void PrikaziClanove()
        {
            ListBox1.Items.Clear();
            listaClanova = cDal.VratiClanove();
            foreach (Clan c in listaClanova)
            {
                ListBox1.Items.Add(c);
            }
        }
private void ListBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
{
    if (ListBox1.SelectedIndex > -1)
    {
        Clan c1 = ListBox1.SelectedItem as Clan;
        TextBoxIme.Text = c1.Ime;
        TextBoxPrezime.Text = c1.Prezime;
        TextBoxLicnaKarta.Text = c1.LicnaKarta;
        TextBoxUlicaBroj.Text = c1.UlicaBroj;
        TextBoxMesto.Text = c1.Mesto;
    }
}


private void ButtonUbaci_Click(object sender, RoutedEventArgs e)
{
    WindowClanPromena w1 = new WindowClanPromena();
    w1.Title = "Ubaci podatke o clanu";
    w1.Owner = this;
    if (w1.ShowDialog() == true)
    {
        Clan c = w1.Clan;

        int id = cDal.UbaciClana(c);

        if (id != -1)
        {
            PrikaziClanove();
            ListBox1.SelectedIndex = listaClanova.FindIndex(c1 => c1.ClanId == id);
            MessageBox.Show("Ubacen novi clan");
        }
        else
        {
            MessageBox.Show("Greska pri unosu clana");
        }

    }
}

private void ButtonPromeni_Click(object sender, RoutedEventArgs e)
{
    if (ListBox1.SelectedIndex < 0)
    {
        MessageBox.Show("Odaberi clana");
        return;
    }

    Clan clanPromena = ListBox1.SelectedItem as Clan;
    WindowClanPromena w1 = new WindowClanPromena();
    w1.Owner = this;
    w1.Title = "Promeni podatke o clanu";
    w1.Clan = clanPromena;
    int id = clanPromena.ClanId;
    if (w1.ShowDialog() == true)
    {

        clanPromena = w1.Clan;
        clanPromena.ClanId = id;



        int rezultat = cDal.PromeniClana(clanPromena);

        if (rezultat == 0)
        {
            PrikaziClanove();
            ListBox1.SelectedIndex = listaClanova.FindIndex(c1 => c1.ClanId == clanPromena.ClanId);
            MessageBox.Show("Podaci promenjeni");
        }
        else
        {
            MessageBox.Show("Greska pri promeni");
        }
    }

}

private void ButtonObrisi_Click(object sender, RoutedEventArgs e)
{
    if (ListBox1.SelectedIndex < 0)
    {
        return;
    }
    Clan c = ListBox1.SelectedItem as Clan;

    MessageBoxResult mbr = MessageBox.Show("Da li zelite da obrisete clana " + c.ToString(), "Pitanje", MessageBoxButton.YesNo);

    if (mbr == MessageBoxResult.No)
    {
        return;
    }

    int rezultat = cDal.ObrisiClana(c);

    if (rezultat == 0)
    {
        PrikaziClanove();
        TextBoxIme.Clear();
        TextBoxPrezime.Clear();
        TextBoxLicnaKarta.Clear();
        TextBoxUlicaBroj.Clear();
        TextBoxMesto.Clear();
        ListBox1.SelectedIndex = -1;
        MessageBox.Show("Obrisan clan");
    }

    else
    {
        MessageBox.Show("Greska pri brisanju clana");
    }

}

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PrikaziClanove();
        }
    }
}
