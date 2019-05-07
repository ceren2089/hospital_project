using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Proje_Hastane
{
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }
        public string TCnumara;
    
        sqlbaglantisi bgl = new sqlbaglantisi();

        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {
            LblTc.Text = TCnumara;
        

            //Ad Soyad

            SqlCommand cmd1 = new SqlCommand("select SekreterAdSoyad from Tbl_Sekreter where SekreterTC=@p1", bgl.connection());
            cmd1.Parameters.AddWithValue("@p1", LblTc.Text);
            SqlDataReader dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                LblAdSoyad.Text = dr1[0].ToString();
            }
            bgl.connection().Close();

            //Branşları Datagride Aktarma

            DataTable dt1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Branslar", bgl.connection());
            da.Fill(dt1);
            dataGridView1.DataSource = dt1;

            //Doktorları Listeye Aktarma

            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("select (DoktorAd+' '+DoktorSoyad)as 'Doktorlar',DoktorBrans from Tbl_Doktorlar", bgl.connection());
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;

            //Branşı comboboxa aktarma

            SqlCommand cmd2 = new SqlCommand("select BransAd from Tbl_Branslar", bgl.connection());
            SqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                CmbBrans.Items.Add(dr2[0]);
            }
            bgl.connection().Close();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand cmdkaydet = new SqlCommand("insert into Tbl_Randevular(RandevuTarih,RandevuSaat,RandevuBrans,RandevuDoktor)values(@r1,@r2,@r3,@r4)", bgl.connection());
            cmdkaydet.Parameters.AddWithValue("@r1", MskTarih.Text);
            cmdkaydet.Parameters.AddWithValue("@r2", MskSaat.Text);
            cmdkaydet.Parameters.AddWithValue("@r3", CmbBrans.Text);
            cmdkaydet.Parameters.AddWithValue("@r4", CmbDoktor.Text);
            cmdkaydet.ExecuteNonQuery();
            bgl.connection().Close();
            MessageBox.Show("Randevu Oluşturuldu!");
        }

        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbDoktor.Items.Clear();

            SqlCommand cmd = new SqlCommand("select DoktorAd,DoktorSoyad from Tbl_Doktorlar where DoktorBrans=@p1 ", bgl.connection());
            cmd.Parameters.AddWithValue("@p1", CmbBrans.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                CmbDoktor.Items.Add(dr[0] + " " + dr[1]);
            }
            bgl.connection().Close();
        }

        private void BtnDuyuruOlustur_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("insert into Tbl_Duyurular(Duyuru)values(@d1)", bgl.connection());
            cmd.Parameters.AddWithValue("@d1", RchDuyuru.Text);
            cmd.ExecuteNonQuery();
            bgl.connection().Close();
            MessageBox.Show("Duyuru Oluşturuldu");
        }

        private void BtnDoktorPanel_Click(object sender, EventArgs e)
        {
            FrmDoktorPaneli drp = new FrmDoktorPaneli();
            drp.Show();
        }

        private void BtnBransPanel_Click(object sender, EventArgs e)
        {
            FrmBrans frb = new FrmBrans();
            frb.Show();
        }

        private void BtnListe_Click(object sender, EventArgs e)
        {
            FrmRandevuListesi frl = new FrmRandevuListesi();
            frl.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmDuyurular fr = new FrmDuyurular();
            fr.Show();
        }
    }
}
