using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AUC_Tasma;
namespace FormTest
{
    public partial class Form1 : Form
    {
        // Khai báo hai lớp kết nối SQL và bộ điều khiển phương thức của AUC-Tasma
        SqlConnect SQLC = new SqlConnect();
        TasmaControl tasma;
        // Khai báo tên bảng dữ liệu và tên cột cần dựa vào để tìm kiếm
        public string tableName = "DATA_EXAMPLE";
        public string nameAC = "NAME";
        public Form1()
        {
            InitializeComponent();
        }
        // hàm buộc dữ liệu SQL với Form
        public void activeTasma()
        {
            // Điền thông tin của SQL bao gồm tên Server và tên Cơ Sở Dữ Liệu 
            SQLC.InfoSQL(@"DESKTOP-MC\SQLEXPRESS", "AUCDATA");
            tasma = new TasmaControl(SQLC.connect());
            dataGV.DataSource = tasma.selectData(tableName);
        }
        // hàm xử lí tìm kiếm thông tin
        public void handleSearchInfo()
        {
            dataGV.DataSource = tasma.searchData(
                tableName, 
                nameAC, 
                txt_timkiem.Text);
        }
        // hàm thực hiện gợi ý tìm kiếm
        public void autoCompletely(string nameAC)
        {
            // Khai báo lớp AutoCompletely của textbox
            AutoCompleteStringCollection acsc = new AutoCompleteStringCollection();
            // Truyền dữ liệu của SQL vào bảng gợi ý tìm kiếm
            string[] valAC = tasma.AutoComplete(tableName, nameAC);
            foreach (var val in valAC)
            {
                acsc.Add(val);
            }
            // Thiết lập dịnh đạng cho hệ thống gợi ý tìm kiếm
            txt_timkiem.AutoCompleteMode = AutoCompleteMode.Suggest;
            txt_timkiem.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txt_timkiem.AutoCompleteCustomSource = acsc;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // kích hoạt ràng buộc SQL 
            activeTasma();
            // kích hoạt ràng buộc Gợi ý Tìm Kiếm
            autoCompletely(nameAC);
        }

        private void txt_timkiem_TextChanged(object sender, EventArgs e)
        {
            // Kích hoạt ràng buộc xử lí tìm kiếm thông tin
            handleSearchInfo();
        }

        private void dataSearch_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
