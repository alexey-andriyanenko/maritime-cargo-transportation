﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Ship.DTO;
using Domain.Ship.Entities;

namespace Domain.Ship.Interfaces;

public interface IContainerShipRepository
{
    public Task<List<ContainerShip>> GetListAsync(int companyId);
    public Task<ContainerShip?> GetByIdAsync(int id, int companyId);
    public Task<int?> CreateAsync(ContainerShipCreateRequest ship);
}
