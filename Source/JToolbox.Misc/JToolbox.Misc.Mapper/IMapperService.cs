using System.Collections.Generic;

namespace JToolbox.Misc.Mapper
{
    public interface IMapperService
    {
        void Bind<TIn, TOut>();

        TOut Map<TIn, TOut>(TIn @in);

        TOut Map<TIn, TOut>(TIn @in, TOut @out);

        List<TOut> MapMany<TIn, TOut>(List<TIn> @in);
    }
}