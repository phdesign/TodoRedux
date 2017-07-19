using System;
using LiteDB;
using Redux;

namespace TodoRedux.Middleware
{
    public class LiteDbMiddleware<TState>
    {
        private IStore<TState> _store;
        private bool _isReplaying;
        private readonly BsonMapper _mapper;
        private readonly LiteCollection<ActionHistory> _actionCollection;

        public LiteDbMiddleware(String databaseName)
        {
            var db = new LiteDatabase(databaseName);
            _actionCollection = db.GetCollection<ActionHistory>("ActionHistory");
            _mapper = new BsonMapper();
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

                    var bsonAction = _mapper.ToDocument(action);
                    _actionCollection.Insert(new ActionHistory { Type = action.GetType().AssemblyQualifiedName, Action = bsonAction });
                    return result;
                };
            };
        }

        public void ReplayHistory()
        {
            _isReplaying = true;
            foreach (var actionHistory in _actionCollection.FindAll())
            {
                var typeOfAction = Type.GetType(actionHistory.Type);
                var historicalAction = (IAction)_mapper.ToObject(typeOfAction, actionHistory.Action);
                _store.Dispatch(historicalAction);
            }
            _isReplaying = false;
        }   
    }

    public class ActionHistory
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public BsonDocument Action { get; set; }
    }
}
