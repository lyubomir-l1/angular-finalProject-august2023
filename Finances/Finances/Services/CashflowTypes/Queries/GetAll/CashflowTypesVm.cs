using AutoMapper;
using Finances.Common.Mapping;
using Finances.Models;

namespace Finances.Services.CashflowTypes.Queries.GetAll
{
    public class CashflowTypesVm : IMapFrom<CashflowType>
    {
        public int Id { get; set; }

        public string? Description { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CashflowType, CashflowTypesVm>();
        }
    }
}
