using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MalaGroupERP.Data;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using System.Data.Common;


namespace MalaGroupERP.Models
{
    public class NavigationMenuModel
    {
        public int UserID { get; set; }
        public string NavigationMenu { get; set; }
        public void GetMenuList()
        {
            CurrentUser User = (HttpContext.Current.Session["CurrentUser"] == null) ? null : (CurrentUser)(HttpContext.Current.Session["CurrentUser"]);

            if (User == null)
            {
                this.UserID = 0;
            }
            else
            {
                this.UserID = User.UserID;
            }
            DataTable dtMenu = GetMenuByUserID(this.UserID);
            AddTopMenuItems(dtMenu);
        }
        private void AddTopMenuItems(DataTable menuData)
        {
            DataView view = null;
            try
            {
                HtmlGenericControl ulMenu = new HtmlGenericControl("ul");
                ulMenu.ID = "side-menu-ul";

                HtmlGenericControl hgcLIHomeSMC = new HtmlGenericControl("li");
                hgcLIHomeSMC.Attributes.Add("class", "active");
                hgcLIHomeSMC.InnerHtml = "<a href='/Home/Index'><i class='icon icon-home'></i><span >Dashboard</span></a>";
                ulMenu.Controls.Add(hgcLIHomeSMC);

                view = new DataView(menuData);

                view.RowFilter = "ParentID IS NULL";
                foreach (DataRowView row in view)
                {
                    string strController = row["Controller"].ToString();
                    string strAction = "";

                    if (strController.Contains("_"))
                    {
                        string[] strArray = strController.Split('_');
                        strController = strArray[0];
                        strAction = row["Action"].ToString();
                    }
                    HtmlGenericControl hgcLIL = new HtmlGenericControl("li");
                    hgcLIL.Attributes.Add("class", "site-menu-item has-sub");
                    if (row["ClickEvent"].ToString() == "")
                    {
                        hgcLIL.InnerHtml = "<a href='" + (strController + "" != "#" ? "/" + strController + (strAction != "" ? "/" + strAction : "") : "javascript:void(0)") + "'><i class='icon " + row["Icon"].ToString() + "'></i><span >" + row["Resource"].ToString() + "</span></a>";
                    }
                    else
                    {
                        hgcLIL.InnerHtml = "<a href='#' " + row["ClickEvent"].ToString() + "><i class='icon " + row["Icon"].ToString() + "'></i><span >" + row["Resource"].ToString() + "</span></a>";
                    }
                    hgcLIL.InnerHtml = "<a href='" + (strController + "" != "#" ? "/" + strController + (strAction != "" ? "/" + strAction : "") : (row["ClickEvent"].ToString() != "" ? "" : "javascript:void(0)")) + "' " + row["ClickEvent"].ToString() + "><i class='icon " + row["Icon"].ToString() + "'></i><span >" + row["Resource"].ToString() + "</span></a>";

                    AddChildMenuItems(menuData, hgcLIL, row["ResourceId"].ToString(), 2);
                    ulMenu.Controls.Add(hgcLIL);
                }
                StringBuilder sb = new StringBuilder();
                StringWriter tw = new StringWriter(sb);
                HtmlTextWriter hw = new HtmlTextWriter(tw);
                ulMenu.RenderControl(hw);
                var html = sb.ToString();
                this.NavigationMenu = html;
            }
            catch
            {
                HtmlGenericControl ulMenu = new HtmlGenericControl("ul");
                ulMenu.ID = "side-menu-ul";
                HtmlGenericControl hgcLIHomeSMC = new HtmlGenericControl("li");
                hgcLIHomeSMC.Attributes.Add("class", "active");
                hgcLIHomeSMC.InnerHtml = "<a href='~/Home/Index'><i class='icon icon-home'></i><span >Dashboard</span></a>";
                ulMenu.Controls.Add(hgcLIHomeSMC);
                StringBuilder sb = new StringBuilder();
                StringWriter tw = new StringWriter(sb);
                HtmlTextWriter hw = new HtmlTextWriter(tw);
                ulMenu.RenderControl(hw);
                var html = sb.ToString();
                this.NavigationMenu = html;
            }
            finally
            {
                view = null;
            }
        }
        private void AddChildMenuItems(DataTable menuData, HtmlGenericControl hgcParent, string ParentID, int Level)
        {
            DataView view = null;
            try
            {
                view = new DataView(menuData);
                view.RowFilter = "ParentID=" + ParentID;
                if (view.Count > 0)
                {
                    HtmlGenericControl hgcUL = new HtmlGenericControl("ul");
                    foreach (DataRowView row in view)
                    {
                        HtmlGenericControl hgcLI = new HtmlGenericControl("li");
                        if (row["Controller"].ToString() + "" != "#")
                        {
                            string strController = row["Controller"].ToString();
                            string strAction = "";
                            if (strController.Contains("_"))
                            {
                                string[] strArray = strController.Split('_');
                                strController = strArray[0];
                                strAction = row["Action"].ToString();
                            }
                            Level = 2;
                            if (row["ClickEvent"].ToString() == "")
                            {
                                hgcLI.InnerHtml = "<a href='/" + strController + (strAction != "" ? "/" + strAction : "") + "'>" + row["Resource"].ToString() + "</a>";
                            }
                            else
                            {
                                hgcLI.InnerHtml = "<a href='javascript:void(0)' " + row["ClickEvent"].ToString() + ">" + row["Resource"].ToString() + "</a>";
                            }

                        }
                        else
                        {
                            Level += 1;
                            hgcLI.InnerHtml = "<a href='javascript:void(0)' data-dropdown-toggle='false'><span class='site-menu-title'>" + row["Resource"].ToString() + "</span><span class='site-menu-arrow'></span></a>";
                        }
                        hgcUL.Controls.Add(hgcLI);
                        hgcParent.Controls.Add(hgcUL);
                        AddChildMenuItems(menuData, hgcLI, row["ResourceId"].ToString(), Level);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                view = null;
            }
        }
        public DataTable GetMenuByUserID(int UserID)
        {
            DataTable dtTable = new DataTable();
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            using (var cmd = db.Database.Connection.CreateCommand())
            {
                try
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetMenuByUserID";
                    cmd.CommandType = CommandType.StoredProcedure;
                    DbParameter paramUserID = cmd.CreateParameter();
                    paramUserID.ParameterName = "UserID";
                    paramUserID.Value = UserID;
                    cmd.Parameters.Add(paramUserID);


                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                catch
                {
                    db.Database.Connection.Close();
                }
            }
            db.Dispose();
            return dtTable;
        }
    }
}