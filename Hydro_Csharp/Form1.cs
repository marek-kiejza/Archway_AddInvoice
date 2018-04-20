using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sage.Integration.Client;
using Sage.Integration.Accounts50.SDOAdapter;
using Sage.Integration.Adapter;
using Sage.Integration.Diagnostics;
using Sage.Integration.Messaging;
using Sage.Integration.ObjectAdapter;
using Sage.Integration.Presentation;
using Sage.Integration.Server;
using Sage.SData.Service.Config;
using Sage.Utilities;
using Sage.Common.Syndication;
using Sage.Integration.Accounts50.SDOAdapter.Feeds;
//using ArchwaySageUtils;

namespace Hydro_Csharp
{

    public partial class Form1 : Form
    {




        commodityGroupFeed commodityGroup;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBoxPass.Text != "")
            {
                // Create a new instance of a salesInvoice
                salesInvoiceFeedEntry salesInvoice = new salesInvoiceFeedEntry();

                // Find a customer to associate with the new sales invoice
                salesInvoice.tradingAccount = GetCustomer(textBoxPass.Text);

                if (salesInvoice.tradingAccount == null)
                {
                    // No customer record means we can go no further
                    Console.WriteLine("Unable to find a customer record");
                    Console.ReadKey(true);
                    return;
                }
                // Lookup a commodity to use on the new sales invoice
                commodityFeedEntry commodity = GetCommodity(textBoxPass.Text);
                if (commodity == null)
                {
                    
                    // No commodity record means we go no further
                    Console.WriteLine("Unable to find a commodity record");
                    Console.ReadKey(true);
                    return;
                }
                else
                {
                    MessageBox.Show("Password Correct");
                }

                commodityFeedEntry commodityReference = new commodityFeedEntry();
                commodityReference.UUID = commodity.UUID;

                // Lookup a commodity to use on the new sales invoice
                taxCodeFeedEntry taxCode = GetTaxCode(textBoxPass.Text);
                if (taxCode == null)
                {
                    // No record means we go no further
                    Console.WriteLine("Unable to find a tax code record");
                    Console.ReadKey(true);
                    return;
                }

                // Example of creating a historical invoice
                //salesInvoice.date = salesInvoice.taxDate = DateTime.UtcNow.AddDays(-2);

                // NOTE: This example omits the use of tax code for brevity.
                // Not specifying tax codes means that appropriate defaults will be used automatically.
                // However it is strongly recommended that tax codes are explicitly set to ensure expected results
                taxCodeFeedEntry taxReference = new taxCodeFeedEntry();
                taxReference.UUID = taxCode.UUID;

                salesInvoice.taxCodes = new taxCodeFeed();
                salesInvoice.taxCodes.Entries.Add(taxReference);

                salesInvoice.carrierTotalPrice = 100;
                salesInvoice.carrierTaxPrice = 20;
                salesInvoice.carrierNetPrice = 80;

                // Create a new sale invoice line using the commodity we just looked up
                salesInvoiceLineFeedEntry orderLine = new salesInvoiceLineFeedEntry();
                orderLine.type = "Standard";
                orderLine.text = commodity.description;
                orderLine.commodity = commodityReference;
                orderLine.quantity = 2;
                orderLine.netTotal = 50;
                orderLine.taxTotal = 10;
                orderLine.grossTotal = 60;
                orderLine.taxCodes = new taxCodeFeed();
                orderLine.taxCodes.Entries.Add(taxReference);

                // Create another invoice line this time as free text
                salesInvoiceLineFeedEntry freetextOrderLine = new salesInvoiceLineFeedEntry();
                freetextOrderLine.type = "Free Text"; // Equivalent to S1 stock code
                freetextOrderLine.text = textBoxDes.Text;
                try
                {
                    freetextOrderLine.quantity = Convert.ToInt32(textBoxQuan.Text);
                    freetextOrderLine.netTotal = Convert.ToInt32(textBoxPrice.Text);
                }
                catch (FormatException fe)
                {
                    MessageBox.Show("Only Int");
                }
                freetextOrderLine.taxTotal = 0;
                freetextOrderLine.grossTotal = 0;
                freetextOrderLine.taxCodes = new taxCodeFeed();
                freetextOrderLine.taxCodes.Entries.Add(taxReference);


                /*
                // Create a 3rd invoice line this time as a message
                salesInvoiceLineFeedEntry messageOrderLine = new salesInvoiceLineFeedEntry();
                messageOrderLine.type = "Commentary"; // Equivalent to M stock code
                messageOrderLine.text = "A message line created via Sdata";
                */



    

                // Associate the lines with our invoice
                salesInvoice.salesInvoiceLines = new salesInvoiceLineFeed();
             //   salesInvoice.salesInvoiceLines.Entries.Add(orderLine);
                salesInvoice.salesInvoiceLines.Entries.Add(freetextOrderLine);
             //   salesInvoice.salesInvoiceLines.Entries.Add(messageOrderLine);

                // Now we have constructed our new invoice we can submit it using the HTTP POST verb  
                Sage.Common.Syndication.SDataUri salesInvoiceUri = new Sage.Common.Syndication.SDataUri();
                salesInvoiceUri.BuildLocalPath("Accounts50", "GCRM", "-", "salesInvoices");

                SDataRequest invoiceRequest = new SDataRequest(salesInvoiceUri.Uri, salesInvoice, Sage.Integration.Messaging.Model.RequestVerb.POST);
                invoiceRequest.Username = "MANAGER";
                invoiceRequest.Password = textBoxPass.Text;

                // IF successful the POST operation will provide us with a the newly created sales invoice
                salesInvoiceFeedEntry savedSalesInvoice = new salesInvoiceFeedEntry();
                invoiceRequest.RequestFeedEntry<salesInvoiceFeedEntry>(savedSalesInvoice);

                if (invoiceRequest.IsStatusValidForVerb)
                {
                    Console.WriteLine(string.Format("Successfully created sales invoice {0}", savedSalesInvoice.reference));
                }
                else
                {
                    // There was a problem
                    Console.WriteLine("Create failed. Response was {0}", invoiceRequest.HttpStatusCode.ToString());
                    if (invoiceRequest.Diagnoses != null)
                    {
                        foreach (Diagnosis diagnosis in invoiceRequest.Diagnoses)
                            Console.WriteLine(diagnosis.Message);
                    }
                }


                Console.WriteLine("We\'re finished!!!");

                MessageBox.Show("Successfully created sales invoice");
            }
        }

        static commodityFeedEntry GetCommodity(string pass)
        {
            // Look up the first commodity (product) record 
            Sage.Common.Syndication.SDataUri commodityUri = new Sage.Common.Syndication.SDataUri();
            commodityUri.BuildLocalPath("Accounts50", "GCRM", "-", "commodities");
            commodityUri.Count = 1;

            SDataRequest commodityRequest = new SDataRequest(commodityUri.Uri);
            commodityRequest.Username = "MANAGER";
            commodityRequest.Password = pass;

            commodityFeed commodities = new commodityFeed();
            commodityRequest.RequestFeed<commodityFeedEntry>(commodities);

            // If we found a record return it
            if (commodityRequest.IsStatusValidForVerb && commodities.Entries != null && commodities.Entries.Count > 0)
                return commodities.Entries[0];
            else
            {
                // There was a problem
                Console.WriteLine("Commodity lookup failed. Response was {0}", commodityRequest.HttpStatusCode.ToString());
                if (commodityRequest.Diagnoses != null)
                {
                    foreach (Diagnosis diagnosis in commodityRequest.Diagnoses)
                        Console.WriteLine(diagnosis.Message);
                }

                return null;
            }
        }
        static taxCodeFeedEntry GetTaxCode(string pass)
        {
            // Look up the tax code record 
            Sage.Common.Syndication.SDataUri taxCodeUri = new Sage.Common.Syndication.SDataUri();
            taxCodeUri.BuildLocalPath("Accounts50", "GCRM", "-", "taxCodes");
            taxCodeUri.Where = "reference eq 'T1'";

            SDataRequest taxcodeRequest = new SDataRequest(taxCodeUri.Uri);
            taxcodeRequest.Username = "MANAGER";
            taxcodeRequest.Password = pass;

            taxCodeFeed taxcodes = new taxCodeFeed();
            taxcodeRequest.RequestFeed<taxCodeFeedEntry>(taxcodes);

            // If we found a customer record return it
            if (taxcodeRequest.IsStatusValidForVerb && taxcodes.Entries != null && taxcodes.Entries.Count > 0)
                return taxcodes.Entries[0];
            else
            {
                // There was a problem
                Console.WriteLine("Tax code lookup failed. Response was {0}", taxcodeRequest.HttpStatusCode.ToString());
                if (taxcodeRequest.Diagnoses != null)
                {
                    foreach (Diagnosis diagnosis in taxcodeRequest.Diagnoses)
                        Console.WriteLine(diagnosis.Message);
                }

                return null;
            }
        }

        static tradingAccountFeedEntry GetCustomer(string pass)
        {
            // Look up the first customer record 
            Sage.Common.Syndication.SDataUri accountUri = new Sage.Common.Syndication.SDataUri();
            accountUri.BuildLocalPath("Accounts50", "GCRM", "-", "tradingAccounts");
            accountUri.Where = "customerSupplierFlag eq 'Customer'";
            accountUri.Count = 1;

            SDataRequest accountRequest = new SDataRequest(accountUri.Uri);
            accountRequest.AllowPromptForCredentials = false;
            accountRequest.Username = "MANAGER";
            accountRequest.Password = pass;

            tradingAccountFeed accounts = new tradingAccountFeed();
            accountRequest.RequestFeed<tradingAccountFeedEntry>(accounts);

            // If we found a customer record return it
            if (accountRequest.IsStatusValidForVerb && accounts.Entries != null && accounts.Entries.Count > 0)
                return accounts.Entries[0];
            else
            {


                MessageBox.Show("Wrong password!!! Error");
                // There was a problem
                Console.WriteLine("Account lookup failed. Response was {0}", accountRequest.HttpStatusCode.ToString());
                if (accountRequest.Diagnoses != null)
                {
                    foreach (Diagnosis diagnosis in accountRequest.Diagnoses)
                        Console.WriteLine(diagnosis.Message);
                }

                return null;
            }
        }

        private void textBoxPass_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
