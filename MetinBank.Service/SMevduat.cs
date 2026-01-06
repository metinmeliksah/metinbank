using System;
using System.Collections.Generic;
using MetinBank.Business;
using MetinBank.Models;

namespace MetinBank.Service
{
    public class SMevduat
    {
        private readonly BMevduat _bMevduat;

        public SMevduat()
        {
            _bMevduat = new BMevduat();
        }

        public List<MevduatOranModel> GetMevduatOranlari()
        {
            return _bMevduat.GetMevduatOranlari();
        }

        public Dictionary<string, object> HesaplaGetiri(decimal tutar, int gun, string paraBirimi = "TL")
        {
            return _bMevduat.HesaplaGetiri(tutar, gun, paraBirimi);
        }
    }
}
