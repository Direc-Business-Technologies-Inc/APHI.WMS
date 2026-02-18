using Application.DataTransferObjects.Administration.User;
using Application.DataTransferObjects.Others;
using Application.DataTransferObjects.Transactions.Commons;
using Application.DataTransferObjects.Transactions.Procurement.Order;
using Application.DataTransferObjects.Transactions.Receiving;
using Domain.Entities.Administration.User.Management;
using Domain.Entities.Enums.Transaction.Commons;
using Domain.Entities.Enums.Transaction.Receiving;
using Domain.ValueObjects.Others;
using Domain.ValueObjects.Transaction;
using Integration.SAP.Entities.Transactional.Receiving;
using Mapster;
using Shared.Kernel;

namespace Application.UseCases.Registers;

public class MappingRegistration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        #region DEM to DTO

        #region User Management
        config.NewConfig<UserDEM, UserDataGridDTO>()
            .Map(d => d.Id, s => s.Id)
            .Map(d => d.FullName, s => s.Name.FullName)
            .Map(d => d.Email, s => s.Email.Address)
            .Map(d => d.Phone, s => s.PhoneNumber)
            .Map(d => d.Position, _ => string.Empty)
            .Map(d => d.Active, s => s.Active);
        #endregion User Management

        #endregion DEM to DTO

        #region DTO to VO

        #region System

        #region Common
        config.NewConfig<PersonNameDTO, PersonNameVO>()
            .ConstructUsing(dto => new PersonNameVO(
                dto.FirstName,
                dto.MiddleName,
                dto.LastName));
        #endregion Common

        #region Transactional Documents
        config.NewConfig<AppDocNumDTO, AppDocNumVO>()
            .ConstructUsing(dto => new AppDocNumVO(
                dto.Value));

        config.NewConfig<SapDocumentReferenceDTO, SapDocumentReferenceVO>()
            .ConstructUsing(dto => new SapDocumentReferenceVO(
                dto.DocEntry,
                dto.DocNum,
                dto.BaseEntry,
                dto.BaseDocNum));
        #endregion Transactional Documents

        #endregion System

        #region Administration

        #region User Management

        config.NewConfig<UserNameDTO, UserNameVO>()
            .ConstructUsing(dto => new UserNameVO(dto.Value));
        config.NewConfig<EmailDTO, EmailVO>()
            .ConstructUsing(dto => new EmailVO(dto.Address));
        config.NewConfig<AccountDTO, AccountVO>()
            .ConstructUsing(dto => new AccountVO(
                dto.UserName.Adapt<UserNameVO>(),
                dto.HashedPassword,
                dto.LockoutEnabled,
                dto.Locked));

        #endregion User Management

        #endregion Administration

        #endregion DTO to VO

        #region SAP DTO to DTO

        #region Receiving

        config.NewConfig<PurchaseOrderSAPDTO, PurchaseOrderDataGridDTO>()
            .Map(d => d.DocEntry, s => s.DocEntry)
            .Map(d => d.DocNum, s => s.DocNum)
            .Map(d => d.DocDate, s => s.DocDate)
            .Map(d => d.DocDueDate, s => s.DocDueDate)
            .Map(d => d.DocStatus, s => EnumHelper.ParseStringToEnum<DocumentStatus>(s.DocStatus))
            .Map(d => d.CardCode, s => s.CardCode)
            .Map(d => d.CardName, s => s.CardName)
            .Map(d => d.SupplierContactPerson, s => s.SupplierContactPerson);

        config.NewConfig<PurchaseOrderHeaderSAPDTO, PurchaseOrderDTO>()
            .Map(d => d.SapReference, s => new SapDocumentReferenceDTO()
            {
                DocEntry = s.DocEntry,
                DocNum = s.DocNum,
            })
            .Map(d => d.DocDate, s => s.DocDate)
            .Map(d => d.DocDueDate, s => s.DocDueDate)
            .Map(d => d.BusinessPartner, s => new BusinessPartnerDTO()
            {
                CardCode = s.CardCode,
                CardName = s.CardName,
            })
            .Map(d => d.SupplierContactPerson, s => s.SupplierContactPerson)
            .Map(d => d.Remarks, s => s.Comments);

        config.NewConfig<PurchaseOrderLineSAPDTO, PurchaseOrderLineDTO>()
            .Map(d => d.DocEntry, s => s.DocEntry)
            .Map(d => d.DocNum, s => s.DocNum)
            .Map(d => d.LineNum, s => s.LineNum + 1)
            .Map(d => d.ItemCode, s => s.ItemCode)
            .Map(d => d.ItemName, s => s.ItemName)
            .Map(d => d.TargetQuantity, s => s.TargetQuantity)
            .Map(d => d.OpenQuantity, s => s.OpenQuantity)
            .Map(d => d.UoMCode, s => s.UoMCode)
            .Map(d => d.UoMValue, s => s.UoMValue)
            .Map(d => d.UoMName, s => s.UoMName)
            .Map(d => d.Warehouse, s => new WarehouseDTO()
            {
                WhsCode = s.WhsCode,
                WhsName = s.WhsName,
            })
            .Map(d => d.VatGroup, s => s.VatGroup);

        config.NewConfig<PurchaseDeliveryNoteSAPDTO, PurchaseDeliveryNoteDataGridDTO>()
            .Map(d => d.DocEntry, s => s.DocEntry)
            .Map(d => d.DocNum, s => s.DocNum)
            .Map(d => d.BaseEntry, s => s.BaseEntry)
            .Map(d => d.BaseDocNum, s => s.BaseDocNum)
            .Map(d => d.DocDate, s => s.DocDate)
            .Map(d => d.PoDocDate, s => s.PoDocDate)
            .Map(d => d.DocDueDate, s => s.DocDueDate)
            .Map(d => d.CardCode, s => s.CardCode)
            .Map(d => d.CardName, s => s.CardName)
            .Map(d => d.WhsCode, s => s.WhsCode)
            .Map(d => d.WhsName, s => s.WhsName)
            .Map(d => d.ItemDesc, s => s.ItemDesc)
            .Map(d => d.ReceivedBy, s => s.ReceivedBy)
            .Map(d => d.SupplierContactPerson, s => s.SupplierContactPerson);

        config.NewConfig<PurchaseDeliveryNoteHeaderSAPDTO, PurchaseDeliveryNoteDTO>()
           .Map(d => d.SapReference, s => new SapDocumentReferenceDTO()
           {
               DocEntry = s.DocEntry,
               DocNum = s.DocNum,
               BaseEntry = s.BaseEntry,
               BaseDocNum = s.BaseDocNum,
           })
           .Map(d => d.DocDate, s => s.DocDate)
           .Map(d => d.DocDueDate, s => s.DocDueDate)
           .Map(d => d.BusinessPartner, s => new BusinessPartnerDTO()
           {
               CardCode = s.CardCode,
               CardName = s.CardName,
           })
           .Map(d => d.SupplierContactPerson, s => s.SupplierContactPerson)
           .Map(d => d.ReceivedBy, s => s.ReceivedBy);

        config.NewConfig<PurchaseDeliveryNoteLineSAPDTO, PurchaseDeliveryNoteLineDTO>()
            .Map(d => d.DocEntry, s => s.DocEntry)
            .Map(d => d.DocNum, s => s.DocNum)
            .Map(d => d.LineNum, s => s.LineNum + 1)
            .Map(d => d.ItemCode, s => s.ItemCode)
            .Map(d => d.ItemName, s => s.ItemName)
            .Map(d => d.Quantity, s => s.Quantity)
            .Map(d => d.UoMCode, s => s.UoMCode)
            .Map(d => d.UoMValue, s => s.UoMValue)
            .Map(d => d.UoMName, s => s.UoMName)
            .Map(d => d.InputType, s => EnumHelper.ParseStringToEnum<InputType>(s.InputType));

        config.NewConfig<PurchaseOrderDTO, PurchaseDeliveryNoteDTO>()
            .Map(d => d.SapReference, s => new SapDocumentReferenceDTO()
            {
                BaseEntry = s.SapReference.DocEntry,
                BaseDocNum = s.SapReference.DocNum,
            })
            .Map(d => d.BusinessPartner, s => s.BusinessPartner)
            .Map(d => d.DocDate, s => s.DocDate)
            .Map(d => d.DocDueDate, s => s.DocDueDate)
            .Map(d => d.ReceivedBy, s => s.ReceivedBy);

        config.NewConfig<PurchaseOrderLineDTO, PurchaseDeliveryNoteLineDTO>()
            .Map(d => d.BaseEntry, s => s.DocEntry)
            .Map(d => d.BaseDocNum, s => s.DocNum)
            .Map(d => d.LineNum, s => s.LineNum)
            .Map(d => d.BaseLine, s => s.LineNum)
            .Map(d => d.ItemCode, s => s.ItemCode)
            .Map(d => d.ItemName, s => s.ItemName)
            .Map(d => d.TaxCode, s => s.VatGroup)
            .Map(d => d.Quantity, s => s.Quantity)
            .Map(d => d.Warehouse, s => s.Warehouse)
            .Map(d => d.InputType, s => s.InputType);

        #endregion Receiving

        #endregion SAP DTO to DTO
    }
}

