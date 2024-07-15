using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITests.APICore
{
    public class LogOnFailureFixture
    {
        public void RunLoggedTest(Action test, NLog.Logger logger)
        {
            try
            {
                test();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Test failed unexpectedly");
                throw;
            }
        }
    }
}
