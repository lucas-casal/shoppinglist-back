namespace Shoppinglist_back.Dtos.ProductDtos;

public class GetAllProductDto
{
    public string ProductName { get; set; }
    public int Limit { get; set; }
    public int Skip { get; set; }
}
