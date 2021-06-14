using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace TG.Exam.SQL
{
    public class DAL
    {
        private SqlConnection GetConnection()
        {
            var connectionString = ConfigurationManager.AppSettings["ConnectionString"];

            var con = new SqlConnection(connectionString);

            con.Open();

            return con;
        }

        private DataSet GetData(string sql)
        {
            var ds = new DataSet();

            using (var con = GetConnection())
            {
                using (var cmd = new SqlCommand(sql, con))
                {
                    using (var adp = new SqlDataAdapter(cmd))
                    {
                        adp.Fill(ds);
                    }
                }
            }

            return ds;
        }

        private void Execute(string sql)
        {
            using (var con = GetConnection())
            {
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable GetAllOrders()
        {
            var sql = @"Select * From [Orders]";

            var ds = GetData(sql);

            var result = ds.Tables.OfType<DataTable>().FirstOrDefault();

            return result;
        }

        public DataTable GetAllOrdersWithCustomers()
        {
            var sql = @"Select o.OrderId, o.OrderDate, c.CustomerFirstName, c.CustomerLastName 
                        From [Orders] As o Join [Customers] As c On c.CustomerId = o.OrderCustomerId";

            var ds = GetData(sql);

            var result = ds.Tables.OfType<DataTable>().FirstOrDefault();

            return result;
        }

        public DataTable GetAllOrdersWithPriceUnder(int price)
        {
            var sql = $@"Select o.OrderId, Sum([ItemPrice] *oi.Count) As PriseOrder
                        From [Items] As i Join [OrdersItems] As oi On i.ItemId = oi.ItemId Join [Orders] As o on o.OrderId = oi.OrderId
                        Group By o.OrderId
                        Having Sum([ItemPrice] *oi.Count) < {price}";

            var ds = GetData(sql);

            var result = ds.Tables.OfType<DataTable>().FirstOrDefault();

            return result;
        }

        public void DeleteCustomer(int orderId)
        {
            var sql = $@"Delete From [Customers] Where [CustomerId] In
                            (Select [OrderCustomerId] From [Orders] Where [OrderId]={orderId});
                        Delete From [OrdersItems] Where [OrderId] = {orderId};
                        Delete From [Orders] Where [OrderId] = {orderId};
                        ";
            Execute(sql);
        }

        internal DataTable GetAllItemsAndTheirOrdersCountIncludingTheItemsWithoutOrders()
        {
            var sql = @"Select i.ItemId, i.ItemName, count(oi.ItemId) As Count
                        From [OrdersItems] As oi 
                        Right Join Items i On oi.ItemId = i.ItemId 
                        Group By i.ItemId, i.ItemName";

            var ds = GetData(sql);

            var result = ds.Tables.OfType<DataTable>().FirstOrDefault();

            return result;
        }
    }
}