using System;
using System.Collections.Generic;
using System.Linq;
using LiteDB;
using Redux;
using TodoRedux.Actions;

namespace TodoRedux.Middleware
{
    public class LiteDbMiddleware<TState>
    {
        private readonly LiteDatabase _db;
        private IStore<TState> _store;
        private bool _isReplaying;

        public LiteDbMiddleware(String databaseName)
        {
            _db = new LiteDatabase(databaseName);
        }

        public Middleware<TState> CreateMiddleware()
        {
            var actionCollection = _db.GetCollection<ActionHistory>("ActionHistory");
            var mapper = new BsonMapper();

            return store =>
            {
                _store = store;
                return next => action =>
                {
                    if (action is ReplayHistoryAction)
                    {
                        foreach (var actionHistory in actionCollection.FindAll())
                        {
                            var typeOfAction = Type.GetType(actionHistory.Type);
                            var historicalAction = (IAction)mapper.ToObject(typeOfAction, actionHistory.Action);
                            store.Dispatch(historicalAction);
                        }
                        return next(action);
                    }
                    var result = next(action);
                    if (!_isReplaying)
                    {
                        var bsonAction = mapper.ToDocument(action);
                        actionCollection.Insert(new ActionHistory { Type = action.GetType().AssemblyQualifiedName, Action = bsonAction });
                    }
                    return result;
                };
            };
        }

        public void ReplayHistory()
        {
            _isReplaying = true;
            _store.Dispatch(new ReplayHistoryAction());
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
