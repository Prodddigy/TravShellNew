using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Microsoft.Extensions.DependencyInjection;
using TravShell.App.Attractions;
using TravShell.App.GoogleAPI;
using TravShell.App.PublicTrips;
using TravShell.App.Reviews;
using TravShell.App.Tags;
using TravShell.App.Trips;
using TravShell.App.Users;

namespace TravShell.App;
public static class Extensions
{
	public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
	{
		services.AddScoped<IUserService, UserService>();
		services.AddScoped<ITripService, TripService>();
		services.AddScoped<IAttractionService, AttractionService>();
		services.AddScoped<IGoogleService,GoogleService>();
		services.AddScoped<ITagService, TagService>();
		services.AddScoped<IReviewService, ReviewService>();
		services.AddScoped<IPublicTripService, PublicTripService>();
		return services;
	}


}

