using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GastonvarelaSPR3
{
    public partial class frmMain : Form
    {
        public struct structRepuestos
        {
            public string strMarca;
            public string strOrigen;
            public int intNumeroRepuesto;
            public string strDescripcion;
            public float floPrecio;
        }
        structRepuestos[] vecRepuestos = new structRepuestos[100];
        public int intIndiceRegistro = 0;
        public int intIndiceConsulta = 0;

        public void limpiarControles()
        {
            cmbMarca.SelectedIndex = -1;
            optNacional.Checked = true;
            txtNumeroRepuesto.Text = "";
            txtDescripcion.Text = "";
            txtPrecio.Text = "";
        }

        public frmMain()
        {
            InitializeComponent();
        }

        private void btnConsulta_Click(object sender, EventArgs e)
        {
            lstDatosRepuestos.Items.Clear();
            intIndiceConsulta = 0;
            if (optNacionalConsulta.Checked == true)
            {
                lstDatosRepuestos.Items.Add("Repuestos " + cmbMarcaConsulta.Text + " Origen Nacional:");
                lstDatosRepuestos.Items.Add("────────────────────");
                while (intIndiceConsulta < vecRepuestos.Length)
                {
                    if (vecRepuestos[intIndiceConsulta].strMarca == cmbMarcaConsulta.Text)
                    {
                        if (vecRepuestos[intIndiceConsulta].strOrigen == "Nacional")
                        {
                            lstDatosRepuestos.Items.Add("Nº de Repuesto: " + vecRepuestos[intIndiceConsulta].intNumeroRepuesto);
                            lstDatosRepuestos.Items.Add("Precio: " + vecRepuestos[intIndiceConsulta].floPrecio);
                            lstDatosRepuestos.Items.Add("Descripción: " + vecRepuestos[intIndiceConsulta].strDescripcion);
                            lstDatosRepuestos.Items.Add("────────────────────");
                        }
                    }
                    intIndiceConsulta++;
                }
            }
            else
            {
                lstDatosRepuestos.Items.Add("Repuestos " + cmbMarcaConsulta.Text + " Origen Importado");
                lstDatosRepuestos.Items.Add("────────────────────");
                while (intIndiceConsulta < vecRepuestos.Length)
                {
                    if (vecRepuestos[intIndiceConsulta].strMarca == cmbMarcaConsulta.Text)
                    {
                        if (vecRepuestos[intIndiceConsulta].strOrigen == "Importado")
                        {
                            lstDatosRepuestos.Items.Add("Nº de Repuesto: " + vecRepuestos[intIndiceConsulta].intNumeroRepuesto);
                            lstDatosRepuestos.Items.Add("Precio: " + vecRepuestos[intIndiceConsulta].floPrecio);
                            lstDatosRepuestos.Items.Add("Descripción: " + vecRepuestos[intIndiceConsulta].strDescripcion);
                            lstDatosRepuestos.Items.Add("────────────────────");
                        }
                    }
                    intIndiceConsulta++;
                }
            }
        }

        private void btnRegistro_Click(object sender, EventArgs e)
        {
            bool existeNroRepuesto = false;
            intIndiceConsulta = 0;
            while (intIndiceConsulta < vecRepuestos.Length)
            {
                if (vecRepuestos[intIndiceConsulta].intNumeroRepuesto == int.Parse(txtNumeroRepuesto.Text))
                {
                    existeNroRepuesto = true;
                    MessageBox.Show("El Nº de Repuesto " + txtNumeroRepuesto.Text + " ya se encunetra registrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                intIndiceConsulta++;
            }
            if (existeNroRepuesto == false)
            {
                vecRepuestos[intIndiceRegistro].strMarca = cmbMarca.Text;
                if (optNacional.Checked == true)
                {
                    vecRepuestos[intIndiceRegistro].strOrigen = "Nacional";
                }
                else
                {
                    vecRepuestos[intIndiceRegistro].strOrigen = "Importado";
                }
                vecRepuestos[intIndiceRegistro].intNumeroRepuesto = int.Parse(txtNumeroRepuesto.Text);
                vecRepuestos[intIndiceRegistro].strDescripcion = txtDescripcion.Text;
                vecRepuestos[intIndiceRegistro].floPrecio = float.Parse(txtPrecio.Text);
                intIndiceRegistro++;
                MessageBox.Show("Se regisro con exito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mrcConsultaRepuestos.Enabled = true;
                limpiarControles();
            }
        }

        private void cmbMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMarca.SelectedIndex != -1)
            {
                lblNumeroRepuesto.Enabled = true;
                txtNumeroRepuesto.Enabled = true;
            }
            else
            {
                lblNumeroRepuesto.Enabled = false;
                txtNumeroRepuesto.Enabled = false;
            }
        }

        private void txtNumeroRepuesto_TextChanged(object sender, EventArgs e)
        {
            if (txtNumeroRepuesto.Text != "")
            {
                lblDescripcion.Enabled = true;
                txtDescripcion.Enabled = true;
            }
            else
            {
                lblDescripcion.Enabled = false;
                txtDescripcion.Enabled = false;
            }
        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {
            if (txtDescripcion.Text != "")
            {
                lblPrecio.Enabled = true;
                txtPrecio.Enabled = true;
            }
            else
            {
                lblPrecio.Enabled = false;
                txtPrecio.Enabled = false;
            }
        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            if (txtPrecio.Text != "")
            {
                btnRegistro.Enabled = true;
            }
            else
            {
                btnRegistro.Enabled = false;
            }
        }

        private void cmbMarcaConsulta_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMarcaConsulta.SelectedIndex != -1)
            {
                btnConsulta.Enabled = true;
            }
            else
            {
                btnConsulta.Enabled = false;
            }
        }

        private void txtNumeroRepuesto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Solo se permite el uso de caracteres numericos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Solo se permite el uso de caracteres numericos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtDescripcion.Text.Length == 50)
            {
                MessageBox.Show("no se pueden escribir mas caracteres", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
