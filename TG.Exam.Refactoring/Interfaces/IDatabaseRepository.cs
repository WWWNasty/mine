namespace TG.Exam.Refactoring
{
    public interface IDatabaseRepository
    {
        Order GetOrder(string orderId);
    }
}
