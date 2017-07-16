using System.Collections.Immutable;

namespace TodoRedux.States
{
    public class ApplicationState
    {
        public ImmutableArray<TodoItem> Todos { get; set; }

        public ApplicationState()
        {
            Todos = ImmutableArray<TodoItem>.Empty;
        }
    }
}
