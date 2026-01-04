using System.Configuration;
using System.Collections.Generic;
using System.Data;
using MetinBank.Business;
using MetinBank.Models;

namespace MetinBank.Service
{
    public class SKredi
    {
        private readonly BKredi _bKredi;

        public SKredi()
        {
            _bKredi = new BKredi();
        }

        public List<KrediOranModel> GetKrediOranlari()
        {
            return _bKredi.GetKrediOranlari();
        }

        public Dictionary<string, object> Hesapla(decimal tutar, int vade)
        {
            return _bKredi.Hesapla(tutar, vade);
        }

        public string BasvuruYap(KrediBasvuruModel model)
        {
            return _bKredi.BasvuruYap(model);
        }

        public DataTable GetBekleyenBasvurular()
        {
            return _bKredi.GetBekleyenBasvurular();
        }

        public string BasvuruOnaylaReddet(int basvuruID, bool onayla, int mudurID, string aciklama = "")
        {
            return _bKredi.BasvuruOnaylaReddet(basvuruID, onayla, mudurID, aciklama);
        }

        public void KrediKullandir(int basvuruID)
        {
            _bKredi.KrediKullandir(basvuruID);
        }
    }
}
