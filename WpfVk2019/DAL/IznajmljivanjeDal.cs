using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfVk2019.Models;

namespace WpfVk2019.DAL
{
    class IznajmljivanjeDal
    {
        private VideoKlub2019 db;

        public IznajmljivanjeDal(VideoKlub2019 _db)
        {
            db = _db;
        }

        //public List<Iznajmljivanje> VratiIznajmljivanja()
        //{
        //    return db.Iznajmljivanja.ToList();

        //}

        public List<View_Iznajmljivanja> VratiIznajmljivanja()
        {
            using (VideoKlub2019 db1 = new VideoKlub2019())
            {
                return db1.View_Iznajmljivanja.ToList();
            }

            //  return db.View_Iznajmljivanja.ToList();

        }

        

        public Iznajmljivanje VratiIznajmjivanje(int id)
        {
            return db.Iznajmljivanja.Find(id);
        }

        public int UbaciIznajmljivanje(Iznajmljivanje iz)
        {
            try
            {
                db.Iznajmljivanja.Add(iz);
                db.SaveChanges();
                return iz.IznajmljivanjeId;
            }
            catch (Exception)
            {
                db.Entry(iz).State = EntityState.Detached;
                return -1;
            }
        }


        public int PromeniIznajmljivanje(Iznajmljivanje iz)
        {
            Iznajmljivanje iz1 = null;
            try
            {
                iz1 = db.Iznajmljivanja.Find(iz.IznajmljivanjeId);
                iz1.FilmId = iz.FilmId;
                iz1.ClanId = iz.ClanId;
                iz1.DatumIznajmljivanja = iz.DatumIznajmljivanja;
                iz1.DatumVracanja = iz.DatumVracanja;
                iz1.CenaPoDanu = iz.CenaPoDanu;
                db.SaveChanges();
                return 0;
            }
            catch (Exception)
            {

                db.Entry(iz1).State = EntityState.Unchanged;
                return -1;
            }

        }

        public int ObrisiIznajmljivanje(Iznajmljivanje iz)
        {
            Iznajmljivanje iz1 = null;
            try
            {
                iz1 = db.Iznajmljivanja.Find(iz.IznajmljivanjeId);
                db.Iznajmljivanja.Remove(iz1);
                db.Entry(iz1).State = EntityState.Deleted;
                db.SaveChanges();
                return 0;
            }
            catch (Exception)
            {
                db.Entry(iz1).State = EntityState.Unchanged;
                return -1;
            }

        }


       
    }
}

