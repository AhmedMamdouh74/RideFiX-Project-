using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities.PaymentEntites;
using Microsoft.Extensions.Configuration;
using ServiceAbstraction.CoreServicesAbstractions;
using SharedData.DTOs.Payment;
using Stripe;

namespace Service.CoreServices.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork unitOfWork;
        private IConfiguration configuration;
        public PaymentService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            this.unitOfWork = unitOfWork;
            this.configuration = configuration;
        }

        public async Task<RideCoinsPaymentDto> CreateOrUpdatePaymentIntentRideCoinsAsync(int CoinChargeId)
        {
            // Config Stripe
            StripeConfiguration.ApiKey = configuration["Stripe:SecretKey"];
            // Get Coin Top Up
            var coinPaidRequest = await unitOfWork.GetRepository<CoinChargeEntity, int>().GetByIdAsync(CoinChargeId);
            ArgumentNullException.ThrowIfNull(coinPaidRequest);

            // Create or update payment intent
            var PaymentService = new PaymentIntentService();
            if (coinPaidRequest.PaymentIntentId is null)
            {
                var Options = new PaymentIntentCreateOptions()
                {
                    Amount = coinPaidRequest.AmountCents,
                    Currency = "USD",
                    PaymentMethodTypes = ["card"]
                };
                var PaymentIntent = await PaymentService.CreateAsync(Options);
                coinPaidRequest.ClientSecret = PaymentIntent.ClientSecret;
                coinPaidRequest.PaymentIntentId = PaymentIntent.Id;
            }
            else
            {
                var Options = new PaymentIntentUpdateOptions()
                {
                    Amount = coinPaidRequest.AmountCents,
                };
                await PaymentService.UpdateAsync(coinPaidRequest.PaymentIntentId, Options);
            }
            // Save changes
            await unitOfWork.SaveChangesAsync();
            // Return DTO
            return new RideCoinsPaymentDto
            {
                Id = coinPaidRequest.Id,
                Coins = coinPaidRequest.Coins,
                AmountCents = coinPaidRequest.AmountCents,
                ClientSecret = coinPaidRequest.ClientSecret,
                PaymentIntentId = coinPaidRequest.PaymentIntentId
            };
        }

        public async Task<int> CreateChargeEntityAsync(int Coins)
        {
            var repository = unitOfWork.GetRepository<CoinChargeEntity, int>();
            var coinChargeEntity = new CoinChargeEntity
            {
                Coins = Coins,
                AmountCents = Coins * 100, // Assuming 1 coin = 1 USD
                ClientSecret = null,
                PaymentIntentId = null
            };
            await repository.AddAsync(coinChargeEntity);
            await unitOfWork.SaveChangesAsync();
            return coinChargeEntity.Id;
        }
    }
}
