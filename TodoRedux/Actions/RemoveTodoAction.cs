using System;
using Redux;

namespace TodoRedux.Actions
{
    internal class RemoveTodoAction : IUniqueIdAction
    {
        public Guid Id { get; internal set; }
        public int UniqueId { get; set; }
    }
}