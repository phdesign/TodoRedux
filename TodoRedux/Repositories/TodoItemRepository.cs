using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoRedux.Models;

namespace TodoRedux.Repositories
{
	public class TodoItemRepository
	{
        private List<TodoItem> _items;

        public TodoItemRepository()
        {
            _items = new List<TodoItem>() {
                new TodoItem { ID = 1, Name = "Add your first Todo", Done = false }
            };
        }

        public Task<List<TodoItem>> GetItemsAsync()
		{
            return Task.FromResult(_items);
		}

		public async Task<List<TodoItem>> GetItemsNotDoneAsync()
		{
            var items = await GetItemsAsync();
            return items.Where(x => !x.Done).ToList();
		}

		public Task<TodoItem> GetItemAsync(int id)
		{
            return Task.FromResult(_items.FirstOrDefault(x => x.ID == id));
		}

		public Task<int> SaveItemAsync(TodoItem item)
		{
            var nextId = _items.Max(x => x.ID) + 1;
            item.ID = nextId;
            _items.Add(item);
            return Task.FromResult(item.ID);
		}

		public async Task<int> DeleteItemAsync(TodoItem item)
		{
            var toRemove = await GetItemAsync(item.ID);
            if (toRemove != null)
                _items.Remove(toRemove);
            return item.ID;
		}
	}
}

