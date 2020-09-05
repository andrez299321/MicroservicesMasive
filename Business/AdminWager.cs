using DataBase;
using Models;
using Newtonsoft.Json;
using Roulette.EnumerationTypes;
using Roulette.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Business
{
    public class AdminWager
    {
        MethodHash WagerDb;
        AdminRoulette Roulette;
        AdminCustomer Customer;
        public AdminWager()
        {
            WagerDb = new MethodHash();
            Roulette = new AdminRoulette();
            Customer = new AdminCustomer();
        }
        public string CreateWager(Wager ObjWager, string user)
        {
            WagerSave FinalWager = JoinObj(ObjWager, user);
            string ResultValidate = ValidateWager(FinalWager);
            if (ResultValidate.Equals("OK"))
            {
                string resultjson = "";
                List<WagerSave> ListResult = new List<WagerSave>();
                Dictionary<string, string>  dictionary = WagerDb.FetchAll(FinalWager.IdRoulette);
                if (dictionary.Count>0)
                {
                    string json = dictionary["Wager"];
                    ListResult = JsonConvert.DeserializeObject<List<WagerSave>>(json);
                    ListResult.Add(FinalWager);
                    resultjson = JsonConvert.SerializeObject(ListResult);
                    dictionary["Wager"] = resultjson;
                    ListResult.Clear();
                }
                else 
                {
                    ListResult.Add(FinalWager);
                    resultjson = JsonConvert.SerializeObject(ListResult);
                    dictionary.Add("Wager", resultjson);
                    ListResult.Clear();
                }
                if (WagerDb.Save(FinalWager.IdRoulette, dictionary).Equals("-1"))
                {
                    return "Hubo una desconexion en el guardado";
                }
                else 
                {
                    Customer.UpdateBalanceCustomer(FinalWager.User, Convert.ToDouble(FinalWager.Cash));

                    return "OK";
                }
            }
            else 
            {
                return ResultValidate;
            }
        }

        public string ListWager(string IdRoulette)
        {
            Roulette.CloseRoulette(IdRoulette);
            List<WagerSave> ListResult = new List<WagerSave>();
            Dictionary<string, string> dictionary = WagerDb.FetchAll(IdRoulette);
            string json = dictionary["Wager"].Replace('\"','"');

            return json;
        }

        public string ValidateWager(WagerSave ObjWager) 
        {
            int temp = 0;
            if (!Enum.IsDefined(typeof(EColor), ObjWager.Color.ToUpper()))
            {
                return "Solo se admite el color Negro y Rojo";
            }
            if (!Enumerable.Range(0, 36).Contains(Convert.ToInt32(ObjWager.Number)))
            {
                return "Solo se admiten numeros de 0 a 36";
            }
            if (!int.TryParse(ObjWager.Cash, out temp))
            {
                return "El valor a apostar debe ser numerico";
            }
            else
            {
                if (Convert.ToDouble(ObjWager.Cash) > 10000)
                {
                    return "El valor a apostar no debe exceder de los 10.000 dolares";
                }
            }
            if (!Roulette.ValidateStateRoulette(ObjWager.IdRoulette, "OPEN"))
            {
                return "La ruleta debe estar abierta para poder hacer sus apuestas";
            }
            if (!Customer.ValidateCustomerWager(ObjWager.User, ObjWager.Cash))
            {
                return "El usuario no tiene suficiente saldo";
            }

            return "OK";
        }

        public WagerSave JoinObj(Wager ObjWager, string user)
        {
            WagerSave Obj = new WagerSave();
            Obj.IdRoulette = ObjWager.IdRoulette;
            Obj.Cash = ObjWager.Cash;
            Obj.Color = ObjWager.Color.ToUpper();
            Obj.Number = ObjWager.Number;
            Obj.User = user;

            return Obj;
        }


    }
}
