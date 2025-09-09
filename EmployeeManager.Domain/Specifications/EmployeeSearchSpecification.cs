using System.Linq.Expressions;
using EmployeeManager.Domain.Entities;
using EmployeeManager.Domain.ValueObjects;

namespace EmployeeManager.Domain.Specifications
{
    public class EmployeeSearchSpecification : ISpecification<Employee>
    {
        private readonly string _searchTerm;

        public EmployeeSearchSpecification(string searchTerm = null)
        {
            
            _searchTerm = string.IsNullOrWhiteSpace(searchTerm) ? null : searchTerm.ToLower().Trim();
            
        }

        public Expression<Func<Employee, bool>> ToExpression()
        {
            Console.WriteLine($"[DEBUG] ToExpression - _searchTerm: '{_searchTerm}'");
            
            try
            {
                if (string.IsNullOrWhiteSpace(_searchTerm))
                {
                    Console.WriteLine("[DEBUG] Sem termo de busca, retornando todos");
                    return emp => true;
                }

                Expression<Func<Employee, bool>> expression = emp =>
                    (emp.Name != null && emp.Name.ToLower().Contains(_searchTerm)) ||
                    (emp.Email != null && emp.Email.ToLower().Contains(_searchTerm)) ||
                    (emp.Department != null && emp.Department.ToLower().Contains(_searchTerm));

                Console.WriteLine($"[DEBUG] Expression criada: {expression}");
                return expression;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DEBUG] Erro ao criar expression: {ex.Message}");
                Console.WriteLine($"[DEBUG] Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        public bool IsSatisfiedBy(Employee entity)
        {
            return ToExpression().Compile()(entity);
        }
    }
}