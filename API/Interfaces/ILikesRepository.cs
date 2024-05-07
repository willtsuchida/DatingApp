using API.Entities;

namespace API;

public interface ILikesRepository
{
    Task<UserLike> GetUserLike(int sourceUserId, int targetUserId);
    Task<AppUser> GetUserWithLikes(int userId);
    Task<PagedList<LikeDto>> GetUserLikes(LikesParams likesParams); //predicate means do they want to get the users they liked or the users they r liked by..
}
