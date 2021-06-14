namespace TG.Exam.Refactoring
{
    public interface IDatabaseRepository
    {
        Order GetOrder(int orderId);
    }
}
