using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testschool
{
    public class AppSettings
    {
        public string StorageConnectionString { get; set; }

        public string AzureStorageAccountContainer { get; set; }
    }

    public class ConnectionStrings
    {
        public string DefaultConnection { get; set; }
    }
}
