using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using Tree_Node.Models;

namespace Tree_Node.Controllers
{
    public class HomeController : Controller
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public ActionResult Index()
        {

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("GetTreeNodesMaster", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<TreeNodeMaster> result = new List<TreeNodeMaster>();

                        while (reader.Read())
                        {
                            var item = new TreeNodeMaster
                            {
                                NodeId = reader.GetInt32(0),
                                NodeName = reader.GetString(1),
                                ParentNodeId = reader.GetInt32(2),
                                IsActive = reader.GetBoolean(3)

                            };

                            result.Add(item);
                        }

                        return View(result);
                    }
                }
            }
        }

        public ActionResult TreeNodeView()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            TreeNodeView treeNodeView = new TreeNodeView();
            List<List<string>> x = new List<List<string>>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("GetTreeNodeView", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Create a DataSet to hold the result
                    DataSet dataSet = new DataSet();

                    // Create a SqlDataAdapter and fill the DataSet with the result
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dataSet);
                    }

                    List<Parent> ParentList = new List<Parent>();
                    if (dataSet.Tables[0].Rows.Count != 0)
                    {
                        foreach (DataRow row in dataSet.Tables[0].Rows)
                        {
                            Parent EachparentName = new Parent
                            {
                                Name = row["NodeName"].ToString()
                            };

                            ParentList.Add(EachparentName);
                        }
                    }
                    treeNodeView.Parents = ParentList;
                    int i = 0;
                    child childs = new child();
                    List<child> childrenlist = new List<child>();
                    childs ch = new childs();

                    foreach (DataTable table in dataSet.Tables)
                    {
                        i++;
                        if (i == 1)
                        {
                            continue;
                        }
                        string s = null;
                        List<string> names = new List<string>();

                        foreach (DataRow row in table.Rows)
                        {
                            s = row["NodeName"].ToString();
                            names.Add(s);
                        }
                        x.Add(names);

                    }
                    treeNodeView.ChildNames = x;

                }
                return View(treeNodeView);
            }
        }

        public ActionResult Contact()
        {
            ViewBag.Title = "Developer contact page.";

            return View();
        }

        public string DeleteRow(string NodeId)
        {
            string s;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("DeleteNode", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Nodeid", SqlDbType.Int) { Value = Convert.ToInt32(NodeId) });

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            s = reader.GetString(0);
                            return s;
                        }
                        return null;
                    }
                }
            }

        }

        public string AddRow(string nodename, string parentnodeid)
        {
            string s;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("AddNode", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Nodename", SqlDbType.VarChar) { Value = nodename });
                    cmd.Parameters.Add(new SqlParameter("@Parentnodeid", SqlDbType.VarChar) { Value = parentnodeid });
                    //cmd.Parameters.Add(new SqlParameter("@Nodeid", SqlDbType.Bit) { Value = isactive?1:0});


                    int x = cmd.ExecuteNonQuery();
                    return x.ToString();
                }
            }

        }

        public string EditRow(string nodeid, string parentnodeid)
        {

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("EditNode", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Nodeid", SqlDbType.VarChar) { Value = nodeid });
                    cmd.Parameters.Add(new SqlParameter("@Parentnodeid", SqlDbType.VarChar) { Value = parentnodeid });

                    int x = cmd.ExecuteNonQuery();
                    return x.ToString();
                }
            }

        }
    }
}