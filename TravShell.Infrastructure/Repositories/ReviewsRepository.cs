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

        if(reviewedTrip.Reviews ==null)
        {
            reviewedTrip.Reviews = new List<Review>();
        }
        
        if(reviewedTrip.Reviews.Where(x=> x.User.UserId ==userId).FirstOrDefault() != null)
        {
            return await UpdateReview( userId, tripId, content, rating);
        }

        Review review = new Review
        {
            Content = content,
            IsEdited = false,
            StarReview = rating,
            Trip = reviewedTrip,
            User = Reviewer


        };
        await _dbContext.Reviews.AddAsync(review);
        await _dbContext.SaveChangesAsync();

        return review;
    }

    public async Task<List<Review>> GetFourTripReviews(int tripId, int take)
    {
        Console.WriteLine("before count");
        var reviewCount = await _dbContext.Reviews.Where(x => x.Trip.TripId == tripId).CountAsync();
        Console.WriteLine("after count: "+reviewCount );

        if (reviewCount != 0)
        {

            var reviews = await _dbContext.Reviews.Where(x => x.Trip.TripId == tripId).Include(x => x.User)
                                .Skip(take).Take(4).ToListAsync();
            return reviews;
        }
        return null;
    }

    public async Task<Review> GetUserReview(int userId, int tripId)
    {
        var review = await _dbContext.Reviews.Where(x => x.User.UserId == userId && x.Trip.TripId == tripId)
                                                .FirstOrDefaultAsync();

        return review;
    }

    public async Task<Review> UpdateReview(int userId, int tripId, string content, int rating)
    {
        var review  = await _dbContext.Reviews.Where(x=>x.User.UserId == userId && x.Trip.TripId == tripId)
                                                                    .FirstOrDefaultAsync();
        review.Content = content;
        review.StarReview = rating;

        await _dbContext.SaveChangesAsync();

        return review;
    }
}

