using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AplikasiDataMahasiswa
{
    public partial class Form1 : Form
    {
        private List<Mahasiswa> list = new List<Mahasiswa>();
        public Form1()
        {
            InitializeComponent();
            InisialisasiListView();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // cek apakah data mahasiswa sudah dipilih
            if (lvwMahasiswa.SelectedItems.Count > 0)
            {
                // tampilkan konfirmasi
                var konfirmasi = MessageBox.Show("Apakah data mahasiswa ingin dihapus ? ", "Konfirmasi",
               
                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (konfirmasi == DialogResult.Yes)
                {
                    // ambil index list yang di pilih
                    var index = lvwMahasiswa.SelectedIndices[0];
                    // hapus objek mahasiswa dari list
                    list.RemoveAt(index);
                    // refresh tampilan listivew
                    TampilkanData();
                }
            }
            else // data belum dipilih
            {
                MessageBox.Show("Data mahasiswa belum dipilih !!!", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void InisialisasiListView()
        {
            lvwMahasiswa.View = View.Details;
            lvwMahasiswa.FullRowSelect = true;
            lvwMahasiswa.GridLines = true;
            lvwMahasiswa.Columns.Add("No.", 30, HorizontalAlignment.Center);
            lvwMahasiswa.Columns.Add("Nim", 91, HorizontalAlignment.Center);
            lvwMahasiswa.Columns.Add("Nama", 200, HorizontalAlignment.Left);
            lvwMahasiswa.Columns.Add("Kelas", 70, HorizontalAlignment.Center);
            lvwMahasiswa.Columns.Add("Nilai", 50, HorizontalAlignment.Center);
            lvwMahasiswa.Columns.Add("Nilai Huruf", 70, HorizontalAlignment.Center);

        }
        private void ResetForm()
        {
            txtNim.Clear();
            txtNama.Clear();
            txtKelas.Clear();
            txtNilai.Text = "0";
            txtNim.Focus();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void lvwMahasiswa_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            int nilai;
            if (int.TryParse(txtNilai.Text, out nilai))
            {
                // membuat objek mahasiswa
                Mahasiswa mhs = new Mahasiswa
                {
                    Nim = txtNim.Text,
                    Nama = txtNama.Text,
                    Kelas = txtKelas.Text,
                    Nilai = nilai
                };

                // tambahkan objek mahasiswa ke dalam collection
                list.Add(mhs);

                var msg = "Data mahasiswa berhasil disimpan.";
                // tampilkan dialog informasi
                MessageBox.Show(msg, "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // reset form input
                ResetForm();
            }
            else
            {
                MessageBox.Show("Nilai yang dimasukkan tidak valid!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool NumericOnly(KeyPressEventArgs e)
        {
            var strValid = "0123456789";
            if (!(e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                // input selain angka
                if (strValid.IndexOf(e.KeyChar) < 0)
                {
                    return true;  // input tidak valid, blokir
                }
            }
            return false;  // input valid, izinkan
        }


        private void txtNilai_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = NumericOnly(e);
        }

        private void TampilkanData()
        {
            // kosongkan data listview
            lvwMahasiswa.Items.Clear();

            // lakukan perulangan untuk menampilkan data mahasiswa ke listview
            foreach (var mhs in list)
            {
                var noUrut = lvwMahasiswa.Items.Count + 1;
                var item = new ListViewItem(noUrut.ToString());
                item.SubItems.Add(mhs.Nim);
                item.SubItems.Add(mhs.Nama);
                item.SubItems.Add(mhs.Kelas);
                item.SubItems.Add(mhs.Nilai.ToString());

                // Logika penilaian yang benar
                string grade;
                if (mhs.Nilai < 20)
                {
                    grade = "E";
                }
                else if (mhs.Nilai < 40)
                {
                    grade = "D";
                }
                else if (mhs.Nilai < 60)
                {
                    grade = "C";  // Mengubah nilai C yang hilang
                }
                else if (mhs.Nilai < 80)
                {
                    grade = "B";
                }
                else
                {
                    grade = "A";
                }

                item.SubItems.Add(grade);
                lvwMahasiswa.Items.Add(item);
            }
        }

        private void btnTampilkanData_Click(object sender, EventArgs e)
        {
            TampilkanData();
        }
    }
}
