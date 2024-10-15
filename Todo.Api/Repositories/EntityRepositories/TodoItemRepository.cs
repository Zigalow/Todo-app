using Microsoft.EntityFrameworkCore;
using Todo.Api.Data;
using Todo.Api.Interfaces.EntityInterfaces;
using Todo.Core.Entities;

namespace Todo.Api.Repositories.EntityRepositories;

public class TodoItemRepository : GenericRepository<TodoItem>, ITodoItemRepository
{
    private readonly TodoDbContext _dbContext;

    public TodoItemRepository(TodoDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<TodoItem>?> GetAllTodoItemsForTodoList(int todoListId)
    {
        var todoItemsFromList = await _dbContext.TodoLists
            .Where(tl => tl.Id == todoListId)
            .Select(tl => new { tl.Items })
            .FirstOrDefaultAsync();

        return todoItemsFromList?.Items.ToList();
    }

    public async Task<List<TodoItem>?> GetAllTodoItemsForProject(int projectId)
    {
        var todoItemsFromProject = await _dbContext.Projects
            .Where(p => p.Id == projectId)
            .SelectMany(p => p.TodoLists)
            .Select(tl => new { tl.Items })
            .FirstOrDefaultAsync();

        return todoItemsFromProject?.Items.ToList();
    }
}