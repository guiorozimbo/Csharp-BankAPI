﻿using AutoMapper;
using BankAPI.Models;

namespace BankAPI.Profiles
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            // CreateMap<Source, Destination>();
            CreateMap<RegisterNewAccountModel, Account>();
            CreateMap<UpdateAccountModel, Account>();
            CreateMap<Account, GetAccountModel>();
        }
    }
}
