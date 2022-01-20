using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Web.Script.Serialization;
using Oracle.DataAccess.Client;

namespace WebApplication3
{
    public class insurance2
    {
        public string noofaccidents;
        public string year;
    }
    public class position
    {
        public string lat;
        public string lon;
        public string kms;
        public string noofaccidents;
        public string noofaccidentsnorth;
        public string noofaccidentssouth;
        public string noofaccidentseast;
        public string noofaccidentswest;
        public string year;
    }
    public class vehicletypes
    {
        public string vehicletype;

    }
    public class colors
    {
        public string color;
        public string vehicletype;

    }
    public class models
    {
        public string model;

    }
    public class makes
    {
        public string make;

    }
    public class years
    {
        public string year;

    }
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
    public class Option
    {
        public string vehicletype;
        public string color;
        public string model;
        public string make;
        public string year;
        public string hazmat;
        public string cvehicle;
        public string resultVehicleID;

    }
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
       
        [WebMethod]
        public string queryOnePartOne(Option cmp)
        {
            List<Option> OptionList = new List<Option>();
            string vehicleID = "";
            string connString = "User ID=k.iyer;Password=1TS21P4FGtVtklUwNW9h96DX;Data Source=oracle.cise.ufl.edu:1521/orcl";


            using (OracleConnection con = new OracleConnection(connString))
            {
                string vehicletype = cmp.vehicletype;
                string color = cmp.color;
                string model = cmp.model;
                string make = cmp.make;
                string year = cmp.year;
                string hazmat = cmp.hazmat;
                string cvehicle = cmp.cvehicle;


                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT vehicleid FROM vehicle WHERE vehicletype =" + vehicletype + " AND color =" + color + " AND model = "+model+" AND make = "+make+" AND year = "+year+" AND factorid IN (SELECT factorid FROM vehiclefactor WHERE hazmat IN "+hazmat+" AND commercial_vehicle = "+cvehicle+" )";

                con.Open();
                OracleDataReader rdr = cmd.ExecuteReader();


                while (rdr.Read())
                {
                   
                     vehicleID= rdr["vehicleid"].ToString();
                    

                }
                
            }

            return vehicleID;

        }
        [WebMethod]
        public void Querypart(string lon,string lat,string kms)

