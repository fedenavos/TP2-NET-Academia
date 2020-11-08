using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Entities {
    public class BusinessEntity {
        public enum States {
            Deleted,
            New,
            Modified,
            Unmodified
        }
        //Declaracion de variables
        private int id;
        private States state;

        //Properties
        public int Id {
            get { return id; }
            set { id = value; }
        }
        public States State {
            get { return state; }
            set { state = value; }
        }
    }
}
