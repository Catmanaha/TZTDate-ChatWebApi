using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetHandler : IRequestHandler<GetCommand, PrivateChat>
{
    private readonly TZTDateDbContext tZTDateDbContext;
    private readonly ISender sender;

    public GetHandler(TZTDateDbContext tZTDateDbContext, ISender sender)
    {
        this.tZTDateDbContext = tZTDateDbContext;
        this.sender = sender;
    }

    public async Task<PrivateChat?> Handle(GetCommand request, CancellationToken cancellationToken)
    {
        var currentUser = await sender.Send(new FindByIdCommand { Id = request.CurrentUserId });
        var companionUser = await sender.Send(new FindByIdCommand { Id = request.CompanionUserId });

        var privateChats = await this.tZTDateDbContext.PrivateChats.FirstOrDefaultAsync(o => o.PrivateChatHashName.Contains(currentUser.Email) && o.PrivateChatHashName.Contains(companionUser.Email));
        if (privateChats == null)
        {
            return null;
        }
        privateChats.Messages = await this.tZTDateDbContext.Message.Where(m => m.PrivateChatId == privateChats.Id).ToListAsync();
        return privateChats;
    }
}