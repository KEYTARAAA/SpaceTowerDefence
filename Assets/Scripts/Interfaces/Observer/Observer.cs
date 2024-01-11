using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Interfaces.Observer
{
    public interface Observer
    {
        public void UpdateObserver<T>(T info);
    }
}
