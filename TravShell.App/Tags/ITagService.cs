using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravShell.Core.Entities;

namespace TravShell.App.Tags;
public interface ITagService
{


    //Task<Tag> CreateTag(int userId, string tagname);

    //Task AttachTagToTrip(int tripId, Tag tag);

    //Task DeleteTag(int tripId, Tag tag);

    //Task<List<Tag>> GetTripTags(int userId, int tripId);

    //Task<List<Tag>> GetAllTags();

    Task<List<Tag>> GetAllUserTripTags(int userId);



}

