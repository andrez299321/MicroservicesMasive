using System;
using System.Collections.Generic;
using System.Text;
using DataBase;
using Roulette.Models;

namespace Business
{
    public class AdminRoulette
    {
        MethodHash RouletteDb;
        string hash = "roulette";
        public AdminRoulette() {
            RouletteDb = new MethodHash();
        }
        public int CreateRoulette() 
        {
            int length = RouletteDb.HashLength(hash);
            if (length <= 0)
            {
                length = 0;
            }
            Dictionary<string, string> Dictionary =  new Dictionary<string, string>();
            Dictionary.Add((length + 1).ToString(), "CREATE");

            return Convert.ToInt32((RouletteDb.Save(hash, Dictionary)));
        }
        public bool OpenRoulette(string idRoulette) {
 
            if (ValidateStateRoulette(idRoulette, "CREATE"))
            {
                ChangeState(idRoulette, "OPEN");

                return true;
            }
            else 
            {
                return false;
            }
        }
        public bool CloseRoulette(string idRoulette)
        {
            if (ValidateStateRoulette(idRoulette, "OPEN"))
            {
                ChangeState(idRoulette, "CLOSE");

                return true;
            }
            else
            {
                return false;
            }
        }
        public Dictionary<string, string> ListRoulette()
        {
            return RouletteDb.FetchAll(hash); 
        }
        public bool ValidateStateRoulette(string idRoulette, string state) 
        {
            string Result = RouletteDb.FetchValue(hash, idRoulette);
            if (Result.Equals(state))
            {
                return true;
            }
            else 
            {
                return false;
            }
        }
        private void ChangeState(string idRoulette, string state)
        {
            Dictionary<string, string> Dictionary = new Dictionary<string, string>();
            Dictionary.Add(idRoulette, state);
            RouletteDb.Save(hash, Dictionary);
        }


    }
}
