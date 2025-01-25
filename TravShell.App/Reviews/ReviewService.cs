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

    public async Task<Review> CreateReview(int userId, int tripId, string content, int rating)
    {
        return await _reviewsRepository.CreateReview(userId, tripId, content, rating);
    }

    public async Task<List<Review>> GetFourTripReviews(int tripId, int take)
    {
        return  await _reviewsRepository.GetFourTripReviews(tripId, take);
    }

    public async Task<Review> GetUserReview(int userId, int tripId)
    {
        return await _reviewsRepository.GetUserReview(userId, tripId);
    }

    public Task<Review> UpdateReview(int userId, int tripId, string content, int rating)
    {
        throw new NotImplementedException();
    }
}

