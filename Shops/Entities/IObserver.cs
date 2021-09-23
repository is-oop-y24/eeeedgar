namespace Shops.Entities
{
    public interface IObserver
    {
        // Получает обновление от издателя
        void Update(ISubject subject);
    }
}