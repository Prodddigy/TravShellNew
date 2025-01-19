using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TravShell.Core.Repositories;
using TravShell.Infrastructure;
using TravShell.Infrastructure.Data;
using TravShell.Infrastructure.Repositories;


namespace TravShell.Infrastructure;
public static class Extensions
{

	public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
	{
		services.AddDbContext<DataContext>();
		services.AddScoped<IUsersRepository, UsersRepository>();
		services.AddScoped<ITripsRepository, TripsRepository>();
        services.AddScoped<IAttractionsRepository, AttractionsRepository>();
		services.AddScoped<ITagsRepository, TagsRepository>();
		services.AddScoped<IPublicTripsRepository, PublicTripsRepository>();
		//services.AddScoped<IReviewsRepository, ReviewsRepository>();
        return services;
	}

}
