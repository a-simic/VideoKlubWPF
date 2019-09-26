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

namespace WpfVk2019.Views.Iznajmljivanja
{
    /// <summary>
    /// Interaction logic for WindowIznajmljivanjePromena.xaml
    /// </summary>
    public partial class WindowIznajmljivanjePromena : Window
    {
        private ClanDal cDal = null;
        private FilmDal fDal = null;

        private List<Clan> listaClanova = null;
        private List<Film> listaFilmova = null;
        public WindowIznajmljivanjePromena()
        {
            InitializeComponent();
            VideoKlub2019 db = new VideoKlub2019();
            cDal = new ClanDal(db);
            fDal = new FilmDal(db);
            listaClanova = cDal.VratiClanove();
            listaFilmova = fDal.VratiFilmove();
        }

        private void PrikaziClanove()
        {

            ComboBoxClan.ItemsSource = null;
            listaClanova = cDal.VratiClanove();
            ComboBoxClan.ItemsSource = listaClanova;
        }

        private void PrikaziFilmove()
        {
            ComboBoxFilm.ItemsSource = null;
            listaFilmova = fDal.VratiFilmove();
            ComboBoxFilm.ItemsSource = listaFilmova;
        }

        public Iznajmljivanje Iznajmljivanje
        {
            get {

                Iznajmljivanje i1 = new Iznajmljivanje();

                Film f1 = ComboBoxFilm.SelectedItem as Film;
                Clan c1 = ComboBoxClan.SelectedItem as Clan;
                i1.FilmId = f1.FilmId;
                i1.ClanId = c1.ClanId;
                   
                

                i1.DatumIznajmljivanja = DatePicker1.SelectedDate.Value;

                i1.DatumVracanja = DatePicker2.SelectedDate;

                if (!string.IsNullOrWhiteSpace(TextBoxCena.Text))
                {
                    i1.CenaPoDanu = decimal.Parse(TextBoxCena.Text);
                }
                else
                {
                    i1.CenaPoDanu = null;
                }
                return i1;
            }
            set {
               ComboBoxFilm.SelectedIndex = listaFilmova.FindIndex(f=>f.FilmId== value.FilmId);
               ComboBoxClan.SelectedIndex = listaClanova.FindIndex(f => f.ClanId == value.ClanId);
                DatePicker1.SelectedDate = value.DatumIznajmljivanja;
                DatePicker2.SelectedDate = value.DatumVracanja;
                TextBoxCena.Text = value.CenaPoDanu.ToString();
               
            }
        }


        private bool Validacija()
        {
            if (ComboBoxClan.SelectedIndex < 0)
            {
                MessageBox.Show("Morate odabrati clana", "Poruka");
                return false;
            }

            if (ComboBoxFilm.SelectedIndex < 0)
            {
                MessageBox.Show("Morate odabrati film", "Poruka");
                return false;
            }

            if (DatePicker1.SelectedDate == null)
            {
                MessageBox.Show("Morate odabrati datum uzimanja", "Poruka");
                return false;
            }
            
            return true;
        }

        private void ButtonPotvrdi_Click(object sender, RoutedEventArgs e)
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
            PrikaziClanove();
            PrikaziFilmove();
        }
    }
}
