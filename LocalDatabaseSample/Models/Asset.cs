using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

/* Asset Model for Extended Database
 * Has a Many-One relationship with Component
 * 
 */

namespace LocalDatabaseSample.Models
{
    public class Asset
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Comments { get; set; }
        public string Address { get; set; }
        public string LocationDescription { get; set; }
        public string InspectionString { get; set; }
        public bool HasInspectionType { get; set; }
        public string ProgressIcon { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Component> ComponentList { get; set; }
    }
}
