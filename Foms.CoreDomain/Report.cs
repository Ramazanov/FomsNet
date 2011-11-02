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
using System.Diagnostics;
using System.IO;
using System.Xml;
using CrystalDecisions.CrystalReports.Engine;
using System.Collections.Generic;
using Foms.Shared.Settings;
using ICSharpCode.SharpZipLib.Zip;

namespace Foms.CoreDomain
{
    [Serializable]
    public class ReportParam
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public string Label { get; set; }
    }

    [Serializable]
    public class ReportComparer : IComparer<Report>
    {
        public int Compare(Report left, Report right)
        {
            return left.Order - right.Order;
        }
    }

    [Serializable]
    public enum AttachmentPoint
    {
        None = 0,
        LoanDetails = 1,
        CreditCommittee = 2,
        LoanRepayment = 3,
        SavingsBook = 4,
        Guarantors = 5,
        SavingsDeposit = 6,
        SavingsCompulsory = 7,
        GuaranteeCommittee = 8,
        GuaranteeDetails = 9
    }

    [Serializable]
    public enum Visibility
    {
        Individual = 1,
        Group = 2,
        Corporate = 3,
        All = 4
    }

    [Serializable]
    public class Query
    {
        public Query()
        {
            HideIfRows = -1;
        }
        public string query;
        public int selected;
        public object key;
        public object value;
        public int HideIfRows;
    }

    public enum ReportFilter
    {
        All,
        Starred
    };

    [Flags]
    public enum Flag
    {
        None = 0,
        Standard = 1,
        Internal = 2,
        Starred = 4,
        Consolidated = 8
    }

    [Serializable]
    public class Report
    {
        private readonly string _fileName;
        private string _title;
        private string _description;
        private bool _isLoaded;
        private Guid _guid;
        private ArrayList _params;
        private int _order;
        private AttachmentPoint _attachmentPoint = AttachmentPoint.None;
        private Visibility _visibility = Visibility.All;
        private readonly Hashtable _datasources = new Hashtable();
        private bool _manualDatasources;
        private readonly ArrayList _helpers = new ArrayList();
        private readonly string _lang = UserSettings.Language;
        [NonSerialized]
        private XmlDocument _docLabels;

        private Flag _flag = Flag.None;

        private ReportDocument _document;

        public Report(string fileName)
        {
            _fileName = fileName;

            try
            {
                _InitializeReport(fileName);
            }
            catch (Exception e)
            {
               Trace.Indent();
               Trace.WriteLine("EXCEPTION: ");
               Trace.WriteLine(e.Message + "\n");
               Debug.WriteLine("Exception while loading report package");
            }
        }
        public Report(string fileName, string paramName, int value)
        {
            _fileName = fileName;

            try
            {
                _InitializeReport(fileName);
                if (_isLoaded)
                {
                    ReportParam param = new ReportParam
                                            {
                                                Name = paramName,
                                            };
                    param.Value = value;
                    ArrayList reportParams = new ArrayList();
                    reportParams.Add(param);
                    _params = reportParams;
                }

            }
            catch (Exception e)
            {
                Trace.Indent();
                Trace.WriteLine("EXCEPTION: ");
                Trace.WriteLine(e.Message + "\n");
                Debug.WriteLine("Exception while loading report package");
            }
        }

        private void _InitializeReport(string fileName)
        {
            Trace.IndentLevel = 1;
            Trace.Indent();
            Trace.WriteLine("Loading " + fileName);
            Trace.Unindent();
            XmlDocument doc = new XmlDocument();
            doc.Load(_GetMetaStream());
            XmlElement root = doc.DocumentElement;
            Debug.Assert(root != null, "Root element not found");

            // Get GUID
            XmlNode nodeGuid = root.SelectSingleNode("guid");
            _guid = null == nodeGuid ? Guid.NewGuid() : new Guid(nodeGuid.InnerText);

            // Get title
            XmlNode nodeTitle = root.SelectSingleNode(string.Format("localized[@lang='{0}']/title", _lang));
            nodeTitle = nodeTitle ?? root.SelectSingleNode("title");
            _title = nodeTitle.InnerText;

            // Get description
            XmlNode nodeDesc = root.SelectSingleNode(string.Format("localized[@lang='{0}']/description", _lang));
            nodeDesc = nodeDesc ?? root.SelectSingleNode("description");
            _description = nodeDesc.InnerText;
            
            XmlNode rep = doc.GetElementsByTagName("report")[0];
            XmlAttribute order = rep.Attributes["order"];
            _order = order != null ? int.Parse(order.Value) : 0;
            XmlAttribute ap = rep.Attributes["attachmentPoint"];
            if (ap != null && Enum.IsDefined(typeof (AttachmentPoint), ap.Value))
            {
                _attachmentPoint = (AttachmentPoint) Enum.Parse(typeof (AttachmentPoint), ap.Value, true);
            }
            XmlAttribute vis = rep.Attributes["visibility"];
            if (vis != null && Enum.IsDefined(typeof (Visibility), vis.Value))
            {
                _visibility = (Visibility) Enum.Parse(typeof (Visibility), vis.Value, true);
            }
            // Get list of utility functions
            foreach (XmlNode node in doc.GetElementsByTagName("helper"))
            {
                XmlAttribute fn = node.Attributes["name"];
                if (null == fn) continue;
                _helpers.Add(fn.Value);
            }
            _isLoaded = true;
        }

        private Stream _GetMetaStream()
        {
            ZipFile package = new ZipFile(_fileName);
            const string metaDefault = "meta.xml";
            string meta = string.Format(@"meta.{0}.xml", UserSettings.Language);
            ZipEntry entry = package.GetEntry(meta);
            entry = entry ?? package.GetEntry(metaDefault);
            return package.GetInputStream(entry);
        }

        public bool LoadLabels()
        {
            try
            {
                ZipFile package = new ZipFile(_fileName);
                string path = string.Format("labels.{0}.xml", _lang);
                ZipEntry entry = package.GetEntry(path);
                if (null == entry) return false;
                _docLabels = new XmlDocument();
                _docLabels.Load(package.GetInputStream(entry));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string GetLabel(string subreport, string name)
        {
            Debug.Assert(_docLabels != null, "Labels not loaded");
            XmlElement root = _docLabels.DocumentElement;
            Debug.Assert(root != null, "Root element not found");
            string path = string.Format("{0}/{1}", subreport, name);
            XmlNode node = root.SelectSingleNode(path);
            return null == node ? string.Empty : node.InnerText;
        }

        public Stream GetReportStream()
        {
            ZipFile package = new ZipFile(_fileName);
            const string entryName = "report.rpt";
            ZipEntry entry = package.GetEntry(entryName);
            Stream stream = package.GetInputStream(entry);
            stream.Flush();
            return stream;
        }

        public Stream GetDatasourceStream(string name)
        {
            ZipFile package = new ZipFile(_fileName);
            string entryName = name + ".sql";
            ZipEntry entry = package.GetEntry(entryName);
            return package.GetInputStream(entry);
        }

        public ArrayList Params
        {
            get
            {
                return _params;
            }
            set
            {
                _params = value;
            }
        }

        public void LoadParams()
        {
            if (null == _params)
            {
                _params = new ArrayList();
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(_GetMetaStream());
                    XmlElement root = doc.DocumentElement;
                    XmlNodeList paramList = root.SelectNodes(string.Format("localized[@lang='{0}']/param", _lang));
                    paramList = 0 == paramList.Count ? root.SelectNodes("param") : paramList;
                    paramList = 0 == paramList.Count ? root.SelectNodes("params/param") : paramList;
                    foreach (XmlNode node in paramList)
                    {
                        ReportParam param = new ReportParam
                        {
                            Name = node.Attributes["name"].Value,
                            Label = node.Attributes["label"].Value
                        };
                        XmlAttribute d = node.Attributes["default"];
                        switch (node.Attributes["type"].Value)
                        {
                            case "date":
                                if (null == d)
                                {
                                    param.Value = DateTime.Today;
                                    break;
                                }
                                if ("bom" == d.Value)
                                {
                                    param.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                                }
                                else if ("eom" == d.Value)
                                {
                                    param.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
                                }
                                else
                                {
                                    param.Value = DateTime.Today;
                                }
                                break;

                            case "int":
                                param.Value = null == d ? 0 : int.Parse(d.Value);
                                break;

                            case "string":
                                param.Value = null == d ? string.Empty : d.Value;
                                break;

                            case "bool":
                                param.Value = null == d ? false : ("true" == d.Value ? true : false);
                                break;

                            case "query":
                                Query q = new Query();
                                q.query = node.InnerText;
                                XmlAttribute attr = node.Attributes["hideIfRows"];
                                q.HideIfRows = null == attr ? -1 : int.Parse(attr.Value);
                                param.Value = q;
                                break;
                        }
                        _params.Add(param);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }

        public object GetParamValueByName(string name)
        {
            name = name.TrimStart(new[] { '@' });
            foreach (ReportParam param in _params)
            {
                if (name == param.Name)
                {
                    if (param.Value is Query)
                    {
                        return (param.Value as Query).key;
                    }
                    return param.Value;
                }
                if (name.EndsWith("_name") && name.Substring(0, name.Length - 5) == param.Name && param.Value is Query)
                {
                    return (param.Value as Query).value;
                }
            }
            return null;
        }

        public string Title
        {
            get { return _title; }
        }

        public string Description
        {
            get { return _description; }
        }

        public bool IsLoaded
        {
            get { return _isLoaded; }
        }

        public Guid Guid
        {
            get { return _guid; }
        }

        public int Order
        {
            get { return _order; }
        }

        public void AddParam(string name, object value)
        {
            if (null == _params) _params = new ArrayList();
            ReportParam param = new ReportParam {Name = name, Value = value};
            _params.Add(param);
        }

        public void RemoveParams()
        {
            _params = null;
        }

        public AttachmentPoint AttachmentPoint
        {
            get { return _attachmentPoint; }
        }

        public Visibility Visibility
        {
            get { return _visibility; }
        }

        public void RemoveDatasources()
        {
            _datasources.Clear();
            _manualDatasources = false;
        }

        public void AddDatasource(string name, System.Data.DataSet datasource)
        {
            _datasources.Add(name, datasource);
            _manualDatasources = true;
        }

        public Hashtable GetDatasources()
        {
            return _datasources;
        }

        public bool ManualDatasources
        {
            get { return _manualDatasources; }
        }

        public IEnumerable<string> Helpers
        {
            get
            {
                foreach (string name in _helpers)
                {
                    yield return name;
                }
            }
        }

        private void LoadDocument()
        {
            _document = new ReportDocument();
            lock (_document)
            {
                // Save the .rpt from the package into a temporary file
                string temp = Path.GetTempFileName() + ".rpt";
                Stream input = GetReportStream();
                FileStream output = new FileStream(temp, FileMode.Create);
                const int size = 4096;
                byte[] bytes = new byte[4096];
                int numBytes;
                while ((numBytes = input.Read(bytes, 0, size)) > 0)
                {
                    output.Write(bytes, 0, numBytes);
                }
                output.Close();

                _document.Load(temp);
                File.Delete(temp);
            }
        }

        public ReportDocument GetDocument()
        {
            if (null == _document)
            {
                LoadDocument();
            }
            return _document;
        }

        public string Name
        {
            get
            {
                return Path.GetFileName(_fileName);
            }
        }
        
        public void SetFlag(Flag flag)
        {
            _flag = _flag | flag;
        }

        public void UnsetFlag(Flag flag)
        {
            _flag = _flag ^ flag;
        }

        public bool HasFlag(Flag flag)
        {
            return (_flag & flag) == flag;
        }

        public Flag Flag
        {
            get
            {
                return _flag;
            }
        }

        public bool UseCents { get; set; }

        public IEnumerable<ReportDocument> GetPartIterator()
        {
            ReportDocument doc = GetDocument();
            yield return doc;

            foreach (ReportDocument part in doc.Subreports)
            {
                yield return part;
            }
        }

        public IEnumerable<ReportObject> GetObjectIterator()
        {
            foreach (ReportDocument part in GetPartIterator())
            {
                foreach (ReportObject obj in part.ReportDefinition.ReportObjects)
                {
                    yield return obj;
                }
            }
        }
    }
}