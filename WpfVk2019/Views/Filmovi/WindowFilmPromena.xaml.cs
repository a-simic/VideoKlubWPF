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

namespace WpfVk2019.Views.Filmovi
{
    /// <summary>
    /// Interaction logic for WindowFilmPromena.xaml
    /// </summary>
    public partial class WindowFilmPromena : Window
    {
        private ZanrDal zDal = null;
        private List<Zanr> listaZanrova = null;
        public Film Film
        {
            get {
                return
                  new Film
                  {
                      Naziv = TextBoxNaziv.Text,
                      ZanrId = (ComboBox1.SelectedItem as Zanr).ZanrId,
                      Reziser = TextBoxReziser.Text,
                      Godina = int.Parse( TextBoxGodina.Text)
                  };
                }
            set {
                TextBoxNaziv.Text = value.Naziv;
                ComboBox1.SelectedIndex = listaZanrova.FindIndex(z => z.ZanrId == value.ZanrId);
                TextBoxReziser.Text = value.Reziser;
                TextBoxGodina.Text = value.Godina.ToString();
            }
        }

        public WindowFilmPromena()
        {
            InitializeComponent();
            VideoKlub2019 db = new VideoKlub2019();
            zDal = new ZanrDal(db);
            listaZanrova= zDal.VratiZanrove();
        }

        private void PrikaziZanrove()
        {
            ComboBox1.ItemsSource = null;
            ComboBox1.ItemsSource = zDal.VratiZanrove();
        }
        private bool Validacija()
        {
            if (string.IsNullOrWhiteSpace(TextBoxNaziv.Text))
            {
                MessageBox.Show("Morate uneti naziv filma", "Poruka");
                TextBoxNaziv.Focus();
                return false;
            }
            if (ComboBox1.SelectedIndex < 0)
            {
                MessageBox.Show("Morate odabrati zanr filma", "Poruka");

                return false;
            }
            if (string.IsNullOrWhiteSpace(TextBoxReziser.Text))
            {
                MessageBox.Show("Morate uneti rezisera filma", "Poruka");
                TextBoxReziser.Focus();
                return false;
            }

            if (!int.TryParse(TextBoxGodina.Text, out int godina))
            {
                MessageBox.Show("Godina filma je ceo broj", "Poruka");
                TextBoxGodina.Clear();
                TextBoxGodina.Focus();
                return false;
            }

           
            return true;
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PrikaziZanrove();
        }
    }
}
