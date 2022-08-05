using Flunt.Validations;

namespace IWantApp.Domain.Products
{
    public class Product : Entity
    {
        public string Description { get; private set; }
        public Guid CategoryId { get; private set; }
        public Category Category { get; set; }
        public bool HasStock { get; private set; }
        public bool Active { get; private set; } = true;
        public  decimal Price { get; private set; }

        private Product() { }

        public Product(string Name, string Description, Category Category, bool HasStock,string CreatedBy, decimal  Price)
        {
            this.Name = Name;
            this.Description = Description;
            this.Category = Category;
            this.HasStock = HasStock;
            this.CreatedBy = CreatedBy;
            this.EditedBy = CreatedBy;
            CreatedOn = DateTime.Now;
            EditedOn = DateTime.Now;

            Validation();
                this.Price = Price;
        }

        private void Validation()
        {
            var contract = new Contract<Product>()
                .IsNotNullOrEmpty(Name, "Name")
                .IsGreaterOrEqualsThan(Name, 3, "Name")
                .IsNotUrlOrEmpty(Description, "Description")
                .IsGreaterOrEqualsThan(Description, 3, "Description")
                .IsNotNull(Category, "Category", "CategoryId not found")
                .IsNotNullOrEmpty(CreatedBy, "CreatedBy")
                .IsNotNullOrEmpty(EditedBy, "EditedBy");
            AddNotifications(contract);

               


        }
    }
}
