using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServicesLib;

namespace PrintLib
{
    public class PrintLableEntity: IPrintEntity
    {
        public string ItemNum { get; set; }
        public string ItemName { get; set; }
        public string LotNum { get; set; }
        public DateTime ProDate
        {
            get { return DateTime.Now.Date; }
        }
        public decimal Qty { get; set; }

        public string QRCode { get; set; }

        public string LabelNum { get; set; }

        public decimal PackQty { get; set; }

        public decimal PrintQty { get; set; }

        public DataTable ToDataTable()
        {
            //计算要打印的标签张数
            var count = Math.Ceiling(this.Qty / this.PackQty);
            //最后一张标签的数量
            var lastQty = this.Qty % this.PackQty;

            var table = Common.ObjectToDataTable<PrintLableEntity>("Table1");

            for (int i = 0; i < count; i++)
            {
                var row = table.NewRow();

                row["ItemNum"] = this.ItemNum;
                row["ItemName"] = this.ItemName;
                row["LotNum"] = this.LotNum;
                row["ProDate"] = this.ProDate;

                row["LabelNum"] = $"{count}-{i+1}";
                row["QRCode"] = this.ItemNum+";"+this.LotNum;

                row["PrintQty"] = i == count - 1 ? lastQty : this.PackQty;

                table.Rows.Add(row);
            }

            return table;

        }

    }


}
