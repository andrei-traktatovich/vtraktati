using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using VTraktate.Domain;
using VTraktate.Models;
using VTraktate.Models.Order;
using VTraktate.Domain.ComplexTypes;
using VTraktate.Domain.Snapshots;
using VTraktate.Models.Order.JobPart;

namespace VTraktate 
{
    public static class AutoMapperConfig
    {
        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>
            (this IMappingExpression<TSource, TDestination> expression)
        {
            var sourceType = typeof(TSource);
            var destinationType = typeof(TDestination);
            var existingMaps = Mapper.GetAllTypeMaps().First(x => x.SourceType.Equals(sourceType) && x.DestinationType.Equals(destinationType));
            foreach (var property in existingMaps.GetUnmappedPropertyNames())
            {
                expression.ForMember(property, opt => opt.Ignore());
            }
            return expression;
        }
        
        public static void RegisterMappings()
        {
            Mapper.CreateMap<Email, EmailViewModel>()
                .ForMember(x => x.CreatedByName, conf => conf.MapFrom(y => y.CreatedBy.PersonName.FullName))
                .ForMember(x => x.ModifiedByName, conf => conf.MapFrom(y => y.ModifiedBy.PersonName.FullName))
                .ForMember(x => x.Email, conf => conf.MapFrom(y => y.Address.Email))
                .ForMember(x => x.Comment, conf => conf.MapFrom(y => y.Address.Comment))
                .ForMember(x => x.Active, conf => conf.MapFrom(y => y.Address.Active))
                .ForMember(x => x.Id, conf => conf.MapFrom(y => y.Id))
                .ForMember(x => x.ContactPersonId, conf => conf.MapFrom(y => y.ContactPersonId))
                .ForMember(x => x.IsDeleted, conf => conf.MapFrom(y => y.IsDeleted));

            Mapper.AssertConfigurationIsValid();

            Mapper.CreateMap<Phone, PhoneViewModel>()
                .ForMember(x => x.Phone, conf => conf.MapFrom(y => y.PhoneNumber))
                .ForMember(x => x.CreatedByName, conf => conf.MapFrom(y => y.CreatedBy.PersonName.FullName))
                .ForMember(x => x.ModifiedByName, conf => conf.MapFrom(y => y.CreatedBy.PersonName.FullName));
            Mapper.AssertConfigurationIsValid();

            Mapper.CreateMap<OtherContactType, IdNamePairBindingModel>();
            Mapper.CreateMap<OtherContact, OtherContactViewModel>()
                .ForMember(x => x.Active, conf => conf.MapFrom(y => y.Active))
                .ForMember(x => x.Address, conf => conf.MapFrom(y => y.Address))
                .ForMember(x => x.Comment, conf => conf.MapFrom(y => y.Comment))
                .ForMember(x => x.CreatedByName, conf => conf.MapFrom(y => y.CreatedBy.PersonName.FullName))
                .ForMember(x => x.CreatedDate, conf => conf.MapFrom(y => y.CreatedDate))
                .ForMember(x => x.ModifiedByName, conf => conf.MapFrom(y => y.ModifiedBy.PersonName.FullName))
                .ForMember(x => x.ModifiedDate, conf => conf.MapFrom(y => y.ModifiedDate))
                .ForMember(x => x.Type, conf => conf.MapFrom(y => y.Type));
            
            Mapper.CreateMap<Person, ContactPersonViewModel>()
                .ForMember(x => x.FullName, conf => conf.MapFrom(y => y.PersonName.FullName))
                .ForMember(x => x.CreatedByName, conf => conf.MapFrom(y => y.CreatedBy.PersonName.FullName))
                .ForMember(x => x.ModifiedByName, conf => conf.MapFrom(y => y.ModifiedBy.PersonName.FullName))
                .ForMember(x => x.Emails, conf => conf.MapFrom(y => y.Emails))
                .ForMember(x => x.Phones, conf => conf.MapFrom(y => y.Phones));
            Mapper.AssertConfigurationIsValid();

            Mapper.CreateMap<ServiceDomainInfo, ServiceDomainViewModel>()
                .ForMember(x => x.DomainName, conf => conf.MapFrom(y => y.Domain.Name))
                .ForMember(x => x.Stars, conf => conf.MapFrom(y => (int)y.QA.Stars))
                .ForMember(x => x.Grade, conf => conf.MapFrom(y => y.QA.Grade))
                .ForMember(x => x.Comment, conf => conf.MapFrom(y => y.QA.Comment))
                .ForMember(x => x.CreatedByName, conf => conf.MapFrom(y => y.CreatedBy.PersonName.FullName))
                .ForMember(x => x.ModifiedByName, conf => conf.MapFrom(y => y.ModifiedBy.PersonName.FullName));
                 

            Mapper.AssertConfigurationIsValid();

            Mapper.CreateMap<ServiceLanguageInfo, ServiceLanguageViewModel>()
                .ForMember(x => x.LanguagePairName, conf => conf.MapFrom(y => y.LanguagePair.Name))
                .ForMember(x => x.QAStars, conf => conf.MapFrom(y => (int)y.QA.Stars))
                .ForMember(x => x.MinRate, conf => conf.MapFrom(y => y.Rate.Minrate))
                .ForMember(x => x.MaxRate, conf => conf.MapFrom(y => y.Rate.MaxRate))
                .ForMember(x => x.CreatedByName, conf => conf.MapFrom(y => y.CreatedBy.PersonName.FullName))
                .ForMember(x => x.ModifiedByName, conf => conf.MapFrom(y => y.ModifiedBy.PersonName.FullName));
            
            Mapper.AssertConfigurationIsValid();

            Mapper.CreateMap<Service, ServiceViewModel>()
                .ForMember(x => x.ServiceTypeName, conf => conf.MapFrom(y => y.ServiceType.Name))
                .ForMember(x => x.MinRate, conf => conf.MapFrom(y => y.Rate.Minrate))
                .ForMember(x => x.MaxRate, conf => conf.MapFrom(y => y.Rate.MaxRate))
                .ForMember(x => x.QAStars, conf => conf.MapFrom(y => (int)y.QA.Stars))
                .ForMember(x => x.CreatedByName, conf => conf.MapFrom(y => y.CreatedBy.PersonName.FullName))
                .ForMember(x => x.ModifiedByName, conf => conf.MapFrom(y => y.ModifiedBy.PersonName.FullName))
                .ForMember(x => x.NeedsDomains, conf => conf.MapFrom(y => y.ServiceType.SpecifyDomains))
                .ForMember(x => x.NeedsLanguage, conf => conf.MapFrom(y => y.ServiceType.SpecifyLanguage))
                .ForMember(x => x.ServiceUOMName, conf => conf.MapFrom(y => y.ServiceUOM.Name));


            Mapper.AssertConfigurationIsValid();

            Mapper.CreateMap<Office, IdNamePairBindingModel>();
            Mapper.CreateMap<Title, IdNamePairBindingModel>();
            Mapper.CreateMap<EmploymentStatus, IdNamePairBindingModel>();

            Mapper.CreateMap<Employment, EmploymentViewModel>()
                .ForMember(x => x.CreatedByName, conf => conf.MapFrom(y => y.CreatedBy.PersonName.FullName))
                .ForMember(x => x.ModifiedByName, conf => conf.MapFrom(y => y.ModifiedBy.PersonName.FullName));

            Mapper.AssertConfigurationIsValid();

            Mapper.CreateMap<FreelanceStatus, IdNamePairBindingModel>();
            Mapper.CreateMap<Freelance, FreelanceViewModel>()
                .ForMember(x => x.CreatedByName, conf => conf.MapFrom(y => y.CreatedBy.PersonName.FullName))
                .ForMember(x => x.ModifiedByName, conf => conf.MapFrom(y => y.ModifiedBy.PersonName.FullName));
            Mapper.AssertConfigurationIsValid();

            Mapper.CreateMap<ProviderSoft, IdNamePairBindingModel>();
            Mapper.CreateMap<Region, IdNamePairBindingModel>();
            Mapper.CreateMap<LegalForm, IdNamePairBindingModel>();


            Mapper.CreateMap<Provider, ProviderProfileAddressViewModel>()
                .ForMember(x => x.City, conf => conf.MapFrom(y => y.City))
                .ForMember(x => x.Address, conf => conf.MapFrom(y => y.Address))
                .ForMember(x => x.RegionId, conf => conf.MapFrom(y => y.Region.Id))
                .ForMember(x => x.LegalFormId, conf => conf.MapFrom(y => y.LegalFormId))
                .ForMember(x => x.WorksNightly, conf => conf.MapFrom(y => y.WorksNightly))
                .ForMember(x => x.TimeDifference, conf => conf.MapFrom(y => y.TimeDifference))
                .ForMember(x => x.Id, conf => conf.MapFrom(y => y.Id));

            Mapper.CreateMap<Provider, ProviderProfileViewModel>()
                .ForMember(x => x.ModifiedByName, conf => conf.MapFrom(y => y.ModifiedBy.PersonName.FullName))
                .ForMember(x => x.CreatedByName, conf => conf.MapFrom(y => y.ModifiedBy.PersonName.FullName))
                .ForMember(x => x.ContactPersons, conf => conf.MapFrom(y => y.ContactPersons))
                .ForMember(x => x.Services, conf => conf.MapFrom(y => y.Services))
                .ForMember(x => x.Employments, conf => conf.MapFrom(y => y.Employments))
                .ForMember(x => x.Soft, conf => conf.MapFrom(y => y.Soft))
                .ForMember(x => x.General, conf => conf.MapFrom(y => y));
            Mapper.AssertConfigurationIsValid();

            Mapper.CreateMap<DomainBindingModel, ServiceDomainInfo>()
                .ForMember(x => x.Id, conf => conf.Ignore())
                .ForMember(x => x.CreatedBy, conf => conf.Ignore())
                .ForMember(x => x.ModifiedBy, conf => conf.Ignore())
                .ForMember(x => x.LanguageId, conf => conf.Ignore())
                .ForMember(x => x.Domain, conf => conf.Ignore())
                .ForMember(x => x.Language, conf => conf.Ignore())
                .ForMember(x => x.CreatedById, conf => conf.Ignore())
                .ForMember(x => x.ModifiedById, conf => conf.Ignore())
                .ForMember(x => x.CreatedDate, conf => conf.Ignore())
                .ForMember(x => x.ModifiedDate, conf => conf.Ignore())
                .ForMember(x => x.DomainId, conf => conf.MapFrom(source => source.Domain.Id))
                .ForMember(x => x.QA, conf => conf.MapFrom(source => new QA
                    {
                        Comment = source.Comment,
                        Grade = 0,
                        Stars = (Stars)source.Stars
                    }))
                .ForMember(x => x.GradesAsPrimary, conf => conf.Ignore())
                .ForMember(x => x.GradesAsSecondary, conf => conf.Ignore());
            Mapper.AssertConfigurationIsValid();

            Mapper.CreateMap<LanguageBindingModel, ServiceLanguageInfo>()
                .ForMember(x => x.CreatedById, conf => conf.Ignore())   // my use case = this stuff is only for Create
                .ForMember(x => x.CreatedDate, conf => conf.Ignore())
                .ForMember(x => x.ModifiedById, conf => conf.Ignore())
                .ForMember(x => x.ModifiedDate, conf => conf.Ignore())
                .ForMember(x => x.LanguagePair, conf => conf.Ignore())
                .ForMember(x => x.Id, conf => conf.Ignore())
                .ForMember(x => x.ServiceId, conf => conf.Ignore())
                .ForMember(x => x.Service, conf => conf.Ignore())
                .ForMember(x => x.CreatedBy, conf => conf.Ignore())
                .ForMember(x => x.ModifiedBy, conf => conf.Ignore())
                .ForMember(x => x.Comment, conf => conf.MapFrom(source => source.Comment))
                .ForMember(x => x.Domains, conf => conf.MapFrom(source => source.Domains))
                .ForMember(x => x.LanguagePairId, conf => conf.MapFrom(source => source.LanguagePair.Id))
                .ForMember(x => x.QA, conf => conf.MapFrom(source => new QA
                {
                    Comment = source.Comment,
                    Grade = 0,
                    Stars = (Stars)source.Stars
                }))
                .ForMember(x => x.Rate, conf => conf.MapFrom(source => new ServiceRate
                {
                    Minrate = source.MinRate,
                    MaxRate = source.MaxRate
                }))
                .ForMember(x => x.NativeSpeaker, conf => conf.Ignore())
                .ForMember(x => x.Grades, conf => conf.Ignore());
                
            Mapper.AssertConfigurationIsValid();

            Mapper.CreateMap<ServiceBindingModel, Service>()
                .ForMember(x => x.CreatedById, conf => conf.Ignore())   // my use case = this stuff is only for Create
                .ForMember(x => x.CreatedDate, conf => conf.Ignore())
                .ForMember(x => x.ModifiedById, conf => conf.Ignore())
                .ForMember(x => x.ModifiedDate, conf => conf.Ignore())

                .ForMember(x => x.Id, conf => conf.Ignore())
                .ForMember(x => x.Provider, conf => conf.Ignore())
                .ForMember(x => x.ProviderId, conf => conf.Ignore())
                .ForMember(x => x.ServiceType, conf => conf.Ignore())
                .ForMember(x => x.Currency, conf => conf.Ignore())
                .ForMember(x => x.ServiceUOM, conf => conf.Ignore())
                .ForMember(x => x.CreatedBy, conf => conf.Ignore())
                .ForMember(x => x.ModifiedBy, conf => conf.Ignore())
                .ForMember(x => x.LegacyId, conf => conf.Ignore())
                .ForMember(x => x.CurrencyId, conf => conf.MapFrom(source => source.CurrencyId))
                .ForMember(x => x.Languages, conf => conf.MapFrom(source => source.Languages))
                .ForMember(x => x.QA, conf => conf.MapFrom(source => new QA
                {
                    Comment = source.Comment,
                    Grade = 0,
                    Stars = (Stars)source.Stars
                }))
                .ForMember(x => x.Rate, conf => conf.MapFrom(source => new ServiceRate
                {
                    Minrate = source.MinRate,
                    MaxRate = source.MaxRate
                }))
                .ForMember(x => x.ServiceTypeId, conf => conf.MapFrom(source => source.Type.Id))
                .ForMember(x => x.ServiceUOMId, conf => conf.MapFrom(source => source.UomId))
                .ForMember(x => x.Grades, conf => conf.Ignore());
            Mapper.AssertConfigurationIsValid();

            Mapper.CreateMap<EmailBindingModel, Email>()
                .ForMember(x => x.CreatedById, conf => conf.Ignore())   // my use case = this stuff is only for Create
                .ForMember(x => x.CreatedDate, conf => conf.Ignore())
                .ForMember(x => x.ModifiedById, conf => conf.Ignore())
                .ForMember(x => x.ModifiedDate, conf => conf.Ignore())
                .ForMember(x => x.Id, conf => conf.Ignore())
                .ForMember(x => x.ContactPersonId, conf => conf.Ignore())
                .ForMember(x => x.ContactPerson, conf => conf.Ignore())
                .ForMember(x => x.IsDeleted, conf => conf.Ignore())
                .ForMember(x => x.CreatedBy, conf => conf.Ignore())
                .ForMember(x => x.ModifiedBy, conf => conf.Ignore())
                .ForMember(x => x.Address, conf => conf.MapFrom(source => new Domain.ComplexTypes.EmailAddress
                {
                    Email = source.Email,
                    Active = source.Active,
                    Comment = source.Comment
                }));
            Mapper.AssertConfigurationIsValid();

            Mapper.CreateMap<EmploymentBindingModel, Employment>()
                .ForMember(x => x.CreatedById, conf => conf.Ignore())   // my use case = this stuff is only for Create
                .ForMember(x => x.CreatedDate, conf => conf.Ignore())
                .ForMember(x => x.ModifiedById, conf => conf.Ignore())
                .ForMember(x => x.ModifiedDate, conf => conf.Ignore())
                .ForMember(x => x.Id, conf => conf.Ignore())
                .ForMember(x => x.Provider, conf => conf.Ignore())
                .ForMember(x => x.Title, conf => conf.Ignore())
                .ForMember(x => x.Office, conf => conf.Ignore())
                .ForMember(x => x.Status, conf => conf.Ignore())
                .ForMember(x => x.IsDeleted, conf => conf.Ignore())
                .ForMember(x => x.ProviderId, conf => conf.Ignore())
                .ForMember(x => x.CreatedBy, conf => conf.Ignore())
                .ForMember(x => x.ModifiedBy, conf => conf.Ignore())
                .ForMember(x => x.CalendarPeriods, conf => conf.Ignore())
                .ForMember(x => x.Comment, conf => conf.MapFrom(source => source.Comment))
                .ForMember(x => x.TitleId, conf => conf.MapFrom(source => source.TitleId))
                .ForMember(x => x.OfficeID, conf => conf.MapFrom(source => source.OfficeId))
                .ForMember(x => x.StartDate, conf => conf.MapFrom(source => source.StartDate))
                .ForMember(x => x.EndDate, conf => conf.MapFrom(source => source.EndDate))
                .ForMember(x => x.StatusID, conf => conf.MapFrom(source => source.StatusId));
            Mapper.AssertConfigurationIsValid();

            AutoMapper.Mapper.CreateMap<FreelanceBindingModel, Freelance>()
                .ForMember(x => x.FreelanceStatusID, conf => conf.MapFrom(source => source.StatusId))
                .ForMember(x => x.Comment, conf => conf.MapFrom(source => source.Comment))
                .ForMember(x => x.Id, conf => conf.Ignore())
                .ForMember(x => x.CreatedBy, conf => conf.Ignore())
                .ForMember(x => x.CreatedById, conf => conf.Ignore())
                .ForMember(x => x.EndDate, conf => conf.Ignore())
                .ForMember(x => x.FreelanceStatus, conf => conf.Ignore())
                .ForMember(x => x.IsDeleted, conf => conf.Ignore())
                .ForMember(x => x.ModifiedBy, conf => conf.Ignore())
                .ForMember(x => x.ModifiedById, conf => conf.Ignore())
                .ForMember(x => x.ModifiedDate, conf => conf.Ignore())
                .ForMember(x => x.Provider, conf => conf.Ignore())
                .ForMember(x => x.ProviderID, conf => conf.Ignore())
                .ForMember(x => x.CreatedDate, conf => conf.Ignore())
                .ForMember(x => x.StartDate, conf => conf.Ignore()); // ATTN: I am here ignoring the start date !!! 

            Mapper.AssertConfigurationIsValid();

            Mapper.CreateMap<PhoneBindingModel, Phone>()
                .ForMember(x => x.CreatedById, conf => conf.Ignore())   // my use case = this stuff is only for Create
                .ForMember(x => x.CreatedDate, conf => conf.Ignore())
                .ForMember(x => x.ModifiedById, conf => conf.Ignore())
                .ForMember(x => x.ModifiedDate, conf => conf.Ignore())
                .ForMember(x => x.Id, conf => conf.Ignore())
                .ForMember(x => x.ContactPersonId, conf => conf.Ignore())
                .ForMember(x => x.ContactPerson, conf => conf.Ignore())
                .ForMember(x => x.IsDeleted, conf => conf.Ignore())
                .ForMember(x => x.CreatedBy, conf => conf.Ignore())
                .ForMember(x => x.ModifiedBy, conf => conf.Ignore())
                .ForMember(x => x.PhoneNumber, conf => conf.MapFrom(src => src.Phone))
                .ForMember(x => x.Ext, conf => conf.MapFrom(src => src.Ext))
                .ForMember(x => x.Active, conf => conf.MapFrom(src => src.Active))
                .ForMember(x => x.TypeId, conf => conf.MapFrom(src => src.TypeId))
                .ForMember(x => x.Type, conf => conf.Ignore())
                .ForMember(x => x.Comment, conf => conf.MapFrom(src => src.Comment));
            Mapper.AssertConfigurationIsValid();

            Mapper.CreateMap<OtherContactsBindingModel, OtherContact>()
                .ForMember(x => x.CreatedById, conf => conf.Ignore())   // my use case = this stuff is only for Create
                .ForMember(x => x.CreatedDate, conf => conf.Ignore())
                .ForMember(x => x.ModifiedById, conf => conf.Ignore())
                .ForMember(x => x.ModifiedDate, conf => conf.Ignore())
                .ForMember(x => x.Id, conf => conf.Ignore())
                .ForMember(x => x.PersonId, conf => conf.Ignore())
                .ForMember(x => x.Person, conf => conf.Ignore())
                .ForMember(x => x.IsDeleted, conf => conf.Ignore())
                .ForMember(x => x.CreatedBy, conf => conf.Ignore())
                .ForMember(x => x.ModifiedBy, conf => conf.Ignore())
                .ForMember(x => x.Address, conf => conf.MapFrom(src => src.Address))
                .ForMember(x => x.Active, conf => conf.MapFrom(src => src.Active))
                .ForMember(x => x.TypeId, conf => conf.MapFrom(src => src.Type))
                .ForMember(x => x.Type, conf => conf.Ignore())
                .ForMember(x => x.Comment, conf => conf.MapFrom(src => src.Comment));
            Mapper.AssertConfigurationIsValid();

            Mapper.CreateMap<ProviderProfileOtherContactBindingModel, OtherContact>()
                .ForMember(x => x.CreatedById, conf => conf.Ignore())   // my use case = this stuff is only for Create
                .ForMember(x => x.CreatedDate, conf => conf.Ignore())
                .ForMember(x => x.ModifiedById, conf => conf.Ignore())
                .ForMember(x => x.ModifiedDate, conf => conf.Ignore())
                .ForMember(x => x.Id, conf => conf.Ignore())
                .ForMember(x => x.PersonId, conf => conf.Ignore())
                .ForMember(x => x.Person, conf => conf.Ignore())
                .ForMember(x => x.IsDeleted, conf => conf.Ignore())
                .ForMember(x => x.CreatedBy, conf => conf.Ignore())
                .ForMember(x => x.ModifiedBy, conf => conf.Ignore())
                .ForMember(x => x.Address, conf => conf.MapFrom(src => src.Address))
                .ForMember(x => x.Active, conf => conf.MapFrom(src => src.Active))
                .ForMember(x => x.TypeId, conf => conf.MapFrom(src => src.TypeId))
                .ForMember(x => x.Type, conf => conf.Ignore())
                .ForMember(x => x.Comment, conf => conf.MapFrom(src => src.Comment));
            Mapper.AssertConfigurationIsValid();

            Mapper.CreateMap<ProviderAvailabilityStatus, IdNamePairBindingModel>();
            Mapper.CreateMap<FreelanceCalendarPeriod, FreelanceCalendarPeriodViewModel>()
                .ForMember(x => x.EndDate, conf => conf.MapFrom(y => y.EndDate))
                .ForMember(x => x.Id, conf => conf.MapFrom(y => y.Id))
                .ForMember(x => x.StartDate, conf => conf.MapFrom(y => y.StartDate))
                .ForMember(x => x.Status, conf => conf.MapFrom(y => y.Status));
            Mapper.AssertConfigurationIsValid();

            Mapper.CreateMap<FreelanceCalendarPeriodBindingModel, FreelanceCalendarPeriod>()
                .ForMember(x => x.Comment, conf => conf.MapFrom(y => y.Comment))
                .ForMember(x => x.CreatedBy, conf => conf.Ignore())
                .ForMember(x => x.CreatedById, conf => conf.Ignore())
                .ForMember(x => x.CreatedDate, conf => conf.Ignore())
                .ForMember(x => x.EndDate, conf => conf.MapFrom(y => y.EndDate))
                .ForMember(x => x.IsDeleted, conf => conf.Ignore())
                .ForMember(x => x.ModifiedBy, conf => conf.Ignore())
                .ForMember(x => x.ModifiedById, conf => conf.Ignore())
                .ForMember(x => x.ModifiedDate, conf => conf.Ignore())
                .ForMember(x => x.Provider, conf => conf.Ignore())
                .ForMember(x => x.ProviderId, conf => conf.Ignore())
                .ForMember(x => x.StartDate, conf => conf.MapFrom(y => y.StartDate))
                .ForMember(x => x.StatusId, conf => conf.MapFrom(y => y.StatusId))
                .ForMember(x => x.Id, conf => conf.Ignore())
                .ForMember(x => x.Status, conf => conf.Ignore());
            Mapper.AssertConfigurationIsValid();

            Mapper.CreateMap<GradeErrorDetailsModel, ErrorInfo>();
            Mapper.CreateMap<GradeBonusDetailsModel, Bonus>()
                .ForMember(x => x.NativeSpeaker, conf => conf.MapFrom(y => y.Native));

            Mapper.CreateMap<LegacyGradeBindingModel, Grade>()
                .ForMember(x => x.Bonus, conf => conf.MapFrom(y => y.Bonus))
                .ForMember(x => x.Comment, conf => conf.MapFrom(y => y.Comment))
                .ForMember(x => x.CreatedBy, conf => conf.Ignore())
                .ForMember(x => x.CreatedById, conf => conf.Ignore())
                .ForMember(x => x.CreatedDate, conf => conf.Ignore())
                .ForMember(x => x.Error, conf => conf.MapFrom(y => y.Error))
                .ForMember(x => x.Id, conf => conf.MapFrom(y => y.Id))
                .ForMember(x => x.IsDeleted, conf => conf.Ignore())
                .ForMember(x => x.JobPart, conf => conf.Ignore())
                .ForMember(x => x.JobPartId, conf => conf.Ignore())
                .ForMember(x => x.LegacyJobName, conf => conf.MapFrom(y => y.LegacyJobName))
                .ForMember(x => x.LanguagePair, conf => conf.Ignore())
                .ForMember(x => x.LanguagePairId, conf => conf.MapFrom(y => y.LanguageId))

                .ForMember(x => x.PrimaryDomain, conf => conf.Ignore())
                .ForMember(x => x.PrimaryDomainId, conf => conf.MapFrom(y => y.Domain1Id))

                .ForMember(x => x.SecondaryDomain, conf => conf.Ignore())
                .ForMember(x => x.SecondaryDomainId, conf => conf.MapFrom(y => y.Domain2Id))

                .ForMember(x => x.ModifiedBy, conf => conf.Ignore())
                .ForMember(x => x.ModifiedById, conf => conf.Ignore())
                .ForMember(x => x.ModifiedDate, conf => conf.Ignore())
                .ForMember(x => x.Provider, conf => conf.Ignore())
                .ForMember(x => x.ProviderId, conf => conf.MapFrom(y => y.ProviderId))
                .ForMember(x => x.Score, conf => conf.MapFrom(y => y.Score))
                .ForMember(x => x.ServiceGraded, conf => conf.Ignore())
                .ForMember(x => x.ServiceGradedId, conf => conf.Ignore())
                .ForMember(x => x.ServiceLanguageInfoGraded, conf => conf.Ignore())
                .ForMember(x => x.ServiceLanguageInfoGradedId, conf => conf.Ignore())
                .ForMember(x => x.PrimaryDomainGraded, conf => conf.Ignore())
                .ForMember(x => x.PrimaryDomainGradedId, conf => conf.Ignore())
                .ForMember(x => x.SecondaryDomainGraded, conf => conf.Ignore())
                .ForMember(x => x.SecondaryDomainGradedId, conf => conf.Ignore())
                .ForMember(x => x.ServiceType, conf => conf.Ignore())
                .ForMember(x => x.ServiceTypeId, conf => conf.MapFrom(y => y.ServiceTypeId.GetValueOrDefault()));
            Mapper.AssertConfigurationIsValid();

            Mapper.CreateMap<Customer, CustomerAutoSuggestModel>()
                .ForMember(x => x.Code, conf => conf.MapFrom(y => y.Code))
                .ForMember(x => x.ShortName, conf => conf.MapFrom(y => y.ShortName))
                .ForMember(x => x.LongName, conf => conf.MapFrom(y => y.LongName));
            Mapper.AssertConfigurationIsValid();

            // OrderContactInfo deprecated  Mapper.CreateMap<OrderContactInfo, CustomerContactPersonViewModel>();
            Mapper.AssertConfigurationIsValid();

            // TODO: FUCKING SHEET!!! 
            Mapper.CreateMap<CustomerProfile, CustomerProfileViewModel>()
                .ForMember(x => x.DefaultContactPerson, conf => conf.Ignore());
            Mapper.AssertConfigurationIsValid();

            Mapper.CreateMap<VolumeModel, Volume>();
            Mapper.CreateMap<JobPricingModel, JobPricing>();
            Mapper.CreateMap<JobVolumeAndPricingModel, JobVolumeAndPricing>();
            Mapper.CreateMap<JobCreateModel, Job>()
                .ForMember(x => x.StartDate, conf => conf.MapFrom(y => y.StartDate.ToLocalTime()))
                .ForMember(x => x.EndDate, conf => conf.MapFrom(y => y.EndDate.ToLocalTime()))
                .IgnoreAllNonExisting();
            Mapper.AssertConfigurationIsValid();

            Mapper.CreateMap<JobEditBindingModel, Job>()
                .IncludeBase<JobCreateModel, Job>();
            Mapper.AssertConfigurationIsValid();

            // deprecated Mapper.CreateMap<OrderContactPersonModel, OrderContactPerson>();
            Mapper.CreateMap<OrderCreateBindingModel, Order>()
                // DEPRECATED  .ForMember(x => x.ContactPerson, conf => conf.MapFrom(y => y.ContactPerson))
                .IgnoreAllNonExisting();
            Mapper.AssertConfigurationIsValid();

            // Deprecated             Mapper.CreateMap<OrderContactPerson, OrderContactPersonModel>();
            Mapper.AssertConfigurationIsValid();

            // TODO: FUCKING SHEET 
            Mapper.CreateMap<Order, OrderGridViewModel.OrderGridViewCustomerViewModel>()
                .ForMember(x => x.Code, conf => conf.MapFrom(y => y.Customer.Code))
                .ForMember(x => x.Id, conf => conf.MapFrom(y => y.Customer.Id))
                .ForMember(x => x.LongName, conf => conf.MapFrom(y => y.Customer.LongName))
                .ForMember(x => x.ShortName, conf => conf.MapFrom(y => y.Customer.ShortName))
                .ForMember(x => x.ContactPerson, conf => conf.Ignore())
                // DEPRECATED .ForMember(x => x.ContactPerson, conf => conf.MapFrom(y => y.ContactPerson))
                ;
            Mapper.AssertConfigurationIsValid();

            Mapper.CreateMap<TranslationDomain, IdNamePairBindingModel>()
                .IgnoreAllNonExisting();
            Mapper.AssertConfigurationIsValid();

            Mapper.CreateMap<JobCompletionStatus, IdNamePairBindingModel>()
                .IgnoreAllNonExisting();
            Mapper.AssertConfigurationIsValid();

            Mapper.CreateMap<JobType, IdNamePairBindingModel>()
                .IgnoreAllNonExisting();
            Mapper.AssertConfigurationIsValid();

            Mapper.CreateMap<LanguagePair, IdNamePairBindingModel>()
                .IgnoreAllNonExisting();
            Mapper.AssertConfigurationIsValid();

            Mapper.CreateMap<JobPartCompletionStatus, IdNamePairBindingModel>();
            Mapper.AssertConfigurationIsValid();

            Mapper.CreateMap<Provider, IdNamePairBindingModel>();
            Mapper.AssertConfigurationIsValid();

            Mapper.CreateMap<VTraktate.Domain.ComplexTypes.Volume, VolumeModel>();
            Mapper.CreateMap<VTraktate.Domain.ComplexTypes.JobPricing, JobPricingModel>();
            Mapper.AssertConfigurationIsValid();

            Mapper.CreateMap<VTraktate.Domain.ComplexTypes.JobVolumeAndPricing, VTraktate.Models.Order.JobVolumeAndPricingModel>();
            Mapper.AssertConfigurationIsValid();

            Mapper.CreateMap<Currency, IdNamePairBindingModel>();
            Mapper.AssertConfigurationIsValid();

            Mapper.CreateMap<ServiceUOM, IdNamePairBindingModel>();
            Mapper.AssertConfigurationIsValid();

            Mapper.CreateMap<JobPart, OrderGridJobPartViewModel>()
                .ForMember(x => x.CreatedByName, conf => conf.MapFrom(y => y.CreatedBy.PersonName.FullName))
                .ForMember(x => x.ModifiedByName, conf => conf.MapFrom(y => y.ModifiedBy.PersonName.FullName))
                .IgnoreAllNonExisting();
            Mapper.AssertConfigurationIsValid();

            Mapper.CreateMap<Order, IdNamePairBindingModel>();
            Mapper.AssertConfigurationIsValid();

            
            /*this model is deprecated 
            Mapper.CreateMap<Order, OrderContactPersonModel>()
                // CONTACT PERSON DEPRECATED, rethink .ForMember(x => x.Comment, y => y.MapFrom(z => z.ContactPerson.Comment))
                //.ForMember(x => x.Email, y => y.MapFrom(z => z.ContactPerson.Email))
                //.ForMember(x => x.Phone, y => y.MapFrom(z => z.ContactPerson.Phone))
                //.ForMember(x => x.Ext, y => y.MapFrom(z => z.ContactPerson.Ext))
                //.ForMember(x => x.FullName, y => y.MapFrom(z => z.ContactPerson.FullName));
            Mapper.AssertConfigurationIsValid();
            */

            // TODO: FUCKING SHEET
            Mapper.CreateMap<Order, OrderGridViewModel.OrderGridViewCustomerViewModel>()
                .ForMember(x => x.ContactPerson, conf => conf.MapFrom(y => y))
                .ForMember(x => x.Code, conf => conf.MapFrom(y => y.Customer.Code))
                .ForMember(x => x.LongName, conf => conf.MapFrom(y => y.Customer.LongName))
                .ForMember(x => x.ShortName, conf => conf.MapFrom(y => y.Customer.ShortName))
                .ForMember(x => x.ContactPerson, conf => conf.Ignore());
            Mapper.AssertConfigurationIsValid();

            Mapper.CreateMap<Currency, IdNamePairBindingModel>();
            Mapper.AssertConfigurationIsValid();

            Mapper.CreateMap<JobUOM, IdNamePairBindingModel>();
            Mapper.AssertConfigurationIsValid();

            Mapper.CreateMap<Job, OrderGridViewModel>()
                .ForMember(x => x.PaymentStatus, conf => conf.Ignore()) // I am not yet supporting this !!! 
                .ForMember(x => x.EndDate, conf => conf.MapFrom(y => y.EndDate))
                .ForMember(x => x.Customer, conf => conf.MapFrom(y => y.Order))
                .ForMember(x => x.Document, conf => conf.MapFrom(y => y.Document))
                .ForMember(x => x.Domain1, conf => conf.MapFrom(y => y.Domain1))
                .ForMember(x => x.Domain2, conf => conf.MapFrom(y => y.Domain2))
                .ForMember(x => x.JobType, conf => conf.MapFrom(y => y.JobType))
                .ForMember(x => x.Language, conf => conf.MapFrom(y => y.Language))
                .ForMember(x => x.Status, conf => conf.MapFrom(y => y.Status))
                .ForMember(x => x.Initial, conf => conf.MapFrom(y => y.Initial))
                .ForMember(x => x.Final, conf => conf.MapFrom(y => y.Final))
                .ForMember(x => x.JobParts, conf => conf.MapFrom(y => y.JobParts))
                .ForMember(x => x.Order, conf => conf.MapFrom(y => y.Order))
                .ForMember(x => x.CreatedByName, conf => conf.MapFrom(y => y.CreatedBy.PersonName.FullName))
                .ForMember(x => x.ModifiedByName, conf => conf.MapFrom(y => y.ModifiedBy.PersonName.FullName));

            Mapper.AssertConfigurationIsValid();
            // a way to map needed !!!
            /*Mapper.CreateMap<Person, CustomerContactPersonViewModel>()
                .ForMember(x => x.FullName, conf => conf.MapFrom(y => y.PersonName.FullName))
                .ForMember(x => x.Email, conf => conf.MapFrom(y => y.Emails))
                .ForMember(x => x.Phone, conf => conf.MapFrom(y => y.Phones));
            */
            Mapper.CreateMap<Volume, VolumeModel>();
            Mapper.AssertConfigurationIsValid();

            Mapper.CreateMap<JobPricingModel, JobPricing>();
            Mapper.AssertConfigurationIsValid();

            Mapper.CreateMap<JobVolumeAndPricingModel, JobVolumeAndPricing>();
            Mapper.AssertConfigurationIsValid();

            Mapper.CreateMap<JobPartCreateBindingModel, JobPart>()
                .IgnoreAllNonExisting();
            Mapper.AssertConfigurationIsValid();

            Mapper.CreateMap<JobPartEditBindingModel, JobPart>()
                .IgnoreAllNonExisting();
            Mapper.AssertConfigurationIsValid();
        }
    }
}