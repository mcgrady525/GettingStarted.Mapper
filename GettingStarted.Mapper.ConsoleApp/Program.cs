using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GettingStarted.Mapper.Common;
using EmitMapper;
using EmitMapper.MappingConfiguration;
using AutoMapper;
using Mapster;
using SSharing.Frameworks.Common.Helpers;

namespace GettingStarted.Mapper.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //NormalMapping();
            //DifferentPropertyName();
            //IgnoreProperty();
            //DoNullSubstitution();
            TestPerformance();


            Console.ReadKey();
        }

        /// <summary>
        /// 比较性能
        /// </summary>
        private static void TestPerformance()
        {
            var from = new From
            {
                P1 = "p1",
                P2 = "p2",
                P3 = "p3",
                P4 = "p4",
                P5 = "p5"
            };

            var iteration = 1000 * 10000;
            CodeTimerHelper.Initialize();

            var emitMapper = ObjectMapperManager.DefaultInstance.GetMapper<From, To>(
                    new DefaultMapConfig()
                    .NullSubstitution<DateTime?, DateTime>((value) => DateTime.Now));
            CodeTimerHelper.Time(string.Format("EmitMapper的性能，循环:{0}次", iteration), iteration, () =>
            {
                var to = emitMapper.Map(from);
            });

            AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<From, To>()
                    .ForMember(dest => dest.P6, opt => opt.NullSubstitute(DateTime.Now)));
            CodeTimerHelper.Time(string.Format("AutoMapper的性能，循环:{0}次", iteration), iteration, () =>
            {
                var to = AutoMapper.Mapper.Map<To>(from);
            });

            TypeAdapterConfig<From, To>
                .NewConfig()
                .IgnoreNullValues(true);
            CodeTimerHelper.Time(string.Format("Mapster的性能，循环:{0}次", iteration), iteration, () =>
            {
                var to = from.Adapt<To>();
            });

        }

        /// <summary>
        /// 可空类型赋默认值
        /// </summary>
        private static void DoNullSubstitution()
        {
            var from = new From
            {
                P1 = "p1",
                P2 = "p2",
                P3 = "p3",
                P4 = "p4",
                P5 = "p5"
            };

            //EmitMapper
            //var mapper = ObjectMapperManager.DefaultInstance.GetMapper<From, To>(
            //    new DefaultMapConfig()
            //    .NullSubstitution<DateTime?, DateTime>((value) => DateTime.Now)
            //    );
            //var to = mapper.Map(from);

            //AutoMapper
            //AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<From, To>()
            //    .ForMember(dest => dest.P6, opt => opt.NullSubstitute(DateTime.Now)));
            //var to = AutoMapper.Mapper.Map<To>(from);

            //Mapster
            TypeAdapterConfig<From, To>
                .NewConfig()
                .IgnoreNullValues(true);
            var to = from.Adapt<To>();
        }

        /// <summary>
        /// 忽略属性
        /// </summary>
        private static void IgnoreProperty()
        {
            var from = new From
            {
                P1 = "p1",
                P2 = "p2",
                P3 = "p3",
                P4 = "p4",
                P5 = "p5"
            };

            //EmitMapper
            //var mapper = ObjectMapperManager.DefaultInstance.GetMapper<From, To>(
            //    new DefaultMapConfig()
            //    .IgnoreMembers<From, To>(new string[] { "P1"})
            //    );
            //var to = mapper.Map(from);

            //AutoMapper
            //AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<From, To>()
            //    .ForMember(dest => dest.P1, opt => opt.Ignore()));
            //var to = AutoMapper.Mapper.Map<To>(from);

            //Mapster
            //TypeAdapterConfig<From, To>
            //    .NewConfig()
            //    .Ignore(dest => dest.P1);
            //var to = from.Adapt<To>();

        }

        /// <summary>
        /// 源和目标的属性名称不一致的映射
        /// </summary>
        private static void DifferentPropertyName()
        {
            var from = new From
            {
                P1 = "p1",
                P2 = "p2",
                P3 = "p3",
                P4 = "p4",
                P5 = "p5"
            };

            //EmitMapper
            //var mapper = ObjectMapperManager.DefaultInstance.GetMapper<From, To>(
            //    new DefaultMapConfig()
            //    .MatchMembers((x, y) =>
            //    {
            //        if (x == "P5" && y == "P5String")
            //        {
            //            return true;
            //        }
            //        return x == y;
            //    }));
            //var to = mapper.Map(from);

            //AutoMapper
            //AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<From, To>()
            //    .ForMember(t => t.P5String, opt => { opt.MapFrom(s => s.P5); }));
            //var to = AutoMapper.Mapper.Map<To>(from);

            //Mapster
            //TypeAdapterConfig<From, To>
            //    .NewConfig()
            //    .Map(dest => dest.P5String, src => src.P5);
            //var to = from.Adapt<To>();


        }

        /// <summary>
        /// 正常映射，当源和目标类型的所有属性名称全部相同时
        /// </summary>
        private static void NormalMapping()
        {
            var from = new From
            {
                P1 = "p1",
                P2 = "p2",
                P3 = "p3",
                P4 = "p4",
                P5 = "p5"
            };

            //EmitMapper
            //var mapper = ObjectMapperManager.DefaultInstance.GetMapper<From, To>();
            //var to = mapper.Map(from);

            //AutoMapper
            //AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<From, To>());
            //var to = AutoMapper.Mapper.Map<To>(from);

            //Mapster
            //var to = from.Adapt<To>();
        }
    }

    public class From
    {
        public string P1 { get; set; }
        public string P2 { get; set; }
        public string P3 { get; set; }
        public string P4 { get; set; }
        public string P5 { get; set; }

        public DateTime? P6 { get; set; }
        //and so on
    }

    public class To
    {
        public string P1 { get; set; }
        public string P2 { get; set; }
        public string P3 { get; set; }
        public string P4 { get; set; }
        public string P5 { get; set; }
        public DateTime P6 { get; set; }
        //and so on
    }

}
