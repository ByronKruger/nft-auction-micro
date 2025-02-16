using System.Numerics;
using AutoMapper;
// using FundingContract.FundingContract;
using Microsoft.AspNetCore.Identity;
// using Nethereum.RPC.Accounts;
// using Nethereum.Web3;
using NftApi.Entities;
// using NftContract.NftContract;
using NftApi.Helpers;
using NftApi.Interfaces;

// using NftContract.Contracts.NftContract;
// using NftContract.Contracts.NftContract.ContractDefinition;

namespace NftApi;

public class NftService : INftService
{
    // private readonly IUserRepository _userRepository;
    // private readonly NftContractService _nftContractService;
    // private readonly FundingContractService _fundingContractService;
    private readonly IUnitOfWork _uow;
    private readonly UserManager<User> _userManager;
    // private readonly INftRepository _nftRepository;
    private readonly IImageService _imageService;
    private readonly IMapper _mapper;
    // private readonly Web3 _web3 = new Web3();
    private string _blockchainProvider = "https://rpc.ankr.com/bsc_testnet_chapel/";
    // private IAccount _admin = new Nethereum.Web3.Accounts.Account("be9c8d4fccf913b66dc94d20f8c703b68afee910a57b89edea7a0046752006f1"); //0x9276553A045Dae2D35C13dc6c4ebfA8Eb36AEB85
    // private IAccount _admin = new Nethereum.Web3.Accounts.Account("fb30fffd721982d7c1f485bca28f4b38af5c7cd8032ff0a28df46078ec328f4c");
    // private string _contractAddress = "0xe376913D8Dd708CFE2f2ecb65ba18b3E2072830a";
    private string _contractAddress = "0x9f8e19f3daeafce19ac7a7e043165ab7edbd20a2";
    private string _fundingContractAddress = "";

    public NftService(
        IUnitOfWork uow,
        // IUserRepository userRepository, 
        UserManager<User> userManager, 
        // INftRepository nftRepository, 
        IMapper mapper, IImageService imageService)
    {
        // _userRepository = userRepository;
        // _nftContractService = new NftContractService(new Web3(_admin, _blockchainProvider), _contractAddress);
        // _fundingContractService = new FundingContractService(new Web3(_admin, _blockchainProvider), _fundingContractAddress);
        _userManager = userManager;
        // _nftRepository = nftRepository;
        _mapper = mapper;
        _imageService = imageService;
        _uow = uow;
    }

    // [Fact]
    public async Task MintNft()
    { 


    //     var users = await _userManager.GetUsersInRoleAsync("ADMIN");

    //     var admin = users.ElementAt(0);

    //     var admin.

    //    //await this.Setup(userType);

    //    //var contractBalance = await this.Web3.Eth.GetBalance.SendRequestAsync(this.ContractOwenr.Address);
    //    var ids = new List<BigInteger>() { NFTIDOneOfThree, NFTIDTwoOfThree, NFTIDThreeOfThree };
    //    var accounts = new List<string>(ids.Count);

    //    foreach (uint _ in ids)
    //        accounts.Add(this.ContractOwenr.Address);

    //    var batchMintNftsFunction = new MintNFTFunction() { Ids = ids };
    //    //var balanceOfFunction = new BalanceOfFunction() { Account = this.ContractOwenr.Address, Id = NFTIDOneOfThree };
    //    var batchBalanceOfFunction = new BalanceOfBatchFunction() { Accounts = accounts, Ids = ids };

    //    //var balanceBeforeMint = await _nftContractService.BalanceOfQueryAsync(balanceOfFunction); // this.Web3.Eth.GetBalance.SendRequestAsync(this.ParticipantAccount.Address);
    //    //Console.WriteLine("balanceBeforeMint:");
    //    //Console.WriteLine(balanceBeforeMint);

    //    // before mint
    //    var batchBalance = await _nftContractService.BalanceOfBatchQueryAsync(batchBalanceOfFunction); // this.Web3.Eth.GetBalance.SendRequestAsync(this.ParticipantAccount.Address);
    //    int index = 1;
    //    foreach (uint e in batchBalance)
    //    {
    //        //Console.WriteLine($"balance of NFT with ID {ids.Find(x => x.)}: {batchBalanceAfterMint}");
    //        Console.WriteLine($"balance of NFT {index} BEFORE mint: {e}");
    //        index += 1;
    //    }

    //    // perform batch mint
    //    await _nftContractService.MintNFTRequestAndWaitForReceiptAsync(batchMintNftsFunction);

    //    //await Task.Delay(15000);

    //    var a = await _nftContractService.GetAccountsQueryAsync();
    //    foreach (uint e in a)
    //    {
    //        Console.WriteLine($"here is part of a: {e}");
    //    }

    //    //var balanceAfterMint = await _nftContractService.BalanceOfQueryAsync(balanceOfFunction); // this.Web3.Eth.GetBalance.SendRequestAsync(this.ParticipantAccount.Address);
    //    //Console.WriteLine("balanceAfterMint:");
    //    //Console.WriteLine(balanceAfterMint);

    //    // after mint
    //    batchBalance = await _nftContractService.BalanceOfBatchQueryAsync(batchBalanceOfFunction); // this.Web3.Eth.GetBalance.SendRequestAsync(this.ParticipantAccount.Address);
    //    index = 1;
    //    foreach (uint e in batchBalance)
    //    {
    //        //Console.WriteLine($"balance of NFT with ID {ids.Find(x => x.)}: {batchBalanceAfterMint}");
    //        Console.WriteLine($"balance of NFT {index} AFTER mint: {e}");
    //        index += 1;
    //    }
    }

