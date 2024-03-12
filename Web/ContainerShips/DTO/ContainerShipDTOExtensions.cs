using Domain.Ship.Entities;
 
 namespace Web.ContainerShips.DTO;
 
 public static class ContainerShipDTOExtensions
 {
     public static ContainerShipResponse ToResponse(this ContainerShip model)
     {
         return new ContainerShipResponse
         {
             Id = model.Id,
             Name = model.Name,
             Type = model.Type,
             Country = model.Country,
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