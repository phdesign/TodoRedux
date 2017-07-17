using System;
using Redux;

namespace TodoRedux.Actions
{
    internal class UpdateTodoAction : IUniqueIdAction
    {
        public string Text { get; internal set; }
        public Guid Id { get; internal set; }
        public int UniqueId { get; set; }
    }
}