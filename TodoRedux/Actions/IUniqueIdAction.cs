using Redux;

namespace TodoRedux.Actions
{
    public interface IUniqueIdAction : IAction
    {
        int UniqueId { get; set; }
    }
}
