using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BileriumCafe_POS.Utils
{
    internal class AppUtils
    {

        public static string GetDesktopDirectoryPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        }

        public static string GetAppUsersFilePath()
        {
            return Path.Combine(GetDesktopDirectoryPath(), "users.json");
        }
        public static string GetCoffeFile()
        {
            return Path.Combine(GetDesktopDirectoryPath(), "coffee.json");
        }

    }
}