    public async Task<NftDto> MintNftImage(User user, IFormFile file) 
    {
        var result = await _imageService.UploadPhotoAsync(file);
        if (result.Error != null) throw new Exception(result.Error.Message);

        var newNft = new Nft
        {
            Image = result.SecureUrl.AbsoluteUri,
            User = user
            // PublicId = result.PublicId
        };

        _uow.NftRepo.AddNft(newNft);
        if (!await _uow.Complete()) throw new Exception("Could not save new NFT image");

        return _mapper.Map<NftDto>(newNft);
    }

    // public async Task<List<NftDto>> MintNfts(int amount, User user)
    public async Task<bool> MintNfts(NftDto nftDto)
    {
        // ===============================================================
        // var newNfts = new List<BigInteger>();
        // // Console.WriteLine("1");

        // // Console.WriteLine("user");
        // // Console.WriteLine(user);
        // // user.Nfts = new List<Nft>();
        // var existingUser = await _userRepository.GetUserByIdIncludeNfts(user.Id);

        // for (var index = 0; index < amount; index++) 
        //     existingUser.Nfts.Add(new Nft());
        // // Console.WriteLine("2");

        // await _nftRepository.SaveAllAsync();
        // List<Nft> nfts = await _nftRepository.GetNftsAsync();
        // // Console.WriteLine("3");

        // for (var i = amount; i > 0; i--)
        // {   
        //     newNfts.Add(nfts.ElementAt(i-1).Id);
        // }
        // // Console.WriteLine("4");
        // UserDto updatedUser = _mapper.Map<UserDto>(existingUser);
        // List<NftDto> updatedUserNfts = updatedUser.Nfts;

        // // var batchMintNftsFunction = new MintNFTFunction() { Ids = newNfts };
        // // await _nftContractService.MintNFTRequestAndWaitForReceiptAsync(batchMintNftsFunction);
        
        // return updatedUserNfts;
        // ===============================================================
        var nft = await _uow.NftRepo.GetNftsByIdAsync(nftDto.Id);
        nft.Description = nftDto.Description;
        nft.ArtistName = nftDto.ArtistName;
        nft.shortDescription = nftDto.shortDescription;
        return await _uow.Complete();
    }

    public async Task<bool> ClaimDeposit(string address, User user) 
    {
        var balance = 0.9; // var balance = await _fundingContractService.GetBalanceByAddressQueryAsync(address);
        user.Wallet = new Wallet
        {
            Address = address,
            AvailableBalance = (decimal)balance
        };
        user.hasFunds = true;
        return await _uow.Complete(); 
    }

    public async Task<List<NftDto>> GetUserNfts(User user)
    {
        List<NftDto> nfts = await _uow.NftRepo.GetUserNftsAsync(user.Id);
        return nfts;
    }

    // public async Task<List<NftDto>> GetAllNfts()
    // {
    //     return _mapper.Map<List<NftDto>>(await _uow.NftRepo.GetAllNfts());
    // }

    public async Task<PagedList<NftDto>> GetAllNfts(UserParams userParams, bool isStartAuction)
    {
        return await _uow.NftRepo.GetAllNfts(userParams, isStartAuction);
    }
    
    public async Task<PagedList<NftDto>> GetAllNftsForUser(UserParams userParams)
    {
        return await _uow.NftRepo.GetAllNftsForUser(userParams);
    }
}
