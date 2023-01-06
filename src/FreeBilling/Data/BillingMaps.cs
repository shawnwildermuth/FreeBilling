using AutoMapper;
using FreeBilling.Data.Entities;

namespace FreeBilling.Data;

public class BillingMaps : Profile
{
	public BillingMaps()
	{
		CreateMap<Customer, Customer>();
		CreateMap<Address, Address>();
	}
}