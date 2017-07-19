using System;
using LiteDB;
using Redux;

namespace TodoRedux.Middleware
{
    public class LiteDbMiddleware<TState>
    {
        private IStore<TState> _store;
        private readonly LiteCollection<ActionHistory> _actionCollection;
        private bool _isReplaying;

        public LiteDbMiddleware(String databaseName)
        {
            var db = new LiteDatabase(databaseName);
            _actionCollection = db.GetCollection<ActionHistory>("ActionHistory");
        }

        public Middleware<TState> CreateMiddleware()
        {
            return store =>
            {
                _store = store;
                return next => action =>
                {
                    var result = next(action);
                    if (_isReplaying) return result;
                    _actionCollection.Insert(new ActionHistory { Action = action });
                    return result;
                };
            };
        }

        public void ReplayHistory()
        {
            _isReplaying = true;
            foreach (var actionHistory in _actionCollection.FindAll())
            {
                _store.Dispatch(actionHistory.Action);
            }
            _isReplaying = false;
        }   
    }

    public class ActionHistory
    {
        public int Id { get; set; }
        public IAction Action { get; set; }
    }
}
