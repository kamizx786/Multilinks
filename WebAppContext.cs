using System.Data.Entity.Migrations;
using System.Data.Entity;
using Multilinks.Models;

namespace Multilinks
{
    [DbConfigurationType(typeof(Multilinks.Models.Model1))]
    public class WebAppContext : DbContext
    {
   
        public WebAppContext()
            //Reference the name of your connection string ( WebAppCon )  
            : base("WebAppCon") { }
    }
}