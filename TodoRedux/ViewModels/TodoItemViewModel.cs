using System;
using System.ComponentModel;
using PropertyChanged;
using TodoRedux.Actions;
using Xamarin.Forms;

namespace TodoRedux.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class TodoItemViewModel
    {
		private readonly INavigation _navigation;

		public string Text { get; set; }

        public Command Save 
        {
            get 
            {
                return new Command(async () => {
                    App.Store.Dispatch(new AddTodoAction { Id = Guid.NewGuid(), Text = Text });
                    await _navigation.PopAsync();
                });
            }
        }

        public TodoItemViewModel(INavigation navigation)
        {
            this._navigation = navigation;
        }
    }
}
