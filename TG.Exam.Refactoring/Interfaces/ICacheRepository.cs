using System.Diagnostics;

namespace TG.Exam.Refactoring
{
    public interface ICacheRepository
    {
        Order Get(string orderId, Stopwatch stopWatch);
        void Set(Order order);
    }
}
