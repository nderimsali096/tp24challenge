using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP24.UnitTests.Utilities
{
    public interface IConfigurationSettings
    {
        string GetConnectionString(string name);
    }
    public class MockConfigurationSettings : IConfigurationSettings
    {
        public string GetConnectionString(string name)
        {
            return "connection_string";
        }
    }

}
