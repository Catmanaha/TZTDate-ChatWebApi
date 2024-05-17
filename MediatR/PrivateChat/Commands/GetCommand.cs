using MediatR;

public class GetCommand : IRequest<PrivateChat>
{
    public int CurrentUserId { get; set; }
    public int CompanionUserId { get; set; }
}