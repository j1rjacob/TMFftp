using NetFwTypeLib;
using System;


namespace TMF_ftp.Helpers
{
    public static class FirewallManager
    {
        public static void SetRule(string status)
        {
            INetFwRule firewallRule = (INetFwRule)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));
            firewallRule.Action = NET_FW_ACTION_.NET_FW_ACTION_ALLOW;
            firewallRule.Description = "Allow TMF ftp";
            firewallRule.ApplicationName = AppDomain.CurrentDomain.BaseDirectory + "\\TMF ftp.exe";
            firewallRule.Enabled = true;
            firewallRule.InterfaceTypes = "All";
            firewallRule.Name = "TMF ftp";

            INetFwPolicy2 firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(
                Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
            if(status == "ON")
                firewallPolicy.Rules.Add(firewallRule);
            else
                firewallPolicy.Rules.Remove(firewallRule.Name);
        }

        public static void RemoveFirewallRule()
        {
            try
            {
                string RuleName = "TMF ftp";

                Type tNetFwPolicy2 = Type.GetTypeFromProgID("HNetCfg.FwPolicy2");
                INetFwPolicy2 fwPolicy2 = (INetFwPolicy2)Activator.CreateInstance(tNetFwPolicy2);
                foreach (INetFwRule rule in fwPolicy2.Rules)
                {
                    if (rule.Name.IndexOf(RuleName) != -1)
                    {
                        INetFwPolicy2 firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
                        firewallPolicy.Rules.Remove(rule.Name);
                        Console.WriteLine(rule.Name + " has been deleted from Firewall Policy");
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error deleting rule from firewall");
            }
        }
    }
}
