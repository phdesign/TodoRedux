using System;
using Redux;
using TodoRedux.Actions;

namespace TodoRedux.Middleware
{
	public class UniqueIdMiddleware
	{
		private int nextId = 0;

		public Middleware<TState> CreateMiddleware<TState>()
		{
			return store => next => action =>
            {
                if (action is IUniqueIdAction)
                {
                    ((IUniqueIdAction)action).UniqueId = nextId++;
                }
				return next(action);
			};
		}
	}
}
