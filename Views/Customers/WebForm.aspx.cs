using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Net;
using System.IO;
using ASPSnippets.GoogleAPI;
using System.Web.Script.Serialization;

using Multilinks.Models;
namespace Multilinks
{

    public partial class WebForm : System.Web.Mvc.ViewPage
    {



      private Model1 db= new Model1();
        protected void Page_Load(object sender, EventArgs e)
        {
            GoogleConnect.ClientId = "570588916398-agra8v7ker2qja4e3j9a195c858a3hba.apps.googleusercontent.com";
            GoogleConnect.ClientSecret = "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";
            GoogleConnect.RedirectUri = Request.Url.AbsoluteUri.Split('?')[0];

            if (!this.IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["code"]))
                {
                    string code = Request.QueryString["code"];
                    string json = GoogleConnect.Fetch("me", code);
                    Customer profile = new JavaScriptSerializer().Deserialize<Customer>(json);
                   
                }
                if (Request.QueryString["error"] == "access_denied")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Access denied.')", true);
                }
            }
        }
        protected void Submit(object sender, EventArgs e)
        {
           
        }

        protected void Clear(object sender, EventArgs e)
        {
            GoogleConnect.Clear(Request.QueryString["code"]);
        }

    }
    }