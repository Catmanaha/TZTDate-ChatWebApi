using MediatR;

public class AddHandler : IRequestHandler<AddCommand>
{
    private readonly TZTDateDbContext tZTDateDbContext;

    public AddHandler(TZTDateDbContext tZTDateDbContext)
    {
        this.tZTDateDbContext = tZTDateDbContext;
    }
    public async Task Handle(AddCommand request, CancellationToken cancellationToken)
    {
        await tZTDateDbContext.PrivateChats
           .AddAsync(new PrivateChat
           {
               PrivateChatHashName = request.NewPrivateChatHashName,
               Messages = new List<Message>()
           });
        await tZTDateDbContext.SaveChangesAsync();
    }
}