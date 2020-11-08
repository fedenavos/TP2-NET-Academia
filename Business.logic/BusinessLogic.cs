using Business.Entities;
using Data.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.logic {
    public class BusinessLogic {
        Adapter adapterData;
        public BusinessLogic() {
            adapterData = new Adapter();
        }

        Adapter AdapterData {
            get { return adapterData; }
            set { adapterData = value; }
        }
    }
}
