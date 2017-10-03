using System;
using System.Collections.Generic;
using LocalDatabaseSample.Models;

using Xamarin.Forms;

namespace LocalDatabaseSample
{
	public partial class AssetListPage : ContentPage
	{
		public AssetListPage()
		{
			InitializeComponent();
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			((App)App.Current).ResumeAtTodoId = -1;

			AssetListView.ItemsSource = await App.Database3.GetAssetsAsync();

		}

		async void OnItemAdded(object sender, System.EventArgs e)
		{
			App.Database3.CreateAsset("CVC Head Office");

			var aList = App.Database3.GetAssetsAsync().Result;

			var mostRecent = App.Database3.GetAssetAsync(aList.Count).Result;

			await Navigation.PushAsync(new AssetItemPage(mostRecent)
			{
				BindingContext = mostRecent
			});
		}

		void OnListItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
		{
			//((App)App.Current).ResumeAtTodoId = (e.SelectedItem as Asset);
			((App)App.Current).ResumeAt = (e.SelectedItem as Asset).Id;
			System.Diagnostics.Debug.WriteLine("setting ResumeAt = " + (e.SelectedItem as Asset).Id);
			/*
            await Navigation.PushAsync(new TodoItemPage
            {
                BindingContext = e.SelectedItem as Asset
            });
            */
		}


		async void Inspection_Clicked(object sender, System.EventArgs e)
		{
			var btn = sender as Button;
			var asset = btn.BindingContext as Asset;
			await Navigation.PushAsync(new AssetItemPage(asset)
			{
				BindingContext = asset
			});
		}

		void Reset_Clicked(object sender, System.EventArgs e)
		{
			App.Database3.Clear();
		}
	}
}
