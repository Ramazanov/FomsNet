using System.Collections.Generic;
using Foms.CoreDomain.Export.Fields;

namespace Foms.CoreDomain.Export.Files
{
    public interface IFile
    {
        List<IField> DefaultList { get; }
        List<IField> SelectedFields { get; set; }
        string Name { get; set; }
        bool HasFieldsDelimiter { get; set; }
        char FieldsDelimiter { get; set; }
        bool HasFieldsSpecificLength { get; set; }
        bool HasStringEncloseChar { get; set; }
        char EncloseChar { get; set; }
        string Extension { get; set; }
        bool DisplayHeader { get; set; }
        bool IsExportFile { get; }
    }
}
