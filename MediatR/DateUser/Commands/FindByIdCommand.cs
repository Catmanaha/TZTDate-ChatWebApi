using MediatR;

public class FindByIdCommand : IRequest<User>
{
    public int Id { get; set; }
}