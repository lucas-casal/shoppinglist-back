using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shoppinglist_back.Data;
using Shoppinglist_back.Dtos.ProductDtos;
using Shoppinglist_back.Models;

namespace Shoppinglist_back.Services;

public class ProductService
{
    private IMapper _mapper;
    private ShoppinglisterContext _context;

    public ProductService(IMapper mapper, ShoppinglisterContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<ReadProductAloneDto> Create(CreateProductDto dto)
    {
        dto.Name = dto.Name.TrimStart().TrimEnd();
        var product = await _context.Product.FirstOrDefaultAsync(p => p.Name.ToUpper() == dto.Name.ToUpper());

        if (product is not null) throw new InvalidOperationException("Esse produto já existe!");

        var newProduct = _mapper.Map<Product>(dto);
        _context.Product.Add(newProduct);
        await _context.SaveChangesAsync();
        return _mapper.Map<ReadProductAloneDto>(newProduct);
    }

    public async Task<ReadProductAloneDto> GetOneById(int productId)
    {
        var product = await _context.Product.FirstOrDefaultAsync(p => p.Id == productId);

        if (product is null) throw new NullReferenceException("Esse produto não existe!");

        return _mapper.Map<ReadProductAloneDto>(product);
    }

    public async Task<IEnumerable<ReadProductAloneDto>> GetAll(GetAllProductDto dto)
    {
        dto.ProductName = dto.ProductName is not null ? dto.ProductName.TrimStart().TrimEnd() : "";
        var products = await _context.Product.Select(p => new ReadProductAloneDto { Id = p.Id, Name = p.Name })
                                              .Where(p => p.Name.ToUpper().StartsWith(dto.ProductName.ToUpper()))
                                              .OrderBy(p => p.Name)
                                              .Skip(dto.Skip)
                                              .Take(dto.Limit)
                                              .ToListAsync();

        return products;
    }

    public async Task<ReadProductAloneDto> UpdateOne(int productId, UpdateProductDto dto)
    {
        dto.Name = dto.Name.TrimStart().TrimEnd();
        var homonymous = await _context.Product.FirstOrDefaultAsync(p => p.Name.ToUpper() == dto.Name.ToUpper());

        if (homonymous is not null) throw new InvalidOperationException($"Esse produto já existe com id={homonymous.Id}!");

        var product = await _context.Product.FirstOrDefaultAsync(p => p.Id == productId);

        if (product is null) throw new NullReferenceException("Esse produto não existe!");

        product.Name = dto.Name;

        await _context.SaveChangesAsync();

        return _mapper.Map<ReadProductAloneDto>(product);
    }

    public async Task DeleteOne(int productId)
    {
        var product = await _context.Product.FirstOrDefaultAsync(p => p.Id == productId);

        if (product is null) throw new NullReferenceException("Esse produto não existe!");

        _context.Product.Remove(product);

        await _context.SaveChangesAsync();
    }

}
