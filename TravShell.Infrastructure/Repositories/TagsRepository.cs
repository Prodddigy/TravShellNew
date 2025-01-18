using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravShell.Core.Entities;
using TravShell.Core.Repositories;
using TravShell.Infrastructure.Data;

namespace TravShell.Infrastructure.Repositories;
public class TagsRepository : ITagsRepository
{

    private readonly DataContext _dbContext;

    public TagsRepository(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task AttachTagToTrip(int tripId, Tag tag)
    {
        throw new NotImplementedException();
    }

    public Task<Tag> CreateTag(int userId, string tagname)
    {
        throw new NotImplementedException();
    }

    public Task DeleteTag(int tripId, Tag tag)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Tag>> GetAllTags()
    {
        throw new NotImplementedException();
    }

    public async Task<List<Tag>> GetAllUserTripTags(int userId)
    {
		return await _dbContext.Trips.Where(x => x.User.UserId == userId).
		 SelectMany(x => x.Tags)
		 .Distinct().ToListAsync();
	}

    public async Task<List<Tag>> GetTripTags(int userId, int tripId)
    {
        throw new NotImplementedException();
    }
}

