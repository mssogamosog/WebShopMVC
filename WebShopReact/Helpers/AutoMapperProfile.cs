using AutoMapper;

using WebShopMVC.Models;
using WebShopReact.Models;

namespace WebShopReact.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
			CreateMap<Customer, CustomerDTO>();
			CreateMap<CustomerDTO, Customer>();
        }
    }
}