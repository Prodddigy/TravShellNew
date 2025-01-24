using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravShell.Core.Entities;

namespace TravShell.App.Reviews;
public interface IReviewService
{
    public Task<Review> GetUserReview(int userId,int tripId);

    public Task<Review> CreateReview(int userId,int tripId, string content, int rating);

    public Task<Review> UpdateReview(int userId, int tripId, string content, int rating);

    public Task<List<Review>> GetFourTripReviews(int tripId,int skip);

}

