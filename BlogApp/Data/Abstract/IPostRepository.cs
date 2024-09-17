using BlogApp.Entity;

namespace BlogApp.Data.Abstract
{
    public interface IPostRepository
    {
        IQueryable<Post> Posts { get; }
        void CreatePost(Post post);
        void EditPost(Post post);
        void EditPost(Post post, int[] tagIds);
    }
    /*
     * numarable 100 bilgiyi getirri sonra filtereleme yapar querable sadece filtrelenmiş olanılaı getirir
     *  IQueryable<Post> Posts Özelliği:
        Posts özelliği, Post nesnelerinin sorgulanabilir bir koleksiyonunu sağlar. 
        Bu özellik, Post nesnelerini sorgulamak için kullanılır.
       
        Void CreatePost(Post post) Metodu:
        CreatePost metodu, bir Post nesnesi alır ve yeni bir Post oluşturur.
     */
}
