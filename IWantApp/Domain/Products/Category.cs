using Flunt.Validations;

namespace IWantApp.Domain.Products;

public class Category : Entity
{

    public bool Active { get;private set; }
    
    

    /// <summary>
    /// constructor created to analyze if Name, CreatedBy, and EditedBy are null or empty. If they are null or empty, so will be generated a notification
    /// </summary>
    /// <param name="Name"></param>
    /// <param name="CreatedBy"></param>
    /// <param name="EditedBy"></param>

    public Category(string Name, string CreatedBy, string EditedBy)
    {
        this.Name = Name;
        Active = true;
        this.CreatedBy = CreatedBy;
        this.EditedBy = EditedBy;
        CreatedOn = DateTime.Now;
        EditedOn = DateTime.Now;

        Validation();
    }

    private void Validation()
    {
        var contract = new Contract<Category>()
            .IsNotNullOrEmpty(Name, "Name", "nome está nulo ou vazio...")
            .IsGreaterOrEqualsThan(Name, 3, "Name")
            .IsNotNullOrEmpty(CreatedBy, "CreatedBy")
            .IsNotNullOrEmpty(EditedBy, "EditedBy");

        AddNotifications(contract);
    }

    public void EditInfo(string Name, bool Active)
    {
        this.Name = Name;
        this.Active = Active;

        Validation();
    }


}
