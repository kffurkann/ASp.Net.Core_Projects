using RazorPagesProject.Models;

namespace RazorPagesProject.Repository
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
        Employee GetById(int id);
        Employee Update(Employee entity);
    }
}
