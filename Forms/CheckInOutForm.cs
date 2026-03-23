using System.Data;
using Microsoft.Data.SqlClient;
using hotel_management.Data;

namespace hotel_management;

public partial class CheckInOutForm : Form
{
    public CheckInOutForm()
    {
        InitializeComponent();
    }

    private void checkin_out_Load(object sender, EventArgs e)
    {
        LoadStays();
    }

    private void LoadStays(string keyword = "")
    {
        const string baseSql = """
                               SELECT StayId, SS, CheckInDate, CheckOutDate, Comment, PaymentStatus, ReceiptID, RoomId
                               FROM [Stay List]
                               """;

        if (string.IsNullOrWhiteSpace(keyword))
        {
            dataGridView1.DataSource = HotelDb.Query(baseSql);
            return;
        }

        const string searchSql = """
                                 SELECT StayId, SS, CheckInDate, CheckOutDate, Comment, PaymentStatus, ReceiptID, RoomId
                                 FROM [Stay List]
                                 WHERE StayId LIKE @search
                                    OR SS LIKE @search
                                    OR RoomId LIKE @search
                                    OR ReceiptID LIKE @search
                                 """;

        dataGridView1.DataSource = HotelDb.Query(searchSql, new SqlParameter("@search", $"%{keyword.Trim()}%"));
    }

    private void txtsearchbar_TextChanged(object sender, EventArgs e)
    {
        LoadStays(txtsearchbar.Text);
    }

    private void btnadd_Click(object sender, EventArgs e)
    {
        const string sql = """
                           INSERT INTO [Stay List] (StayId, SS, CheckInDate, CheckOutDate, Comment, PaymentStatus, ReceiptID, RoomId)
                           VALUES (@StayId, @SS, @CheckInDate, @CheckOutDate, @Comment, @PaymentStatus, @ReceiptID, @RoomId)
                           """;

        HotelDb.Execute(sql, BuildStayParameters());
        LoadStays(txtsearchbar.Text);
    }

    private void btnupd_Click(object sender, EventArgs e)
    {
        const string sql = """
                           UPDATE [Stay List]
                           SET SS = @SS,
                               CheckInDate = @CheckInDate,
                               CheckOutDate = @CheckOutDate,
                               Comment = @Comment,
                               PaymentStatus = @PaymentStatus,
                               ReceiptID = @ReceiptID,
                               RoomId = @RoomId
                           WHERE StayId = @StayId
                           """;

        HotelDb.Execute(sql, BuildStayParameters());
        LoadStays(txtsearchbar.Text);
    }

    private void btndel_Click(object sender, EventArgs e)
    {
        const string sql = "DELETE FROM [Stay List] WHERE StayId = @StayId";
        HotelDb.Execute(sql, new SqlParameter("@StayId", txtStayid.Text.Trim()));
        LoadStays(txtsearchbar.Text);
    }

    private SqlParameter[] BuildStayParameters()
    {
        return
        [
            new SqlParameter("@StayId", txtStayid.Text.Trim()),
            new SqlParameter("@SS", txtSS.Text.Trim()),
            new SqlParameter("@CheckInDate", dtpcheckin.Value.Date),
            new SqlParameter("@CheckOutDate", dtpCheckout.Value.Date),
            new SqlParameter("@Comment", txtcomment.Text.Trim()),
            new SqlParameter("@PaymentStatus", txtPaymentStatus.Text.Trim()),
            new SqlParameter("@ReceiptID", txtReceiptID.Text.Trim()),
            new SqlParameter("@RoomId", txtroomID.Text.Trim())
        ];
    }

    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || e.RowIndex >= dataGridView1.Rows.Count)
        {
            return;
        }

        DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
        txtStayid.Text = row.Cells[0].Value?.ToString() ?? string.Empty;
        txtSS.Text = row.Cells[1].Value?.ToString() ?? string.Empty;

        if (DateTime.TryParse(row.Cells[2].Value?.ToString(), out DateTime checkIn))
        {
            dtpcheckin.Value = checkIn;
        }

        if (DateTime.TryParse(row.Cells[3].Value?.ToString(), out DateTime checkOut))
        {
            dtpCheckout.Value = checkOut;
        }

        txtcomment.Text = row.Cells[4].Value?.ToString() ?? string.Empty;
        txtPaymentStatus.Text = row.Cells[5].Value?.ToString() ?? string.Empty;
        txtReceiptID.Text = row.Cells[6].Value?.ToString() ?? string.Empty;
        txtroomID.Text = row.Cells[7].Value?.ToString() ?? string.Empty;
    }
}
