using System;
using System.Collections.Generic;

using Xamarin.Forms;

using LocalDatabaseSample.Models;


/*Code-Behind class for the AssetItemPage view
 * 
 * This page contains functions such as
 * 
 * -being able to delete an asset's component by selecting it
 * -being able to add a component to the asset by clicking the add button
 * 
 */

namespace LocalDatabaseSample
{
	public partial class AssetItemPage : ContentPage
	{

		Asset asset;

		//Default Contsuctor
		public AssetItemPage()
		{
			InitializeComponent();
		}

		//Constructor
		public AssetItemPage(Asset asset)
		{
			this.asset = asset;
			InitializeComponent();
			PopulateListView(); //add items to listview
		}

		/*Event Handler. Triggers whenever the user presses the '+' aka Add button on the menu bar
         *This will add a new component to the database
         */
		async void OnItemAdded(object sender, System.EventArgs e)
		{
			App.Database3.InsertComponentAsync(asset);
		}

		//Populate the list view with the Asset's components
		async void PopulateListView()
		{
			var selectedAsset = await App.Database3.GetAssetAsync(asset.Id);
			var cList = App.Database3.GetComponents(selectedAsset).Result;
			ComponentListView.ItemsSource = cList;
		}

		/*Event Handler. Triggers whenever the user selects a component.
         *This will delete the currently selected component from the database
         */
		async void OnListItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
		{
			var item = e.SelectedItem as Component;
			await App.Database3.DeleteComponentAsync(item);
		}

		/*Event Handler. Triggers whenever the user presses the delete button.
         *This will delete the selected asset fromthe database.
         */
		async void Delete_Clicked(object sender, System.EventArgs e)
		{
			var btn = sender as Button;
			var assetObj = btn.BindingContext as Asset;

			//await App.Database2.DeleteAssetAsync(assetObj);
			await Navigation.PopAsync();
		}

		/*Event Handler. Triggers whenever the user presses the OK button.
         *This will direct the user back to the previous page.
         */
		async void Ok_Clicked(object sender, System.EventArgs e)
		{
			await Navigation.PopAsync();
		}

	}
}
