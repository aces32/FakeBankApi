using AutoMapper;
using FakeBank.BankAPI.Application.Features.Account.Command.CreateAccount;
using FakeBank.BankAPI.Application.Features.Account.Queries.GetAccountBalance;
using FakeBank.BankAPI.Application.Features.Transfers.Command.IntraBankTransfers;
using FakeBank.BankAPI.Application.Features.Transfers.Queries.GetTransferHistory;
using FakeBank.BankAPI.Domain.Entities;

namespace FakeBank.BankAPI.Application.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Mapping For Accounts
            CreateMap<Accounts, CreatedAccountViewModel>()
                .ForMember(
                    dest => dest.AccountBalance,
                    opt => opt.MapFrom(src => src.ClearedBalance)
                ).ReverseMap();

            CreateMap<CreateAccountCommand, Accounts>()
                .ForMember(
                    dest => dest.ClearedBalance,
                    opt => opt.MapFrom(src => src.InitialDepAmount)
                )
                .ForMember(
                    dest => dest.CustomersID,
                    opt => opt.MapFrom(src => src.CustomerID)
                )
                .ForMember(
                    dest => dest.AccountType,
                    opt => opt.MapFrom(src => src.AccountType.ToLower())
                )
                .ReverseMap();

            CreateMap<Accounts, GetAccountViewModel>()
                .ForMember(
                    dest => dest.AccountBalance,
                    opt => opt.MapFrom(src => src.ClearedBalance)
                )
                .ReverseMap();


            // Mapping for Transactions
            CreateMap<IntraBankTransfersCommand, TransactionHistory>().ReverseMap();
            CreateMap<TransactionHistory, IntraBankTransfersViewModel>().ReverseMap();
            CreateMap<TransactionHistory, TransferHistoryDto>()
                .ForMember(
                    dest => dest.DebitCreditIndicator,
                    opt => opt.MapFrom(src => src.DebCredFlag == 'C' ? "Credit" : "Debit")
                )
                .ReverseMap();
        }
    }
}
