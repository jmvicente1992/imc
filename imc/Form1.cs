using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace imc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            double v1 = Convert.ToDouble(txtPeso.Text);
            double v2 = Convert.ToDouble(txtAltura.Text);
            double imc;

            imc = v1 / (v2 * v2);
            txtImc.Text = imc.ToString();

            if (imc <18.5)
            {
                txtResultado.Text = "Abaixo do peso";
            }
            else
            
                if (imc >=18.5 && imc <= 24.9)
                {
                    txtResultado.Text = "Peso Ideal";
                }

               else
               
               if (imc >25 && imc <=29.9)
                {
                    txtResultado.Text = "Excesso de peso";
                }

                else
                {
                    txtResultado.Text = "Obesidade";
                }
            }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtAltura.Clear();
            txtImc.Clear();
            txtPeso.Clear();
            txtResultado.Clear();
            txtNome.Clear();
            
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            MySqlConnection conexao = new MySqlConnection("Persist Security info=False;Server=127.0.0.1;Port=3307;" + "Database=bd_imc;Uid=root;Pwd=usbw;");
            conexao.Open();

            MySqlCommand comando;
            if (txtId.Text == String.Empty)
            {
                comando = new MySqlCommand("INSERT INTO tb_imc(ID, PESO, ALTURA, IMC, RESULTADO, NOME)" + "VALUES('" + txtId.Text + "','" + txtPeso.Text + "','" + txtAltura.Text + "','" + txtImc.Text + "','" + txtResultado.Text + "', '" + txtNome.Text + "');", conexao);
            }
            else
            {
                comando = new MySqlCommand("Update tb_imc SET PESO='" + txtPeso.Text + "',ALTURA='" + txtAltura.Text + "', IMC='" + txtImc.Text + "', RESULTADO='" + txtResultado.Text + "', NOME='"+txtNome.Text+"'WHERE ID='" + txtId.Text + "'", conexao);
            }
            comando.ExecuteNonQuery();
            MessageBox.Show("SALVO COM SUCESSO", "ATENÇÃO");
            conexao.Close();
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string nome = dgv.CurrentRow.Cells[0].Value.ToString();
            int id = Convert.ToInt32(dgv.CurrentRow.Cells[1].Value.ToString());
            string peso = dgv.CurrentRow.Cells[2].Value.ToString();
            string altura = dgv.CurrentRow.Cells[3].Value.ToString();
            string imc = dgv.CurrentRow.Cells[4].Value.ToString();
            string resultado = dgv.CurrentRow.Cells[5].Value.ToString();

           
            txtNome.Text = nome;
            txtId.Text = id.ToString();
            txtPeso.Text = peso;
            txtAltura.Text = altura;
            txtImc.Text = imc;
            txtResultado.Text = resultado;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            MySqlConnection conexao = new MySqlConnection("Persist Security info=False;Server=127.0.0.1;Port=3307;" + "Database=bd_imc;Uid=root;Pwd=usbw;");
            conexao.Open();

            MySqlCommand pesquisa = new MySqlCommand("SELECT * FROM tb_imc ", conexao);

            MySqlDataAdapter da = new MySqlDataAdapter(pesquisa);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgv.DataSource = dt;

            conexao.Close();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            MySqlConnection conexao = new MySqlConnection("Persist Security info=False;Server=127.0.0.1;Port=3307;" + "Database=bd_imc;Uid=root;Pwd=usbw;");

            conexao.Open();

            MySqlCommand deleta = new MySqlCommand("DELETE FROM tb_imc WHERE ID = '" + txtId.Text + "';", conexao);

            MySqlDataAdapter da = new MySqlDataAdapter(deleta);

            DataTable dt = new DataTable();
            da.Fill(dt);

            dgv.DataSource = dt;

            conexao.Close();
        }
    }
    }

