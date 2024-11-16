using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentX.Models;
using RentX.RentXDTOs;

namespace RentX.Handlers;

public class PropertyHandler
{
    public readonly RentXContext _db;

    //constructor
    public PropertyHandler(RentXContext context) {
        _db = context;
    }

    public GetPropertyDTO? GetPropertyDetails(int propertyId)
    {
        var prop = _db.Properties.Find(propertyId);
        if (prop == null)
        {
            return null;
        }

        GetPropertyDTO returnProp = mapToDto(prop);
        return returnProp;
    }

    public async Task<List<GetPropertyDTO>> GetAllProperties()
    {
        var properties = await _db.Properties.ToListAsync();
        return properties.Select(p => mapToDto(p)).ToList();
    }

    public async Task<int> CreateProperty(PostPropertyDTO request)
    {
        // Validations before entering the fields [check UserId and PropertyType]
        if (
            (await _db.Users.FindAsync(request.UserId) == null) || 
            (await _db.PropertyTypes.FindAsync(request.PropertyTypeId) == null))
        {
            return -1;
        }

        var res = await _db.Properties.AddAsync(mapDtoToProperty(request));
        int rowsAffected = _db.SaveChanges();
        if (rowsAffected > 0)
        {
            return res.Entity.Id;
        }

        return -1;
    }

    public bool DeleteProperty(int id)
    {
        var property = _db.Properties.Find(id);
        if (property == null)
        {
            return false;
        }

        _db.Properties.Remove(property);
        var rowsAffected = _db.SaveChanges();

        return rowsAffected > 0 ? true : false;

    }

    public bool ModifyProperty(int id, PostPropertyDTO request)
    {
        var prop = _db.Properties.Find(id);
        if (prop == null)
        {
            return false;
        }
        Property modifiedProperty = mapDtoToProperty(request);

        prop.PropertyType = modifiedProperty.PropertyType;
        prop.PropertyTypeId = modifiedProperty.PropertyTypeId;
        prop.Address = modifiedProperty.Address;
        prop.RentLow = modifiedProperty.RentLow;
        prop.RentHigh = modifiedProperty.RentHigh;
        prop.CreatorUserId = modifiedProperty.CreatorUserId;
        prop.City = modifiedProperty.City;

        var rowsAffected = _db.SaveChanges();
        if (rowsAffected > 0)
        {
            return true;
        }
        return false;
    }
    

    #region Dto To Property Mappers
    public GetPropertyDTO mapToDto(Property property)
    {

        PropertyType? propertyType = _db.PropertyTypes.Find(property.PropertyTypeId);
        string propertyTypeName = propertyType?.PropertyTypeName ?? "";

        return new GetPropertyDTO
        {
            Id = property.Id,
            Address = property.Address,
            RentHigh = property.RentHigh,
            RentLow = property.RentLow,
            City = property.City,
            PropertyTypeId = property.PropertyTypeId,
            PropertyName = propertyTypeName
        };
    }
    public Property mapDtoToProperty(PostPropertyDTO request)
    {
        PropertyType? propertyType = _db.PropertyTypes.Find(request.PropertyTypeId);
        string propertyTypeName = propertyType?.PropertyTypeName ?? "";

        return new Property
        {
            Address = request.Address,
            RentHigh = request.RentHigh,
            RentLow = request.RentLow,
            City = request.City,
            PropertyTypeId = request.PropertyTypeId,
            CreatorUserId = request.UserId,
            PropertyName = propertyTypeName
        };
    }
    #endregion

}
