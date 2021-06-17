using Nelibur.ObjectMapper;
using System.Collections.Generic;

namespace JToolbox.Misc.Mapper
{
    public class MapperService : IMapperService
    {
        public void Bind<TIn, TOut>()
        {
            TinyMapper.Bind<TIn, TOut>();
        }

        public TOut Map<TIn, TOut>(TIn @in)
        {
            TryCreateBinding<TIn, TOut>();
            return TinyMapper.Map<TOut>(@in);
        }

        public List<TOut> MapMany<TIn, TOut>(List<TIn> @in)
        {
            TryCreateBinding<List<TIn>, List<TOut>>();
            return TinyMapper.Map<List<TOut>>(@in);
        }

        public TOut Map<TIn, TOut>(TIn @in, TOut @out)
        {
            TryCreateBinding<TIn, TOut>();
            return TinyMapper.Map(@in, @out);
        }

        private void TryCreateBinding<TIn, TOut>()
        {
            if (!TinyMapper.BindingExists<TIn, TOut>())
            {
                TinyMapper.Bind<TIn, TOut>();
            }
        }
    }
}