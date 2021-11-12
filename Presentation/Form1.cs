﻿using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using OrcaFascio.Entity;
using OrcaFascio.Util;
using OrcaFascio.Repository;
using System.Diagnostics;
using OrcaFascio.Service;

namespace OrcaFascio
{
    public partial class Form1 : Form
    {

        ColigadaRepository coligadaRepository;
        FilialRepository filialRepository;
        ProjetoRepository projetoRepository;
        UnidadeRepository unidadeRepository;
        GrupoCustoRepository grupoCustoRepository;
        AutoincrementoRepository autoincrementoRepository;
        AutoincrementoService autoincrementoService;
        InsumoRepository insumoRepository;
        InsumoService insumoService;
        ComposicaoRepository composicaoRepository;
        ComposicaoService composicaoService;
        RecursoRepository recursoRepository;
        RecursoService recursoService;
        TarefaRepository tarefaRepository;
        TarefaService tarefaService;
        StringBuilder log = new StringBuilder();

        public Form1()
        {
            InitializeComponent();
            this.CenterToScreen();

            coligadaRepository = new ColigadaRepository();
            filialRepository = new FilialRepository();
            projetoRepository = new ProjetoRepository();
            unidadeRepository = new UnidadeRepository();
            grupoCustoRepository = new GrupoCustoRepository();
            autoincrementoRepository = new AutoincrementoRepository();
            autoincrementoService = new AutoincrementoService(autoincrementoRepository);
            insumoRepository = new InsumoRepository();
            insumoService = new InsumoService(insumoRepository, autoincrementoService);
            composicaoRepository = new ComposicaoRepository();
            composicaoService = new ComposicaoService(composicaoRepository, autoincrementoService);
            recursoRepository = new RecursoRepository();
            recursoService = new RecursoService(recursoRepository, autoincrementoService);
            tarefaRepository = new TarefaRepository();
            tarefaService = new TarefaService(tarefaRepository, autoincrementoService);

            this.Text = string.Format("OrçaFascio x {0}", coligadaRepository.GetDatabaseName()); 

            LimparCampos();

            CarregarColigadas();
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("Todas as composições e insumos do projeto {0} serão excluídos. Deseja continuar?", cmbProjetos.Items[cmbProjetos.SelectedIndex]), "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    Projeto projeto = (Projeto)cmbProjetos.Items[cmbProjetos.SelectedIndex];

                    log.Clear();
                    projeto.Insumos.Clear();
                    projeto.Composicoes.Clear();
                    projeto.Tarefas.Clear();

                    DataTable dt = ConvertExcelToDataTable(txtCaminhoArquivoComposicoes.Text);
                    DataTable dtOrcamento = ConvertExcelToDataTable(txtCaminhoArquivoOrcamento.Text);
                    Composicao composicaoPrincipal = null;

                    //Planilha Composicoes
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        //Colunas Planilha Composicoes
                        int columnA = 0;
                        int columnCodigo = 1;
                        int columnBanco = 2;
                        int columnDescricao = 3;
                        int columnTipo = 4;
                        int columnF = 5;
                        int columnUnidade = 6;
                        int columnQuant = 7;
                        int columnValor = 8;
                        int columnTotal = 9;

                        if (dt.Rows[i].ItemArray[0].ToString() == "Composição")
                        {
                            composicaoPrincipal = new Composicao();

                            Unidade unidade = unidadeRepository.GetById(dt.Rows[i].ItemArray[columnUnidade].ToString());

                            if (unidade != null)
                                composicaoPrincipal.CodUnd = unidade.CodUnd;
                            else
                                log.AppendLine(string.Format("Linha {0}: Composição - Unidade {1} não cadastrada no RM.", i + 1, dt.Rows[i].ItemArray[columnUnidade].ToString()));

                            composicaoPrincipal.Banco = dt.Rows[i].ItemArray[columnBanco].ToString();
                            composicaoPrincipal.CodColigada = projeto.CodColigada;
                            composicaoPrincipal.IdPrj = projeto.IdPrj;
                            composicaoPrincipal.CodCmp = dt.Rows[i].ItemArray[columnCodigo].ToString();
                            composicaoPrincipal.DescCmp = dt.Rows[i].ItemArray[columnDescricao].ToString();
                            composicaoPrincipal.ValorBdi = 0;
                            composicaoPrincipal.ValorTotal = 0;

                            projeto.Composicoes.Add(composicaoPrincipal);
                        }

                        if (dt.Rows[i].ItemArray[0].ToString() == "Composição Auxiliar")
                        {
                            Recurso recurso = new Recurso();
                            recurso.CodColigada = projeto.CodColigada;
                            recurso.IdPrj = projeto.IdPrj;
                            recurso.CodCmpPrincipal = composicaoPrincipal.CodCmp;
                            recurso.CodCmpFilha = dt.Rows[i].ItemArray[columnCodigo].ToString();
                            recurso.Quantidade = Convert.ToDouble(dt.Rows[i].ItemArray[columnQuant]);
                            recurso.ValorUnit = Convert.ToDouble(dt.Rows[i].ItemArray[columnValor]);
                            recurso.ValorTotal = 0;

                            composicaoPrincipal.recursos.Add(recurso);
                        }

                        if (dt.Rows[i].ItemArray[0].ToString() == "Insumo")
                        {
                            Insumo insumo = new Insumo();

                            Unidade unidade = unidadeRepository.GetById(dt.Rows[i].ItemArray[columnUnidade].ToString());

                            if (unidade != null)
                                insumo.CodUnd = unidade.CodUnd;
                            else
                                log.AppendLine(string.Format("Linha {0}: Insumo - Unidade {1} não cadastrada no RM.", i + 1, dt.Rows[i].ItemArray[columnUnidade].ToString()));

                            GrupoCusto grupoCusto = grupoCustoRepository.GetByDescription(projeto.CodColigada, projeto.IdPrj, dt.Rows[i].ItemArray[columnTipo].ToString());

                            if (grupoCusto != null)
                            {
                                insumo.IdGis = Convert.ToInt32(grupoCusto.IdGis);
                                insumo.GrupoDner = grupoCusto.GrupoDner;
                            }
                            else
                                log.AppendLine(string.Format("Linha {0}: Insumo - Grupo de Custo {1} não cadastrado no RM.", i + 1, dt.Rows[i].ItemArray[columnTipo].ToString()));

                            insumo.Banco = dt.Rows[i].ItemArray[columnBanco].ToString();
                            insumo.CodColigada = projeto.CodColigada;
                            insumo.IdPrj = projeto.IdPrj;
                            insumo.CodIsm = dt.Rows[i].ItemArray[columnCodigo].ToString();
                            insumo.DescIsm = dt.Rows[i].ItemArray[columnDescricao].ToString();
                            insumo.Valor = Convert.ToDouble(dt.Rows[i].ItemArray[columnValor]);

                            if (!projeto.Insumos.Any(x => x.CodIsm.Trim() == insumo.CodIsm.Trim() && x.Banco.Trim() == insumo.Banco.Trim()))
                            {
                                projeto.Insumos.Add(insumo);
                            }

                            Recurso recurso = new Recurso();
                            recurso.CodColigada = insumo.CodColigada;
                            recurso.IdPrj = insumo.IdPrj;
                            recurso.CodCmpPrincipal = composicaoPrincipal.CodCmp;
                            recurso.CodIsm = insumo.CodIsm;
                            recurso.Quantidade = Convert.ToDouble(dt.Rows[i].ItemArray[columnQuant]);
                            recurso.ValorUnit = Convert.ToDouble(dt.Rows[i].ItemArray[columnValor]);
                            recurso.ValorTotal = 0;

                            composicaoPrincipal.recursos.Add(recurso);
                        }
                    }

