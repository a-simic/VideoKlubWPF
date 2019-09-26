using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfVk2019.Models;

namespace WpfVk2019.DAL
{
    class ZanrDal
    {
        private VideoKlub2019 db;

        public ZanrDal(VideoKlub2019 _db)
        {
            db = _db;
        }

        public List<Zanr> VratiZanrove()
        {
            return db.Zanrovi.ToList();
        }
    }
}
