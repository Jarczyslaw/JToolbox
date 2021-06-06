using Nelibur.ObjectMapper;
using System.Collections.Generic;
using System.Linq;

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

        public List<TOut> MapMany<TIn, TOut>(IEnumerable<TIn> @in)
        {
            TryCreateBinding<TIn, TOut>();
            return @in.Select(s => Map<TIn, TOut>(s)).ToList();
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