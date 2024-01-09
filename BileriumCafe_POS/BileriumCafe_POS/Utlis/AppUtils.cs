
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
            return Path.Combine(GetDesktopDirectoryPath(), "coffe.json");
        }

        public static string GetProductFile()
        {
            return Path.Combine(GetDesktopDirectoryPath(), "addIn.json");
        }
        public static string GetOrderItemListPath()
        {
            return Path.Combine(GetDesktopDirectoryPath(), "orderItem.json");
        }

        public static string GetOrderListPath()
        {
            return Path.Combine(GetDesktopDirectoryPath(), "order.json");
        }
        public static string GetCustomerListPath()
        {
            return Path.Combine(GetDesktopDirectoryPath(), "customer.json");
        }

    }
}