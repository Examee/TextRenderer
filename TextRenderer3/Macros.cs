using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRenderer3 {
    public class SerialPeaker {
        int m_serial = 0;
        public int NextSerial() {
            return m_serial++;
        }
        public int CurrentSerial() {
            return m_serial;
        }
    }
}
