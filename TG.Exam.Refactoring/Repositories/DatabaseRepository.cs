using System;
using System.Data.SqlClient;
using System.Configuration;


namespace TG.Exam.Refactoring.Repositories
{
    //для работы с бд лучше использовать Dapper, т.к. мы получим более безопасное выполнение запросов и удобное приведение типов
    public class DatabaseRepository : IDatabaseRepository
    {
        //написать переменную connectionString - _connectionString с модификатором доступа private
        //readonly string connectionString = ConfigurationManager.ConnectionStrings["OrdersDBConnectionString"].ConnectionString;
        private readonly string _connectionString =
            ConfigurationManager.ConnectionStrings["OrdersDBConnectionString"].ConnectionString;

        public Order GetOrder(string orderId)
        {
            var connection = new SqlConnection(_connectionString);

            //завернуто в using, т.к. необходимо закрытие подключения к бд
            using (connection)
            {
                //where не верный стиль написания, необходимо соблюсти стиль написания операторов
                // string queryTemplate =
                //   "SELECT OrderId, OrderCustomerId, OrderDate" +
                //   "  FROM dbo.Orders where OrderId='{0}'";
                var queryTemplate = @"STRING_ESCAPE( 'SELECT OrderId, OrderCustomerId, OrderDate
                                                  FROM dbo.Orders WHERE OrderId={0}' , 'string' ) ";

                var query = string.Format(queryTemplate, orderId);

                var command = new SqlCommand(query, connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (!reader.Read()) return null;
                var order = new Order
                {
                    OrderId = (int) reader[0],
                    OrderCustomerId = (int) reader[1],
                    OrderDate = (DateTime) reader[2]
                };
                return order;
            }
        }
    }
}
