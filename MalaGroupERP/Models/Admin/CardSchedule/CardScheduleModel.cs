using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using MalaGroupERP.Data;
using OfficeOpenXml;
using System.Text;
using System.Threading;
using System.Globalization;
using System.Data.Common;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers.Bases;


namespace MalaGroupERP.Models
{
    public class CardScheduleModel
    {
        public long ID { get; set; }
        public string OpportunityID { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentStartDate { get; set; }
        public string InvoiceNumber { get; set; }
        public string ChargeAmountAfterFirstPayment { get; set; }
        public string TransactionTotal { get; set; }
        public string PaymentMethod { get; set; }
        public string CardNumber { get; set; }
        public string CardExpirationMonth { get; set; }
        public string CardExpirationYear { get; set; }
        public string TransactionCount { get; set; }
        public string PaymentCount { get; set; }
        public string ChargeDate { get; set; }
        public string PasswordId { get; set; }
        public string PinNumber { get; set; }
        public string BillingFirstName { get; set; }
        public string BillingLastName { get; set; }
        public string BillingAddress { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingZipPostal { get; set; }
        public string BillingCountry { get; set; }
        public string PrimaryPhone { get; set; }
        public string PersonAccountEmail { get; set; }
        public string PersonAccountFirstName { get; set; }
        public string PersonAccountLastName { get; set; }
        public string ShippingStreet { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingStateProvince { get; set; }
        public string ShippingZipPostalCode { get; set; }
        public string ShippingCountry { get; set; }
        public string VehicleYear { get; set; }
        public string VehicleMake { get; set; }
        public string ChargeAmount { get; set; }
        public string TransactionID { get; set; }
        public string TransactionStatus { get; set; }
        public string TransactionMessage { get; set; }
        public string TransactionDate { get; set; }
        public string AuthCode { get; set; }
        public Nullable<int> IsSend { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedById { get; set; }
        public string ImportCardSchedule(HttpPostedFileBase fb)
        {
            string msg = "";
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            Stream inputStream = fb.InputStream;
            Stream fs = fb.InputStream;
            BinaryReader br = new BinaryReader(fs);
            byte[] bytes = br.ReadBytes((Int32)fs.Length);
            DataTable dtTable = new DataTable();
            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                using (ExcelPackage expkg = new ExcelPackage(memoryStream))
                {
                    dtTable = ExcelPackageToDataTable(expkg);
                }
            }

            foreach (DataRow dr in dtTable.Rows)
            {
                var cs = new tbl_ABSCreditCrad()
                {
                    OpportunityID = dr[0].ToString(),
                    PaymentStatus = dr[1].ToString(),
                    PaymentStartDate = dr[2].ToString(),
                    InvoiceNumber = dr[3].ToString(),
                    ChargeAmountAfterFirstPayment = dr[4].ToString(),
                    TransactionTotal = dr[5].ToString(),
                    PaymentMethod = dr[6].ToString(),
                    CardNumber = dr[7].ToString(),
                    CardExpirationMonth = dr[8].ToString(),
                    CardExpirationYear = dr[9].ToString(),
                    TransactionCount = dr[10].ToString(),
                    PaymentCount = dr[11].ToString(),
                    ChargeDate = dr[12].ToString(),
                    PasswordId = dr[13].ToString(),
                    PinNumber = dr[14].ToString(),
                    BillingFirstName = dr[15].ToString(),
                    BillingLastName = dr[16].ToString(),
                    BillingAddress = dr[17].ToString(),
                    BillingCity = dr[18].ToString(),
                    BillingState = dr[19].ToString(),
                    BillingZipPostal = dr[20].ToString(),
                    BillingCountry = dr[21].ToString(),
                    PrimaryPhone = dr[22].ToString(),
                    PersonAccountEmail = dr[23].ToString(),
                    PersonAccountFirstName = dr[24].ToString(),
                    PersonAccountLastName = dr[25].ToString(),
                    ShippingStreet = dr[26].ToString(),
                    ShippingCity = dr[27].ToString(),
                    ShippingStateProvince = dr[28].ToString(),
                    ShippingZipPostalCode = dr[29].ToString(),
                    ShippingCountry = dr[30].ToString(),
                    VehicleYear = dr[31].ToString(),
                    VehicleMake = dr[32].ToString(),
                    ChargeAmount = dr[33].ToString(),
                    IsSend = 0,
                    CreatedById = MalaGroupWebSession.CurrentUser.UserID,
                    CreatedDate = DateTime.Now
                };
                db.tbl_ABSCreditCrad.Add(cs);
                db.SaveChanges();
            }

            return msg;
        }
        public static DataTable ExcelPackageToDataTable(ExcelPackage excelPackage)
        {
            DataTable dt = new DataTable();
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[1];
            if (worksheet.Dimension == null)
            {
                return dt;
            }
            List<string> columnNames = new List<string>();
            int currentColumn = 1;
            foreach (var cell in worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column])
            {
                string columnName = cell.Text.Trim();
                if (cell.Start.Column != currentColumn)
                {
                    columnNames.Add("Header_" + currentColumn);
                    dt.Columns.Add("Header_" + currentColumn);
                    currentColumn++;
                }
                columnNames.Add(columnName);
                int occurrences = columnNames.Count(x => x.Equals(columnName));
                if (occurrences > 1)
                {
                    columnName = columnName + "_" + occurrences;
                }
                dt.Columns.Add(columnName);
                currentColumn++;
            }
            for (int i = 2; i <= worksheet.Dimension.End.Row; i++)
            {
                var row = worksheet.Cells[i, 1, i, worksheet.Dimension.End.Column];
                DataRow newRow = dt.NewRow();
                foreach (var cell in row)
                {
                    newRow[cell.Start.Column - 1] = cell.Text;
                }
                dt.Rows.Add(newRow);
            }
            return dt;
        }
        public List<CardScheduleModel> GetCaredScheduleDetails(string ChargeDate)
        {
            try
            {
                List<CardScheduleModel> model = new List<CardScheduleModel>();
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    try
                    {
                        db.Database.Connection.Open();
                        cmd.CommandText = "usp_GetCaredScheduleDetails";
                        cmd.CommandType = CommandType.StoredProcedure;

                        DbParameter paramCD = cmd.CreateParameter();
                        paramCD.ParameterName = "ChargeDate";
                        paramCD.Value = ChargeDate;
                        cmd.Parameters.Add(paramCD);

                        DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dtTable);
                        db.Database.Connection.Close();

                        foreach (DataRow dr in dtTable.Rows)
                        {
                            model.Add(new CardScheduleModel()
                            {
                                ID = Convert.ToInt64(dr["ID"].ToString()),
                                OpportunityID = dr["OpportunityID"].ToString(),
                                InvoiceNumber = dr["InvoiceNumber"].ToString(),
                                PaymentMethod = dr["PaymentMethod"].ToString(),
                                CardNumber = dr["CardNumber"].ToString(),
                                CardExpirationMonth = dr["CardExpirationMonth"].ToString(),
                                CardExpirationYear = dr["CardExpirationYear"].ToString(),
                                PaymentStatus = dr["PaymentStatus"].ToString(),
                                ChargeDate = dr["ChargeDate"].ToString(),
                                PinNumber = dr["PinNumber"].ToString(),
                                BillingFirstName = dr["BillingFirstName"].ToString(),
                                BillingLastName = dr["BillingLastName"].ToString(),
                                ChargeAmount = dr["ChargeAmount"].ToString(),
                            });
                        }
                    }

                    catch
                    {
                        db.Database.Connection.Close();
                    }
                }

                db.Dispose();
                return model.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string ChargeCreditCards(string CardIDs)
        {
            string result = "";
            string error = "";
            try
            {
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                MalaGroupERPEntities dbUT = new MalaGroupERPEntities();
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    try
                    {
                        db.Database.Connection.Open();
                        cmd.CommandText = "usp_GetCredsInfoForARB";
                        cmd.CommandType = CommandType.StoredProcedure;

                        DbParameter paramCD = cmd.CreateParameter();
                        paramCD.ParameterName = "CardIDs";
                        paramCD.Value = CardIDs;
                        cmd.Parameters.Add(paramCD);

                        DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dtTable);
                        db.Database.Connection.Close();
                        db.Dispose();
                    }
                    catch
                    {
                        db.Database.Connection.Close();
                    }

                }
                try
                {
                    if (dtTable != null && dtTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtTable.Rows)
                        {
                            var cardInfo = (new CardScheduleModel()
                            {
                                OpportunityID = dr["OpportunityID"].ToString(),
                                InvoiceNumber = dr["InvoiceNumber"].ToString(),
                                PaymentMethod = dr["PaymentMethod"].ToString(),
                                CardNumber = dr["CardNumber"].ToString(),
                                CardExpirationMonth = (dr["CardExpirationMonth"].ToString().Length != 2 ? "0" + dr["CardExpirationMonth"].ToString() : dr["CardExpirationMonth"].ToString()),
                                CardExpirationYear = dr["CardExpirationYear"].ToString(),
                                PinNumber = dr["PinNumber"].ToString(),
                                BillingFirstName = dr["BillingFirstName"].ToString(),
                                BillingLastName = dr["BillingLastName"].ToString(),
                                ChargeAmount = dr["ChargeAmount"].ToString(),
                                BillingAddress = dr["BillingAddress"].ToString(),
                                BillingCity = dr["BillingCity"].ToString(),
                                BillingState = dr["BillingState"].ToString(),
                                BillingZipPostal = dr["BillingZipPostal"].ToString(),
                                BillingCountry = dr["BillingCountry"].ToString(),
                                PrimaryPhone = dr["PrimaryPhone"].ToString(),
                                PersonAccountEmail = dr["PersonAccountEmail"].ToString(),
                                PersonAccountFirstName = dr["PersonAccountFirstName"].ToString(),
                                PersonAccountLastName = dr["PersonAccountLastName"].ToString(),
                                ShippingStreet = dr["ShippingStreet"].ToString(),
                                ShippingCity = dr["ShippingCity"].ToString(),
                                ShippingStateProvince = dr["ShippingStateProvince"].ToString(),
                                ShippingZipPostalCode = dr["ShippingZipPostalCode"].ToString(),
                                ShippingCountry = dr["ShippingCountry"].ToString(),
                            });
                            long arbid = Convert.ToInt64(dr["ID"].ToString());
                            var transUpdate = dbUT.tbl_ABSCreditCrad.Where(p => p.ID == arbid).FirstOrDefault();
                            createTransactionResponse response = new PaymentTransactionModel().ChargeCreditCardARB(cardInfo);
                            if (response.messages.resultCode == messageTypeEnum.Ok)
                            {
                                if (response.transactionResponse.messages != null)
                                {
                                    result += response.transactionResponse.accountNumber + "|" + response.transactionResponse.accountType + "|" + DateTime.Now.ToString("MM/dd/yyyy") + "|Approved|" + response.transactionResponse.messages[0].description + "|" + response.transactionResponse.transId + "|" + response.transactionResponse.authCode + "<br/>";
                                    transUpdate.AuthCode = response.transactionResponse.authCode;
                                    transUpdate.TransactionID = response.transactionResponse.transId;
                                    transUpdate.TransactionStatus = "Approved";
                                    transUpdate.TransactionMessage = response.transactionResponse.accountNumber + "|" + response.transactionResponse.accountType + "|" + DateTime.Now.ToString("MM/dd/yyyy") + "|Approved|" + response.transactionResponse.messages[0].description + "|" + response.transactionResponse.transId;
                                    transUpdate.AuthCode = response.transactionResponse.authCode;
                                    transUpdate.TransactionDate = DateTime.Now.ToString();
                                    transUpdate.IsSend = 1;
                                    transUpdate.ModifiedDate = DateTime.Now;
                                    transUpdate.ModifiedById = MalaGroupWebSession.CurrentUser.UserID;
                                }
                                else
                                {
                                    result += response.transactionResponse.accountNumber + "|" + response.transactionResponse.accountType + "|" + DateTime.Now.ToString("MM/dd/yyyy") + "|Declined|" + response.transactionResponse.errors[0].errorText + "|" + response.transactionResponse.transId + "|" + response.transactionResponse.authCode + "<br/>";
                                    transUpdate.AuthCode = response.transactionResponse.authCode;
                                    transUpdate.TransactionID = response.transactionResponse.transId;
                                    transUpdate.TransactionStatus = "Declined";
                                    transUpdate.TransactionMessage = response.transactionResponse.accountNumber + "|" + response.transactionResponse.accountType + "|" + DateTime.Now.ToString("MM/dd/yyyy") + "|Declined|" + response.transactionResponse.errors[0].errorText + "|" + response.transactionResponse.transId;
                                    transUpdate.AuthCode = response.transactionResponse.authCode;
                                    transUpdate.TransactionDate = DateTime.Now.ToString();
                                    transUpdate.IsSend = 1;
                                    transUpdate.ModifiedDate = DateTime.Now;
                                    transUpdate.ModifiedById = MalaGroupWebSession.CurrentUser.UserID;
                                }
                            }
                            else
                            {
                                transUpdate.AuthCode = "";
                                transUpdate.TransactionID = "";
                                transUpdate.TransactionStatus = "Failed";
                                transUpdate.TransactionMessage = response.transactionResponse.accountNumber + "|" + response.transactionResponse.accountType + "|" + DateTime.Now.ToString("MM/dd/yyyy") + "|Failed|" + response.transactionResponse.errors[0].errorText + "|";
                                transUpdate.AuthCode = "";
                                transUpdate.TransactionDate = DateTime.Now.ToString();
                                transUpdate.IsSend = 1;
                                transUpdate.ModifiedDate = DateTime.Now;
                                transUpdate.ModifiedById = MalaGroupWebSession.CurrentUser.UserID;
                                result += response.transactionResponse.accountNumber + "|" + response.transactionResponse.accountType + "|" + DateTime.Now.ToString("MM/dd/yyyy") + "|Failed|" + response.transactionResponse.errors[0].errorText + "|" + response.transactionResponse.transId + "<br/>";
                            }
                            dbUT.SaveChanges();
                        }
                        dbUT.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    error += ex.Message + " Inner : " + ex.InnerException.Message;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ChargeScheduleCards()
        {
            string result = "";
            try
            {
                MalaGroupERPEntities db = new MalaGroupERPEntities();

                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    try
                    {
                        db.Database.Connection.Open();
                        cmd.CommandText = "usp_GetSchedulePayments";
                        cmd.CommandType = CommandType.StoredProcedure;

                        DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dtTable);
                        db.Database.Connection.Close();
                        db.Dispose();
                    }
                    catch
                    {
                        db.Database.Connection.Close();
                    }
                }
                try
                {
                    if (dtTable != null && dtTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtTable.Rows)
                        {
                            try
                            {
                                result += ChargePayment(dr);
                            }
                            catch (Exception ex)
                            {
                                new CommonModel().Log("Date : " + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + "\r\n==============================\r\nERROR : \r\n Message : " + ex.Message + "\r\n Inner Message : " + ex.InnerException.Message + "\r\n Stack Trace : " + ex.InnerException.StackTrace + "\r\n==============================\r\n");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    new CommonModel().Log("Date : " + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + "\r\n==============================\r\nERROR : \r\n Message : " + ex.Message + "\r\n Inner Message : " + ex.InnerException.Message + "\r\n Stack Trace : " + ex.InnerException.StackTrace + "\r\n==============================\r\n");
                }
                new CommonModel().Log("Date : " + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + "\r\n==============================\r\nRESULT : \r\n" + result + "\r\n==============================\r\n");
            }
            catch (Exception ex)
            {
                new CommonModel().Log("Date : " + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + "\r\n==============================\r\nERROR : \r\n Message : " + ex.Message + "\r\n Inner Message : " + ex.InnerException.Message + "\r\n Stack Trace : " + ex.InnerException.StackTrace + "\r\n==============================\r\n");
            }
        }
        public void ChargeScheduleCardsWithDate(string ScheduleDate)
        {
            string result = "";
            try
            {
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    try
                    {
                        db.Database.Connection.Open();
                        cmd.CommandText = "usp_GetSchedulePaymentsWithDate";
                        cmd.CommandType = CommandType.StoredProcedure;

                        DbParameter SDate = cmd.CreateParameter();
                        SDate.ParameterName = "ScheduleDate";
                        SDate.Value = ScheduleDate;
                        cmd.Parameters.Add(SDate);


                        DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dtTable);
                        db.Database.Connection.Close();
                        db.Dispose();
                    }
                    catch
                    {
                        db.Database.Connection.Close();
                    }
                }
                try
                {
                    if (dtTable != null && dtTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtTable.Rows)
                        {
                            try
                            {
                                result += ChargePayment(dr);
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    new CommonModel().Log("Date : " + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + "\r\n==============================\r\nERROR : \r\n" + ex.StackTrace + "\r\n==============================\r\n");
                }
                new CommonModel().Log("Date : " + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + "\r\n==============================\r\nRESULT : \r\n" + result + "\r\n==============================\r\n");
            }
            catch (Exception ex)
            {
                new CommonModel().Log("Date : " + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + "\r\n==============================\r\nERROR : \r\n" + ex.StackTrace + "\r\n==============================\r\n");
                throw ex;
            }
        }
        public string ChargePayment(DataRow dr)
        {
            MalaGroupERPEntities dbUT = new MalaGroupERPEntities();
            string filePath = HttpContext.Current.Server.MapPath("~/EmailTemplate");
            var bodySend = File.ReadAllText(filePath + "\\transaction_success.html");
            var body = "";
            string emailSubject = "";
            string result = "";

            var cardInfo = (new CardScheduleModel()
            {
                OpportunityID = "",
                InvoiceNumber = dr["InvoiceNumber"].ToString(),
                PaymentMethod = "card",
                CardNumber = dr["CardNumber"].ToString(),
                CardExpirationMonth = (dr["CardExpirationMonth"].ToString().Length != 2 ? "0" + dr["CardExpirationMonth"].ToString() : dr["CardExpirationMonth"].ToString()),
                CardExpirationYear = dr["CardExpirationYear"].ToString(),
                ChargeAmount = dr["ChargeAmt"].ToString(),
                PinNumber = dr["PinNo"].ToString(),
                BillingFirstName = dr["BillingFirstName"].ToString(),
                BillingLastName = dr["BillingLastName"].ToString(),
                BillingAddress = dr["BillingAddress"].ToString(),
                BillingCity = dr["BillingCity"].ToString(),
                BillingState = dr["BillingState"].ToString(),
                BillingZipPostal = dr["BillingZipPostal"].ToString(),
                BillingCountry = "USA",
                PrimaryPhone = dr["PrimaryPhone"].ToString(),
                PersonAccountEmail = dr["LeadEmail"].ToString(),
                PersonAccountFirstName = dr["PersonAccountFirstName"].ToString(),
                PersonAccountLastName = dr["PersonAccountLastName"].ToString(),
                ShippingStreet = dr["ShippingStreet"].ToString(),
                ShippingCity = dr["ShippingCity"].ToString(),
                ShippingStateProvince = dr["ShippingStateProvince"].ToString(),
                ShippingZipPostalCode = dr["ShippingZipPostalCode"].ToString(),
                ShippingCountry = "USA",
                VehicleMake = dr["VehicleMake"].ToString(),
                VehicleYear = dr["VehicleYear"].ToString(),
            });
            long arbid = Convert.ToInt64(dr["CDID"].ToString());
            long aoid = Convert.ToInt64(dr["InvoiceNumber"].ToString());
            long accountID = Convert.ToInt64(dr["AccountID"].ToString());
            var transUpdate = dbUT.tbl_OrderTransactions.Where(p => p.CDID == arbid).FirstOrDefault();
            var accountInfo = dbUT.tbl_Accounts.Where(p => p.ID == accountID).FirstOrDefault();

            int statusCode = 0;
            string statusText = "";
            string reasonText = "";
            string gatewayResponse = "";

            createTransactionResponse response = new PaymentTransactionModel().ChargeCreditCardARB(cardInfo);
            if (response != null)
            {
                cardInfo.TransactionID = response.transactionResponse.transId;
                if (response.transactionResponse.messages != null)
                {
                    if (response.transactionResponse.messages[0].code == "1")
                    {
                        statusCode = 1;
                    }
                    else
                    {
                        statusCode = 2;
                    }
                    statusText = response.transactionResponse.messages[0].code == "1" ? "Approved" : "Declined";
                    reasonText = response.transactionResponse.messages[0].description;
                    gatewayResponse = DateTime.Now.ToString("MM/dd/yyyy") + "|" + statusText + "|" + reasonText + "|" + response.transactionResponse.transId;
                }
                else if (response.transactionResponse.errors != null)
                {
                    if (response.transactionResponse.errors[0].errorCode == "1")
                    {
                        statusCode = 1;
                    }
                    else
                    {
                        statusCode = 2;
                    }
                    statusText = response.transactionResponse.errors[0].errorCode == "1" ? "Approved" : "Declined";
                    reasonText = response.transactionResponse.errors[0].errorText;
                    gatewayResponse = DateTime.Now.ToString("MM/dd/yyyy") + "|" + statusText + "|" + reasonText + "|" + response.transactionResponse.transId;
                }
                else
                {
                    statusCode = -1;
                    statusText = "Unknown";
                    reasonText = "The transaction has been failed due to unknown reason.";
                    gatewayResponse = DateTime.Now.ToString("MM/dd/yyyy") + "|" + statusText + "|" + reasonText + "|0";

                }

                result += gatewayResponse + "\r\n";
                transUpdate.ChargeDate = DateTime.Now;
                transUpdate.GatwayResponse = gatewayResponse;
                transUpdate.AuthCode = response.transactionResponse.authCode;
                transUpdate.TransactionID = response.transactionResponse.transId;
                transUpdate.TransHashCode = response.transactionResponse.transHash;
                transUpdate.CardCheckNumber = response.transactionResponse.accountNumber;
                transUpdate.PaymentMethod = 1;
                transUpdate.CardType = response.transactionResponse.accountType;
                transUpdate.Status = statusCode;
                transUpdate.StatusText = statusText;
                transUpdate.ResonText = reasonText;
                transUpdate.TransType = 1;
                dbUT.SaveChanges();

                if (statusCode == 1 && statusText == "Approved")
                {
                    //if (string.IsNullOrWhiteSpace(cardInfo.PersonAccountEmail) == false)
                    //{
                    //    var transOtherCharge = dbUT.tbl_OrderTransactions.Where(p => p.AOID == aoid && p.TransactionID == null).ToList();

                    //    decimal todaysCharge = 0;
                    //    decimal balanceCharge = 0;

                    //    todaysCharge = Convert.ToDecimal(cardInfo.ChargeAmount);
                    //    foreach (var toc in transOtherCharge)
                    //    {
                    //        balanceCharge += toc.ChargeAmt.Value;
                    //        toc.PaymentMethod = 1;
                    //        dbUT.SaveChanges();
                    //    }
                    //    body = bodySend;

                    //    body = body.Replace("{HeaderParagraph}", "Our customers are important to us. Your vehicle is now registered into the National Theft Search and Recovery database. This includes Vehicle Plate Recognition for every vehicle located at your address. In the event of a Theft, we will send out a search alert for Anti-Theft tracking services.");
                    //    body = body.Replace("{AccountFullName}", cardInfo.BillingFirstName + " " + cardInfo.BillingLastName);
                    //    body = body.Replace("{BillingAddress}", cardInfo.BillingAddress);
                    //    body = body.Replace("{BillingCity}", cardInfo.BillingCity);
                    //    body = body.Replace("{BillingState}", cardInfo.BillingState);
                    //    body = body.Replace("{BillingZip}", cardInfo.BillingZipPostal);
                    //    body = body.Replace("{CustomerEmail}", cardInfo.PersonAccountEmail);
                    //    body = body.Replace("{PrimaryPhone}", cardInfo.PrimaryPhone);

                    //    body = body.Replace("{ShippingAddress}", cardInfo.ShippingStreet);
                    //    body = body.Replace("{ShippingCity}", cardInfo.ShippingCity);
                    //    body = body.Replace("{ShippingState}", cardInfo.ShippingStateProvince);
                    //    body = body.Replace("{ShippingZip}", cardInfo.ShippingZipPostalCode);

                    //    body = body.Replace("{TodaysChargeAmount}", todaysCharge.ToString("0.00"));
                    //    body = body.Replace("{BalanceDue}", balanceCharge.ToString("0.00"));
                    //    body = body.Replace("{ChargeDateTime}", DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt"));

                    //    body = body.Replace("{TransactionID}", response.transactionResponse.transId);
                    //    body = body.Replace("{CardLastDigit}", response.transactionResponse.accountNumber);
                    //    body = body.Replace("{TransactionType}", "Charge");
                    //    body = body.Replace("{TransactionTypeHeader}", "CHARGE");
                    //    body = body.Replace("{AuthCode}", response.transactionResponse.authCode);
                    //    //emailSubject = "NTSR - Urgent Please Contact Us";
                    //    emailSubject = "Transaction CHARGE from National Theft Search and Recovery for " + cardInfo.ChargeAmount + " (USD)";
                    //}
                    //else
                    //{
                    //    body = "";                    
                    //}

                    //if (body != "")
                    //{
                    //    new CommonModel().SendEmailPayments(cardInfo.PersonAccountEmail, emailSubject, body, accountID);
                    //    body = "";
                    //}
                    //accountInfo.AccountStatus = 1;
                    //dbUT.SaveChanges();
                }
                else
                {
                    int transCount = dbUT.tbl_OrderTransactions.Where(p => p.AOID == aoid).Count();
                    var transChargeDate = dbUT.tbl_OrderTransactions.Where(p => p.AOID == aoid && p.TransactionID == null && p.ChargeNo == transCount).FirstOrDefault();
                    transCount = transCount + 1;
                    DateTime dtChargeDate = DateTime.Now;
                    if (transChargeDate != null)
                    {
                        dtChargeDate = transChargeDate.ChargeDate.Value;
                    }
                    dtChargeDate = dtChargeDate.AddMonths(1);
                    var transSave = new tbl_OrderTransactions()
                    {
                        PaymentMethod = 1,
                        Status = 0,
                        ChargeAmt = Convert.ToDecimal(cardInfo.ChargeAmount),
                        AOID = aoid,
                        ChargeDate = dtChargeDate,
                        ChargeNo = transCount
                    };
                    dbUT.tbl_OrderTransactions.Add(transSave);
                    dbUT.SaveChanges();

                    //accountInfo.AccountStatus = 2;
                    //dbUT.SaveChanges();

                    //if (string.IsNullOrWhiteSpace(cardInfo.PersonAccountEmail) == false)
                    //{
                    //    var declinedTemplate = File.ReadAllText(filePath + "\\transaction_declined.html");
                    //    body = declinedTemplate;

                    //    body = body.Replace("{HeaderParagraph}", "Our customers are important to us. Your vehicle is now registered into the National Theft Search and Recovery database. This includes Vehicle Plate Recognition for every vehicle located at your address. In the event of a Theft, we will send out a search alert for Anti-Theft tracking services.");
                    //    body = body.Replace("{Account_LastName}", cardInfo.BillingLastName);
                    //    body = body.Replace("{Account_FirstName}", cardInfo.BillingFirstName);
                    //    body = body.Replace("{Account_Pin_Number}", cardInfo.PinNumber);
                    //    body = body.Replace("{Account_Vehicle_Year}", cardInfo.VehicleYear);
                    //    body = body.Replace("{Account_Vehicle_Make}", cardInfo.VehicleMake);
                    //    body = body.Replace("{Account_Sale_Date}", accountInfo.SalesDate.Value.ToString("MM/dd/yyyy"));
                    //    body = body.Replace("{Account_LastModifiedDate}", DateTime.Now.ToString("MM/dd/yyyy"));
                    //    emailSubject = "NTSR - Urgent Please Contact Us";
                    //    //emailSubject = "Transaction DECLINED from National Theft Search and Recovery for " + cardInfo.ChargeAmount + " (USD)";
                    //}
                    //else
                    //{
                    //    body = "";
                    //}
                    //if (body != "")
                    //{
                    //    //new CommonModel().SendEmailPayments(cardInfo.PersonAccountEmail, emailSubject, body, accountID);
                    //    body = "";
                    //}
                }
            }
            else
            {
                statusCode = -1;
                statusText = "Unknown";
                reasonText = "The transaction has been failed due to unknown reason.";
                gatewayResponse = DateTime.Now.ToString("MM/dd/yyyy") + "|" + statusText + "|" + reasonText + "|0";

                result += gatewayResponse + "\r\n";

                transUpdate.ChargeDate = DateTime.Now;
                transUpdate.GatwayResponse = gatewayResponse;
                transUpdate.AuthCode = "";
                transUpdate.TransactionID = "0";
                transUpdate.TransHashCode = "";
                transUpdate.CardCheckNumber = "";
                transUpdate.PaymentMethod = 1;
                transUpdate.CardType = "";
                transUpdate.Status = statusCode;
                transUpdate.StatusText = statusText;
                transUpdate.ResonText = reasonText;
                transUpdate.TransType = 1;

                int transCount = dbUT.tbl_OrderTransactions.Where(p => p.AOID == transUpdate.AOID).Count();
                var transChargeDate = dbUT.tbl_OrderTransactions.Where(p => p.AOID == transUpdate.AOID && p.TransactionID == null && p.ChargeNo == transCount).FirstOrDefault();
                transCount = transCount + 1;

                DateTime dtChargeDate = DateTime.Now;
                if (transChargeDate != null)
                {
                    dtChargeDate = transChargeDate.ChargeDate.Value;
                }
                dtChargeDate = dtChargeDate.AddMonths(1);

                var transSave = new tbl_OrderTransactions()
                {
                    PaymentMethod = 1,
                    Status = 0,
                    ChargeAmt = transUpdate.ChargeAmt.Value,
                    AOID = transUpdate.AOID,
                    ChargeDate = dtChargeDate,
                    ChargeNo = transCount
                };
                dbUT.tbl_OrderTransactions.Add(transSave);
                dbUT.SaveChanges();

                cardInfo.TransactionID = response.transactionResponse.transId;
                accountInfo.AccountStatus = 2;
                dbUT.SaveChanges();
            }
            dbUT.Dispose();
            return result;
        }
        public List<CardScheduleModel> GetCardScheduleDatewise(string ChargeDate, int PaymentStatus, ref string fname)
        {

            try
            {
                (new CommonModel()).DeleteFiles();
                string filePath = HttpContext.Current.Server.MapPath("~/TempFiles");
                string fileName = Guid.NewGuid().ToString() + ".xlsx";
                fname = fileName;

                List<CardScheduleModel> model = new List<CardScheduleModel>();
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    try
                    {
                        db.Database.Connection.Open();
                        cmd.CommandText = "usp_GetCardScheduleDatewise";
                        cmd.CommandType = CommandType.StoredProcedure;

                        DbParameter paramCD = cmd.CreateParameter();
                        paramCD.ParameterName = "ScheduleDate";
                        paramCD.Value = ChargeDate;
                        cmd.Parameters.Add(paramCD);

                        DbParameter paramPS = cmd.CreateParameter();
                        paramPS.ParameterName = "PaymentStatus";
                        paramPS.Value = PaymentStatus;
                        cmd.Parameters.Add(paramPS);

                        DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dtTable);
                        db.Database.Connection.Close();

                        foreach (DataRow dr in dtTable.Rows)
                        {
                            model.Add(new CardScheduleModel()
                            {
                                ID = Convert.ToInt64(dr["AccountID"].ToString()),
                                OpportunityID = dr["CDID"].ToString(),
                                InvoiceNumber = dr["InvoiceNumber"].ToString(),
                                //PaymentMethod = dr["PaymentMethod"].ToString(),
                                CardNumber = dr["CardNumber"].ToString(),
                                CardExpirationMonth = dr["CardExpirationMonth"].ToString(),
                                CardExpirationYear = dr["CardExpirationYear"].ToString(),
                                PaymentStatus = dr["PaymentStatus"].ToString(),
                                // ChargeDate = dr["ChargeDate"].ToString(),
                                PinNumber = dr["PinNo"].ToString(),
                                BillingFirstName = dr["BillingFirstName"].ToString(),
                                BillingLastName = dr["BillingLastName"].ToString(),
                                ChargeAmount = dr["ChargeAmt"].ToString(),
                                BillingAddress = dr["BillingAddress"].ToString(),
                                BillingCity = dr["BillingCity"].ToString(),
                                BillingState = dr["BillingState"].ToString(),
                                BillingZipPostal = dr["BillingZipPostal"].ToString(),
                                PersonAccountEmail = dr["LeadEmail"].ToString(),
                                PrimaryPhone = dr["PrimaryPhone"].ToString(),
                            });
                        }
                    }

                    catch
                    {
                        db.Database.Connection.Close();
                    }
                }

                db.Dispose();
                using (ExcelPackage excelPackage = new ExcelPackage())
                {
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");
                    worksheet.Cells["A1"].LoadFromDataTable(dtTable, true);
                    FileInfo fi = new FileInfo(filePath + "/" + fileName);
                    excelPackage.SaveAs(fi);
                }
                return model.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CardScheduleModel> GetToDaysSchedules(string ChargeDate, int PaymentStatus, ref string fname)
        {

            try
            {
                (new CommonModel()).DeleteFiles();
                string filePath = HttpContext.Current.Server.MapPath("~/TempFiles");
                string fileName = "PaymentSchedule_" + DateTime.Now.ToString("MMddyyyy") + ".xlsx";
                fname = fileName;

                FileInfo fiCheck = new FileInfo(filePath + "/" + fileName);
                if (fiCheck.Exists)
                {
                    fiCheck.Delete();
                }

                List<CardScheduleModel> model = new List<CardScheduleModel>();
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    try
                    {
                        db.Database.Connection.Open();
                        cmd.CommandText = "usp_GetCardScheduleDatewise";
                        cmd.CommandType = CommandType.StoredProcedure;

                        DbParameter paramCD = cmd.CreateParameter();
                        paramCD.ParameterName = "ScheduleDate";
                        paramCD.Value = ChargeDate;
                        cmd.Parameters.Add(paramCD);

                        DbParameter paramPS = cmd.CreateParameter();
                        paramPS.ParameterName = "PaymentStatus";
                        paramPS.Value = PaymentStatus;
                        cmd.Parameters.Add(paramPS);

                        DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dtTable);
                        db.Database.Connection.Close();

                        foreach (DataRow dr in dtTable.Rows)
                        {
                            model.Add(new CardScheduleModel()
                            {
                                ID = Convert.ToInt64(dr["AccountID"].ToString()),
                                OpportunityID = dr["CDID"].ToString(),
                                InvoiceNumber = dr["InvoiceNumber"].ToString(),
                                //PaymentMethod = dr["PaymentMethod"].ToString(),
                                CardNumber = dr["CardNumber"].ToString(),
                                CardExpirationMonth = dr["CardExpirationMonth"].ToString(),
                                CardExpirationYear = dr["CardExpirationYear"].ToString(),
                                // PaymentStatus = dr["PaymentStatus"].ToString(),
                                // ChargeDate = dr["ChargeDate"].ToString(),
                                PinNumber = dr["PinNo"].ToString(),
                                BillingFirstName = dr["BillingFirstName"].ToString(),
                                BillingLastName = dr["BillingLastName"].ToString(),
                                ChargeAmount = dr["ChargeAmt"].ToString(),
                                BillingAddress = dr["BillingAddress"].ToString(),
                                BillingCity = dr["BillingCity"].ToString(),
                                BillingState = dr["BillingState"].ToString(),
                                BillingZipPostal = dr["BillingZipPostal"].ToString(),
                                PersonAccountEmail = dr["LeadEmail"].ToString(),
                                PrimaryPhone = dr["PrimaryPhone"].ToString(),
                            });
                        }
                    }

                    catch
                    {
                        db.Database.Connection.Close();
                    }
                }

                db.Dispose();
                using (ExcelPackage excelPackage = new ExcelPackage())
                {
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Payment Schedule");
                    worksheet.Cells["A1"].LoadFromDataTable(dtTable, true);
                    FileInfo fi = new FileInfo(filePath + "/" + fileName);
                    excelPackage.SaveAs(fi);
                }
                return model.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CheckAuthorizeSattelment(DateTime ChargeDate, int SendApproved, int SendDecline)
        {
            string result = "";
            string filePath = HttpContext.Current.Server.MapPath("~/EmailTemplate");
            var approvedSend = File.ReadAllText(filePath + "\\transaction_success.html");
            var declinedSend = File.ReadAllText(filePath + "\\transaction_declined.html");
            var body = "";
            string emailSubject = "";
            try
            {
                MalaGroupERPEntities dbDI = new MalaGroupERPEntities();
                DataTable dtTable = new DataTable();
                using (var cmd = dbDI.Database.Connection.CreateCommand())
                {
                    try
                    {
                        dbDI.Database.Connection.Open();
                        cmd.CommandText = "usp_GetSchedulePaymentDetails";
                        cmd.CommandType = CommandType.StoredProcedure;

                        DbParameter paramCD = cmd.CreateParameter();
                        paramCD.ParameterName = "ChargeDate";
                        paramCD.Value = ChargeDate;
                        cmd.Parameters.Add(paramCD);

                        DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dtTable);
                        dbDI.Database.Connection.Close();
                        dbDI.Dispose();
                    }
                    catch
                    {
                        dbDI.Database.Connection.Close();
                    }
                }
                try
                {
                    DateTime firstDate = Convert.ToDateTime(ChargeDate.ToString("MM/dd/yyyy"));
                    DateTime lastDate = Convert.ToDateTime(ChargeDate.ToString("MM/dd/yyyy") + " 23:59:59");
                    getSettledBatchListResponse sbr = new TransactionReportingModel().GetSettledBatchList(firstDate, lastDate);
                    if (sbr.batchList != null)
                    {
                        foreach (var bi in sbr.batchList)
                        {
                            getTransactionListResponse tlr = new TransactionReportingModel().GetTransactionList(bi.batchId, 1000, 1);
                            if (tlr.transactions != null)
                            {
                                var tlrList = tlr.transactions.ToList().OrderBy(p => p.invoiceNumber);
                                var trlDGV = tlrList.Select(p => p.invoiceNumber).Distinct();

                                foreach (var tiD in trlDGV)
                                {
                                    var ti = tlrList.Where(p => p.invoiceNumber == tiD).ToList().OrderByDescending(p => p.transactionStatus).FirstOrDefault();

                                    try
                                    {
                                        DataRow[] drTrans = dtTable.Select("PinNo='" + ti.invoiceNumber + "' AND ChargeAmt=" + ti.settleAmount);
                                        if (drTrans.Length > 0)
                                        {
                                            MalaGroupERPEntities db = new MalaGroupERPEntities();

                                            long arbid = Convert.ToInt64(drTrans[0]["CDID"].ToString());
                                            long aoid = Convert.ToInt64(drTrans[0]["InvoiceNumber"].ToString());
                                            long accountID = Convert.ToInt64(drTrans[0]["AccountID"].ToString());
                                            var transUpdate = db.tbl_OrderTransactions.Where(p => p.CDID == arbid).FirstOrDefault();
                                            var accountInfo = db.tbl_Accounts.Where(p => p.ID == accountID).FirstOrDefault();
                                         
                                            if (ti.transactionStatus.ToLower() == "settledsuccessfully")
                                            {
                                                if (drTrans[0]["TransactionID"].ToString() != ti.transId)
                                                {
                                                    if (transUpdate != null)
                                                    {
                                                        transUpdate.TransactionID = ti.transId;
                                                        transUpdate.Status = 1;
                                                        transUpdate.StatusText = "Approved";
                                                        transUpdate.ResonText = "This transaction has been approved.";
                                                        transUpdate.GatwayResponse = ti.submitTimeLocal.ToString("MM/dd/yyyy") + "|Approved|This transaction has been approved.|" + ti.transId;
                                                        db.SaveChanges();
                                                    }
                                                    if (accountInfo != null)
                                                    {
                                                        if (accountInfo != null)
                                                        {
                                                            accountInfo.AccountStatus = 1;
                                                            db.SaveChanges();
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (accountInfo != null)
                                                    {
                                                        accountInfo.AccountStatus = 1;
                                                        db.SaveChanges();
                                                    }
                                                }

                                                if (string.IsNullOrWhiteSpace(drTrans[0]["LeadEmail"].ToString()) == false)
                                                {
                                                    var transOtherCharge = db.tbl_OrderTransactions.Where(p => p.AOID == aoid && p.TransactionID == null).ToList();

                                                    decimal todaysCharge = 0;
                                                    decimal balanceCharge = 0;

                                                    todaysCharge = Convert.ToDecimal(drTrans[0]["ChargeAmt"].ToString());
                                                    foreach (var toc in transOtherCharge)
                                                    {
                                                        balanceCharge += toc.ChargeAmt.Value;
                                                        toc.PaymentMethod = 1;
                                                        db.SaveChanges();
                                                    }
                                                    body = approvedSend;

                                                    body = body.Replace("{HeaderParagraph}", "Our customers are important to us. Your vehicle is now registered into the National Theft Search and Recovery database. This includes Vehicle Plate Recognition for every vehicle located at your address. In the event of a Theft, we will send out a search alert for Anti-Theft tracking services.");
                                                    body = body.Replace("{AccountFullName}", drTrans[0]["BillingFirstName"].ToString() + " " + drTrans[0]["BillingLastName"].ToString());
                                                    body = body.Replace("{BillingAddress}", drTrans[0]["BillingAddress"].ToString());
                                                    body = body.Replace("{BillingCity}", drTrans[0]["BillingCity"].ToString());
                                                    body = body.Replace("{BillingState}", drTrans[0]["BillingState"].ToString());
                                                    body = body.Replace("{BillingZip}", drTrans[0]["BillingZipPostal"].ToString());
                                                    body = body.Replace("{CustomerEmail}", drTrans[0]["LeadEmail"].ToString());
                                                    body = body.Replace("{PrimaryPhone}", drTrans[0]["PrimaryPhone"].ToString());

                                                    body = body.Replace("{ShippingAddress}", drTrans[0]["ShippingStreet"].ToString());
                                                    body = body.Replace("{ShippingCity}", drTrans[0]["ShippingCity"].ToString());
                                                    body = body.Replace("{ShippingState}", drTrans[0]["ShippingStateProvince"].ToString());
                                                    body = body.Replace("{ShippingZip}", drTrans[0]["ShippingZipPostalCode"].ToString());

                                                    body = body.Replace("{TodaysChargeAmount}", todaysCharge.ToString("0.00"));
                                                    body = body.Replace("{BalanceDue}", balanceCharge.ToString("0.00"));
                                                    body = body.Replace("{ChargeDateTime}", ti.submitTimeLocal.ToString("MM/dd/yyyy"));

                                                    body = body.Replace("{TransactionID}", ti.transId);
                                                    body = body.Replace("{CardLastDigit}", ti.accountNumber);
                                                    body = body.Replace("{TransactionType}", "Charge");
                                                    body = body.Replace("{TransactionTypeHeader}", "CHARGE");
                                                    body = body.Replace("{AuthCode}", drTrans[0]["AuthCode"].ToString());
                                                    //emailSubject = "NTSR - Urgent Please Contact Us";
                                                    emailSubject = "Transaction CHARGE from National Theft Search and Recovery for " + ti.settleAmount.ToString("0.00") + " (USD)";
                                                }
                                                else
                                                {
                                                    body = "";
                                                }

                                                if (body != "")
                                                {
                                                    if (SendApproved == 1)
                                                    {
                                                        var transEmail = db.tbl_OrderTransactions.Where(p => p.TransactionID == ti.transId && p.MailSend==0).FirstOrDefault();
                                                        if(transEmail!=null)
                                                        {
                                                            new CommonModel().SendEmailPayments(drTrans[0]["LeadEmail"].ToString(), emailSubject, body, accountID);
                                                            if (transEmail != null)
                                                            {
                                                                transEmail.MailSend = 1;
                                                                db.SaveChanges();
                                                            }
                                                        }
                                                        
                                                    }
                                                    body = "";
                                                    
                                                }
                                            }
                                            else if (ti.transactionStatus.ToLower() == "declined")
                                            {
                                                if (drTrans[0]["TransactionID"].ToString() != ti.transId)
                                                {
                                                    if (transUpdate != null)
                                                    {
                                                        transUpdate.TransactionID = ti.transId;
                                                        transUpdate.Status = 2;
                                                        transUpdate.StatusText = "Declined";
                                                        transUpdate.ResonText = "This transaction has been declined.";
                                                        transUpdate.GatwayResponse = ti.submitTimeLocal.ToString("MM/dd/yyyy") + "|Declined|TThis transaction has been declined.|" + ti.transId;
                                                        db.SaveChanges();
                                                    }
                                                    if (accountInfo != null)
                                                    {
                                                        if (accountInfo != null)
                                                        {
                                                            accountInfo.AccountStatus = 2;
                                                            db.SaveChanges();
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (accountInfo != null)
                                                    {
                                                        accountInfo.AccountStatus = 2;
                                                        db.SaveChanges();
                                                    }
                                                }

                                                if (string.IsNullOrWhiteSpace(drTrans[0]["LeadEmail"].ToString()) == false)
                                                {
                                                    body = declinedSend;

                                                    body = body.Replace("{HeaderParagraph}", "Our customers are important to us. Your vehicle is now registered into the National Theft Search and Recovery database. This includes Vehicle Plate Recognition for every vehicle located at your address. In the event of a Theft, we will send out a search alert for Anti-Theft tracking services.");
                                                    body = body.Replace("{Account_LastName}", drTrans[0]["BillingLastName"].ToString());
                                                    body = body.Replace("{Account_FirstName}", drTrans[0]["BillingFirstName"].ToString());
                                                    body = body.Replace("{Account_Pin_Number}", drTrans[0]["PinNo"].ToString());
                                                    body = body.Replace("{Account_Vehicle_Year}", drTrans[0]["VehicleYear"].ToString());
                                                    body = body.Replace("{Account_Vehicle_Make}", drTrans[0]["VehicleMake"].ToString());
                                                    body = body.Replace("{Account_Sale_Date}", accountInfo.SalesDate.Value.ToString("MM/dd/yyyy"));
                                                    body = body.Replace("{Account_LastModifiedDate}", ti.submitTimeLocal.ToString("MM/dd/yyyy"));
                                                    emailSubject = "NTSR - Urgent Please Contact Us";
                                                }
                                                else
                                                {
                                                    body = "";
                                                }
                                                if (body != "")
                                                {
                                                    if (SendDecline == 1)
                                                    {
                                                        var transEmail = db.tbl_OrderTransactions.Where(p => p.TransactionID == ti.transId && p.MailSend == 0).FirstOrDefault();
                                                        if (transEmail != null)
                                                        {
                                                            new CommonModel().SendEmailPayments(drTrans[0]["LeadEmail"].ToString(), emailSubject, body, accountID);
                                                            if (transEmail != null)
                                                            {
                                                                transEmail.MailSend = 1;
                                                                db.SaveChanges();
                                                            }
                                                        }
                                                      
                                                    }
                                                    body = "";
                                                    
                                                }
                                            }

                                            // Fix Schedule Transactions //
                                            using (var cmdFix = db.Database.Connection.CreateCommand())
                                            {
                                                try
                                                {
                                                    db.Database.Connection.Open();
                                                    cmdFix.CommandText = "usp_FixedScheduleTransaction";
                                                    cmdFix.CommandType = CommandType.StoredProcedure;

                                                    DbParameter paramOID = cmdFix.CreateParameter();
                                                    paramOID.ParameterName = "OrderID";
                                                    paramOID.Value = aoid;
                                                    cmdFix.Parameters.Add(paramOID);


                                                    cmdFix.ExecuteNonQuery();
                                                    db.Database.Connection.Close();
                                                    db.Dispose();
                                                }
                                                catch
                                                {
                                                    db.Database.Connection.Close();
                                                }
                                            }
                                            // Fix Schedule Transactions //
                                        }
                                    }
                                    catch { }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    new CommonModel().Log("Date : " + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + "\r\n==============================\r\nERROR : \r\n Message : " + ex.Message + "\r\n Inner Message : " + ex.InnerException.Message + "\r\n Stack Trace : " + ex.InnerException.StackTrace + "\r\n==============================\r\n");
                }
                new CommonModel().Log("Date : " + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + "\r\n==============================\r\nRESULT : \r\n" + result + "\r\n==============================\r\n");
            }
            catch (Exception ex)
            {
                new CommonModel().Log("Date : " + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + "\r\n==============================\r\nERROR : \r\n Message : " + ex.Message + "\r\n Inner Message : " + ex.InnerException.Message + "\r\n Stack Trace : " + ex.InnerException.StackTrace + "\r\n==============================\r\n");
            }
        }
    }
}