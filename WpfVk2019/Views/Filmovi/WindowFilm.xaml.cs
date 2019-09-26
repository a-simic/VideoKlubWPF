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
    /// Interaction logic for WindowFilm.xaml
    /// </summary>
    public partial class WindowFilm : Window
    {
        private List<Film> listaFilmova = null;
        private FilmDal fDal = null;
       

        private void PrikaziFilmove()
        {
            ListBox1.ItemsSource = null;
            listaFilmova = fDal.VratiFilmove();
            ListBox1.ItemsSource = listaFilmova;
        }

       
        public WindowFilm()
        {
            InitializeComponent();
            VideoKlub2019 db = new VideoKlub2019();
            fDal = new FilmDal(db);
           
        }

        private void ListBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBox1.SelectedIndex < 0)
            {
                return;
            }
            Film f1 = ListBox1.SelectedItem as Film;
            TextBoxNaziv.Text = f1.Naziv;
            TextBoxZanr.Text = f1.Zanr.NazivZanra;
            TextBoxReziser.Text = f1.Reziser;
            TextBoxGodina.Text = f1.Godina.ToString();
        }

        private void ButtonUbaci_Click(object sender, RoutedEventArgs e)
        {
            WindowFilmPromena w1 = new WindowFilmPromena();
            w1.Title = "Ubaci podatke o filmu";
            w1.Owner = this;
            if (w1.ShowDialog() == true)
            {
                Film f = w1.Film;

                int id = fDal.UbaciFilm(f);

                if (id != -1)
                {
                    PrikaziFilmove();
                    int indeks = listaFilmova.FindIndex(c1 => c1.FilmId == id);
                    ListBox1.SelectedIndex = indeks;
                    ListBox1.ScrollIntoView(ListBox1.Items[indeks]);
                    MessageBox.Show("Ubacen novi film");
                }
                else
                {
                    MessageBox.Show("Greska pri unosu filma");
                }

            }
        }

        private void ButtonPromeni_Click(object sender, RoutedEventArgs e)
        {
            if (ListBox1.SelectedIndex < 0)
            {
                return;
            }

            Film f1 = ListBox1.SelectedItem as Film;
            WindowFilmPromena w1 = new WindowFilmPromena();
            w1.Title = "Promeni podatke o filmu";
            w1.Film = f1;

            int id = f1.FilmId;
            if (w1.ShowDialog() == true)
            {
                f1 = w1.Film;
                f1.FilmId = id;


                int rezultat = fDal.PromeniFilm(f1);
                if (rezultat == 0)
                {
                    PrikaziFilmove();
                    int indeks = listaFilmova.FindIndex(f2 => f2.FilmId == id);
                    ListBox1.SelectedIndex = indeks;
                    ListBox1.ScrollIntoView(ListBox1.Items[indeks]);
                    MessageBox.Show("Uspesno ste izmenili film", "Film promenjen");
                }
                else
                {
                    MessageBox.Show("Greska pri izmeni filma", "Film nije promenjen");
                }
            }
        }

        private void ButtonObrisi_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PrikaziFilmove();
           
        }
    }
}
