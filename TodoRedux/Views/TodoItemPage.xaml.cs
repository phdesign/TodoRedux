using System;
using TodoRedux.States;
using TodoRedux.ViewModels;
using Xamarin.Forms;

namespace TodoRedux.Views
{
	public partial class TodoItemPage : ContentPage
	{
		public TodoItemPage() : this(null)
		{
		}

        public TodoItemPage(TodoItem model)
		{
			InitializeComponent();
			BindingContext = new TodoItemViewModel(Navigation, model);
		}
	}
}
