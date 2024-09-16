using BlogApp.Entity;

namespace BlogApp.Data.Abstract
{
    public interface IUserRepository
    {
        IQueryable<User> Users { get; } //getirme
        void CreateUser(User User); //işleme
    }
}
