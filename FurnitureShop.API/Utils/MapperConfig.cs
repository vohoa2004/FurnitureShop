﻿using AutoMapper;
using FurnitureShop.API.DTOs.RequestDTOs;
using FurnitureShop.BLL.BusinessModels;
using FurnitureShop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FurnitureShop.BLL.Utils
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<SearchRequestDTO, SearchFurnitureModel>();
            CreateMap<FurnitureRequestDTO, FurnitureModel>().ReverseMap();
            CreateMap<Furniture, FurnitureModel>();
            CreateMap<FurnitureModel, Furniture>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<RegisterRequestDTO, RegisterModel>();
            CreateMap<RegisterModel, Account>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            // map from order request dto to order model
            CreateMap<OrderLineRequestDTO, OrderLineModel>();
            CreateMap<OrderRequestDTO, OrderModel>()
                .ForMember(dest => dest.OrderLines, opt => opt.MapFrom(src => src.Lines));

            // map from order model to order
            CreateMap<OrderLineModel, OrderLine>();
            CreateMap<OrderModel, Order>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.OrderLines, opt => opt.MapFrom(src => src.OrderLines));

            // map from order 
            CreateMap<OrderLine, OrderLineModel>()
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));
            CreateMap<Order, OrderModel>()
                .ForMember(dest => dest.OrderLines, opt => opt.MapFrom(src => src.OrderLines));
        }
    }
}
