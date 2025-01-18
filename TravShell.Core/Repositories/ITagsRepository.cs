using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravShell.Core.Entities;

namespace TravShell.Core.Repositories;
public interface ITagsRepository
{
    Task<Tag> CreateTag(int userId, string tagname);

    Task AttachTagToTrip(int tripId, Tag tag);

    Task RemoveTagFromTrip(int tripId, Tag tag);

    Task<List<Tag>> GetTripTags( int tripId);

    Task<List<Tag>> GetAllTags();

    Task<List<Tag>> GetAllUserTripTags(int userId);


}

