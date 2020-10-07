using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Net;
using System.Threading.Tasks;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers.Bases;

namespace MalaGroupERP.Models
{
    public class AuthorizeNetModel
    {

    }
    public class CustomerProfileModel
    {
        string ApiLoginID = ConfigurationManager.AppSettings["ApiLoginID"];
        string ApiTransactionKey = ConfigurationManager.AppSettings["ApiTransactionKey"];
        public createCustomerProfileResponse CreateCustomerProfile(CardModel card)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey
            };
            var creditCard = new creditCardType
            {
                cardNumber = card.CardNumber,
                expirationDate = card.ExpirationDate,
                cardCode=card.CardCode
            };
            paymentType cc = new paymentType { Item = creditCard };

            List<customerPaymentProfileType> paymentProfileList = new List<customerPaymentProfileType>();
            customerPaymentProfileType ccPaymentProfile = new customerPaymentProfileType();
            ccPaymentProfile.payment = cc;

            paymentProfileList.Add(ccPaymentProfile);

            customerProfileType customerProfile = new customerProfileType();
            customerProfile.merchantCustomerId = card.AOModel.AccountID.ToString();
            customerProfile.email = card.AOModel.LeadEmail;
            customerProfile.paymentProfiles = paymentProfileList.ToArray();

            var request = new createCustomerProfileRequest { profile = customerProfile, validationMode = validationModeEnum.none };

