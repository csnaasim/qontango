using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RoleUserApi.Model.Repositories.Implementation;
using RoleUserApi.Model.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace RoleUserApi.Model
{
    public class Debtor
    {
        public int Debtor_ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime FollowupDate { get; set; }
        public string FollowupDateS { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string WebAddress { get; set; }
        public string STAT_ID { get; set; }
        public int Q_ID { get; set; }

        public Account Account { get; set; }
        public List<Account> Accounts { get; set; }
        public List<Phone> Phones { get; set; }
        public List<Promise> Promises { get; set; }
        public List<Note> Notes { get; set; }
        public int ACCNO { get; set; }

        public double AllTotalDues { get; set; }
        public double CurrentBalance { get; set; }
        public double AllDebtAmount { get; set; }
        public double AllAmntCollected { get; set; }
        public double AllLastPayAmt { get; set; }
        public int TotalPromisesCount { get; set; }
        public int ApprovedPromises { get; set; }

        public string LastPaymentDate { get; set; }
        public double LastPaymentAmount { get; set; }
        public double AverageAge { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public object TimeZone { get; set; }
        public string phone { get; set; }
        public string Fax { get; set; }
        public Debtor Debtorobj { get; set; }
        public CQ cqobj { get; set; }
        public DbContact dbc { get; set; }
        public string TimeZoneID { get; set; }
        public string PhoneMask { get; set; }
        #region Functions
        public static Debtor Parse(DataRow row, string ColPrefix = "")
        {



            string[] requiredColumns = new[]
                {
                $"{ColPrefix}Debtor_ID",
                $"{ColPrefix}Name",
                $"{ColPrefix}Email",
                $"{ColPrefix}Q_ID",

                $"{ColPrefix}FollowupDate",
                $"{ColPrefix}ACCNO",
                $"{ColPrefix}Q_ID",
                $"{ColPrefix}STAT_ID",

                $"{ColPrefix}Address",
                $"{ColPrefix}City",
                $"{ColPrefix}State",
                $"{ColPrefix}Zip",
                $"{ColPrefix}Country",
                $"{ColPrefix}WebAddress",
                $"{ColPrefix}TimeZoneID",
                $"{ColPrefix}PhoneMask",

        };
            if (!row.HasColumns(requiredColumns))
                return null;
            Debtor dbt = new Debtor();
            dbt.Debtor_ID = row.GetValue<int>($"{ColPrefix}Debtor_ID");
            dbt.Name = row.GetValue<string>($"{ColPrefix}Name");
            dbt.Address = row.GetValue<string>($"{ColPrefix}Address");
            dbt.City = row.GetValue<string>($"{ColPrefix}City");
            dbt.State = row.GetValue<string>($"{ColPrefix}State");
            dbt.Zip = row.GetValue<string>($"{ColPrefix}Zip");
            dbt.Country = row.GetValue<string>($"{ColPrefix}Country");
            dbt.WebAddress = row.GetValue<string>($"{ColPrefix}WebAddress");
            dbt.STAT_ID = row.GetValue<string>($"{ColPrefix}STAT_ID");
            dbt.Q_ID = row.GetValue<int>($"{ColPrefix}Q_ID");
            dbt.cqobj = Debtor.getCQbyID(dbt.Q_ID);
            dbt.FollowupDate = row.GetValue<DateTime>($"{ColPrefix}FollowupDate");
            dbt.FollowupDateS = row.GetValue<DateTime>($"{ColPrefix}FollowupDate").ToString("MMMM dd,yyyy");
            dbt.TimeZoneID = row.GetValue<string>($"{ColPrefix}TimeZoneID");
            dbt.ACCNO = row.GetValue<int>($"{ColPrefix}ACCNO");
            dbt.PhoneMask = row.GetValue<string>($"{ColPrefix}PhoneMask");
            //dbt.Latitude = row.GetValue<double>($"{ColPrefix}latitude");
            //dbt.Longitude = row.GetValue<double>($"{ColPrefix}longitude");
            //if (dbt.Latitude != 0 || dbt.Longitude != 0)
            //{ dbt.TimeZone = gettimezone(dbt.Latitude, dbt.Longitude); }
            //else
            //{
            //    dbt.TimeZone = null;
            //}




            return dbt;
        }
        public static Debtor AccountBasedParse(DataRow row, string ColPrefix = "")
        {
            string[] requiredColumns = new[]
            {
                $"{ColPrefix}Debtor_ID",
                $"{ColPrefix}AllTotalDues",
                $"{ColPrefix}AllDebtAmount",

                $"{ColPrefix}AllAmntCollected",
                $"{ColPrefix}AllLastPayAmt",


        };
            if (!row.HasColumns(requiredColumns))
                return null;
            Debtor dbt = new Debtor();
            dbt.Debtor_ID = row.GetValue<int>($"{ColPrefix}Debtor_ID");
            dbt.AllTotalDues = row.GetValue<double>($"{ColPrefix}AllTotalDues");
            dbt.AllDebtAmount = row.GetValue<double>($"{ColPrefix}AllDebtAmount");
            dbt.AllAmntCollected = row.GetValue<double>($"{ColPrefix}AllAmntCollected");
            dbt.AllLastPayAmt = row.GetValue<double>($"{ColPrefix}AllLastPayAmt");
            dbt.CurrentBalance = dbt.AllTotalDues - dbt.AllAmntCollected;
            dbt.Debtorobj = Debtor.getDebtorbyID(dbt.Debtor_ID);
            dbt.Accounts = Debtor.SelectAllAccounts(dbt.Debtor_ID);
            var chkdbc = Debtor.getdbaccountbyDebtorID(dbt.Debtor_ID);
            if (chkdbc == null)
            {
                dbt.dbc = new DbContact();
            }
            else
            {
                dbt.dbc = chkdbc;
            }
            dbt.Phones = Debtor.SelectAllPhones(dbt.Debtor_ID);
            if (dbt.Phones.Count > 0)
            {
                dbt.phone = dbt.Phones.Where(a => a.PhoneType1.PHONETYPE == "Phone1").Select(b => b.Phone_ID).FirstOrDefault();
                dbt.Fax = dbt.Phones.Where(a => a.PhoneType1.PHONETYPE == "Fax").Select(b => b.Phone_ID).FirstOrDefault();
            }
            else
            {
                dbt.phone = "No Phone Number Available";
                dbt.Fax = "No Fax Number Available";
            }

            dbt.Promises = Debtor.SelectAllPromise(dbt.Debtor_ID);

            if (dbt.Promises.Count() > 0)
            {
                dbt.TotalPromisesCount = dbt.Promises.Count();
                dbt.ApprovedPromises = dbt.Promises.Where(a => a.BrokenPromise == 1).Count();
            }
            else
            {
                dbt.TotalPromisesCount = 0;
                dbt.ApprovedPromises = 0;
            }
            if (dbt.Accounts.Count > 0)
            {
                dbt.LastPaymentDate = dbt.Accounts.Max(a => a.LastPayDate).ToString("MMMM dd,yyyy");
                DateTime a = dbt.Accounts.Max(a => a.LastPayDate);
                dbt.LastPaymentAmount = dbt.Accounts.Where(b => b.LastPayDate == a).Select(a => a.LastPayAmt).FirstOrDefault();
            }
            else
            {
                dbt.LastPaymentDate = "00 00,0000";

                dbt.LastPaymentAmount = 0;
            }





            return dbt;
        }

        public static Debtor AccountBasedParse1(DataRow row, List<Account> Actlst, Debtor dbtt, DbContact dbcc, string phone1, string fax, List<Promise> promises,
            /*List<Note> Notes, */
            string ColPrefix = "")
        {
            string[] requiredColumns = new[]
            {
                $"{ColPrefix}Debtor_ID",
                $"{ColPrefix}AllTotalDues",
                $"{ColPrefix}AllDebtAmount",

                $"{ColPrefix}AllAmntCollected",
                $"{ColPrefix}AllLastPayAmt",


        };
            if (!row.HasColumns(requiredColumns))
                return null;
            Debtor dbt = new Debtor();
            dbt.Debtor_ID = row.GetValue<int>($"{ColPrefix}Debtor_ID");
            dbt.AllTotalDues = row.GetValue<double>($"{ColPrefix}AllTotalDues");
            dbt.AllDebtAmount = row.GetValue<double>($"{ColPrefix}AllDebtAmount");
            dbt.AllAmntCollected = row.GetValue<double>($"{ColPrefix}AllAmntCollected");
            dbt.AllLastPayAmt = row.GetValue<double>($"{ColPrefix}AllLastPayAmt");
            dbt.CurrentBalance = dbt.AllTotalDues - dbt.AllAmntCollected;
            dbt.Accounts = Actlst;
            dbt.Debtorobj = dbtt;
            var chkdbc = dbcc;
            if (chkdbc == null)
            {
                dbt.dbc = new DbContact();
            }
            else
            {
                dbt.dbc = chkdbc;
            }

            if ((phone1 != null || phone1 != ""))
            {
                if (dbt.Debtorobj != null)
                {
                    if (dbt.Debtorobj.PhoneMask != null)
                    {
                        var a = dbt.Debtorobj.PhoneMask;
                        string b = a.Replace('%', '#');
                        var c = "{0:" + b + "}";
                        dbt.phone = String.Format(c, Convert.ToInt64(phone1));
                    }
                    else
                    {
                        var a = "(%%%) %%%-%%%%";
                        string b = a.Replace('%', '#');
                        var c = "{0:" + b + "}";
                        dbt.phone = String.Format(c, Convert.ToInt64(phone1));

                    }
                }
                else
                {
                    var a = "(%%%) %%%-%%%%";
                    string b = a.Replace('%', '#');
                    var c = "{0:" + b + "}";
                    dbt.phone = String.Format(c, Convert.ToInt64(phone1));
                }
            }
            else
            {
                dbt.phone = "No Phone Number Available";
            }


            if ((fax != null || fax != ""))
            {
                if (dbt.Debtorobj != null)
                {
                    if (dbt.Debtorobj.PhoneMask != null)
                    {
                        var a = dbt.Debtorobj.PhoneMask;
                        string b = a.Replace('%', '#');
                        var c = "{0:" + b + "}";
                        dbt.Fax = String.Format(c, Convert.ToInt64(fax));
                    }
                    else
                    {
                        var a = "(%%%) %%%-%%%%";
                        string b = a.Replace('%', '#');
                        var c = "{0:" + b + "}";
                        dbt.Fax = String.Format(c, Convert.ToInt64(fax));

                    }
                }
                else
                {
                    var a = "(%%%) %%%-%%%%";
                    string b = a.Replace('%', '#');
                    var c = "{0:" + b + "}";
                    dbt.Fax = String.Format(c, Convert.ToInt64(fax));

                }
            }
            else
            {
                dbt.Fax = "No Fax Number Available";
            }
            if (dbt.Accounts.Count > 0)
            {
                dbt.AverageAge = dbt.Accounts.Sum(a => a.DateDifference) / dbt.Accounts.Count;
                dbt.LastPaymentDate = dbt.Accounts.Max(a => a.LastPayDate).ToString("MMMM dd,yyyy");
                DateTime a = dbt.Accounts.Max(a => a.LastPayDate);
                dbt.LastPaymentAmount = dbt.Accounts.Where(b => b.LastPayDate == a).Select(a => a.LastPayAmt).FirstOrDefault();
            }
            else
            {
                dbt.LastPaymentDate = "00 00,0000";

                dbt.LastPaymentAmount = 0;
            }
            dbt.Promises = promises;

            if (dbt.Promises.Count() > 0)
            {
                dbt.TotalPromisesCount = dbt.Promises.Count();
                dbt.ApprovedPromises = dbt.Promises.Where(a => a.BrokenPromise == 1).Count();
            }
            else
            {
                dbt.TotalPromisesCount = 0;
                dbt.ApprovedPromises = 0;
            }
            //if (Notes.Count > 0)
            //{
            //    dbt.Notes = Notes;
            //}
            return dbt;
        }
        public static dynamic getdbaccountbyDebtorID(int debid)
        {

            try
            {
                INetworkRepo networkRepo = new NetworkRepo();
                object[] obj = {
               0,
               debid
            };
                DataSet ds = networkRepo.PostDataTable("sp_Fourgetdbcontactbydebtorid", obj);
                DbContact debtor = new DbContact();
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow item in ds.Tables[0].Rows)
                        {
                            debtor = DbContact.Parse(item);
                        }

                        var result = debtor;
                        return result;
                    }
                    else
                    {
                        //throw new Exception("No results found.");
                        return null;
                    }

                }
                else
                {
                    throw new Exception("No results found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public static dynamic getCQbyID(int qid)
        {

            try
            {
                INetworkRepo networkRepo = new NetworkRepo();
                object[] obj = {
               0,
               qid
            };
                DataSet ds = networkRepo.PostDataTable("sp_FourgetCQbyID", obj);
                CQ cq = new CQ();
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow item in ds.Tables[0].Rows)
                        {
                            cq = CQ.Parse(item);
                        }

                        var result = cq;
                        return result;
                    }
                    else
                    {
                        //throw new Exception("No results found.");
                        return null;
                    }

                }
                else
                {
                    throw new Exception("No results found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }




        public static List<Phone> SelectAllPhones(int dbtid)
        {
            //dummy entry for testing purposes
            //  dbtid = 1;
            INetworkRepo networkRepo = new NetworkRepo();
            object[] obj = {
               0,
             dbtid
            };
            DataSet ds = networkRepo.PostDataTable("sp_FourgetPhoneByDebtorID", obj);
            List<Phone> Phones = new List<Phone>();
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                Phones.Add(Phone.Parse(item));
            }
            return Phones;
        }

        public static List<Promise> SelectAllPromise(int dbtid)
        {
            //dummy value  for testing purposes
            //  dbtid = 2;
            INetworkRepo networkRepo = new NetworkRepo();
            object[] obj = {
               0,
             dbtid
            };
            DataSet ds = networkRepo.PostDataTable("sp_getpromisesbydebtorid", obj);
            List<Promise> Promises = new List<Promise>();
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                Promises.Add(Promise.Parse(item));
            }
            return Promises;
        }



        public static dynamic getDebtorbyID(int debid)
        {

            try
            {
                INetworkRepo networkRepo = new NetworkRepo();
                object[] obj = {
               0,
               debid
            };
                DataSet ds = networkRepo.PostDataTable("sp_Fourgetdebtorbyid", obj);
                Debtor debtor = new Debtor();
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow item in ds.Tables[0].Rows)
                        {
                            debtor = Parse(item);
                        }

                        var result = debtor;
                        return result;
                    }
                    else
                    {
                        //throw new Exception("No results found.");
                        return null;
                    }

                }
                else
                {
                    throw new Exception("No results found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public static dynamic Getdebtors()
        {
            try
            {
                INetworkRepo networkRepo = new NetworkRepo();
                object[] obj = {
               0,

            };
                DataSet ds = networkRepo.PostDataTable("sp_FourGetAllDebtors", obj);
                List<Debtor> Debtores = new List<Debtor>();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow item in ds.Tables[0].Rows)
                        {

                            Debtores.Add(Parse(item));

                        }

                        var result = Debtores;
                        return result;
                    }
                    else
                    {
                        //throw new Exception("No results found.");
                        return null;
                    }

                }
                else
                {
                    throw new Exception("No results found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static dynamic GetAllCities()
        {
            try
            {
                INetworkRepo networkRepo = new NetworkRepo();
                object[] obj = {
               0,

            };
                DataSet ds = networkRepo.PostDataTable("sp_Foursetallcityregions", obj);
                List<City> Cities = new List<City>();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow item in ds.Tables[0].Rows)
                        {

                            Cities.Add(Model.City.Parse(item));

                        }

                        var result = Cities;
                        return result;
                    }
                    else
                    {
                        //throw new Exception("No results found.");
                        return null;
                    }

                }
                else
                {
                    throw new Exception("No results found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public static List<Account> SelectAllAccounts(int dbtid)
        {
            // dummy entry  for testing purposes
            //dbtid = 2;
            INetworkRepo networkRepo = new NetworkRepo();
            object[] obj = {
               0,
             dbtid
            };
            DataSet ds = networkRepo.PostDataTable("sp_FourgetAccountsByDebtorID", obj);
            List<Account> Accounts = new List<Account>();
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                Accounts.Add(Account.getaccountbyID(item.GetValue<int>($"ACCNO")));
            }
            return Accounts;
        }





        public static dynamic GetdebtorsBasedOnAccounts2(int number, int cid, List<int> balance)
        {

            try
            {
                INetworkRepo networkRepo = new NetworkRepo();
                object[] obj = {
               0,
               number,
                cid,

            };

                DataSet ds = networkRepo.PostDataTable("sp_FourGetAccountsBasedOnDebotr", obj);
                List<Debtor> Debtores = new List<Debtor>();
                List<Account> Accounts = new List<Account>();
                List<Debtor> dbtr = new List<Debtor>();
                List<DbContact> dbc = new List<DbContact>();
                List<Phone> ph = new List<Phone>();
                List<Promise> promises = new List<Promise>();
                List<Note> Notes = new List<Note>();
                if (ds.Tables.Count > 0)
                {

                    if (ds.Tables[1].Rows.Count > 0)
                    {

                        foreach (DataRow item1 in ds.Tables[1].Rows)
                        {

                            Accounts.Add(Account.Parse(item1));

                        }
                    }
                    if (ds.Tables[2].Rows.Count > 0)
                    {

                        foreach (DataRow item2 in ds.Tables[2].Rows)
                        {

                            dbtr.Add(Parse(item2));

                        }
                    }
                    if (ds.Tables[3].Rows.Count > 0)
                    {

                        foreach (DataRow item2 in ds.Tables[3].Rows)
                        {

                            dbc.Add(DbContact.Parse(item2));

                        }
                    }
                    if (ds.Tables[4].Rows.Count > 0)
                    {

                        foreach (DataRow item2 in ds.Tables[4].Rows)
                        {

                            ph.Add(Phone.ParseforPhoneType(item2));

                        }
                    }
                    if (ds.Tables[5].Rows.Count > 0)
                    {

                        foreach (DataRow item2 in ds.Tables[5].Rows)
                        {

                            promises.Add(Promise.Parse(item2));

                        }
                    }
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        foreach (DataRow item in ds.Tables[0].Rows)
                        {

                            Debtores.Add(
                                AccountBasedParse1
                                (
                                item,
                                Accounts.Where(a => a.Debtor_ID == item.GetValue<int>($"Debtor_ID")).ToList(),
                                dbtr.Where(a => a.Debtor_ID == item.GetValue<int>($"Debtor_ID")).FirstOrDefault(),
                                dbc.Where(a => a.Debtor_ID == item.GetValue<int>($"Debtor_ID")).FirstOrDefault(),
                                ph.Where(a => a.PhoneType == "Phone1").Select(b => b.Phone_ID).FirstOrDefault(),
                                ph.Where(a => a.PhoneType == "Fax").Select(b => b.Phone_ID).FirstOrDefault(),
                                promises.Where(a => a.Debtor_ID == item.GetValue<int>($"Debtor_ID")).ToList()
                                //Notes.Where(a => a.DEBTOR_ID == item.GetValue<int>($"DEBTOR_ID")).ToList()


                                )

                                );


                        }


                        var result = Debtores;
                        return result;
                    }

                    else
                    {
                        //throw new Exception("No results found.");
                        return null;
                    }





                }
                else
                {
                    throw new Exception("No results found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static dynamic gettimezone(double lat, double lang)
        {
            INetworkRepo networkRepo = new NetworkRepo();
            // JObject allJsonData = new JObject();
            //lat = 42.09253000;
            //lang = -88.85121000;
            //Fetch the JSON string from URL.
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string firsturlhalf = "https://maps.googleapis.com/maps/api/timezone/json?location=";
            string latlang = lat + "%2C" + lang;
            string secondurlhalf = "&timestamp=1331766000&language=es&key=AIzaSyDQzhOMw-TJGdMAR_BCrYUpD3mcwMhfX4M";
            string concaturl = firsturlhalf + latlang + secondurlhalf;
            string json = (new WebClient()).DownloadString(concaturl);

            string strRemSlash = json.Replace("\"", "\'");
            string strRemNline = strRemSlash.Replace("\n", " ");
            Model.TimeZone a = JsonConvert.DeserializeObject<Model.TimeZone>(@strRemNline);
            //  var a =    JsonConvert.DeserializeObject(strRemNline);
            //allJsonData = JObject.Parse(strRemNline);
            return a.timeZoneId;




        }
        public static dynamic GetdebtorsBasedOnAccounts22(int number, int cid, int[] balance)
        {


            //IList<int> ACCQ = balance;
            //string joinedACCQ = string.Join(",", ACCQ);
            try
            {
                INetworkRepo networkRepo = new NetworkRepo();
                //    object[] obj = {
                //   0,
                //   number,
                //    cid,
                //    balance
                //};
                DataTable LstAcc = new DataTable();
                LstAcc.Columns.Add("Q_ID");
                var bLenght = balance.Length;
                for (int i = 0; i < bLenght; i++)
                {
                    LstAcc.Rows.Add(balance[i]);
                }
                //balance.ForEach(acc =>
                //{

                //    LstAcc.Rows.Add(acc.ToString());
                //});
                Dictionary<string, object> parms1 = new Dictionary<string, object>
                    {
                        {"Q_ID", LstAcc },
                    {  "number", number },
                    {  "cid", cid },

                    };
                DataSet ds = networkRepo.CustomTypeDataTable("sp_FourGetAccountsBasedOnDebotr", parms1);

                //   DataSet ds = networkRepo.PostDataTable("sp_FourGetAccountsBasedOnDebotr", obj);
                List<Debtor> Debtores = new List<Debtor>();
                List<Account> Accounts = new List<Account>();
                List<Debtor> dbtr = new List<Debtor>();
                List<DbContact> dbc = new List<DbContact>();
                List<Phone> ph = new List<Phone>();
                List<Promise> promises = new List<Promise>();
                List<Note> Notes = new List<Note>();
                if (ds.Tables.Count > 0)
                {

                    if (ds.Tables[1].Rows.Count > 0)
                    {

                        foreach (DataRow item1 in ds.Tables[1].Rows)
                        {

                            Accounts.Add(Account.Parse(item1));

                        }
                    }
                    if (ds.Tables[2].Rows.Count > 0)
                    {

                        foreach (DataRow item2 in ds.Tables[2].Rows)
                        {

                            dbtr.Add(Parse(item2));

                        }
                    }
                    if (ds.Tables[3].Rows.Count > 0)
                    {

                        foreach (DataRow item2 in ds.Tables[3].Rows)
                        {

                            dbc.Add(DbContact.Parse(item2));

                        }
                    }
                    if (ds.Tables[4].Rows.Count > 0)
                    {

                        foreach (DataRow item2 in ds.Tables[4].Rows)
                        {

                            ph.Add(Phone.ParseforPhoneType(item2));

                        }
                    }
                    if (ds.Tables[5].Rows.Count > 0)
                    {

                        foreach (DataRow item2 in ds.Tables[5].Rows)
                        {

                            promises.Add(Promise.Parse(item2));

                        }
                    }

                    //if (ds.Tables[6].Rows.Count > 0)
                    //{

                    //    foreach (DataRow item2 in ds.Tables[6].Rows)
                    //    {

                    //        Notes.Add(Note.Parse(item2));

                    //    }
                    //}
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        foreach (DataRow item in ds.Tables[0].Rows)
                        {

                            Debtores.Add(
                                AccountBasedParse1
                                (
                                item,
                                Accounts.Where(a => a.Debtor_ID == item.GetValue<int>($"Debtor_ID")).ToList(),
                                dbtr.Where(a => a.Debtor_ID == item.GetValue<int>($"Debtor_ID")).FirstOrDefault(),
                                dbc.Where(a => a.Debtor_ID == item.GetValue<int>($"Debtor_ID")).FirstOrDefault(),
                                ph.Where(a => a.PhoneType == "Phone1" && a.ProfileID == item.GetValue<int>($"Debtor_ID")).Select(b => b.Phone_ID).FirstOrDefault(),
                                ph.Where(a => a.PhoneType == "Fax" && a.ProfileID == item.GetValue<int>($"Debtor_ID")).Select(b => b.Phone_ID).FirstOrDefault(),
                                promises.Where(a => a.Debtor_ID == item.GetValue<int>($"Debtor_ID")).ToList()
                                //   Notes.Where(a => a.DEBTOR_ID == item.GetValue<int>($"DEBTOR_ID")).ToList() 
                                )

                                );


                        }


                        var result = Debtores;
                        return result;
                    }

                    else
                    {
                        //throw new Exception("No results found.");
                        return null;
                    }





                }
                else
                {
                    throw new Exception("No results found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static dynamic GetdebtorsBasedOnAccounts(int number)
        {
            try
            {
                INetworkRepo networkRepo = new NetworkRepo();
                object[] obj = {
               0,
               number
            };
                DataSet ds = networkRepo.PostDataTable("sp_FourGetAccountsBasedOnDebotr", obj);
                List<Debtor> Debtores = new List<Debtor>();
                List<Account> Accounts = new List<Account>();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow item in ds.Tables[0].Rows)
                        {

                            Debtores.Add(AccountBasedParse(item));
                            foreach (DataRow item1 in ds.Tables[1].Rows)
                            {

                                Accounts.Add(Account.Parse(item1));

                            }

                        }


                        var result = Debtores;
                        return result;
                    }

                    else
                    {
                        //throw new Exception("No results found.");
                        return null;
                    }





                }
                else
                {
                    throw new Exception("No results found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Tuple<List<Note>, int> GetNotesBasedOnDebtorID(int debid, int rowindex, string basicpath , List<string> filterlst)
        {
            string IsStatusChangedNote = "";
            string IsClientAccessNote = "";
            string ByEmp = "";
            string ACCNO = "";
            string supress = "";
            if (filterlst.Count() == 0)
            {
                 IsStatusChangedNote = "-1";
                 IsClientAccessNote  = "-1";
                 ByEmp               = "-1";
                 ACCNO               = "-1";
                 supress             = "-1";
            }
            else
            {
                bool matchingvalues = filterlst.Any(stringToCheck => stringToCheck.Contains("Status Change Note")? true : false);
                if(matchingvalues ==true)
                {
                    IsStatusChangedNote = "1";
                }
                else 
                {
                    IsStatusChangedNote = "-1";
                }
               
                bool matchingvalues2 = filterlst.Any(stringToCheck => stringToCheck.Contains("Client Notes") ? true : false);
                if (matchingvalues2 == true)
                {
                    IsClientAccessNote = "1";
                }
                else
                {
                    IsClientAccessNote = "-1";
                }
                bool matchingvalues3 = filterlst.Any(stringToCheck => stringToCheck.Contains("Collector Notes") ? true : false);
                if (matchingvalues3 == true)
                {
                    ByEmp = "1";
                }
                else
                {
                    ByEmp = "-1";
                }
                bool matchingvalues4 = filterlst.Any(stringToCheck => stringToCheck.Contains("Account Notes") ? true : false);
                if (matchingvalues4 == true)
                {
                    ACCNO = "0";
                }
                else
                {
                    ACCNO = "-1";
                }
                bool matchingvalues5 = filterlst.Any(stringToCheck => stringToCheck.Contains("Suppress Note") ? true : false);
                if (matchingvalues5 == true)
                {
                    supress = "1";
                }
                else
                {
                    supress = "-1";
                }
            }
            rowindex = rowindex - 1;
            if (rowindex < 0)
            {
                rowindex = 0;
            }

            int b = 0;
            try
            {
                INetworkRepo networkRepo = new NetworkRepo();
                object[] obj = {
               0
               ,debid
               ,rowindex
               ,10
               ,IsStatusChangedNote
               ,IsClientAccessNote
               ,ByEmp
               ,ACCNO
               ,supress



            };
                DataSet ds = networkRepo.PostDataTable("sp_FourgetNotesbydebtorid", obj);

                List<Note> Notes = new List<Note>();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        foreach (DataRow item in ds.Tables[0].Rows)
                        {
                            Notes.Add(Note.Parse(item, basicpath, ""));
                        }


                        foreach (DataRow item in ds.Tables[1].Rows)
                        {
                            b = item.GetValue<int>($"TotalRecord");
                        }

                        var result = Notes;
                        // return result;

                        return Tuple.Create(result, b);
                    }

                    else
                    {
                        //throw new Exception("No results found.");
                        return null;
                    }





                }
                else
                {
                    throw new Exception("No results found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }




        public static Tuple<List<Debtor>, List<Account>> GetdebtorsBasedOnAccounts1(int number)
        {
            try
            {
                INetworkRepo networkRepo = new NetworkRepo();
                object[] obj = {
               0,
               number
            };
                DataSet ds = networkRepo.PostDataTable("sp_FourGetAccountsBasedOnDebotr", obj);
                List<Debtor> Debtores = new List<Debtor>();
                List<Account> Accounts = new List<Account>();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow item in ds.Tables[0].Rows)
                        {

                            Debtores.Add(AccountBasedParse(item));


                        }

                        foreach (DataRow item1 in ds.Tables[1].Rows)
                        {

                            Accounts.Add(Account.Parse(item1));

                        }
                        var result = Debtores;
                        var result1 = Accounts;
                        return Tuple.Create(result, result1);
                    }

                    else
                    {
                        //throw new Exception("No results found.");
                        return null;
                    }





                }
                else
                {
                    throw new Exception("No results found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public static int UpdateDebtorName(int debid, string columnname, string columnvalue)
        {

            //Regex r = new Regex("^[a-zA-Z0-9]*$");
            //if (r.IsMatch(columnvalue))
            //{

            //}
            //else
            //{

            //}
            int stat = 0;
        

            try
            {
                INetworkRepo networkRepo = new NetworkRepo();
                object[] obj = {
               0,
               debid,
               columnname,
               columnvalue


            };
                DataSet ds = networkRepo.PostDataTable("sp_FourUpdateDebtor", obj);

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow item in ds.Tables[0].Rows)
                        {
                            stat = item.GetValue<int>("STATUS");
                        }



                        return stat;
                    }

                    else
                    {
                        //throw new Exception("No results found.");
                        return 0;
                    }





                }
                else
                {
                    throw new Exception("No results found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public static int UpdateDebtorPhone(int debid, string columnname, string columnvalue, string fileid , int phonetypeid,int intCode)
        {

            int stat = 0;
            try
            {
                INetworkRepo networkRepo = new NetworkRepo();
                object[] obj = {
               0,
               debid,
               columnname,
               columnvalue,
               fileid,
               phonetypeid,
               intCode
            };
                DataSet ds = networkRepo.PostDataTable("sp_FourUpdateDebtorPhone", obj);

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow item in ds.Tables[0].Rows)
                        {
                            stat = item.GetValue<int>("STATUS");
                        }



                        return stat;
                    }

                    else
                    {
                        //throw new Exception("No results found.");
                        return 0;
                    }





                }
                else
                {
                    throw new Exception("No results found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        #endregion Functions

    }
}
