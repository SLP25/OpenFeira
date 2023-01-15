
namespace OpenFeira.Data ;

public partial class ProductInStand
{
    public int StandId { get; set; }

    public int ProductId { get; set; }

    public int Stock { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Stand Stand { get; set; } = null!;
}
