using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravShell.Core.Entities;
using TravShell.Core.Repositories;

namespace TravShell.App.Reviews;
public class ReviewService : IReviewService
{
    private readonly IReviewsRepository _reviewsRepository;

    public ReviewService(IReviewsRepository reviewsRepository)
    {
        _reviewsRepository = reviewsRepository;
    }

    public Task<Review> CreateReview(int userId, int tripId, string content, int rating)
    {
        throw new NotImplementedException();
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