        {
            List<position> PositionList = new List<position>();

            string connString = "User ID=k.iyer;Password=1TS21P4FGtVtklUwNW9h96DX;Data Source=oracle.cise.ufl.edu:1521/orcl";


            using (OracleConnection con = new OracleConnection(connString))
            {
                


                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT SUBSTR(a.date_of_stop, LENGTH(a.date_of_stop) - 3, 4) as year, count (CASE WHEN substr(regexp_substr(geolocation,'([^,]+)'),2) BETWEEN " + lat + " - " + kms + "/111 AND " + lat + " + " + kms + "/111 AND substr(regexp_substr(geolocation,'(,[^,]+)'),2,length(regexp_substr(geolocation,'(,[^,]+)'))-2) BETWEEN " + lon + " - " + kms + "/111 AND " + lon + " + " + kms + "/111 then 1 ELSE NULL END) as noofaccidents, count (CASE WHEN substr(regexp_substr(geolocation,'([^,]+)'),2) BETWEEN " + lat + " - " + kms + "/111 +" + kms + "/111 AND " + lat + " + " + kms + "/111 +" + kms + "/111 AND substr(regexp_substr(geolocation,'(,[^,]+)'),2,length(regexp_substr(geolocation,'(,[^,]+)'))-2) BETWEEN " + lon + " - " + kms + "/111 AND " + lon + " + " + kms + "/111 then 1 ELSE NULL END) as noofaccidentsNORTH, count (CASE WHEN substr(regexp_substr(geolocation,'([^,]+)'),2) BETWEEN " + lat + " - " + kms + "/111 -" + kms + "/111 AND " + lat + " + " + kms + "/111 -" + kms + "/111 AND substr(regexp_substr(geolocation,'(,[^,]+)'),2,length(regexp_substr(geolocation,'(,[^,]+)'))-2) BETWEEN " + lon + " - " + kms + "/111 AND " + lon + " + " + kms + "/111 then 1 ELSE NULL END) as noofaccidentsSOUTH, count (CASE WHEN substr(regexp_substr(geolocation,'([^,]+)'),2) BETWEEN " + lat + " - " + kms + "/111 AND " + lat + " + " + kms + "/111 AND substr(regexp_substr(geolocation,'(,[^,]+)'),2,length(regexp_substr(geolocation,'(,[^,]+)'))-2) BETWEEN " + lon + " - " + kms + "/111 +" + kms + "/111 AND " + lon + " + " + kms + "/111 +" + kms + "/111 then 1 ELSE NULL END) as noofaccidentsEAST, count (CASE WHEN substr(regexp_substr(geolocation,'([^,]+)'),2) BETWEEN " + lat + " - " + kms + "/111 AND " + lat + " + " + kms + "/111 AND substr(regexp_substr(geolocation,'(,[^,]+)'),2,length(regexp_substr(geolocation,'(,[^,]+)'))-2) BETWEEN " + lon + " - " + kms + "/111 -" + kms + "/111 AND " + lon + " + " + kms + "/111 -" + kms + "/111 then 1 ELSE NULL END) as noofaccidentsWEST FROM ACCIDENT a where geolocation not like '%null%' group by SUBSTR(a.date_of_stop, LENGTH(a.date_of_stop) - 3, 4) ORDER BY SUBSTR(a.date_of_stop, LENGTH(a.date_of_stop) - 3, 4) ASC";

                con.Open();
                OracleDataReader rdr = cmd.ExecuteReader();


                while (rdr.Read())
                {

                    position pos = new position();
                    pos.year = rdr["year"].ToString();
                    pos.noofaccidents = rdr["noofaccidents"].ToString();
                    pos.noofaccidentsnorth = rdr["noofaccidentsnorth"].ToString();
                    pos.noofaccidentssouth = rdr["noofaccidentssouth"].ToString();
                    pos.noofaccidentseast = rdr["noofaccidentseast"].ToString();
                    pos.noofaccidentswest = rdr["noofaccidentswest"].ToString();
                   
                    PositionList.Add(pos);


                }

            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(PositionList));



        }
        /*
        [WebMethod]
        public void Querypart(position cmp)

        {
            List<position> PositionList = new List<position>();
            
            string connString = "User ID=k.iyer;Password=1TS21P4FGtVtklUwNW9h96DX;Data Source=oracle.cise.ufl.edu:1521/orcl";


            using (OracleConnection con = new OracleConnection(connString))
            {
                string lat = cmp.lat;
                string lon = cmp.lon;
                string kms = cmp.kms;
               

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT SUBSTR(a.date_of_stop, LENGTH(a.date_of_stop) - 3, 4) as year, count (CASE WHEN substr(regexp_substr(geolocation,'([^,]+)'),2) BETWEEN "+lat+" - "+kms+"/111 AND "+lat+" + "+kms+"/111 AND substr(regexp_substr(geolocation,'(,[^,]+)'),2,length(regexp_substr(geolocation,'(,[^,]+)'))-2) BETWEEN "+lon+" - "+kms+"/111 AND "+lon+" + "+kms+"/111 then 1 ELSE NULL END) as noofaccidents, count (CASE WHEN substr(regexp_substr(geolocation,'([^,]+)'),2) BETWEEN "+lat+" - "+kms+"/111 +"+kms+"/111 AND "+lat+" + "+kms+"/111 +"+kms+"/111 AND substr(regexp_substr(geolocation,'(,[^,]+)'),2,length(regexp_substr(geolocation,'(,[^,]+)'))-2) BETWEEN "+lon+" - "+kms+"/111 AND "+lon+" + "+kms+"/111 then 1 ELSE NULL END) as noofaccidentsNORTH, count (CASE WHEN substr(regexp_substr(geolocation,'([^,]+)'),2) BETWEEN "+lat+" - "+kms+"/111 -"+kms+"/111 AND "+lat+" + "+kms+"/111 -"+kms+"/111 AND substr(regexp_substr(geolocation,'(,[^,]+)'),2,length(regexp_substr(geolocation,'(,[^,]+)'))-2) BETWEEN "+lon+" - "+kms+"/111 AND "+lon+" + "+kms+"/111 then 1 ELSE NULL END) as noofaccidentsSOUTH, count (CASE WHEN substr(regexp_substr(geolocation,'([^,]+)'),2) BETWEEN "+lat+" - "+kms+"/111 AND "+lat+" + "+kms+"/111 AND substr(regexp_substr(geolocation,'(,[^,]+)'),2,length(regexp_substr(geolocation,'(,[^,]+)'))-2) BETWEEN "+lon+" - "+kms+"/111 +"+kms+"/111 AND "+lon+" + "+kms+"/111 +"+kms+"/111 then 1 ELSE NULL END) as noofaccidentsEAST, count (CASE WHEN substr(regexp_substr(geolocation,'([^,]+)'),2) BETWEEN "+lat+" - "+kms+"/111 AND "+lat+" + "+kms+"/111 AND substr(regexp_substr(geolocation,'(,[^,]+)'),2,length(regexp_substr(geolocation,'(,[^,]+)'))-2) BETWEEN "+lon+" - "+kms+"/111 -"+kms+"/111 AND "+lon+" + "+kms+"/111 -"+kms+"/111 then 1 ELSE NULL END) as noofaccidentsWEST FROM ACCIDENT a where geolocation not like '%null%' group by SUBSTR(a.date_of_stop, LENGTH(a.date_of_stop) - 3, 4) ORDER BY SUBSTR(a.date_of_stop, LENGTH(a.date_of_stop) - 3, 4) ASC";
  
                con.Open();
                OracleDataReader rdr = cmd.ExecuteReader();


                while (rdr.Read())
                {

                    position pos = new position();
                    pos.noofaccidents = rdr["noofaccidents"].ToString();
                    pos.noofaccidentsnorth = rdr["noofaccidentsnorth"].ToString();
                    pos.noofaccidentssouth = rdr["noofaccidentssouth"].ToString();
                    pos.noofaccidentseast = rdr["noofaccidentseast"].ToString();
                    pos.noofaccidentswest = rdr["noofaccidentswest"].ToString();
                    pos.year = rdr["year"].ToString();
                    PositionList.Add(pos);


                }

            }

           JavaScriptSerializer js = new JavaScriptSerializer();
           Context.Response.Write(js.Serialize(PositionList));



        }
        */
        [WebMethod]
        public void GetAllVehicleTypes()
        {

            List<vehicletypes> VehicleTypeList = new List<vehicletypes>();

            string connString = "User ID=k.iyer;Password=1TS21P4FGtVtklUwNW9h96DX;Data Source=oracle.cise.ufl.edu:1521/orcl";
            //String connString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (OracleConnection con = new OracleConnection(connString))
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT DISTINCT vehicletype FROM vehicle";

                con.Open();
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    vehicletypes vehicletypes = new vehicletypes();
                    vehicletypes.vehicletype = rdr["vehicletype"].ToString();
                    VehicleTypeList.Add(vehicletypes);

                }
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(VehicleTypeList));

        }


        [WebMethod]
        public void GetAllColors(colors cmp)
        {

            List<colors> ColorList = new List<colors>();

            string connString = "User ID=k.iyer;Password=1TS21P4FGtVtklUwNW9h96DX;Data Source=oracle.cise.ufl.edu:1521/orcl";
            //String connString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (OracleConnection con = new OracleConnection(connString))
            {
                string vehicletype = cmp.vehicletype;
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT DISTINCT color FROM vehicle where vehicletype='"+vehicletype+"'";
               
                con.Open();
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    colors colors = new colors();
                    colors.color = rdr["color"].ToString();
                    ColorList.Add(colors);

                }
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(ColorList));

        }

        [WebMethod]
        public void GetAllModels()
        {

            List<models> ModelList = new List<models>();

            string connString = "User ID=k.iyer;Password=1TS21P4FGtVtklUwNW9h96DX;Data Source=oracle.cise.ufl.edu:1521/orcl";
            //String connString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (OracleConnection con = new OracleConnection(connString))
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT DISTINCT model FROM vehicle";

                con.Open();
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    models models = new models();
                    models.model = rdr["model"].ToString();
                    ModelList.Add(models);

                }
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(ModelList));

        }

        [WebMethod]
        public void GetAllMakes()
        {

            List<makes> MakesList = new List<makes>();

            string connString = "User ID=k.iyer;Password=1TS21P4FGtVtklUwNW9h96DX;Data Source=oracle.cise.ufl.edu:1521/orcl";
            //String connString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (OracleConnection con = new OracleConnection(connString))
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT DISTINCT make FROM vehicle";

                con.Open();
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    makes makes = new makes();
                    makes.make = rdr["make"].ToString();
                    MakesList.Add(makes);

                }
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(MakesList));

        }

        [WebMethod]
        public void GetAllYears()
        {

            List<years> YearList = new List<years>();

            string connString = "User ID=k.iyer;Password=1TS21P4FGtVtklUwNW9h96DX;Data Source=oracle.cise.ufl.edu:1521/orcl";
            //String connString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            using (OracleConnection con = new OracleConnection(connString))
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT DISTINCT year FROM vehicle where year<=2021 and year>=1950 order by year asc";

                con.Open();
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    years years = new years();
                    years.year = rdr["year"].ToString();
                    YearList.Add(years);

                }
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(YearList));

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

        [WebMethod]
        public void GetAllSites(string vehicleId)
        {

            List<insurance2> SiteList = new List<insurance2>();

            string connString = "User ID=k.iyer;Password=1TS21P4FGtVtklUwNW9h96DX;Data Source=oracle.cise.ufl.edu:1521/orcl";
           
            using (OracleConnection con = new OracleConnection(connString))
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "Select count (a.accidentid) as NoOfAccidents, SUBSTR(a.date_of_stop, LENGTH(a.date_of_stop) - 3, 4) as year from accident a, involvedin i where a.accidentid=i.accidentid and i.vehicleid="+vehicleId+" group by SUBSTR(date_of_stop, LENGTH(date_of_stop) - 3, 4) order by SUBSTR(date_of_stop, LENGTH(date_of_stop) - 3, 4) asc";
             
                con.Open();
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    insurance2 site = new insurance2();
                    site.noofaccidents = rdr["NoOfAccidents"].ToString();
                    site.year = rdr["year"].ToString();
                    SiteList.Add(site);

                }
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(SiteList));

        }
    }
}
