﻿using AutoMapper;
using FreeCourse.Services.Order.Application.Dtos;
using FreeCourse.Services.Order.Domain.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Application.Mapping
{
    public class CustomMapping : Profile
    {
        public CustomMapping() { 
            CreateMap<Domain.OrderAggregate.Order,OrderDto>().ReverseMap();
            CreateMap<Domain.OrderAggregate.OrderItem,OrderItemDto>().ReverseMap();
            CreateMap<Address,AddressDto>().ReverseMap();
        }
    }
}
