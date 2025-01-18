using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravShell.Core.Entities;
using TravShell.Core.Repositories;

namespace TravShell.App.Tags;
public class TagService : ITagService
{

    private readonly ITagsRepository _tagsRepository;

    public TagService(ITagsRepository tagsRepository)
    {
        _tagsRepository = tagsRepository;
    }

    //public Task AttachTagToTrip(int tripId, Tag tag)
    //{
    //    throw new NotImplementedException();
    //}

    //public Task<Tag> CreateTag(int userId, string tagname)
    //{
    //    throw new NotImplementedException();
    //}

    //public Task DeleteTag(int tripId, Tag tag)
    //{
    //    throw new NotImplementedException();
    //}

    //public async Task<List<Tag>> GetAllTags()
    //{
    //    throw new NotImplementedException();
    //}

    public async Task<List<Tag>> GetAllUserTripTags(int userId)
    {
        return await _tagsRepository.GetAllUserTripTags(userId);
    }

    //public async Task<List<Tag>> GetTripTags(int userId, int tripId)
    //{
    //    throw new NotImplementedException();
    //}
}

