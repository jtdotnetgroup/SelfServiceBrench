using System.Data;

namespace PrintLib
{
    public interface IPrintEntity
    {
        DataTable ToDataTable();
    }
}