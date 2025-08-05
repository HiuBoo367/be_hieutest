
using App.Xuatnhapcanh.Dal.DatabaseSpecific;

namespace App.Xuatnhapcanh.Services
{
    public class DataAccessAdapterFactory
    {
       

        public DataAccessAdapterFactory()
        {
         
        }

        private static DataAccessAdapter CreateAdapter(string connectionString)
        {
            return new DataAccessAdapter(connectionString);
        }

        public DataAccessAdapter CreateAdapter()
        {
            return CreateAdapter("Database=XUATNHAPCANH;Server=10.192.4.212;Port=27600;User Id=fepvector;Password=Adminbt123654");
        }
    }
}