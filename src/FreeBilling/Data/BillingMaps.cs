using AutoMapper;
using FreeBilling.Data.Entities;
using FreeBilling.Models;

namespace FreeBilling.Data;

public class BillingMaps : Profile
{
	public BillingMaps()
	{
		CreateMap<Customer, Customer>();
		CreateMap<Address, Address>();
		CreateMap<WorkTicket, WorkTicketModel>()
			.ReverseMap();
	}
}