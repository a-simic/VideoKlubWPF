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
    /// Interaction logic for WindowIznajmljivanje.xaml
    /// </summary>
    public partial class WindowIznajmljivanje : Window
    {
        private IznajmljivanjeDal izDal = null;

        private List<View_Iznajmljivanja> listaIznajmljivanja = null;
        private void PrikaziIznajmljivanja()
        {         
            DataGrid1.Items.Clear();
           
            listaIznajmljivanja = izDal.VratiIznajmljivanja();

            foreach (View_Iznajmljivanja v in listaIznajmljivanja)
            {
                DataGrid1.Items.Add(v);
            }

           
            
        }
        public WindowIznajmljivanje()
        {
            InitializeComponent();

            VideoKlub2019 db = new VideoKlub2019();
            izDal = new IznajmljivanjeDal(db);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PrikaziIznajmljivanja();
        }

        private void ButtonUbaci_Click(object sender, RoutedEventArgs e)
        {
            WindowIznajmljivanjePromena w1 = new WindowIznajmljivanjePromena();
          
            w1.Title = "Ubaci podatke o iznajmljivanju";
            w1.Owner = this;
            w1.DatePicker1.SelectedDate = DateTime.Today;
            w1.DatePicker2.IsEnabled = false;
            w1.TextBoxCena.IsEnabled = false;
            if (w1.ShowDialog() == true)
            {

                
                Iznajmljivanje iz = w1.Iznajmljivanje;


                int id = izDal.UbaciIznajmljivanje(iz);

                if (id != -1)
                {

                    PrikaziIznajmljivanja();
                   
                    DataGrid1.Items.Refresh();
                    DataGrid1.Focus();
                    int indeks = listaIznajmljivanja.FindIndex(i => i.IznajmljivanjeId == id);
                    DataGrid1.SelectedIndex = indeks;
                    DataGrid1.ScrollIntoView(DataGrid1.Items[indeks]);
                    MessageBox.Show("Uspesno ste izvrsili iznajmljivanje", "Poruka");

                }
                else
                {
                    MessageBox.Show("Greska pri unosu iznajmljivanja", "Poruka");
                }

            }
        }

        private void ButtonPromeni_Click(object sender, RoutedEventArgs e)
        {
            int indeks = DataGrid1.SelectedIndex;
            if (indeks < 0)
            {
                MessageBox.Show("Odaberite iznajmljivanje");
                return;
            }

            View_Iznajmljivanja v1 = DataGrid1.SelectedItem as View_Iznajmljivanja;
            int id = v1.IznajmljivanjeId;

            Iznajmljivanje iz1 = izDal.VratiIznajmjivanje(id);

            WindowIznajmljivanjePromena w1 = new WindowIznajmljivanjePromena();
            w1.Title = "Promeni podatke o iznajmljivanju";
            w1.Owner = this;

            w1.Iznajmljivanje = iz1;


            if (w1.ShowDialog() == true)
            {
                iz1 = w1.Iznajmljivanje;

                iz1.IznajmljivanjeId = id;

                int rezultat = izDal.PromeniIznajmljivanje(iz1);
                if (rezultat == 0)
                {

                    PrikaziIznajmljivanja();
                    DataGrid1.Focus();
                    DataGrid1.SelectedIndex = indeks;
                    DataGrid1.ScrollIntoView(DataGrid1.Items[indeks]);
                    MessageBox.Show("Uspesno ste izmenili iznajmljivanje", "Iznajmljivanje promenjeno");
                }
                else
                {
                    MessageBox.Show("Greska pri izmeni iznajmljivanja", "Iznajmljivanje nije promenjeno");
                }
            }

        }

        private void ButtonObrisi_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid1.SelectedIndex < 0)
            {
                return;
            }
            View_Iznajmljivanja v1 = (View_Iznajmljivanja)DataGrid1.SelectedItem;
            Iznajmljivanje iz = izDal.VratiIznajmjivanje(v1.IznajmljivanjeId);

            if (MessageBox.Show("Da li ste siguran da zelite brisanje?", "Upozorenje", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                int rezultat = izDal.ObrisiIznajmljivanje(iz);
                if (rezultat == 0)
                {
                    PrikaziIznajmljivanja();
                }
                else
                {
                    MessageBox.Show("Ne mozete obrisati Iznajmljivanje", "Poruka");
                }
            }
        }

        
    }
}
