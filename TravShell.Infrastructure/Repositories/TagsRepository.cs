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

    public async Task AttachTagToTrip(int tripId, Tag tag)
    {
        var t = await _dbContext.Trips.Where(x=>x.TripId ==  tripId && !x.Tags.Contains(tag)).FirstOrDefaultAsync();

        if(tag != null && t !=null)
        {
            if(t.Tags == null)
                t.Tags = new List<Tag>();

            t.Tags.Add(tag);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<Tag> CreateTag(int userId, string tagname)
    {
        if(tagname.Trim() == "")
        {
            return null;
        }

        var isTagAvailable = await _dbContext.Tags.Where(x => x.Name == tagname.ToLower()).FirstOrDefaultAsync();

        if (isTagAvailable == null)
        {
            Tag newTag = new Tag
            {
                Name = tagname.ToLower().Trim(),
                AuthorId = userId,
            };

			await _dbContext.Tags.AddAsync(newTag);
			await _dbContext.SaveChangesAsync();

			return newTag;
		}
        return isTagAvailable;
    }

    public async Task RemoveTagFromTrip(int tripId, Tag tag)
    {

        

	
		var t = await _dbContext.Tags.Where(x=> x.TagId == tag.TagId).Include(x=> x.TripList).FirstOrDefaultAsync();
        if (t == null) return;
       
        //var trip = await _dbContext.Trips.Where(x => x.TripId == tripId).FirstOrDefaultAsync();

        //if (trip == null) return;

        var trip = t.TripList.Where(x=> x.TripId == tripId).FirstOrDefault();
		if (trip == null) return;

		t.TripList.Remove(trip);

        if (t.TripList.Count == 0)
        {
            _dbContext.Remove(t);
        }
        await _dbContext.SaveChangesAsync();
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

    public async Task<List<Tag>> GetTripTags( int tripId)
    {
        var trip = await _dbContext.Trips.Where(x=> x.TripId == tripId).FirstOrDefaultAsync();

        if (trip == null)
            return null;
       var tags =  await _dbContext.Trips.Where(x=>x.TripId == tripId).SelectMany(x=> x.Tags).ToListAsync();

        return tags;
    }
}

