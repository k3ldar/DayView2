//DayView2 
//original code based on https://calendar.codeplex.com/
//
//modified by Simon Carter (s1cart3r@gmail.com)
//
//Redistribution and use in source and binary forms are permitted
//provided that the above copyright notice and this paragraph are
//duplicated in all such forms and that any documentation,
//advertising materials, and other materials related to such
//distribution and use acknowledge that the software was developed
//by techcoil.com and Simon Carter.  The name of the
//techcoil.com and Simon Carter may not be used to endorse or promote products derived
//from this software without specific prior written permission.
//THIS SOFTWARE IS PROVIDED ``AS IS'' AND WITHOUT ANY EXPRESS OR
//IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
//WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.
//
//change history
// 09/06/2013 - initial release 
//

using System;
using System.Collections.Generic;
using System.Text;

namespace Calendar
{
    /// <summary>
    /// Appointment Event Arguments
    /// </summary>
    public class AppointmentEventArgs : EventArgs
    {
        #region Private Members

        private Appointment _appointment;

        #endregion Private Members

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="appointment">Appointment</param>
        public AppointmentEventArgs( Appointment appointment )
        {
            _appointment = appointment;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// The appointment!
        /// </summary>
        public Appointment Appointment
        {
            get 
            { 
                return (_appointment); 
            }
        }

        #endregion Properties
    }
}