                    //Planilha Orcamento
                    Tarefa primeiraTarefa = null;
                    for (int i = 0; i < dtOrcamento.Rows.Count; i++)
                    {

                        //Colunas Planilha Orcamento
                        int columnOrcItem = 0;
                        int columnOrccodigo = 1;
                        int columnOrcbanco = 2;
                        int columnOrcdescricao = 3;
                        int columnOrcund = 4;
                        int columnOrcquant = 5;
                        decimal aux = 0;

                        if (decimal.TryParse(dtOrcamento.Rows[i].ItemArray[columnOrcItem].ToString(), out aux))
                        {

                            Tarefa tarefa = new Tarefa();

                            if (!string.IsNullOrWhiteSpace(dtOrcamento.Rows[i].ItemArray[columnOrcund].ToString()))
                            {
                                Unidade unidade = unidadeRepository.GetById(dtOrcamento.Rows[i].ItemArray[columnOrcund].ToString().Trim());

                                if (unidade != null)
                                    tarefa.CodUnd = unidade.CodUnd;
                                else
                                    log.AppendLine(string.Format("Linha {0}: Orçamento - Unidade {1} não cadastrada no RM.", i + 1, dtOrcamento.Rows[i].ItemArray[columnOrcund].ToString()));

                                tarefa.Quantidade = Convert.ToDouble(dtOrcamento.Rows[i].ItemArray[columnOrcquant]);
                            }

                            tarefa.CodColigada = projeto.CodColigada;
                            tarefa.IdPrj = projeto.IdPrj;
                            tarefa.Descricao = tarefa.Nome = dtOrcamento.Rows[i].ItemArray[columnOrcdescricao].ToString().Trim();

                            if (projeto.Tarefas.Count == 0)
                            {
                                tarefa.Quantidade = 1;
                                primeiraTarefa = tarefa;
                                primeiraTarefa.CodTrf = tarefa.CodTrf = Mask(dtOrcamento.Rows[i].ItemArray[columnOrcItem].ToString().Trim());
                                tarefa.CodTrfPai = tarefa.CodTrf;
                            }
                            else
                            {
                                tarefa.CodTrf = Mask(primeiraTarefa.CodTrf +"."+ dtOrcamento.Rows[i].ItemArray[columnOrcItem].ToString().Trim()); 
                            }
                            

                            if (!string.IsNullOrWhiteSpace(dtOrcamento.Rows[i].ItemArray[columnOrccodigo].ToString()))
                            {
                                tarefa.CodCmp = dtOrcamento.Rows[i].ItemArray[columnOrccodigo].ToString().Trim();
                            }

                            tarefa.CustoUnit = 0;

                            projeto.Tarefas.Add(tarefa);
                        }
                    }
                    //Gravar no banco se não tiver erro de De-Para
                    if (log.Length == 0)
                    {
                        int qtd = 0;

                        projetoRepository.LimparProjeto(projeto);
                        foreach (var item in projeto.Insumos)
                        {
                            qtd += insumoService.Add(item);
                        }
                        foreach (var item in projeto.Composicoes)
                        {
                            qtd += composicaoService.Add(item);
                        }
                        foreach (var composicao in projeto.Composicoes)
                        {
                            foreach (var recurso in composicao.recursos)
                            {
                                qtd += recursoService.Add(recurso);
                            }
                        }
                        foreach (var item in projeto.Tarefas)
                        {
                            tarefaService.Add(item);
                        }
                        if(qtd > 0)
                        {
                            projetoRepository.AjustarHierarquia(projeto);
                            MessageBox.Show("Importação realizada com sucesso!", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                            MessageBox.Show("Nenhum regitro importado. Verifique se as planilhas estão corretas.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        visualizaLog();
                }
                catch (Exception ex)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        private void visualizaLog()
        {

            string strTempFile = $"{Path.GetTempPath()}{Path.GetRandomFileName()}.txt";

            try
            {
                using (StreamWriter sw = new StreamWriter(strTempFile))
                    sw.WriteLine(log.ToString());

                using (Process p = new Process())
                {
                    p.StartInfo = new ProcessStartInfo()
                    {
                        UseShellExecute = true,
                        FileName = strTempFile
                    };

                    p.Start();
                    // aguardar que o processo conclua o loading
                    p.WaitForInputIdle();
                    // esperar que o processo feche
                    //p.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (File.Exists(strTempFile))
                    File.Delete(strTempFile);
            }
        }
        public void LimparCampos()
        {
            this.cmbColigadas.Items.Clear();
            this.cmbFiliais.Items.Clear();
            this.cmbProjetos.Items.Clear();

        }

        private void cmbColigadas_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (cmbColigadas.Items.Count > 0)
            {
                Coligada s = (Coligada)cmbColigadas.Items[cmbColigadas.SelectedIndex];
                CarregarFiliais(s.CodColigada);
            }
        }

        private void cmbFiliais_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (cmbFiliais.Items.Count > 0)
            {
                Filial filial = (Filial)cmbFiliais.Items[cmbFiliais.SelectedIndex];
                CarregarProjetos(filial.CodColigada, filial.CodFilial);
            }
        }

        public void CarregarColigadas()
        {
            this.cmbColigadas.Items.Clear();

            foreach (var item in coligadaRepository.Get(new Coligada()))
            {
                this.cmbColigadas.Items.Add(item);       
            }
        }

        public void CarregarFiliais(int codColigada)
        {

            this.cmbFiliais.Items.Clear();

            foreach (var item in filialRepository.GetByColigada(codColigada))
            {
                this.cmbFiliais.Items.Add(item);
            }
        }

        public void CarregarProjetos(int codColigada, int codFilial)
        {

            this.cmbProjetos.Items.Clear();

            foreach (var item in projetoRepository.GetByFilial(codColigada, codFilial))
            {
                this.cmbProjetos.Items.Add(item);
            }
        }

        public static DataTable ConvertExcelToDataTable(string FileName)
        {
            DataTable dtResult = null;
            int totalSheet = 0; //No of sheets on excel file  
            using (OleDbConnection objConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties='Excel 12.0;HDR=No;IMEX=1;';"))
            {
                objConn.Open();
                OleDbCommand cmd = new OleDbCommand();
                OleDbDataAdapter oleda = new OleDbDataAdapter();
                DataSet ds = new DataSet();
                DataTable dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string sheetName = string.Empty;
                if (dt != null)
                {
                    var tempDataTable = (from dataRow in dt.AsEnumerable()
                                         where !dataRow["TABLE_NAME"].ToString().Contains("FilterDatabase")
                                         select dataRow).CopyToDataTable();
                    dt = tempDataTable;
                    totalSheet = dt.Rows.Count;
                    sheetName = dt.Rows[0]["TABLE_NAME"].ToString();
                }
                cmd.Connection = objConn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [" + sheetName + "]";
                oleda = new OleDbDataAdapter(cmd);
                oleda.Fill(ds, "excelData");
                dtResult = ds.Tables["excelData"];
                objConn.Close();
                return dtResult; //Returning Dattable  
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCaminhoArquivo_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Excel (*.xls or .xlsx)|.xls;*.xlsx";
                        
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtCaminhoArquivoComposicoes.Text = openFileDialog1.FileName;
            }
        }

        private void btnCaminhoArquivoOrcamento_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Excel (*.xls or .xlsx)|.xls;*.xlsx";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtCaminhoArquivoOrcamento.Text = openFileDialog1.FileName;
            }
        }

        private string Mask(string valor)
        {

            string[] array = valor.Split('.');
            string codTrf_F = "";

            for (int x = 0; x < array.Length; x++)
            {
                if (x == 0)
                    codTrf_F = array[x].PadLeft(3, '0');
                else
                {
                    codTrf_F += ".";
                    codTrf_F += array[x].PadLeft(2, '0');
                }
            }
            return codTrf_F;
        }
    }
}
