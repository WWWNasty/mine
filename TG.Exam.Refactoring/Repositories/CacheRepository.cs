using System;
using System.Collections.Generic;
using System.Diagnostics;
using log4net;

namespace TG.Exam.Refactoring.Repositories
{
    public class CacheRepository : ICacheRepository
    {
        private static volatile CacheRepository _instance;

        private static readonly object SyncRoot = new object();

        //необходимы модификаторы доступа private readonly и назвать переменную cache - _cache
        //public IDictionary<string, Order> cache = new Dictionary<string, Order>();
        private readonly IDictionary<string, Order> _cache = new Dictionary<string, Order>();
        private static readonly ILog Logger = LogManager.GetLogger(typeof(OrderService));

        private CacheRepository()
        {
        }

        public static CacheRepository GetInstance()
        {
            if (_instance != null)
                return _instance;

            lock (SyncRoot)
            {
                if (_instance == null)
                    _instance = new CacheRepository();
            }

            return _instance;
        }

        public Order Get(string orderId, Stopwatch stopWatch)
        {
            lock (_cache)
            {
                if (!_cache.ContainsKey(orderId)) return null;
                stopWatch.Stop();
                //непонятно что выводится в лог,необходимо добавить информацию о данных в вывод лога
                Logger.InfoFormat("Elapsed - {0}", stopWatch.Elapsed);
                return _cache[orderId];
            }
        }

        public void Set(Order order)
        {
            lock (_cache)
            {
                if (!_cache.ContainsKey(order.OrderId.ToString()))
                    _cache[order.OrderId.ToString()] = order;
            }
        }
    }
}
