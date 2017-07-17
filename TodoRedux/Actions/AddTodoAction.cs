using System;
using Redux;

namespace TodoRedux.Actions
{
    internal class AddTodoAction : IUniqueIdAction
    {
        public string Text { get; internal set; }
        public Guid Id { get; internal set; }
        public int UniqueId { get; set;  }
    }
}