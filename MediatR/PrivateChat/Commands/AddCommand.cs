using MediatR;

public class AddCommand : IRequest
{
    public string NewPrivateChatHashName { get; set; }
}