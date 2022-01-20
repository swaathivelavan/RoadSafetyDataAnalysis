using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;
using Oracle.DataAccess.Client;

namespace WebApplication3.BS3
{

    public class factors
    {
        public string year;
        public string FwoBelts;
        public string FwAlcohol;
        public string FwBelts;
        public string FwoAlcohol;
        public string nFwoBelts;
        public string nFwAlcohol;
        public string nFwBelts;
        public string nFwoAlcohol;

    }
    public class queryfive
    {
        public string year;
        public string FwoBelts;
        public string FwBelts;
        public string FwoAlcohol;
        public string FwAlcohol;

    }


    public class times
    {
        public string hour;
        public string noofaccidents;
    }
    public class legals
    {
        public string year;
        public string citeaccident;
        public string citenoaccident;
        public string warningaccident;
        public string warningnoaccident;

    }


    /// <summary>
    /// Summary description for WebService2
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class WebService2 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public void GetAllLegal()
        {

            List<legals> LegalList = new List<legals>();

            string connString = "User ID=k.iyer;Password=1TS21P4FGtVtklUwNW9h96DX;Data Source=oracle.cise.ufl.edu:1521/orcl";
            //String connString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (OracleConnection con = new OracleConnection(connString))
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select * from (select DISTINCT Substr(A.Date_Of_Stop, Length(A.Date_Of_Stop) - 3, 4) As Year , count (CASE WHEN contributed_to_accident = 'Yes' then 1 ELSE NULL END) as caused_accidentcitation, count (CASE WHEN contributed_to_accident = 'No' then 1 ELSE NULL END) as dident_caused_accident_citation from legalcase l, ACCIDENT a , leadingto l2 where l2.casenumber=l.casenumber and l2.accidentid=a.accidentid and l.VIOLATION_TYPE='Citation' group by Substr(A.Date_Of_Stop, Length(A.Date_Of_Stop) - 3, 4), violation_type order by Substr(A.Date_Of_Stop, Length(A.Date_Of_Stop) - 3, 4) asc) a join (select DISTINCT Substr(A.Date_Of_Stop, Length(A.Date_Of_Stop) - 3, 4) As Year , count (CASE WHEN contributed_to_accident = 'Yes' then 1 ELSE NULL END) as caused_accident_warning, count (CASE WHEN contributed_to_accident = 'No' then 1 ELSE NULL END) as dident_caused_accident_warning from legalcase l, ACCIDENT a , leadingto l2 where l2.casenumber=l.casenumber and l2.accidentid=a.accidentid and l.VIOLATION_TYPE='Warning' group by Substr(A.Date_Of_Stop, Length(A.Date_Of_Stop) - 3, 4), violation_type order by Substr(A.Date_Of_Stop, Length(A.Date_Of_Stop) - 3, 4) asc) b on a.year=b.year";
                con.Open();
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    legals legals = new legals();
                    legals.year = rdr["Year"].ToString();
                    legals.citeaccident = rdr["caused_accidentcitation"].ToString();
                    legals.citenoaccident = rdr["dident_caused_accident_citation"].ToString();
                    legals.warningaccident = rdr["caused_accident_warning"].ToString();
                    legals.warningnoaccident = rdr["dident_caused_accident_warning"].ToString();

                    LegalList.Add(legals);


                }
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(LegalList));

        }

        [WebMethod]
        public void QueryFive()
        {

            List<queryfive> QueryFiveList = new List<queryfive>();

            string connString = "User ID=k.iyer;Password=1TS21P4FGtVtklUwNW9h96DX;Data Source=oracle.cise.ufl.edu:1521/orcl";
            //String connString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (OracleConnection con = new OracleConnection(connString))
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "Select SUBSTR(a.date_of_stop, LENGTH(a.date_of_stop) - 3, 4) as year , count (CASE WHEN a.belts='No' AND fatal ='Yes' THEN 1 END) as FwoBelts, count (CASE WHEN a.alcohol='No' AND fatal ='Yes' THEN 1 END) as FwAlcohol, count (CASE WHEN a.belts='Yes' AND fatal ='Yes' THEN 1 END) as FwBelts, count (CASE WHEN a.alcohol='Yes' AND fatal ='Yes' THEN 1 END) as FwoAlcohol, count (CASE WHEN a.belts='No' AND fatal ='No' THEN 1 END) as nFwoBelts, count (CASE WHEN a.alcohol='No' AND fatal ='No' THEN 1 END) as nFwAlcohol, count (CASE WHEN a.belts='Yes' AND fatal ='No' THEN 1 END) as nFwBelts, count (CASE WHEN a.alcohol='Yes' AND fatal ='No' THEN 1 END) as nFwoAlcohol from accident a group by SUBSTR(date_of_stop, LENGTH(date_of_stop) - 3, 4) order by SUBSTR(date_of_stop, LENGTH(date_of_stop) - 3, 4) asc;";
                con.Open();
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    factors factors = new factors();
                    queryfive query = new queryfive();
                    query.year = rdr["year"].ToString();
                    query.FwoBelts = rdr["FwoBelts"].ToString();
                    query.FwBelts = rdr["FwBelts"].ToString();
                    query.FwoAlcohol = rdr["FwoAlcohol"].ToString();
                    query.FwAlcohol = rdr["FwAlcohol"].ToString();



                }
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(QueryFiveList));

        }



        [WebMethod]
        public void QueryTime(string year)
        {

            List<times> TimeList = new List<times>();

            string connString = "User ID=k.iyer;Password=1TS21P4FGtVtklUwNW9h96DX;Data Source=oracle.cise.ufl.edu:1521/orcl";
            //String connString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (OracleConnection con = new OracleConnection(connString))
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "Select * from (select SUBSTR(time_of_stop, 1, 2)+0 as time , count (accidentid) as noofaccidents from accident where SUBSTR(date_of_stop, LENGTH(date_of_stop) - 3, 4) = " + year + " and time_of_stop like '%AM' GROUP BY SUBSTR(time_of_stop, 1, 2)+0 order by SUBSTR(time_of_stop, 1, 2)+0 asc) UNION (select SUBSTR(time_of_stop, 1, 2)+12 as time , count (accidentid) from accident where SUBSTR(date_of_stop, LENGTH(date_of_stop) - 3, 4) = " + year + " and time_of_stop like '%PM' GROUP BY SUBSTR(time_of_stop, 1, 2)+12 )";
                con.Open();
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    factors factors = new factors();
                    times times = new times();
                    times.hour = rdr["time"].ToString();
                    times.noofaccidents = rdr["noofaccidents"].ToString();

                    TimeList.Add(times);




                }
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(TimeList));

        }

    



    [WebMethod]
        public void GetAllFactors()
        {

            List<factors> FactorList = new List<factors>();

            string connString = "User ID=k.iyer;Password=1TS21P4FGtVtklUwNW9h96DX;Data Source=oracle.cise.ufl.edu:1521/orcl";
            //String connString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (OracleConnection con = new OracleConnection(connString))
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "Select SUBSTR(a.date_of_stop, LENGTH(a.date_of_stop) - 3, 4) as year,count(CASE WHEN a.belts = 'No' AND fatal = 'Yes' THEN 1 END) as FwoBelts,count(CASE WHEN a.alcohol = 'No' AND fatal = 'Yes' THEN 1 END) as FwAlcohol,count(CASE WHEN a.belts = 'Yes' AND fatal = 'Yes' THEN 1 END) as FwBelts,count(CASE WHEN a.alcohol = 'Yes' AND fatal = 'Yes' THEN 1 END) as FwoAlcohol,count(CASE WHEN a.belts = 'No' AND fatal = 'No' THEN 1 END) as nFwoBelts,count(CASE WHEN a.alcohol = 'No' AND fatal = 'No' THEN 1 END) as nFwAlcohol,count(CASE WHEN a.belts = 'Yes' AND fatal = 'No' THEN 1 END) as nFwBelts,count(CASE WHEN a.alcohol = 'Yes' AND fatal = 'No' THEN 1 END) as nFwoAlcohol from accident a group by SUBSTR(date_of_stop, LENGTH(date_of_stop) - 3, 4) order by SUBSTR(date_of_stop, LENGTH(date_of_stop) - 3, 4) asc ";
                con.Open();
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    factors factors = new factors();
                    factors.year = rdr["year"].ToString();
                    factors.FwoBelts = rdr["FwoBelts"].ToString();
                    factors.FwAlcohol = rdr["FwAlcohol"].ToString();
                    factors.FwBelts = rdr["FwBelts"].ToString();
                    factors.FwoAlcohol = rdr["FwoAlcohol"].ToString();
                    factors.nFwoBelts = rdr["nFwoBelts"].ToString();
                    factors.nFwAlcohol = rdr["nFwAlcohol"].ToString();
                    factors.nFwBelts = rdr["nFwBelts"].ToString();
                    factors.nFwoAlcohol = rdr["nFwoAlcohol"].ToString();
                    FactorList.Add(factors);




                }
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(FactorList));

        }

    }
}
