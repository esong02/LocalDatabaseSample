using System;
using SQLite;
using SQLiteNetExtensions.Attributes;

/* Component Model for Extended Database
 * Has a One-Many relationship with Asset
 * 
 */

namespace LocalDatabaseSample.Models
{
    public class Component
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        [ForeignKey(typeof(Asset))]
        public int ParentAssetId { get; set; }
        public int ParentComponentId { get; set; }
        public string LocationDescription { get; set; }
        public double Northing { get; set; }
        public double Easting { get; set; }
        public string Action { get; set; }

        [ManyToOne]
        public Asset Asset { get; set; }
    }
}
