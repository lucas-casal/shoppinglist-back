using AutoMapper;
using Shoppinglist_back.Dtos.ProductDtos;
using Shoppinglist_back.Models;

namespace Shoppinglist_back.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<CreateProductDto, Product>();
        CreateMap<Product, ReadProductAloneDto>();
        CreateMap<UpdateProductDto, Product>();
    }
}
