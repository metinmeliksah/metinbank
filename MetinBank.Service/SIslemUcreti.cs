using MetinBank.Business;

namespace MetinBank.Service
{
    public class SIslemUcreti
    {
        private readonly BIslemUcreti _bIslemUcreti;

        public SIslemUcreti()
        {
            _bIslemUcreti = new BIslemUcreti();
        }

        public decimal IslemUcretiHesapla(string islemTipi, string islemKanali, decimal tutar)
        {
            return _bIslemUcreti.IslemUcretiHesapla(islemTipi, islemKanali, tutar);
        }

        public System.Data.DataTable TumUcretleriGetir()
        {
            return _bIslemUcreti.TumUcretleriGetir();
        }

        public System.Data.DataTable IslemTipiUcretleriGetir(string islemTipi, string islemKanali)
        {
            return _bIslemUcreti.IslemTipiUcretleriGetir(islemTipi, islemKanali);
        }
    }
}
