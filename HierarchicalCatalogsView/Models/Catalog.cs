using System.Text.Json.Serialization;

namespace HierarchicalCatalogsView.Models
{
    public class Catalog
    {
        [JsonIgnore]
        public int CatalogId { get; set; }
        public string CatalogName { get; set; }
        public Catalog? ParentCatalog { get; set; }
        [JsonIgnore]
        public int? ParentId { get; set; }
        public ICollection<Catalog>? ChildCatalogs { get; set; }
    }
}
