using System;
using System.Linq;
using System.Collections.Generic;
using TodoRedux.States;
using Xamarin.Forms;
using TodoRedux.Views;
using PropertyChanged;

namespace TodoRedux.ViewModels
{
    [AddINotifyPropertyChangedInterface]
	public class TodoListViewModel
    {
        private readonly INavigation _navigation;

        public List<TodoItem> Todos { get; set; }

        public Command Add 
        {
            get 
            {
                return new Command(async () => {
                    await _navigation.PushAsync(new TodoItemPage());
                });
            }
        }

        public TodoListViewModel(INavigation navigation)
        {
            this._navigation = navigation;
            Todos = new List<TodoItem>();
            App.Store.Subscribe(state => {
                Todos = state.Todos.ToList();
            });
        }
    }
}
