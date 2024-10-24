using Microsoft.EntityFrameworkCore;
using Todo.Api.Data;
using Todo.Api.Interfaces.EntityInterfaces;
using Todo.Core.Entities;

namespace Todo.Api.Repositories.EntityRepositories;

public class LabelRepository : GenericRepository<Label>, ILabelRepository
{
    private readonly TodoDbContext _dbContext;

    public LabelRepository(TodoDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public new async Task<List<Label>> GetAllAsync()
    {
        return await _dbContext.Labels
            .Include(l => l.TodoItems)
            .ToListAsync();
    }

    public async Task<List<Label>?> GetAllLabelsForProject(int projectId)
    {
        return await _dbContext.Labels
            .Where(l => l.ProjectId == projectId)
            .Include(l => l.TodoItems)
            .ToListAsync();
    }

    public new async Task<Label?> GetByIdAsync(int id)
    {
        return await _dbContext.Labels
            .Include(l => l.TodoItems)
            .FirstOrDefaultAsync(l => l.Id == id);
    }

    public async Task<bool> ExistsAsync(Label label)
    {
        return await _dbContext.Labels.AnyAsync(l =>
            l.Name == label.Name && l.ProjectId == label.ProjectId);
    }
}