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




        public static void Map()
        {
            // ATTN this and next are actually an attempt to create a many-to-many relation. 
            // I am not sure if this will work
            Mapper.CreateMap<int, ProviderGroup>()
                .ConstructUsing(x => new ProviderGroup { Id = x });

            Mapper.CreateMap<int, ProviderSoft>()
                .ConstructUsing(x => new ProviderSoft { Id = x });

            Mapper.CreateMap<EmploymentBindingModel, Employment>();

            Mapper.CreateMap<EmploymentBindingModel, IEnumerable<Employment>>()
                .ConvertUsing<SingleToCollectionConverter<EmploymentBindingModel, Employment>>();

            Mapper.CreateMap<FreelanceBindingModel, Freelance>();

            Mapper.CreateMap<FreelanceBindingModel, IEnumerable<Freelance>>()
                .ConvertUsing<SingleToCollectionConverter<FreelanceBindingModel, Freelance>>();

            Mapper.CreateMap<IndividualProviderModel, Person>();

            Mapper.CreateMap<IndividualProviderModel, IEnumerable<Person>>()
                .ConvertUsing<SingleToCollectionConverter<IndividualProviderModel, Person>>();

            Mapper.CreateMap<IndividualProviderModel, Provider>()
                .ForMember(x => x.ProviderTypeId, conf => conf.MapFrom(src => (ProviderTypes)src.Type.Id))
                .ForMember(x => x.Name, conf => conf.MapFrom(src => src.PersonName.FullName))
                .ForMember(x => x.Employments, conf => conf.MapFrom(src => src.Employment)) 
                .ForMember(x => x.Freelances, conf => conf.MapFrom(src => src.Freelance)) 
                .ForMember(x => x.ContactPersons, conf => conf.MapFrom(src => src)); // one to many 

            Mapper.CreateMap<PersonExtendedNameBindingModel, IndividualName>();

            Mapper.AssertConfigurationIsValid();
           

        }
    }
}