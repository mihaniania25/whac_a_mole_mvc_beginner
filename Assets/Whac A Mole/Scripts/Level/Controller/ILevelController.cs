using MeShineFactory.WhacAMole.Level.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeShineFactory.WhacAMole.Level.Controller
{
    public interface ILevelController
    {
        void Setup(ILevelView view, ILevelModel levelModel);
        void Dispose();
    }
}
