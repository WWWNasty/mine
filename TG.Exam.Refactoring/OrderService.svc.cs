using System;
using System.Data.SqlClient;
using System.Diagnostics;
using log4net;
using log4net.Config;
using TG.Exam.Refactoring.Repositories;

namespace TG.Exam.Refactoring
{
    public class OrderService : IOrderService
    {
        //написать с большой буквы переменную logger
        //private static readonly ILog logger = LogManager.GetLogger(typeof(OrderService));
        private static readonly ILog Logger = LogManager.GetLogger(typeof(OrderService));

        //так как класс переполнен логикой, была выделена логика работы с бд в отдельный класс
        private CacheRepository CacheRepository { get; set; } = CacheRepository.GetInstance();

        //так как класс переполнен логикой, была выделена логика работы с кэшем в отдельный класс
        private readonly IDatabaseRepository _databaseRepository = new DatabaseRepository();


        public OrderService()
        {
            BasicConfigurator.Configure();
        }

        //тип orderId должен быть int чтоб избежать sql-иньекций и в бд это поле типа int
        //public Order LoadOrder(string orderId)
        public Order LoadOrder(int orderId)
        {
            try
            {
                Debug.Assert(orderId != 0);
                var stopWatch = new Stopwatch();
                stopWatch.Start();

                var orderFromCache = CacheRepository.Get(orderId, stopWatch);

                if (orderFromCache != null)
                {
                    return orderFromCache;
                }

                var orderFromDataBase = _databaseRepository.GetOrder(orderId);

                if (orderFromDataBase != null)
                {
                    CacheRepository.Set(orderFromDataBase);
                    stopWatch.Stop();

                    //непонятно что выводится в лог,необходимо добавить информацию о данных в вывод лога
                    Logger.InfoFormat("Elapsed - {0}", stopWatch.Elapsed);
                    return orderFromDataBase;
                }

                stopWatch.Stop();

                //непонятно что выводится в лог,необходимо добавить информацию о данных в вывод лога
                Logger.InfoFormat("Elapsed - {0}", stopWatch.Elapsed);
                return null;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex.Message);

                //неполная информация об ощибке, необходимо дополнить
                // throw new ApplicationException("Error");
                throw new ApplicationException("Error", ex);
            }
        }
    }
}
