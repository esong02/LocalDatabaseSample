using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using SQLite;
using SQLiteNetExtensionsAsync.Extensions;

using LocalDatabaseSample.Models;

/* Database class that uses the SQLite-Net Extensions
 * This class holds all the read & write operations & access to the SQLite database
 * 
 */ 
namespace LocalDatabaseSample.Database
{
    public class ExtendedDatabase
    {
        readonly SQLiteAsyncConnection db;

        /* Upon creation. Drop the 2 tables, and create the 2 tables to reset the database.
         * Requires the .db3 file path to access the database
         */ 
		public ExtendedDatabase(string dbPath)
		{
            db = new SQLiteAsyncConnection(dbPath);
			//db.DropTableAsync<Asset>().Wait();
			//db.DropTableAsync<Component>().Wait();

            db.CreateTableAsync<Asset>().Wait();
            db.CreateTableAsync<Component>().Wait();
		}

		/* Creates a new component.
		 * Map it to Parent Asset.
		 * Insert into database.
		 * Update Parent Asset in database.
         *
         */
		public void InsertComponentAsync(Asset asset)
        {
            var component = new Component
            {
                Name = "Hello"
            };

            db.InsertAsync(component);
            asset.ComponentList = GetComponents(asset).Result;
            asset.ComponentList.Add(component);
            db.UpdateWithChildrenAsync(asset);
        }

		/* Retrieves a list of Assets within the Asset Table.
         */
		public Task<List<Asset>> GetAssetsAsync(){
            return db.Table<Asset>().ToListAsync();
        }

		/* Retrieves an Asset from the Asset table via Asset Id.
         */
		public Task<Asset> GetAssetAsync(int id)
		{
			return db.Table<Asset>().Where(i => i.Id == id).FirstOrDefaultAsync();
		}

		/* Retrieves a list of Components from the Component Table by Asset ID
         */
		public Task<List<Component>> GetComponents(Asset asset){
            return db.Table<Component>().Where(i => i.ParentAssetId == asset.Id).ToListAsync();
        }

		/* Save an Asset into the Asset Table
         */
		public Task<int> SaveAssetAsync(Asset asset)
		{
            //if Asset exists in database
			if (asset.Id != 0)
			{
				return db.UpdateAsync(asset);
			}
			else
			{
                //otherwise add to database
				return db.InsertAsync(asset);
			}
		}

		/* Delete an Asset within the Asset Table.
         */
		public Task<int> DeleteAssetAsync(Asset asset)
		{
			return db.DeleteAsync(asset);
		}

		/* Delete a Component within the Component Table.
         */
		public Task<int> DeleteComponentAsync(Component component)
		{
			return db.DeleteAsync(component);
		}

		/* Clear all information from the Database by dropping & recreating the Asset & Component Tables
         */
		public void Clear(){
			db.DropTableAsync<Asset>().Wait();
			db.DropTableAsync<Component>().Wait();

			db.CreateTableAsync<Asset>().Wait();
			db.CreateTableAsync<Component>().Wait();
        }

		/* Create a Sample Asset.
		 * Requires string name to name the Asset.
		 * Creates 4 sample Components to add to the Asset
         */
		public void CreateAsset(string name){

            var c1 = new Component
            {
                Name = "Top-Left Corner"
            };
            db.InsertAsync(c1); //insert to db

			var c2 = new Component
			{
				Name = "Center"
			};
            db.InsertAsync(c2); //insert to db

			var c3 = new Component
			{
				Name = "Bottom-Left Corner"
			};
            db.InsertAsync(c3); //insert to db

			var c4 = new Component
			{
				Name = "Top-Right Corner"
			};
            db.InsertAsync(c4); //insert to db

			var a1 = new Asset
			{
				Name = name,
                ComponentList = new List<Component> { c1, c2, c3, c4 } //Maps relationship to Asset
			};

			db.InsertAsync(a1); //Insert Asset to db
            db.UpdateWithChildrenAsync(a1); //Update Children in a1
        }
	}
}
