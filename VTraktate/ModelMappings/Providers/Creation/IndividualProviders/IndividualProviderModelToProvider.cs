using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTraktate.Domain;
using VTraktate.Domain.ComplexTypes;
using VTraktate.ModelMappings.Converters;
using VTraktate.Models.Person;
using VTraktate.Models.Provider.Creation;
using VTraktate.Models.Provider.Creation.IndividualProvider;

namespace VTraktate.ModelMappings.Providers.Creation.IndividualProvider
{
    public static class IndividualProviderModelToProvider
    {
        internal class IndividualProviderModelToPersonConverter : SingleToCollectionConverter<IndividualProviderModel, Person>
        { }

        internal class PromotionConverter : SingleToCollectionConverter<PromotionModel, Promotion>
        { }

        public static void Map()
        {
            Mapper.CreateMap<IndividualDetailsBindingModel, Provider>();

            Mapper.CreateMap<PersonExtendedNameBindingModel, Provider>()
                .ForMember(x => x.Name, conf => conf.MapFrom(src => src.FullName));

            Mapper
                .CreateMap<IndividualProviderModel, IEnumerable<Person>>()
                .ConvertUsing<IndividualProviderModelToPersonConverter>();

            Mapper
                .CreateMap<PromotionModel, IEnumerable<Promotion>>()
                .ConvertUsing<PromotionConverter>();

            Mapper.CreateMap<IndividualProviderModel, Provider>()
                .ForMember(x => x.ProviderTypeId, conf => conf.MapFrom(src => (ProviderTypes)src.Type.Id))
                .ForMember(x => x.Services, conf => conf.MapFrom(src => src.Services)) /* REDUNDANT */
                .ForMember(x => x.Freelances, conf => conf.MapFrom(src => src.Freelance)) // starting today! e.g. var freelance = Mapper.Map<FreelanceBindingModel, Freelance>(model.Freelance, opts => opts.AfterMap((f, r) => r.StartDate = date)); // encapsulate it in the automapper!
                .ForMember(x => x.Employments, conf => conf.MapFrom(src => src.Employment)) // starting today!
                .ForMember(x => x.Promotions, conf => conf.MapFrom(src => src.Promote)) // starting today, lasting t, promoted by!--> actually, you should change this in data model, it's just CreatedBy!!!
                .ForMember(x => x.ContactPersons, conf => conf.MapFrom(src => src));

            

            Mapper.CreateMap<IndividualProviderModel, ICollection<Person>>(); // single item to collection !!! 
            Mapper.CreateMap<IndividualProviderModel, Person>()
                .ForMember(x => x.BirthDate, conf => conf.MapFrom(src => src.Details.BirthDay))
                .ForMember(x => x.Comment, conf => conf.MapFrom(src => src.Details.Comment))
                .ForMember(x => x.PersonName, conf => conf.MapFrom(src => src.PersonName)) // redundant
                .ForMember(x => x.Emails, conf => conf.MapFrom(src => src.Emails)) // redundant
                .ForMember(x => x.Phones, conf => conf.MapFrom(src => src.Telephones)) // why different name? redundant
                .ForMember(x => x.OtherContacts, conf => conf.MapFrom(src => src.OtherContacts)); // redundant

            Mapper.CreateMap<PersonExtendedNameBindingModel, IndividualName>()
                .ForMember(x => x.AddressName, conf => conf.MapFrom(src => src.Address)) // why different name? redundant!
                .ForMember(x => x.AlternateName, conf => conf.MapFrom(src => src.AlternateName)) // redundant!
                .ForMember(x => x.FullName, conf => conf.MapFrom(src => src.FullName)) // redundant!
                .ForMember(x => x.Initials, conf => conf.MapFrom(src => src.Initials)) // redundant!
                .ForMember(x => x.FirstName, conf => conf.MapFrom(src => src.FirstName)) // redundant!
                .ForMember(x => x.MiddleName, conf => conf.MapFrom(src => src.MiddleName)) // redundant!
                .ForMember(x => x.LastName, conf => conf.MapFrom(src => src.LastName)); // redundant!



            Mapper.AssertConfigurationIsValid();
           

        }
    }
}