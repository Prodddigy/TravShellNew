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
public class ReviewsRepository : IReviewsRepository
{
	private readonly DataContext _dbContext;

	public ReviewsRepository(DataContext dbContext)
	{
		_dbContext = dbContext;
	}

    public async Task<Review> CreateReview(int userId, int tripId, string content, int rating)
    {
        var reviewedTrip = await _dbContext.Trips.Where(x=> x.TripId == tripId)
                                        .Include(x=>x.Reviews).SingleOrDefaultAsync();

        var Reviewer = await _dbContext.Users.Where(x=>x.UserId ==  userId).SingleOrDefaultAsync();

        if (reviewedTrip == null || Reviewer == null)
        {
            return null;
        }

        return null;



    }

    public Task<List<Review>> GetFourTripReviews(int tripId, int take)
    {
        throw new NotImplementedException();
    }

    public Task<Review> GetUserReview(int userId, int tripId)
    {
        throw new NotImplementedException();
    }

    public Task<Review> UpdateReview(int userId, int tripId, string content, int rating)
    {
        throw new NotImplementedException();
    }
}

