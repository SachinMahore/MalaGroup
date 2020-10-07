using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MalaGroupERP.Data;
using System.Data;
using System.Data.Common;

namespace MalaGroupERP.Models
{
    public class HomeModel
    {
        public string ID { get; set; }
        public string FileName { get; set; }
        public string TotalCount { get; set; }
        public string DuplicateCount { get; set; }
        public string DateExported { get; set; }
        public string Status { get; set; }
        public string IsExported { get; set; }
        public List<AutocompleteSuggestions> SearchList { get; set; }
        public DashboardOrderGraph dashOrderGraph { get; set; }
        public DashboardSalesGraph dashSalesGraph { get; set; }
        public DashboardAOGraph dashAOGraph { get; set; }
        public DashboardIdThDecGraph dashIdThDecGraph { get; set; }
        public List<HomeModel> GetExportHistory()
        {
            try
            {
                List<HomeModel> listExportHistory = new List<HomeModel>();
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    try
                    {
                        db.Database.Connection.Open();
                        cmd.CommandText = "usp_GetLeadExportHistory";
                        cmd.CommandType = CommandType.StoredProcedure;

                        DbParameter paramName = cmd.CreateParameter();
                        paramName.ParameterName = "UserID";
                        paramName.Value = MalaGroupWebSession.CurrentUser.UserID;
                        cmd.Parameters.Add(paramName);

                        DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dtTable);
                        db.Database.Connection.Close();

                        foreach (DataRow dr in dtTable.Rows)
                        {
                            DateTime? dateExported = null;
                            try
                            {
                                dateExported = Convert.ToDateTime(dr["DateExported"].ToString());
                            }
                            catch
                            {

                            }
                            listExportHistory.Add(new HomeModel()
                            {
                                ID = dr["ID"].ToString(),
                                FileName = dr["FileName"].ToString().Split('_')[0],
                                TotalCount = dr["TotalCount"].ToString(),
                                DuplicateCount = dr["DuplicateCount"].ToString(),
                                DateExported = dateExported.Value.ToString("MM/dd/yyyy h:mm tt"),
                                Status = dr["Status"].ToString(),
                                IsExported = dr["IsExported"].ToString()
                            });
                        }
                    }
                    catch
                    {
                        db.Database.Connection.Close();
                    }
                }

                db.Dispose();
                return listExportHistory.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<HomeModel> GetTakeOffListReport()
        {
            try
            {
                List<HomeModel> listTakeOffListReport = new List<HomeModel>();
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    try
                    {
                        db.Database.Connection.Open();
                        cmd.CommandText = "usp_GetTakeOffListHistory";
                        cmd.CommandType = CommandType.StoredProcedure;

                        DbParameter paramName = cmd.CreateParameter();
                        paramName.ParameterName = "UserID";
                        paramName.Value = MalaGroupWebSession.CurrentUser.UserID;
                        cmd.Parameters.Add(paramName);

                        DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dtTable);
                        db.Database.Connection.Close();

                        foreach (DataRow dr in dtTable.Rows)
                        {
                            DateTime? dateExported = null;
                            try
                            {
                                dateExported = Convert.ToDateTime(dr["DateExported"].ToString());
                            }
                            catch
                            {

                            }
                            listTakeOffListReport.Add(new HomeModel()
                            {
                                ID = dr["ID"].ToString(),
                                FileName = dr["FileName"].ToString(),
                                TotalCount = "0",
                                DuplicateCount = "0",
                                DateExported = dateExported.Value.ToString("MM/dd/yyyy"),
                                Status = dr["Status"].ToString(),
                                IsExported = dr["IsExported"].ToString()
                            });
                        }
                    }
                    catch
                    {
                        db.Database.Connection.Close();
                    }
                }

