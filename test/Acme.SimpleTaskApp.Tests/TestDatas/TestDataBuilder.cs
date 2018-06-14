using Acme.SimpleTaskApp.EntityFrameworkCore;
using Acme.SimpleTaskApp.Persons;
using Acme.SimpleTaskApp.Tasks;

namespace Acme.SimpleTaskApp.Tests.TestDatas
{
    public class TestDataBuilder
    {
        private readonly SimpleTaskAppDbContext _context;

        public TestDataBuilder(SimpleTaskAppDbContext context)
        {
            _context = context;
        }

        public void Build()
        {
            var neo = new Person("Neo");
            _context.Persons.Add(neo);
            _context.SaveChanges();

            //create test data here...
            _context.Tasks.AddRange(
            new Task("Follow the white rabbit", "Follow the white rabbit in order to know the reality.", neo.Id),
            new Task("Clean your room") { State = TaskState.Completed }
            );
        }
    }
}