            var controller = new createCustomerProfileController(request);
            controller.Execute();
            createCustomerProfileResponse response = controller.GetApiResponse();
            return response;
        }
        public createCustomerProfileResponse CreateCustomerProfileFromTransaction(CardModel card)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey
            };
            var customerProfile = new customerProfileBaseType
            {
                merchantCustomerId = card.AOModel.AccountID.ToString(),
                email = card.AOModel.LeadEmail
            };
            var request = new createCustomerProfileFromTransactionRequest
            {
                transId = card.TransactionID,
                customer = customerProfile
            };

            var controller = new createCustomerProfileFromTransactionController(request);
            controller.Execute();
            var response = controller.GetApiResponse();
            return response;
        }
        public createCustomerPaymentProfileResponse CreateCustomerPaymentProfile(CardModel card)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey
            };

            var creditCard = new creditCardType
            {
                cardNumber = card.CardNumber,
                expirationDate = card.ExpirationDate,
                cardCode = card.CardCode
            };

            paymentType cc = new paymentType { Item = creditCard };
            customerPaymentProfileType paymentProfile = new customerPaymentProfileType { payment=cc};
            var request = new createCustomerPaymentProfileRequest
            {
                customerProfileId =card.CustomerProfileID, 
                paymentProfile = paymentProfile,
                validationMode = validationModeEnum.none
            };


            var controller = new createCustomerPaymentProfileController(request);
            controller.Execute();
            var response = controller.GetApiResponse();
            return response;
        }
        public deleteCustomerPaymentProfileResponse DeleteCustomerPaymentProfile(CardModel card)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey
            };

            var request = new deleteCustomerPaymentProfileRequest
            {
                customerProfileId = card.CustomerProfileID,
                customerPaymentProfileId = card.CustomerPaymentProfileID
            };
            var controller = new deleteCustomerPaymentProfileController(request);
            controller.Execute();
            var response = controller.GetApiResponse();
            return response;
        }
        public deleteCustomerProfileResponse DeleteCustomerProfile(CardModel card)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey
            };

            var request = new deleteCustomerProfileRequest
            {
                customerProfileId = card.CustomerProfileID
            };
            var controller = new deleteCustomerProfileController(request);
            controller.Execute();
            var response = controller.GetApiResponse();
            return response;
        }
        public getCustomerPaymentProfileResponse GetCustomerPaymentProfile(CardModel card)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey
            };

            var request = new getCustomerPaymentProfileRequest();
            request.customerProfileId = card.CustomerProfileID;
            request.customerPaymentProfileId = card.CustomerPaymentProfileID;

            // Set this optional property to true to return an unmasked expiration date
            //request.unmaskExpirationDateSpecified = true;
            //request.unmaskExpirationDate = true;

            // instantiate the controller that will call the service
            var controller = new getCustomerPaymentProfileController(request);
            controller.Execute();
            var response = controller.GetApiResponse();

            return response;
        }
        public getCustomerPaymentProfileListResponse GetCustomerPaymentProfileList(string yearMonth, int pageLimit, int offset)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey
            };

           var request = new getCustomerPaymentProfileListRequest();
            request.searchType = CustomerPaymentProfileSearchTypeEnum.cardsExpiringInMonth;
            request.month = yearMonth;
            request.paging = new Paging();
            request.paging.limit = pageLimit;
            request.paging.offset = offset;
            var controller = new getCustomerPaymentProfileListController(request);
            controller.Execute();
            var response = controller.GetApiResponse();
            return response;
        }
        public getCustomerProfileResponse GetCustomerProfile(CardModel card)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey
            };

            var request = new getCustomerProfileRequest();
            request.customerProfileId = card.CustomerProfileID;
            var controller = new getCustomerProfileController(request);
            controller.Execute();
            var response = controller.GetApiResponse();
            return response;
        }
        public getCustomerProfileIdsResponse GetCustomerProfileIds()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey
            };

           var request = new getCustomerProfileIdsRequest();

            var controller = new getCustomerProfileIdsController(request);
            controller.Execute();

            var response = controller.GetApiResponse();
            return response;
        }
        public updateCustomerPaymentProfileResponse UpdateCustomerPaymentProfile(CardModel card)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey
            };

            var creditCard = new creditCardType
            {
                cardNumber = card.CardNumber,
                expirationDate = card.ExpirationDate
            };

            //===========================================================================
            // NOTE:  For updating just the address, not the credit card/payment data 
            //        you can pass the masked values returned from 
            //        GetCustomerPaymentProfile or GetCustomerProfile
            //        E.g.
            //                * literal values shown below
            //===========================================================================
            /*var creditCard = new creditCardType
            {
                cardNumber = "XXXX1111",
                expirationDate = "XXXX"
            };*/

            var paymentType = new paymentType { Item = creditCard };

            var paymentProfile = new customerPaymentProfileExType
            {
                billTo = new customerAddressType
                {
                    firstName = card.AOModel.FirstName,
                    lastName = card.AOModel.LastName,
                    address = (card.AOModel.IsDiffBillingAdd == 0 ? card.AOModel.Street : card.AOModel.BStreet),
                    city = (card.AOModel.IsDiffBillingAdd == 0 ? card.AOModel.City : card.AOModel.BCity),
                    state = (card.AOModel.IsDiffBillingAdd == 0 ? card.AOModel.State : card.AOModel.BState),
                    zip = (card.AOModel.IsDiffBillingAdd == 0 ? card.AOModel.Zip : card.AOModel.BZip),
                    country = card.AOModel.Country,
                    phoneNumber = card.AOModel.PrimaryPhone,
                },
                payment = paymentType,
                customerPaymentProfileId = card.CustomerPaymentProfileID
            };

            var request = new updateCustomerPaymentProfileRequest();
            request.customerProfileId = card.CustomerProfileID;
            request.paymentProfile = paymentProfile;
            request.validationMode = validationModeEnum.liveMode;
            var controller = new updateCustomerPaymentProfileController(request);
            controller.Execute();
            var response = controller.GetApiResponse();
            return response;
        }
        public updateCustomerProfileResponse UpdateCustomerProfile(CardModel card)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey
            };

            var profile = new customerProfileExType
            {
                merchantCustomerId = card.AOModel.AccountID.ToString(),
                email = card.AOModel.LeadEmail,
                customerProfileId = card.CustomerProfileID
            };

            var request = new updateCustomerProfileRequest();
            request.profile = profile;
            var controller = new updateCustomerProfileController(request);
            controller.Execute();
            var response = controller.GetApiResponse();
            return response;
        }
        public validateCustomerPaymentProfileResponse ValidateCustomerPaymentProfile(CardModel card)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey
            };

            var request = new validateCustomerPaymentProfileRequest();
            request.customerProfileId = card.CustomerProfileID;
            request.customerPaymentProfileId = card.CustomerPaymentProfileID;
            request.validationMode = validationModeEnum.liveMode;
            var controller = new validateCustomerPaymentProfileController(request);
            controller.Execute();
            var response = controller.GetApiResponse();
            return response;
        }
    }
    public class PaymentTransactionModel
    {
        string ApiLoginID = ConfigurationManager.AppSettings["ApiLoginID"];
        string ApiTransactionKey = ConfigurationManager.AppSettings["ApiTransactionKey"];
        public createTransactionResponse AuthorizeCreditCard(CardModel card)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey
            };
            var creditCard = new creditCardType
            {
                cardNumber = card.CardNumber,
                expirationDate = card.ExpirationDate
            };

            var paymentType = new paymentType { Item = creditCard };

            var transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authOnlyTransaction.ToString(),    // authorize only
                amount = card.Amount,
                payment = paymentType
            };

            var request = new createTransactionRequest { transactionRequest = transactionRequest };
            var controller = new createTransactionController(request);
            controller.Execute();
            var response = controller.GetApiResponse();
            return response;
        }
        public createTransactionResponse ChargeCustomerProfile(CardModel card)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey
            };
            var creditCard = new creditCardType
            {
                cardNumber = card.CardNumber,
                expirationDate = card.ExpirationDate,
                cardCode = card.CardCode
            };

            customerProfilePaymentType profileToCharge = new customerProfilePaymentType();
            profileToCharge.customerProfileId = card.CustomerProfileID;
            profileToCharge.paymentProfile = new paymentProfile { paymentProfileId = card.CustomerPaymentProfileID };

            var lineItems = new lineItemType[1];
            lineItems[0] = new lineItemType { itemId = "1", name = "Order ID : " + card.AOModel.OrderID.ToString(), quantity = 1, unitPrice = card.Amount };

            var transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),
                amount = card.Amount,
                profile = profileToCharge,
                lineItems = lineItems,
            };

            var request = new createTransactionRequest { transactionRequest = transactionRequest };
            var controller = new createTransactionController(request);
            controller.Execute();
            var response = controller.GetApiResponse();
            return response;
        }
        public createTransactionResponse ChargeCreditCard(CardModel card)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey
            };
            var creditCard = new creditCardType
            {
                cardNumber = card.CardNumber,
                expirationDate = card.ExpirationDate,
               
            };

            var paymentType = new paymentType { Item = creditCard };
            var customerDT = new customerDataType { id = card.AOModel.PinNo };
            var billTo = new customerAddressType { firstName = card.AOModel.FirstName, lastName = card.AOModel.LastName, address = (card.AOModel.IsDiffBillingAdd == 0 ? card.AOModel.Street : card.AOModel.BStreet), city = (card.AOModel.IsDiffBillingAdd == 0 ? card.AOModel.City : card.AOModel.BCity), state = (card.AOModel.IsDiffBillingAdd == 0 ? card.AOModel.State : card.AOModel.BState), zip = (card.AOModel.IsDiffBillingAdd == 0 ? card.AOModel.Zip : card.AOModel.BZip), email = card.AOModel.LeadEmail, phoneNumber = card.AOModel.PrimaryPhone };
            var shipTo = new nameAndAddressType { firstName = card.AOModel.FirstName, lastName = card.AOModel.LastName, address = card.AOModel.Street, city = card.AOModel.City, state = card.AOModel.State, zip = card.AOModel.Zip };
            var lineItems = new lineItemType[1];
            lineItems[0] = new lineItemType { itemId = "1", name = "PIN NO:"+card.AOModel.PinNo, quantity = 1, unitPrice = card.Amount };
            var orderinfo = new orderType
            {
                invoiceNumber = card.AOModel.PinNo
            };
           
            var transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),
                amount = card.Amount,
                payment = paymentType,
                lineItems = lineItems,
                billTo = billTo,
                shipTo = shipTo,
                customer = customerDT,
                order = orderinfo
            };
            
            var request = new createTransactionRequest { transactionRequest = transactionRequest };
            var controller = new createTransactionController(request);
            controller.Execute();
            var response = controller.GetApiResponse();
            
            return response;
        }
        public createTransactionResponse ChargeCreditCardARB(CardScheduleModel model)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment=  AuthorizeNet.Environment.SANDBOX;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey
            };
            var creditCard = new creditCardType
            {
                cardNumber = model.CardNumber,
                expirationDate = model.CardExpirationMonth+model.CardExpirationYear
            };
            var paymentType = new paymentType { Item = creditCard };
            var customerDT = new customerDataType { id = model.PinNumber };
            var billTo = new customerAddressType { firstName = model.BillingFirstName, lastName = model.BillingLastName, address = model.BillingAddress, city = model.BillingCity, state = model.BillingState, zip = model.BillingZipPostal, email = model.PersonAccountEmail, phoneNumber = model.PrimaryPhone };
            var shipTo = new nameAndAddressType { firstName = model.PersonAccountFirstName, lastName = model.PersonAccountLastName, address = model.ShippingStreet, city = model.ShippingCity, state = model.ShippingStateProvince, zip = model.ShippingZipPostalCode };
            var lineItems = new lineItemType[1];
            lineItems[0] = new lineItemType { itemId = "1", name = "PIN NO:" + model.PinNumber, quantity = 1, unitPrice = Convert.ToDecimal(model.ChargeAmount) };
            var orderinfo = new orderType
            {
                invoiceNumber = model.InvoiceNumber
            };

            var transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),
                amount =  Convert.ToDecimal(model.ChargeAmount) ,
                payment = paymentType,
                lineItems = lineItems,
                billTo = billTo,
                shipTo = shipTo,
                customer = customerDT,
                order=orderinfo
            };

            var request = new createTransactionRequest { transactionRequest = transactionRequest };
            var controller = new createTransactionController(request);
            controller.Execute();
            var response = controller.GetApiResponse();

            return response;
        }
        public createTransactionResponse RefundTransaction(CardModel card)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey
            };
            var creditCard = new creditCardType
            {
                cardNumber = card.CardNumber,
                expirationDate = card.ExpirationDate
            };

            var paymentType = new paymentType { Item = creditCard };

            var transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.refundTransaction.ToString(),    // refund type
                payment = paymentType,
                amount = card.Amount,
                refTransId = card.TransactionID
            };

            var request = new createTransactionRequest { transactionRequest = transactionRequest };
            var controller = new createTransactionController(request);
            controller.Execute();
           var  response = controller.GetApiResponse();

           return response;
        }
        public createTransactionResponse VoidTransaction(CardModel card)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey
            };
            var transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.voidTransaction.ToString(),    // refund type
                refTransId = card.TransactionID
            };

            var request = new createTransactionRequest { transactionRequest = transactionRequest };
            var controller = new createTransactionController(request);
            controller.Execute();
            var response = controller.GetApiResponse();

            return response;
        }
    }
    public class RecurringBillingModel
    {
        string ApiLoginID = ConfigurationManager.AppSettings["ApiLoginID"];
        string ApiTransactionKey = ConfigurationManager.AppSettings["ApiTransactionKey"];
        public ARBCreateSubscriptionResponse CreateSubscription(CardModel card)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey,
            };

            paymentScheduleTypeInterval interval = new paymentScheduleTypeInterval();
            interval.length = 1; 
            interval.unit = ARBSubscriptionUnitEnum.months;

            DateTime dtStartDate = card.AOModel.chargeDateList[1].ChargeDate.Value;
            short numberOfInst = (short)(card.AOModel.NoOfInstallment - 1);

            paymentScheduleType schedule = new paymentScheduleType
            {
                interval = interval,
                startDate = dtStartDate,
                totalOccurrences = numberOfInst
            };

            var creditCard = new creditCardType
            {
                cardNumber = card.CardNumber,
                expirationDate = card.ExpirationDate,
                cardCode = card.CardCode
            };

            var paymentType = new paymentType { Item = creditCard };

            nameAndAddressType addressInfo = new nameAndAddressType()
            {
                firstName = card.AOModel.FirstName,
                lastName = card.AOModel.LastName
            };

            ARBSubscriptionType subscriptionType = new ARBSubscriptionType()
            {
                amount = card.AOModel.chargeDateList[1].ChargeAmt.Value,
                trialAmount = 0.00m,
                paymentSchedule = schedule,
                billTo = addressInfo,
                payment = paymentType
            };

            var request = new ARBCreateSubscriptionRequest { subscription = subscriptionType };
            var controller = new ARBCreateSubscriptionController(request);
            controller.Execute();
            var response = controller.GetApiResponse();

            return response;
        }
        public ARBCreateSubscriptionResponse CreateSubscriptionFromCustomerProfile(CardModel card)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey,
            };

            paymentScheduleTypeInterval interval = new paymentScheduleTypeInterval();
            interval.length = 1;                     
            interval.unit = ARBSubscriptionUnitEnum.months;

            DateTime dtStartDate = card.AOModel.chargeDateList[1].ChargeDate.Value;
            short numberOfInst = (short)(card.AOModel.NoOfInstallment - 1);

            paymentScheduleType schedule = new paymentScheduleType
            {
                interval = interval,
                startDate = dtStartDate,
                totalOccurrences = numberOfInst  
            };

            var creditCard = new creditCardType
            {
                cardNumber =card.CardNumber,
                expirationDate = card.ExpirationDate,
                cardCode = card.CardCode
            };

            var paymentType = new paymentType { Item = creditCard };

            var customerProfile = new customerProfileIdType()
            {
                customerProfileId = card.CustomerProfileID,
                customerPaymentProfileId = card.CustomerPaymentProfileID
            };

            ARBSubscriptionType subscriptionType = new ARBSubscriptionType()
            {
                amount = card.AOModel.chargeDateList[1].ChargeAmt.Value,
                trialAmount = 0.00m,
                paymentSchedule = schedule,
                profile=customerProfile
            };

            var request = new ARBCreateSubscriptionRequest { subscription = subscriptionType };
            var controller = new ARBCreateSubscriptionController(request);
            controller.Execute();
            var response = controller.GetApiResponse();

            return response;
        }
        public ARBCancelSubscriptionResponse CancelSubscription(CardModel card)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey,
            };

            var request = new ARBCancelSubscriptionRequest { subscriptionId = card.SubscriptionID };
            var controller = new ARBCancelSubscriptionController(request);                          // instantiate the controller that will call the service
            controller.Execute();
            var response = controller.GetApiResponse();
            return response;
        }
        public ARBGetSubscriptionListResponse GetListOfSubscriptions()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey,
            };

            var request = new ARBGetSubscriptionListRequest { searchType = ARBGetSubscriptionListSearchTypeEnum.subscriptionActive };    // only gets active subscriptions

            var controller = new ARBGetSubscriptionListController(request);          // instantiate the controller that will call the service
            controller.Execute();
            var response = controller.GetApiResponse();
            return response;
        }
        public ARBGetSubscriptionResponse GetSubscription(CardModel card)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey,
            };

            var request = new ARBGetSubscriptionRequest { subscriptionId = card.SubscriptionID};

            var controller = new ARBGetSubscriptionController(request);          // instantiate the controller that will call the service
            controller.Execute();
            var response = controller.GetApiResponse();   // get the response from the service (errors contained if an
            return response;
        }
        public ARBGetSubscriptionStatusResponse GetSubscriptionStatus(CardModel card)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey,
            };

            var request = new ARBGetSubscriptionStatusRequest { subscriptionId = card.SubscriptionID };
            var controller = new ARBGetSubscriptionStatusController(request);                          // instantiate the controller that will call the service
            controller.Execute();
            var response = controller.GetApiResponse();  
            return response;
        }
        public ARBUpdateSubscriptionResponse UpdateSubscription(CardModel card)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey,
            };

            DateTime dtStartDate = card.AOModel.chargeDateList[1].ChargeDate.Value;
            short numberOfInst = (short)(card.AOModel.NoOfInstallment - 1);

            paymentScheduleType schedule = new paymentScheduleType
            {
                startDate = dtStartDate,
                totalOccurrences = numberOfInst

            };
            customerProfileIdType customerProfile = new customerProfileIdType()
            {
                customerProfileId = card.CustomerProfileID,
                customerPaymentProfileId = card.CustomerPaymentProfileID
            };
            ARBSubscriptionType subscriptionType = new ARBSubscriptionType()
            {
                amount = card.AOModel.chargeDateList[1].ChargeAmt.Value,
                paymentSchedule = schedule,
                profile = customerProfile
            };
            var request = new ARBUpdateSubscriptionRequest { subscription = subscriptionType, subscriptionId = card.SubscriptionID };
            var controller = new ARBUpdateSubscriptionController(request);
            controller.Execute();
            var response = controller.GetApiResponse(); 
            return response;
        }
    }
    public class TransactionReportingModel
    {
        string ApiLoginID = ConfigurationManager.AppSettings["ApiLoginID"];
        string ApiTransactionKey = ConfigurationManager.AppSettings["ApiTransactionKey"];
        public getAUJobDetailsResponse GetAccountUpdaterJobDetails(string month )
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey,
            };

            var request = new getAUJobDetailsRequest();
            request.month = month;
            request.modifiedTypeFilter = AUJobTypeEnum.all;
            request.paging = new Paging
            {
                limit = 1000,
                offset = 1
            };
            var controller = new getAUJobDetailsController(request);
            controller.Execute();
            var response = controller.GetApiResponse();
            return response;
        }
        public getAUJobSummaryResponse GetAccountUpdaterJobSummary(string month)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey,
            };

            var request = new getAUJobSummaryRequest();
            request.month = month;
            var controller = new getAUJobSummaryController(request);
            controller.Execute();
            var response = controller.GetApiResponse();
            return response;
        }
        public getBatchStatisticsResponse GetBatchStatistics(string batchId)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey,
            };
            var request = new getBatchStatisticsRequest();
            request.batchId = batchId;
            var controller = new getBatchStatisticsController(request);
            controller.Execute();
            var response = controller.GetApiResponse();
            return response;
        }
        public getTransactionListResponse GetCustomerProfileTransactionList(string CustomerProfileId)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey,
            };
            var request = new getTransactionListForCustomerRequest();
            request.customerProfileId = CustomerProfileId;
            var controller = new getTransactionListForCustomerController(request);
            controller.Execute();
            var response = controller.GetApiResponse();
            return response;
        }
        public getMerchantDetailsResponse GetMerchantDetails()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey,
            };
            var request = new getMerchantDetailsRequest
            {
                merchantAuthentication = new merchantAuthenticationType() { name = ApiLoginID, Item = ApiTransactionKey, ItemElementName = ItemChoiceType.transactionKey }
            };
            var controller = new getMerchantDetailsController(request);
            controller.Execute();
            var response = controller.GetApiResponse();
            return response;
        }
        public getSettledBatchListResponse GetSettledBatchList(DateTime firstSettlementDate, DateTime lastSettlementDate)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey,
            };
            var request = new getSettledBatchListRequest();
            request.firstSettlementDate = firstSettlementDate;
            request.lastSettlementDate = lastSettlementDate;
            request.includeStatistics = true;
            var controller = new getSettledBatchListController(request);
            controller.Execute();
            var response = controller.GetApiResponse();
            return response;
        }
        public getTransactionDetailsResponse GetTransactionDetails(string transactionId)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey,
            };
            var request = new getTransactionDetailsRequest();
            request.transId = transactionId;
            var controller = new getTransactionDetailsController(request);
            controller.Execute();
            var response = controller.GetApiResponse();
            return response;
        }
        public getTransactionListResponse GetTransactionList(string batchId, int limit, int offset)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey,
            };
            var request = new getTransactionListRequest();
            request.batchId = batchId;
            request.paging = new Paging
            {
                limit = limit,
                offset = offset
            };
            request.sorting = new TransactionListSorting
            {
                orderBy = TransactionListOrderFieldEnum.id,
                orderDescending = true
            };
            var controller = new getTransactionListController(request);
            controller.Execute();
            var response = controller.GetApiResponse();
            return response;
        }
        public getUnsettledTransactionListResponse GetUnsettledTransactionList(int limit, int offset)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey,
            };
            var request = new getUnsettledTransactionListRequest();
            request.status = TransactionGroupStatusEnum.any;
            request.statusSpecified = true;
            request.paging = new Paging
            {
                limit = limit,
                offset = offset
            };
            request.sorting = new TransactionListSorting
            {
                orderBy = TransactionListOrderFieldEnum.id,
                orderDescending = true
            };
            
            var controller = new getUnsettledTransactionListController(request);
            controller.Execute();
            var response = controller.GetApiResponse();
            return response;
        }
    }
}