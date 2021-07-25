using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Group3_Project.DAO
{
    class Database
    {
        internal static SqlConnection GetConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["DataBase"].ToString());
        }

        internal static Account Login(string user, string pass)
        {
            Account acc = new Account();
            DataTable dataTable = Database.GetDataBySQL("SELECT * FROM Account where username = '"+user+"' COLLATE SQL_Latin1_General_CP1_CS_AS" +
                " and username = '"+user+"' and password = '"+pass+"'");
            if(dataTable.Rows.Count > 0)
            {
                acc.Id = Convert.ToInt32(dataTable.Rows[0]["accountId"].ToString());
                acc.Username = dataTable.Rows[0]["username"].ToString();
                acc.Password = dataTable.Rows[0]["password"].ToString();
                return acc;
            }
            return null;
        }

        internal static DataTable GetDataBySQL(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, GetConnection());
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataSet ds = new DataSet(); // database Cache
            ds.Clear();
            da.Fill(ds);
            return ds.Tables[0];
        }

        internal static void AddClass(string cid, string aid)
        {
            string sql = "INSERT INTO Class VALUES ('"+cid+"','"+aid+"')";
            SqlCommand cmd = new SqlCommand(sql, Database.GetConnection());
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        internal static DataTable getAllClassByAccId(string aid)
        {
            string sql = "select * from Class where accountID = '"+aid+"'";
            return Database.GetDataBySQL(sql);
        }
        internal static DataTable search(string txtSearch)
        {
            string sql = "select * from ScoreStudent where StudentID like '%" + txtSearch + "%'";
            return Database.GetDataBySQL(sql);
        }
        internal static ArrayList getStudent(ArrayList studentInfo, string sId)
        {
            DataTable dataTable = Database.GetDataBySQL("select className, scoreDetail, totalScore from ScoreStudent where StudentID = '" + sId + "'");
            studentInfo[0] = dataTable.Rows[0]["className"];
            studentInfo[1] = dataTable.Rows[0]["scoreDetail"];
            studentInfo[2] = dataTable.Rows[0]["totalScore"];
            return studentInfo;
        }
        internal static void AddStudent(string studentId,string paperNo, string className,string studentName)
        {
            string sql = "insert into ScoreStudent values ('"+studentId+"','"+paperNo+"','"+className+"','','','"+studentName+"')";
            SqlCommand cmd = new SqlCommand(sql, Database.GetConnection());
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        internal static DataTable getAllStudentByClass(string cid)
        {
            string sql = "select * from ScoreStudent where className = '" + cid + "'";
            return Database.GetDataBySQL(sql);
        }
        internal static DataTable getAllStudent()
        {
            string sql = "select * from ScoreStudent";
            return Database.GetDataBySQL(sql);
        }
        internal static void DeleteStudent(string classId)
        {
            string sql = "delete from ScoreStudent where className = '"+classId+"'";
            SqlCommand cmd = new SqlCommand(sql, Database.GetConnection());
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        internal static void DeleteClass(string aid)
        {
            string sql = "delete from Class where accountID = '"+aid+"'";
            SqlCommand cmd = new SqlCommand(sql, Database.GetConnection());
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        internal static List<string> listClass(string aid)
        {
            List<string> list = new List<string>();
            DataTable dataTable = Database.GetDataBySQL("select className from Class where accountID = '"+aid+"'");
            foreach (DataRow dataRow in dataTable.Rows)
            {
                list.Add(dataRow["className"].ToString());
            }
            return list;
        }
        internal static List<string> listStudentByClass(string clName)
        {
            List<string> list = new List<string>();
            DataTable dataTable = Database.GetDataBySQL("select StudentID+'_'+studentName+'_'+examCode as 'Student' from ScoreStudent where className = '" + clName + "'");
            foreach (DataRow dataRow in dataTable.Rows)
            {
                list.Add(dataRow["Student"].ToString());
            }
            return list;
        }
        internal static void updateScore(float totalMark, string scoreDetail, string studentId)
        {
            string sql = "UPDATE ScoreStudent SET totalScore = '"+totalMark+"', scoreDetail = '"+scoreDetail+"' WHERE StudentID = '"+studentId+"'";
            SqlCommand cmd = new SqlCommand(sql, Database.GetConnection());
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
    }
}
