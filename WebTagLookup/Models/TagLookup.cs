using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebTagLookup.Models
{
    public class TagLookup
    {
        [Display(Name="License")]
        [RequireOne("license,micro", ErrorMessage = "Must enter either license or microchip number")]
        [MinLength(6, ErrorMessage="License must be at least 6 characters long")]
        public string license { get; set; }
        [Display(Name="Microchip")]
        [MinLength(9, ErrorMessage="Microchip must be at least 9 characters")]
        public string micro { get; set; }

        public List<TagData> getTagInfo()
        {
            string license = this.license;
            string micro = this.micro;

            List<TagData> tdList = new List<TagData>();
            DataSet dsTag = new DataSet();
            string constr = ConfigurationManager.ConnectionStrings["animal"].ToString();
            string SQL = "";

            if (!string.IsNullOrEmpty(license) && !string.IsNullOrEmpty(micro))
            {
                SQL = "SELECT TagNumber, tattoo, FirstName + ' ' + LastName as OwnerName, Phone, PetName, PetType, Breed, Color, CONVERT(varchar,TagExpire,101) AS TagExpDate, CONVERT(varchar,VacExpire,101) AS VacExpDate " +
                         "FROM [Animal].[SYSADM].WebTagLookUp WHERE TagNumber like '%" + license + "%' AND tattoo = '" + micro + "'";
            }
            else if (!string.IsNullOrEmpty(license) && string.IsNullOrEmpty(micro))
            {
                SQL = "SELECT TagNumber, tattoo, FirstName + ' ' + LastName as OwnerName, Phone, PetName, PetType, Breed, Color, CONVERT(varchar,TagExpire,101) AS TagExpDate, CONVERT(varchar,VacExpire,101) AS VacExpDate " +
                         "FROM [Animal].[SYSADM].WebTagLookUp WHERE TagNumber like '%" + license + "%'";
            }
            else if (string.IsNullOrEmpty(license) && !string.IsNullOrEmpty(micro))
            {
                SQL = "SELECT TOP 1 TagNumber, tattoo, FirstName + ' ' + LastName as OwnerName, Phone, PetName, PetType, Breed, Color, CONVERT(varchar,TagExpire,101) AS TagExpDate, CONVERT(varchar,VacExpire,101) AS VacExpDate " +
                         "FROM [Animal].[SYSADM].WebTagLookUp WHERE tattoo = '" + micro + "'";
            }

            SQL = SQL + " ORDER BY TagExpire DESC";

            dsTag = selectRows(constr, SQL);

            tdList = fillModel(dsTag);

            return tdList;
        }        

        public string tagInfoToHTML(List<TagData> tdList)
        {
            string html = "<table class=\"table table-bordered table-condensed table-hover\"><tr><th>Licnese #</th><th>Microchip #</th><th>Owner Name</th><th>Phone Number</th><th>Pet Name</th><th>Animal Type</th><th>Breed</th><th>Tag Expiration Date</th></tr>";

            foreach (TagData td in tdList)
            {
                html = html + "<tr>";

                foreach (var item in td)
                {
                    html = html + "<td>" + item.ToString() + "</td>";
                }

                html = html + "</tr>";
            }

            html = html + "</table>";

            return html;
        }

        private static DataSet selectRows(string connectionString, string queryString)
        {
            using (SqlConnection connection = new SqlConnection(Environment.ExpandEnvironmentVariables(connectionString)))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(queryString, connection);
                DataSet dataset = new DataSet();
                adapter.Fill(dataset);
                return dataset;
            }
        }

        private static List<TagData> fillModel(DataSet ds)
        {
            List<TagData> tdList = new List<TagData>();

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    TagData td = new TagData();

                    td.TagNo = row["TagNumber"].ToString();
                    td.MicroNo = row["tattoo"].ToString();
                    td.OwnerName = row["OwnerName"].ToString();
                    td.Phone = row["Phone"].ToString();
                    td.PetName = row["PetName"].ToString();
                    td.PetType = row["PetType"].ToString();
                    td.Breed = row["Breed"].ToString();
                    td.PetColor = row["Color"].ToString();
                    td.VacExpDate = row["VacExpDate"].ToString();
                    td.TagExpDate = row["TagExpDate"].ToString();
                    
                    tdList.Add(td);
                }
            }

            return tdList;
        }
    }
}