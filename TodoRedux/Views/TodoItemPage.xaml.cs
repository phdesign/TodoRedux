using System;
using TodoRedux.States;
using TodoRedux.ViewModels;
using Xamarin.Forms;

namespace TodoRedux.Views
{
	public partial class TodoItemPage : ContentPage
	{
		public TodoItemPage()
		{
			InitializeComponent();
            BindingContext = new TodoItemViewModel(Navigation);
		}
	}
}
