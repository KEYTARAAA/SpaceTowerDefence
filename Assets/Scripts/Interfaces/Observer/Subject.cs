using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static statics;

namespace Assets.Scripts.Interfaces.Observer
{
    public interface Subject
    {
        public void AddObserver(OBSERVERTYPES ot, Observer observer);
        public void RemoveObserver(OBSERVERTYPES ot, Observer observer);
        public void ClearObservers(OBSERVERTYPES ot);
        public void UpdateObservers(OBSERVERTYPES ot);
    }
}
