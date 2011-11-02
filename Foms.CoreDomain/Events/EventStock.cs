//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
//Copyright © 2006,2007 OCTO Technology & OXUS Development Network
//
//This program is free software; you can redistribute it and/or modify
//it under the terms of the GNU Lesser General Public License as published by
//the Free Software Foundation; either version 2 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU Lesser General Public License for more details.
//
//You should have received a copy of the GNU Lesser General Public License along
//with this program; if not, write to the Free Software Foundation, Inc.,
//51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
//
// Licence : http://www.octopusnetwork.org/OverviewLicence.aspx
//
// Website : http://www.octopusnetwork.org
// Business contact: business(at)octopusnetwork.org
// Technical contact email : tech(at)octopusnetwork.org 

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Foms.CoreDomain.Events
{
    /// <summary>
    /// Summary description for EventStock.
    /// </summary>
    [Serializable]
    public class EventStock : IEnumerable
    {
        private readonly List<Event> _list;

        public EventStock()
        {
            _list = new List<Event>();
        }

        /// <summary>
        /// Add e to events list and sort list by id
        /// </summary>
        /// <param name="e"></param>
        public void Add(Event e)
        {
            _list.Add(e);
            SortEventsById();
        }

        public void AddWithoutSorting(Event e)
        {
            _list.Add(e);
        }
        public void Insert(int index, Event e)
        {
            _list.Insert(index, e);
        }

        public int GetNumberOfEvents
        {
            get { return _list.Count; }
        }

        public int GetNumberOfEventsNotDeleted
        {
            get
            {
                int i = 0;
                foreach (Event e in _list)
                    if (!e.Deleted)
                        i++;

                return i;
            }
        }
        
        public void Add(EventStock eventStock)
        {
            _list.AddRange(eventStock._list);
            SortEventsById();
        }

        public IEnumerator GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        public void SortEventsById()
        {
            _list.Sort((x,y) => x.Id.CompareTo(y.Id));
        }

        public void SortEventsByDate()
        {
            _list.Sort((x, y) => x.Date.CompareTo(y.Date));
        }

        public List<Event> GetEventsByType(object type)
        {
            List<Event> events = new List<Event>();

            foreach (Event e in _list)
            {
                if (e.GetType().Equals(type))
                    events.Add(e);
            }
            events.Sort((x,y) => x.Date.CompareTo(y.Date));
            return events;
        }

        public List<Event> GetEvents()
        {
            return _list;
        }

        public List<Event> GetSortedEvents()
        {
            List<Event> l = (from e in _list
                             orderby e.Id, e.InstallmentNumber 
                             select e).ToList();
            return l;
        }
        
        public List<RepaymentEvent> GetRepaymentEvents()
        {
            List<RepaymentEvent> eventList = new List<RepaymentEvent>();

            foreach (Event e in _list)
            {
                if(e is RepaymentEvent && !(e is PendingRepaymentEvent))
                    eventList.Add((RepaymentEvent)e);
            }
            eventList.Sort((x,y) => x.Date.CompareTo(y.Date));
            return eventList;
        }

        public List<WriteOffEvent> GetWriteOffEvents()
        {
            List<WriteOffEvent> eventList = new List<WriteOffEvent>();

            foreach (Event e in _list)
            {
                if (e is WriteOffEvent)
                    eventList.Add((WriteOffEvent)e);
            }
            eventList.Sort((x, y) => x.Date.CompareTo(y.Date));
            return eventList;
        }


        public List<OverdueEvent> GetOverdueEvents()
        {
            List<OverdueEvent> eventList = new List<OverdueEvent>();

            foreach (Event e in _list)
            {
                if (e is OverdueEvent)
                    eventList.Add((OverdueEvent)e);
            }
            eventList.Sort((x, y) => x.Id.CompareTo(y.Id));
            return eventList;
        }

        public List<ProvisionEvent> GetProvisionEvents()
        {
            List<ProvisionEvent> eventList = new List<ProvisionEvent>();

            foreach (Event e in _list)
            {
                if (e is ProvisionEvent)
                    eventList.Add((ProvisionEvent)e);
            }
            eventList.Sort((x, y) => x.Id.CompareTo(y.Id));
            return eventList;
        }

        public List<AccruedInterestEvent> GetAccruedInterestEvents()
        {
            List<AccruedInterestEvent> eventList = new List<AccruedInterestEvent>();

            foreach (Event e in _list)
            {
                if (e is AccruedInterestEvent)
                    eventList.Add((AccruedInterestEvent)e);
            }
            eventList.Sort((x, y) => x.Date.CompareTo(y.Date));
            return eventList;
        }

        public List<RepaymentEvent> GetLoanRepaymentEvents()
        {
            List<RepaymentEvent> eventList = new List<RepaymentEvent>();

            foreach (Event e in _list)
            {
                if (e is RepaymentEvent)
                    eventList.Add((RepaymentEvent)e);
            }

            eventList.Sort((x, y) => x.InstallmentNumber.CompareTo(y.InstallmentNumber));//x.Date.CompareTo(y.Date));
            return eventList;
        }

        public List<LoanDisbursmentEvent> GetDisbursmentEvents()
        {
            List<LoanDisbursmentEvent> eventList = new List<LoanDisbursmentEvent>();

            foreach (Event e in _list)
            {
                if (e is LoanDisbursmentEvent)
                    eventList.Add((LoanDisbursmentEvent)e);
            }
            
            eventList.Sort((x,y) => x.Id.CompareTo(y.Id));

            return eventList;
        }

        public bool IsLastEvent(Event e)
        {
            for (int i = _list.Count - 1; i >= 0; i--)
            {
                Event _e = _list[i];
                if (!_e.Deleted)
                {
                    return _e.Id == e.Id;
                }
            }
            return false;
        }

        public Event GetLastNonDeletedEvent
        {
            get
            {
                SortEventsById();
                for (int i = _list.Count - 1; i >= 0; i--)
                {
                    Event e = _list[i];
                    if (!e.Deleted && !(e is LoanEntryFeeEvent))
                    {
                        return e;
                    }
                }
                return null;
            }
        }

        public Event GetEvent(int rank)
        {
            return rank < _list.Count ? _list[rank] : null;
        }
    }

    [Serializable]
    public class EventType
    {
        public EventType()
        {
            
        }

        public EventType(string eventCode)
        {
            EventCode = eventCode;
        }
        
        public string EventCode { get; set;}
        public string Description { get; set; }
        public int Id { get; set; }

        public override string ToString()
        {
            return EventCode + @" : " + Description;
        }
    }

    [Serializable]
    public class EventAttribute
    {
        public EventAttribute()
        {
        }

        public EventAttribute(string name, string eventCode)
        {
            Name = name;
            EventCode = eventCode;
        }

        public string EventCode { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}