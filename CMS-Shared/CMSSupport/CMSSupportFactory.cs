using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Infrastructure;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using CMS_Common;
using CMS_DataModel.Models;
using CMS_DTO.CMSRole;
using CMS_DTO.CMSSupport;

namespace CMS_Shared.CMSSupport
{
    public class CMSSupportFactory
    {
        public CMS_SupportModels IsDirty()
        {
            try
            {
                NSLog.Logger.Info("Checking DbContext Changes");
                var config = new CMS_DataModel.Migrations.Configuration();
                NSLog.Logger.Info("Initialized Configuration", config);
                var migrator = new DbMigrator(config);
                NSLog.Logger.Info("Initialized Migrator", migrator);
                var scriptor = new MigratorScriptingDecorator(migrator);
                NSLog.Logger.Info("Initialized Scriptor", scriptor);
                NSLog.Logger.Info("Generating Migration Update Script...");
                var script = scriptor.ScriptUpdate(null, null);
                if (string.IsNullOrEmpty(script)) { NSLog.Logger.Info("No pending explicit migrations."); }
                else
                {
                    using (var sw = new StreamWriter(HttpContext.Current.Server.MapPath($"~/logs/{GetDateTimeCode()}.sql")))
                    {
                        sw.WriteLine(script);
                    }
                }
                script = script.Replace("\n", " ").Replace("\r", " ");

                NSLog.Logger.Info("Generated Script", script);

                return new CMS_SupportModels()
                {
                    Success = true,
                    Message = string.IsNullOrEmpty(script) ? "Hooray! There's no change on Database Structure." : "There are some changes on Database Structure. Please copy all the data in RawData and run it on SQL Server or look for a <yyyymmddhhMMssttt>.sql file inside API 'logs' folder.",
                    RawData = script,
                };
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("IsDirty", ex);
                return new CMS_SupportModels()
                {
                    Success = false,
                    Message = "Exception",
                    RawData = ex
                };
            }


        }

        protected string GetDateTimeCode()
        {
            return $"{DateTime.Now.Year}{DateTime.Now.Month}{DateTime.Now.Day}{DateTime.Now.Hour}{DateTime.Now.Minute}{DateTime.Now.Second}{DateTime.Now.Millisecond}";
        }

    }
    
}
