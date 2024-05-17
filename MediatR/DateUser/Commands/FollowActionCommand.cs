using MediatR;

public class FollowActionCommand : IRequest
{
    public int currentUserId { get; set; }
    public int userToActionId { get; set; }
}