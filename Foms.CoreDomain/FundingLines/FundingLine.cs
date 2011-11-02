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
using System.Collections.Generic;
using Foms.CoreDomain.Accounting;

namespace Foms.CoreDomain.FundingLines
{
    /// <summary>
    /// Summary description for FundingLine.
    /// </summary>
    [Serializable]
    public class FundingLine
    {  
       private bool _deleted;
       private List<FundingLineEvent> _events;

       private string _purpose;
       private string _name;
       private OCurrency   _amount;
       private OCurrency   _realRemainingAmount;
       private OCurrency _anticipatedRemainingAmount;
       private OCurrency _amountCommitted;
       private OCurrency _amountDisbursed;
       
       public int Id { get; set; }

       private ChartOfAccounts fundingLineChart;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public Currency Currency { get; set; }
        public FundingLine()
        {
           _events = new List<FundingLineEvent>();
        }
        public FundingLine(string pName, bool pDeleted)
        {
            _name = pName;
            _deleted = pDeleted;
            _events = new List<FundingLineEvent>();
        }

        public string Purpose
        {
            get { return _purpose; }
            set { _purpose = value; }
        }
     
        public bool Deleted
        {
            get
            {
                return _deleted;
            }
            set
            {
                _deleted = value;
            }
        }

        public override string ToString()
        {
            return _name;
        }

        public OCurrency Amount
        {
           get
           {
              _amount = 0;
              foreach (FundingLineEvent line in _events)
              {
                 if (!line.IsDelete && line.Type == OFundingLineEventTypes.Entry && line.CreationDate.Date <= TimeProvider.Now.Date)
                 {
                    _amount += (line.Movement == OBookingDirections.Credit)
                                   ? line.Amount
                                   : (-1.0) * (line.Amount);
                 }
              }
              return _amount;
           }
           set { _amount = value; }
        }
        public OCurrency RealRemainingAmount
        {
           get
           {
              _realRemainingAmount = 0;
              foreach (FundingLineEvent line in _events)
              {
                 if (!line.IsDelete && line.Type != OFundingLineEventTypes.Commitment && line.CreationDate.Date <= TimeProvider.Now.Date)
                 {
                    _realRemainingAmount += (line.Movement == OBookingDirections.Credit)
                                           ? line.Amount
                                           : (-1.0) * (line.Amount);
                 }
              }
              return _realRemainingAmount;
           }
           set { _realRemainingAmount = value; }
        }
        public OCurrency AmountDisbursed
        {
           get
           {
              _amountDisbursed = 0;
              foreach (FundingLineEvent line in _events)
              {
                 if (!line.IsDelete && line.Type == OFundingLineEventTypes.Disbursment)
                 {
                    _amountDisbursed += line.Amount;
                 }
              }
              return _amountDisbursed;
           }
        }
        public OCurrency AmountCommitted
        {
           get
           {
              _amountCommitted = 0;
              foreach (FundingLineEvent line in _events)
              {
                 if (!line.IsDelete)
                 {
                    if (line.Type == OFundingLineEventTypes.Commitment)
                    {
                       _amountCommitted += line.Amount;
                    }
                    else if (line.Type == OFundingLineEventTypes.Repay)
                       _amountCommitted -= line.Amount;
                 }
              }
              return _amountCommitted;
           }
        }
        public OCurrency AnticipatedRemainingAmount
        {
           get
           {
              _anticipatedRemainingAmount = 0;
              foreach (FundingLineEvent line in _events)
              {
                 if (!line.IsDelete && line.Type != OFundingLineEventTypes.Disbursment && line.CreationDate.Date<= TimeProvider.Now.Date)
                 {
                    _anticipatedRemainingAmount += (line.Movement == OBookingDirections.Credit)
                                                ? line.Amount
                                                : (-1.0) * (line.Amount);
                 }
              }
              return _anticipatedRemainingAmount;
           }
           set { _anticipatedRemainingAmount = value; }
        }

        public List<FundingLineEvent> Events
        {
           get { return _events; }
           set { _events = value; }
        }

        public void AddEvent(FundingLineEvent pFundingLineEvent)
        {
           FundingLineEvent found = _events.Find(e => (e.Id == pFundingLineEvent.Id));
           if(found==null)
           {
              _events.Add(pFundingLineEvent);
           }
           else
           {
              found.IsDelete = false;
           }
            _SortList();
        }

       public void RemoveEvent(FundingLineEvent pFundingLineEvent)
        {
           (_events.Find(e => ((e.Id == pFundingLineEvent.Id) ? true : false))).IsDelete = true;
           _SortList();
        }
        public DateTime EndDate { get; set; }

        public DateTime StartDate { get; set; }

       public ChartOfAccounts FundingLineChartOfAccounts
       {
          get { return fundingLineChart; }
          set { fundingLineChart = value; }
       }
       public double[] CalculateCashProvisionChart(DateTime startDate, int numDays, bool assumeLateLoansRepaidToday)
       {
          double[] data = null;
          if (_events.Count>0)
          {
             data = new double[numDays];
             OCurrency curAmount = 0;

             for (int counter = 0; counter < numDays; counter++)
             {
                curAmount = 0;

                foreach (FundingLineEvent line in _events)
                {
                   if (line.CreationDate.Date <= startDate.AddDays(counter) && !line.Type.Equals(OFundingLineEventTypes.Commitment)
                      && !line.IsDelete)
                   {
                         curAmount += (line.Movement == OBookingDirections.Credit)
                                         ? line.Amount
                                         : (-1.0)*(line.Amount);
                   }
                }
                data[counter] = Convert.ToDouble(curAmount.Value);
             }
          }

          return data;
       }
        private void _SortList()
        {
            _events.Sort((p1, p2) => p2.CreationDate.CompareTo(p1.CreationDate));
        }

    }
}