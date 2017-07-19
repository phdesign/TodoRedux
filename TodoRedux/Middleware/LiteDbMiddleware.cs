using System;
using System.Linq;
using LiteDB;
using Redux;

namespace TodoRedux.Middleware
{
    public class LiteDbMiddleware
    {
        public static Middleware<TState> CreateMiddleware<TState>(String databaseName)
        {
            var db = new LiteDatabase(databaseName);
            var actionCollection = db.GetCollection<ActionHistory>("ActionHistory");
            var mapper = new BsonMapper();

            return store =>
            {
                var actions = actionCollection.FindAll().ToList();
                foreach (var actionHistory in actions)
                {
                    var typeOfAction = Type.GetType(actionHistory.Type);
                    var action = (IAction)mapper.ToObject(typeOfAction, actionHistory.Action);
                    store.Dispatch(action);
                }

                return next => action =>
                {
                    var result = next(action);
                    var bsonAction = mapper.ToDocument(action);
                    actionCollection.Insert(new ActionHistory { Type = action.GetType().AssemblyQualifiedName, Action = bsonAction });
                    return result;
                };
            };
        }
    }

    public class ActionHistory
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public BsonDocument Action { get; set; }
    }
}
