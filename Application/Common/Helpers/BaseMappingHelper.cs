namespace Application.BaseApplicationHelper
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using Domain.AggregateModels.ValueObjects;
    using Domain.Common.Enums;
    using Domain.Common.ValueObjects;
    using Package.Utilities.Net;

    /// <summary>
    /// Clase base de negocio para todas las entidades y DTO de negocio
    /// </summary>
    public class BaseMappingHelper
    {
        #region Mapper

        private IMapper mapper;
        private readonly MapperConfigurationExpression _configurationmapper = new()
        {
            AllowNullCollections = true
        };

        /// <summary>
        /// Creacion de reglas en automapper
        /// </summary>
        private void CreateMapper()
        {
            MapperConfiguration cnfMapper = new(_configurationmapper);
            mapper = cnfMapper.CreateMapper();
        }


        /// <summary>
        /// Configure custom Expresion for mapper
        /// </summary>
        /// <param name="configure">Object mappers</param>
        public void CreateMapperExpresion(Action<IMapperConfigurationExpression> configure)
        {
            _configurationmapper.CreateMap<ValueObjectString, string>().ConvertUsing(n => n.Valor);
            _configurationmapper.CreateMap<NombreValido, string>().ConvertUsing(n => n.Valor);
            _configurationmapper.CreateMap<DateTimeOffset, DateTime>().ConvertUsing(n => n.UtcDateTime);
            _configurationmapper.CreateMap<DateTime, DateTimeOffset>().ConvertUsing(n => DateTime.SpecifyKind(n, DateTimeKind.Utc));
            configure(_configurationmapper);
            CreateMapper();
        }


        /// <summary>
        /// Configure custom Expresion for mapper
        /// </summary>
        /// <param name="configure"></param>
        public void CreateMapperExpresion<ENT, DTO>(Action<IMapperConfigurationExpression> configure) where ENT : class, new() where DTO : class, new()
        {
            _configurationmapper.CreateMap<CustomList<ENT>, CustomList<DTO>>();
            _configurationmapper.CreateMap<CustomList<DTO>, CustomList<ENT>>();
            CreateMapperExpresion(configure);
        }


        /// <summary>
        /// Configure custom Expresion for mapper DateTimeOffset
        /// </summary>
        /// <param name="configure"></param>
        public void CreateDateTimeOffsetMapperExpresion(int timeZone)
        {
            if (timeZone > 0)
            {
                CreateMapperExpresion(cnf =>
                { cnf.CreateMap<DateTime, DateTimeOffset>().ConvertUsing(n => DateTime.SpecifyKind(n, DateTimeKind.Utc).AddHours((timeZone / 60) * -1)); }
              );
            }
        }


        /// <summary>
        /// Conversion de objetos y mapeo de parametros
        /// </summary>
        /// <typeparam name="S">TSource conversion</typeparam>
        /// <typeparam name="D">TDestination conversion</typeparam>
        /// <param name="objActual"></param>
        /// <returns></returns>
        public D MapObj<S, D>(S objActual)
        {
            if (mapper == null)
            {
                throw new ArgumentNullException(EnumMensajesAplicacion.ERROR_MAPPER.ToString());
            }

            return mapper.Map<S, D>(objActual);
        }

        /// <summary>
        /// Conversion Async de objetos y mapeo de parametros
        /// </summary>
        /// <typeparam name="S">TSource conversion</typeparam>
        /// <typeparam name="D">TDestination conversion</typeparam>
        /// <param name="objActual"></param>
        /// <returns></returns>
        public async Task<D> MapObjAsyn<S, D>(S objActual)
        {
            return await Task.FromResult(MapObj<S, D>(objActual));
        }

        #endregion
    }
}
