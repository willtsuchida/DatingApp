using API.Data;
using API.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public UserRepository(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<MemberDto> GetMemberAsync(string username)
    {
        // WITHOUT AutoMapper
        // return await _context.Users
        //     .Where(x => x.UserName == username)
        //     .Select(user => new MemberDto{
        //         Id = user.Id,
        //         UserName = user.UserName
        //         etc....
        //     }) // .Select -> Takes our User into a new thing "MemberDto"

        return await _context.Users.Where(x => x.UserName == username)
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
        //Project Faz o Include automatico pra pegar as Photos
    }

    public async Task<IEnumerable<MemberDto>> GetMembersAsync()
    {
        return await _context.Users
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<AppUser> GetUserByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<AppUser> GetUserByUsernameAsync(string username)
    {
        return await _context.Users
            .Include(p => p.Photos) //Creates a infinite cycle/loop, solve with dto
            .FirstOrDefaultAsync(x => x.UserName == username); //ou SingleOrDefauult
    }

    public async Task<IEnumerable<AppUser>> GetUsersAsync()
    {
        //to get related data, use INCLUDE (in this case User has relation with Photos)
        return await _context.Users.
            Include(p => p.Photos)
            .ToListAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0; //0 = false, nothing saved into db;
    }

    public void Update(AppUser user) //not really useful
    {
        _context.Entry(user).State = EntityState.Modified; //nao salva, so informa o EF que uma entidade monitorada foi alterada
    }
}
