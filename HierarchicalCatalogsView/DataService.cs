using HierarchicalCatalogsView.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HierarchicalCatalogsView
{
    public class DataService
    {
        private readonly IDbContextFactory<CatalogContext> _factory;
        private JsonSerializerOptions _options = new()
        {
            ReferenceHandler = ReferenceHandler.Preserve,
            WriteIndented = true
        };

        public DataService(IDbContextFactory<CatalogContext> factory)
        {
            _factory = factory;
        }

        public List<string> GetCatalogs(string currentCatalog)
        {
            try
            {
                using (var dataContext = _factory.CreateDbContext())
                {
                    Catalog catalog = dataContext.Catalogs.Where(n => n.CatalogName == currentCatalog).Include(c => c.ChildCatalogs).First();
                    return catalog.ChildCatalogs.Select(n => n.CatalogName).ToList();
                }
            }
            catch
            {
                return null;
            }
        }

        public List<string> GetRootCatalogs()
        {
            using (var dataContext = _factory.CreateDbContext())
                return dataContext.Catalogs.Where(p => p.ParentId == null).Select(n => n.CatalogName).ToList();
        }

        public bool TryImportCatalogs(Stream stream, string importTo)
        {
            try
            {
                Catalog catalog = JsonSerializer.Deserialize<Catalog>(GetJsonString(stream), _options);
                using (var dataContext = _factory.CreateDbContext())
                {
                    List<Catalog> catalogs;
                    if (importTo == null) catalogs = dataContext.Catalogs.Where(p => p.ParentId == null).ToList();
                    else
                    {
                        Catalog currentCatalog = dataContext.Catalogs.Where(n => n.CatalogName == importTo).Include(c => c.ChildCatalogs).First(); //Import into specific catalog
                        catalogs = currentCatalog.ChildCatalogs.ToList();
                        catalog.ParentCatalog = currentCatalog;
                    }

                    int amount = catalogs.Where(n => n.CatalogName.Contains(catalog.CatalogName)).Count(); //Add unique name in current catalog
                    if (amount > 0) catalog.CatalogName = catalog.CatalogName + $" ({amount})";

                    dataContext.Add(catalog);
                    dataContext.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public MemoryStream ExportCatalogs(string catalogName)
        {
            try
            {
                using (var dataContext = _factory.CreateDbContext())
                {
                    dataContext.Catalogs.Where(n => n.CatalogName == catalogName).Load();
                    Catalog catalog = dataContext.Catalogs.ToList().Where(n => n.CatalogName == catalogName).First();
                    catalog.ParentCatalog = null; //Make it a root catalog
                    string fileContent = JsonSerializer.Serialize(catalog, _options);
                    byte[] fileBytes = Encoding.UTF8.GetBytes(fileContent);
                    return new MemoryStream(fileBytes);
                }
            }
            catch
            {
                return null;
            }

        }

        private string GetJsonString(Stream stream)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return Encoding.UTF8.GetString(memoryStream.ToArray());
            }
        }
    }
}
