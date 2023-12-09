using ChatrBox.Data;

namespace ChatrBox.CoreComponents
{
    public class CommunityTools
    {
        private readonly ApplicationDbContext _context;

        public CommunityTools(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Create a new community tools instance in place for quick access to the tools
        /// </summary>
        /// <param name="context">Database context to search</param>
        /// <returns>A new instance of CommunityTools that can be used directly or saved for later use</returns>
        public static CommunityTools Create(ApplicationDbContext context)
        {
            return new CommunityTools(context);
        }

        /// <summary>
        /// Takes the id of a community and retrieves the community information from the database
        /// </summary>
        /// <param name="id">Id of the community to retrieve</param>
        /// <returns>An active community or a dummy community "Error Community Not Found" if it can't be found</returns>
        public Community GetCommunity(int id)
        {
            Community community = _context.Communities.Find(id) ?? new Community
            {
                Id = id,
                Name = "Error Community Not Found",
                Description = $"Could not locate community with #{id}"
            };

            return community;
        }

        /// <summary>
        /// Get's a community by Id and checks what type of relationship the user has to said community.
        /// </summary>
        /// <param name="communityId">Community to retrieve</param>
        /// <param name="userId">User to check</param>
        /// <param name="userRole">Outputs user's role withing the community</param>
        /// <returns>The community matching the <paramref name="communityId"/> provided</returns>
        public Community GetCommunity(int communityId, string userId, out CommunityRole userRole)
        {
            var community = GetCommunity(communityId);
            var role = CommunityRole.Nonmember;

            var communityUser = _context.CommunityUsers
                .Where(cu => cu.CommunityId == communityId)
                .Where(cu => cu.ChatrId == userId)
                .ToList();

            //if the user is the owner
            if (community.OwnerId == userId)
                role = CommunityRole.Owner;

            //if the user is not the owner but is a moderator
            else if (communityUser.Any() && communityUser[0].IsModerator)
                role = CommunityRole.Moderator;

            //if the member is a user, but not the owner or the moderator
            else if (communityUser.Any())
                role = CommunityRole.Member;

            //will return original setting (Nonmember) if not modified above.
            userRole = role;
            return community;
        }

        public CommunityUser GetCommunityUser(int communityId, string userId, out CommunityRole role) 
        {
            var user = _context.CommunityUsers
                .FirstOrDefault(cu => cu.CommunityId == communityId && cu.ChatrId == userId);

            role = CommunityRole.Nonmember; //default state

            if (user != null)
            {
                if (GetCommunity(communityId).OwnerId == userId)
                    role = CommunityRole.Owner;
                else if (user.IsModerator)
                    role = CommunityRole.Moderator;
                else
                    role = CommunityRole.Member;

                return user;
            }

            return new CommunityUser
            {
                Id = -1,
                CommunityId = -1,
                ChatrId = "NOT FOUND"
            };
        }
    }

    public enum CommunityRole
    {
        /// <summary>
        /// Chat'r is not a member of this community.
        /// </summary>
        Nonmember,

        /// <summary>
        /// Chat'r has basic member privilages
        /// </summary>
        Member,

        /// <summary>
        /// Chat'r is a community moderator
        /// </summary>
        Moderator,

        /// <summary>
        /// Chat'r owns community
        /// </summary>
        Owner
    }
}

