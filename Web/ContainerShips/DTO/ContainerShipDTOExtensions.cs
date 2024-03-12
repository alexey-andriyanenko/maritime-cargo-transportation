using Domain.Ship.Entities;
using Web.Countries.DTO;

namespace Web.ContainerShips.DTO;
 
 public static class ContainerShipDTOExtensions
 {
     public static ContainerShipResponse ToResponse(this ContainerShip model, string baseUrl)
     {
         return new ContainerShipResponse
         {
             Id = model.Id,
             Name = model.Name,
             Type = model.Type,
             Country = model.Country.ToResponse(baseUrl),
             SizeType = model.SizeType,
             Containers = model.Containers.Select(c => new ContainerWithinShip
             {
                 Id = c.Id,
                 Cargo = c.Cargo,
                 Type = c.Type
             }).ToList()
         };
     }
 }