using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using MetinBank.Models;
using MetinBank.Util;

namespace MetinBank.Business
{
    public class BMevduat
    {
        private readonly DataAccess _dataAccess;

        public BMevduat()
        {
            _dataAccess = new DataAccess();
        }

        public List<MevduatOranModel> GetMevduatOranlari()
        {
            List<MevduatOranModel> list = new List<MevduatOranModel>();
            try
            {
                string query = "SELECT * FROM MevduatOranlari WHERE AktifMi = 1 ORDER BY MinGun";
                DataTable dt;
                string err = _dataAccess.ExecuteQuery(query, null, out dt);
                
                if (err == null && dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(new MevduatOranModel
                        {
                            OranID = Convert.ToInt32(row["OranID"]),
                            ParaBirimi = row["ParaBirimi"].ToString(),
                            MinGun = Convert.ToInt32(row["MinGun"]),
                            MaxGun = Convert.ToInt32(row["MaxGun"]),
                            FaizOrani = Convert.ToDecimal(row["FaizOrani"]),
                            StopajOrani = Convert.ToDecimal(row["StopajOrani"]),
                            Aciklama = row["Aciklama"].ToString()
                        });
                    }
                }
            }
            finally { _dataAccess.CloseConnection(); }
            
            return list;
        }

        public MevduatOranModel GetUygunOran(string paraBirimi, int gun, decimal tutar)
        {
            MevduatOranModel oran = null;
            try
            {
                string query = @"SELECT * FROM MevduatOranlari 
                                WHERE AktifMi = 1 
                                AND ParaBirimi = @pb 
                                AND @gun BETWEEN MinGun AND MaxGun
                                AND @tutar BETWEEN BaslangicTutar AND BitisTutar
                                ORDER BY FaizOrani DESC LIMIT 1";

                MySqlParameter[] p = { 
                    new MySqlParameter("@pb", paraBirimi),
                    new MySqlParameter("@gun", gun),
                    new MySqlParameter("@tutar", tutar)
                };

                DataTable dt;
                _dataAccess.ExecuteQuery(query, p, out dt);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    oran = new MevduatOranModel
                    {
                        OranID = Convert.ToInt32(row["OranID"]),
                        ParaBirimi = row["ParaBirimi"].ToString(),
                        FaizOrani = Convert.ToDecimal(row["FaizOrani"]),
                        StopajOrani = Convert.ToDecimal(row["StopajOrani"])
                    };
                }
            }
            finally { _dataAccess.CloseConnection(); }
            return oran;
        }

        public Dictionary<string, object> HesaplaGetiri(decimal tutar, int gun, string paraBirimi = "TL")
        {
            var oranModel = GetUygunOran(paraBirimi, gun, tutar);
            if (oranModel == null) return new Dictionary<string, object> { { "Hata", "Bu kriterlere uygun faiz oranı bulunamadı." } };

            // Formül: (Anapara * Faiz * Gün) / 36500
            decimal brutGetiri = (tutar * oranModel.FaizOrani * gun) / 36500m;
            
            // Stopaj
            decimal stopajTutari = brutGetiri * (oranModel.StopajOrani / 100m);
            decimal netGetiri = brutGetiri - stopajTutari;
            decimal toplamEleGecen = tutar + netGetiri;

            return new Dictionary<string, object>
            {
                { "Anapara", tutar },
                { "VadeGun", gun },
                { "FaizOrani", oranModel.FaizOrani },
                { "BrutGetiri", Math.Round(brutGetiri, 2) },
                { "StopajOrani", oranModel.StopajOrani },
                { "StopajTutari", Math.Round(stopajTutari, 2) },
                { "NetGetiri", Math.Round(netGetiri, 2) },
                { "ToplamEleGecen", Math.Round(toplamEleGecen, 2) }
            };
        }
    }
}