                db.Dispose();
                return listTakeOffListReport.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void MarkViewedExportHistory(int ID)
        {
            try
            {
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                var exportHistory = db.tbl_ExportHistory.Where(p => p.ID == ID).FirstOrDefault();
                if (exportHistory != null)
                {
                    exportHistory.IsViewed = 1;
                    db.SaveChanges();
                }
                db.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteTakeOffListReport(long ID)
        {
            try
            {
                MalaGroupERPEntities db = new MalaGroupERPEntities();

                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    try
                    {
                        db.Database.Connection.Open();
                        cmd.CommandText = "usp_DeleteTakeOffListHistory";
                        cmd.CommandType = CommandType.StoredProcedure;

                        DbParameter paramRID = cmd.CreateParameter();
                        paramRID.ParameterName = "ReportID";
                        paramRID.Value = ID.ToString();
                        cmd.Parameters.Add(paramRID);

                        cmd.ExecuteNonQuery();
                        db.Database.Connection.Close();
                    }
                    catch (Exception ex)
                    {
                        db.Database.Connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<AutocompleteSuggestions> GetAutocompleteSuggestions(string term)
        {
            try
            {
                string[] SFST = term.Split(':');
                string searchFor = "";
                string searchText = "";

                if (SFST.Length > 1)
                {
                    searchFor = SFST[0];
                    for (int i = 1; i < SFST.Length; i++)
                    {
                        searchText += SFST[i];
                    }
                }
                else
                {
                    searchFor = "";
                    searchText = SFST[0];
                }
                List<AutocompleteSuggestions> model = new List<AutocompleteSuggestions>();
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    try
                    {
                        db.Database.Connection.Open();
                        cmd.CommandText = "usp_GetAutocompleteSuggestions";
                        cmd.CommandType = CommandType.StoredProcedure;

                        DbParameter paramSF = cmd.CreateParameter();
                        paramSF.ParameterName = "SearchFor";
                        paramSF.Value = searchFor;
                        cmd.Parameters.Add(paramSF);

                        DbParameter paramST = cmd.CreateParameter();
                        paramST.ParameterName = "SearchText";
                        paramST.Value = searchText;
                        cmd.Parameters.Add(paramST);

                        DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dtTable);
                        db.Database.Connection.Close();
                    }
                    catch
                    {
                        db.Database.Connection.Close();
                    }

                    foreach (DataRow dtRow in dtTable.Rows)
                    {
                        model.Add(new AutocompleteSuggestions()
                        {
                            SearchFor = dtRow["SearchFor"].ToString(),
                            DValue = dtRow["DValue"].ToString(),
                            DText = dtRow["DText"].ToString(),
                        });
                    }
                }
                db.Dispose();
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HomeModel GetDashGraphData(HomeModel model)
        {

            MalaGroupERPEntities db = new MalaGroupERPEntities();
            DataSet dt1Table = new DataSet("Export Daily");
            long userID = MalaGroupWebSession.CurrentUser.UserID;

            DateTime dateTime = DateTime.UtcNow.Date;

            using (var cmd = db.Database.Connection.CreateCommand())
            {
                try
                {
                    db.Database.Connection.Open();
                    db.Database.CommandTimeout = 0;
                    cmd.CommandText = "usp_GetDashGraphData";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramName = cmd.CreateParameter();
                    paramName.ParameterName = "UserID";
                    paramName.Value = userID;
                    cmd.Parameters.Add(paramName);
                    cmd.CommandTimeout = 0;

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt1Table);
                    db.Database.Connection.Close();
                }
                catch
                {
                    db.Database.Connection.Close();
                }
            }

            db.Dispose();
            //dt1Table.Tables[0].TableName = "ExcelTable";
            DataTable dtTable = dt1Table.Tables[0];
            DataTable dtTable1 = dt1Table.Tables[1];
            DataTable dtTable2 = dt1Table.Tables[2];
            DataTable dtTable3 = dt1Table.Tables[3];

            HomeModel dashModel = new HomeModel();

            DashboardOrderGraph dashOrderGraph = new DashboardOrderGraph();
            DashboardSalesGraph dashSalesGraph = new DashboardSalesGraph();
            DashboardAOGraph dashAOGraph = new DashboardAOGraph();
            DashboardIdThDecGraph dashIdThDecGraph = new DashboardIdThDecGraph();
            // int i = 0;
            foreach (DataRow dr in dtTable.Rows)
            {

                dashOrderGraph.TotalOpen = dr["TotalOpen"].ToString();
                dashOrderGraph.TotalConverted = dr["TotalConverted"].ToString();
                dashOrderGraph.TotalNotConverted = dr["TotalNotConverted"].ToString();
                if (dr["TotalOpen"].ToString() != "0")
                {
                    dashOrderGraph.TotalPercentage = ((Convert.ToDecimal(dr["TotalConverted"].ToString()) * 100) / Convert.ToDecimal(dr["TotalOpen"].ToString())).ToString("0.00");
                }
                else
                {
                    dashOrderGraph.TotalPercentage = "0.00";
                }

            }

            dashModel.dashOrderGraph = dashOrderGraph;

            foreach (DataRow dr in dtTable1.Rows)
            {

                dashSalesGraph.TotalAmt = Convert.ToDecimal(dr["TotalAmt"].ToString()).ToString("0.00");
                dashSalesGraph.TotalRefund = Convert.ToDecimal(dr["TotalRefund"].ToString()).ToString("0.00");
                dashSalesGraph.TotalVoided = Convert.ToDecimal(dr["TotalVoided"].ToString()).ToString("0.00");
                dashSalesGraph.TotalDeclined = Convert.ToDecimal(dr["TotalDeclined"].ToString()).ToString("0.00");
            }

            dashModel.dashSalesGraph = dashSalesGraph;


            int i = 0;
            foreach (DataRow dr in dtTable2.Rows)
            {
                string agentPerc = "";
                if (dr["ATotalOpen"].ToString() != "0")
                {
                    agentPerc = "-" + ((Convert.ToDecimal(dr["ATotalConverted"].ToString()) * 100) / Convert.ToDecimal(dr["ATotalOpen"].ToString())).ToString("0.00") + "%";
                }
                else
                {
                    agentPerc = "- 0.00%";

                }
                dashAOGraph.Labels += (dashAOGraph.Labels == null ? dr["AgentName"].ToString() + agentPerc : "," + dr["AgentName"].ToString() + agentPerc);
                dashAOGraph.ATotalOpen += (dashAOGraph.ATotalOpen == null ? dr["ATotalOpen"].ToString() : "," + dr["ATotalOpen"].ToString());
                dashAOGraph.ATotalConverted += (dashAOGraph.ATotalConverted == null ? dr["ATotalConverted"].ToString() : "," + dr["ATotalConverted"].ToString());
                dashAOGraph.AddDecals += (dashAOGraph.AddDecals == null ? dr["AddDecals"].ToString() : "," + dr["AddDecals"].ToString());
                dashAOGraph.IdTheft += (dashAOGraph.IdTheft == null ? dr["IdentityTheft"].ToString() : "," + dr["IdentityTheft"].ToString());
                dashAOGraph.Renewal += (dashAOGraph.Renewal == null ? dr["Renewal"].ToString() : "," + dr["Renewal"].ToString());
                i++;
            }

            dashModel.dashAOGraph = dashAOGraph;

            foreach (DataRow dr in dtTable3.Rows)
            {

                dashIdThDecGraph.IdTheft = dr["IdTheft"].ToString();
                dashIdThDecGraph.AddDecals = dr["AddDecals"].ToString();

            }

            dashModel.dashIdThDecGraph = dashIdThDecGraph;
            return dashModel;
        }


    }
    public class AutocompleteSuggestions
    {
        public string SearchFor { get; set; }
        public string DValue { get; set; }
        public string DText { get; set; }
        public List<AutocompleteSuggestions> GetMainSearchList(string term)
        {
            try
            {
                string[] SFST = term.Split(':');
                string searchFor = "";
                string searchText = "";

                if (SFST.Length > 1)
                {
                    searchFor = SFST[0];
                    for (int i = 1; i < SFST.Length; i++)
                    {
                        searchText += SFST[i];
                    }
                }
                else
                {
                    searchFor = "";
                    searchText = SFST[0];
                }
                List<AutocompleteSuggestions> SearchList = new List<AutocompleteSuggestions>();
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    try
                    {
                        db.Database.Connection.Open();
                        cmd.CommandText = "usp_GetMainSearch";
                        cmd.CommandType = CommandType.StoredProcedure;

                        DbParameter paramSF = cmd.CreateParameter();
                        paramSF.ParameterName = "SearchFor";
                        paramSF.Value = searchFor;
                        cmd.Parameters.Add(paramSF);

                        DbParameter paramST = cmd.CreateParameter();
                        paramST.ParameterName = "SearchText";
                        paramST.Value = searchText;
                        cmd.Parameters.Add(paramST);

                        DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dtTable);
                        db.Database.Connection.Close();
                    }
                    catch
                    {
                        db.Database.Connection.Close();
                    }

                    foreach (DataRow dtRow in dtTable.Rows)
                    {
                        SearchList.Add(new AutocompleteSuggestions()
                        {
                            SearchFor = dtRow["SearchFor"].ToString(),
                            DValue = dtRow["DValue"].ToString(),
                            DText = dtRow["DText"].ToString(),
                        });
                    }
                }
                db.Dispose();
                return SearchList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    public class DashboardOrderGraph
    {
        public string Labels { get; set; }
        public string TotalOpen { get; set; }
        public string TotalConverted { get; set; }
        public string TotalNotConverted { get; set; }
        public string TotalPercentage { get; set; }
    }
    public class DashboardSalesGraph
    {
        public string Labels { get; set; }
        public string TotalAmt { get; set; }
        public string TotalRefund { get; set; }
        public string TotalVoided { get; set; }
        public string TotalDeclined { get; set; }
    }
    public class DashboardAOGraph
    {
        public string Labels { get; set; }
        public string AgentName { get; set; }
        public string ATotalOpen { get; set; }
        public string ATotalConverted { get; set; }
        public string IdTheft { get; set; }
        public string AddDecals { get; set; }
        public string Renewal { get; set; }
        public string AgentPercentage { get; set; }
    }
    public class DashboardIdThDecGraph
    {
        public string Labels { get; set; }
        public string IdTheft { get; set; }
        public string AddDecals { get; set; }


    }
}