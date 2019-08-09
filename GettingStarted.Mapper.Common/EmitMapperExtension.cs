using EmitMapper;
using EmitMapper.MappingConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingStarted.Mapper.Common
{
    public static class EmitMapperExtension
    {
        /// <summary>
        /// DTO mapping
        /// </summary>
        /// <typeparam name="TFrom">源实体</typeparam>
        /// <typeparam name="TTo">目标实体</typeparam>
        /// <param name="tFrom">源实体输入</param>
        /// <returns></returns>
        public static TTo ToDtoByEmitMapper<TFrom, TTo>(this TFrom tFrom)
        {
            var mapper = ObjectMapperManager.DefaultInstance.GetMapper<TFrom, TTo>();
            return mapper.Map(tFrom);
        }
    }
}
