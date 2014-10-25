
using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;
using NLog.Config;
using NLog.Targets;
using NLog;

namespace TestWeb
{
  public class Global : System.Web.HttpApplication
  {
        
    protected virtual void Application_Start(Object sender, EventArgs e)
    {
      // Step 1. Create configuration object
      var config = new LoggingConfiguration();

      // Step 2. Create targets and add them to the configuration
      var fileTarget = new FileTarget();
      config.AddTarget("file", fileTarget);

      // Step 3. Set target properties
      fileTarget.FileName = "${basedir}/log.txt";
      fileTarget.Layout = @"${date:format=HH\\:MM\\:ss} ${logger} ${message}";

      // Step 4. Define rules
      var rule = new LoggingRule("*", LogLevel.Debug, fileTarget);
      config.LoggingRules.Add(rule);

      // Step 5. Activate the configuration
      LogManager.Configuration = config;
    }

    protected virtual void Session_Start(Object sender, EventArgs e)
    {
    }

    protected virtual void Application_BeginRequest(Object sender, EventArgs e)
    {
    }

    protected virtual void Application_EndRequest(Object sender, EventArgs e)
    {
    }

    protected virtual void Application_AuthenticateRequest(Object sender, EventArgs e)
    {
    }

    protected virtual void Application_Error(Object sender, EventArgs e)
    {
    }

    protected virtual void Session_End(Object sender, EventArgs e)
    {
    }

    protected virtual void Application_End(Object sender, EventArgs e)
    {
    }
  }
}

