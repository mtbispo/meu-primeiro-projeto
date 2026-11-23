// PROJETO - SISTEMA UNIFITNESS

string[] nomes = new string[100];
int[] idades = new int[100];
int[] matriculas = new int[100];
int[] planosDosAlunos = new int[100];
bool[] pagamentos = new bool[100];
DateTime[] datasPagamento = new DateTime[100];
int totalAlunos = 0;

string[] nomesPlanos = new string[20];
double[] valoresPlanos = new double[20];
int totalPlanos = 2;

nomesPlanos[0] = "BASICO";
valoresPlanos[0] = 80;
nomesPlanos[1] = "PREMIUM";
valoresPlanos[1] = 120;

int opcao = -1;

while (opcao != 0)
{
    RenovarMensalidades();
    Console.WriteLine("\n===== SISTEMA UNIFITNESS =====");
    Console.WriteLine("1 - Cadastrar aluno");
    Console.WriteLine("2 - Cadastrar plano");
    Console.WriteLine("3 - Listar alunos");
    Console.WriteLine("4 - Pesquisar aluno");
    Console.WriteLine("5 - Atualizar aluno");
    Console.WriteLine("6 - Registrar pagamento");
    Console.WriteLine("7 - Relatorios");
    Console.WriteLine("0 - Sair");

    Console.Write("Escolha uma opcao: ");
    opcao = int.Parse(Console.ReadLine());

    switch (opcao)
    {
        case 1: CadastrarAluno(); break;
        case 2: CadastrarPlano(); break;
        case 3: ListarAlunos(); Thread.Sleep(5000); break;
        case 4: PesquisarAluno(); break;
        case 5: AtualizarAluno(); break;
        case 6: RegistrarPagamento(); break;
        case 7: MostrarRelatorios(); Thread.Sleep(5000); break;
        case 0: Console.WriteLine("Programa encerrado."); break;
        default: Console.WriteLine("Opcao inexistente."); break;
    }
}

// --- FUNCOES AUXILIARES ---

int BuscarMatricula(int numero)
{
    for (int i = 0; i < totalAlunos; i++)
    {
        if (matriculas[i] == numero) return i;
    }
    return -1;
}

void ListarPlanos()
{
    Console.WriteLine("\nPLANOS DISPONIVEIS:");
    for (int i = 0; i < totalPlanos; i++)
    {
        Console.WriteLine($"{i + 1} - {nomesPlanos[i]} | R$ {valoresPlanos[i]:F2}");
    }
}

void MostrarAluno(int i)
{
    string situacao = pagamentos[i] ? "Paga" : "Pendente";
    Console.WriteLine($"\nMatricula: {matriculas[i]} | Nome: {nomes[i]} | Idade: {idades[i]}");
    Console.WriteLine($"Plano: {nomesPlanos[planosDosAlunos[i]]} | Mensalidade: {situacao}");

    if (datasPagamento[i] != DateTime.MinValue)
    {
        Console.WriteLine($"Ultimo pagamento: {datasPagamento[i]:dd/MM/yyyy} | Proximo: {datasPagamento[i].AddMonths(1):dd/MM/yyyy}");
    }
    else
    {
        Console.WriteLine("Aguardando primeiro pagamento.");
    }
}

void RenovarMensalidades()
{
    for (int i = 0; i < totalAlunos; i++)
    {
        if (pagamentos[i] && DateTime.Today >= datasPagamento[i].AddMonths(1))
        {
            pagamentos[i] = false;
        }
    }
}

// --- FUNCOES DO MENU ---

void CadastrarAluno()
{
    if (totalAlunos == 100) { Console.WriteLine("Limite de alunos atingido."); return; }

    Console.Write("Nome: ");
    string nome = Console.ReadLine().ToUpper().Trim();
    if (nome.Length < 2) { Console.WriteLine("O nome deve ter pelo menos 2 letras."); return; }

    Console.Write("Idade: ");
    int idade = int.Parse(Console.ReadLine());
    if (idade < 14) { Console.WriteLine("A idade deve ser maior que 14 anos."); return; }

    Console.Write("Numero da matricula: ");
    int matricula = int.Parse(Console.ReadLine());
    if (matricula <= 0 || BuscarMatricula(matricula) != -1) { Console.WriteLine("Matricula invalida ou ja cadastrada."); return; }

    ListarPlanos();
    Console.Write("Numero do plano: ");
    int plano = int.Parse(Console.ReadLine());
    if (plano < 1 || plano > totalPlanos) { Console.WriteLine("Plano inexistente."); return; }

    nomes[totalAlunos] = nome;
    idades[totalAlunos] = idade;
    matriculas[totalAlunos] = matricula;
    planosDosAlunos[totalAlunos] = plano - 1;
    pagamentos[totalAlunos] = false;
    totalAlunos++;
    Console.WriteLine("Aluno cadastrado com sucesso!");
}

void CadastrarPlano()
{
    if (totalPlanos == 20) { Console.WriteLine("Limite de planos atingido."); return; }

    Console.Write("Nome do novo plano: ");
    string nome = Console.ReadLine().ToUpper().Trim();
    if (nome.Length < 2) { Console.WriteLine("Nome do plano muito curto."); return; }

    for (int i = 0; i < totalPlanos; i++)
    {
        if (nomesPlanos[i].ToLower() == nome.ToLower()) { Console.WriteLine("Esse plano ja existe."); return; }
    }

    Console.Write("Valor da mensalidade: R$ ");
    double valor = double.Parse(Console.ReadLine());
    if (valor <= 0) { Console.WriteLine("O valor deve ser maior que zero."); return; }

    nomesPlanos[totalPlanos] = nome;
    valoresPlanos[totalPlanos] = valor;
    totalPlanos++;
    Console.WriteLine("Plano cadastrado com sucesso!");
}

void ListarAlunos()
{
    if (totalAlunos == 0) { Console.WriteLine("Nenhum aluno cadastrado."); return; }

    Console.WriteLine("\n===== ALUNOS =====");
    for (int i = 0; i < totalAlunos; i++) MostrarAluno(i);
}

void PesquisarAluno()
{
    if (totalAlunos == 0) { Console.WriteLine("Nenhum aluno cadastrado."); return; }

    Console.Write("Digite parte do nome: ");
    string pesquisa = Console.ReadLine().Trim().ToUpper();
    if (pesquisa.Length == 0) { Console.WriteLine("Digite um nome para pesquisar."); return; }

    bool encontrou = false;
    for (int i = 0; i < totalAlunos; i++)
    {
        if (nomes[i].ToUpper().Contains(pesquisa))
        {
            MostrarAluno(i);
            encontrou = true;
        }
    }
    if (!encontrou) Console.WriteLine("Aluno nao encontrado.");
}

void AtualizarAluno()
{
    Console.Write("Matricula do aluno: ");
    int i = BuscarMatricula(int.Parse(Console.ReadLine()));
    if (i == -1) { Console.WriteLine("Aluno nao encontrado."); return; }

    MostrarAluno(i);
    Console.WriteLine("\n1 - Alterar nome | 2 - Alterar idade | 3 - Alterar plano");
    Console.Write("O que deseja alterar? ");
    int escolha = int.Parse(Console.ReadLine());

    if (escolha == 1)
    {
        Console.Write("Novo nome: ");
        string nome = Console.ReadLine().Trim();
        if (nome.Length >= 2) { nomes[i] = nome; Console.WriteLine("Nome atualizado."); }
        else Console.WriteLine("Nome invalido.");
    }
    else if (escolha == 2)
    {
        Console.Write("Nova idade: ");
        int idade = int.Parse(Console.ReadLine());
        if (idade >= 14) { idades[i] = idade; Console.WriteLine("Idade atualizada."); }
        else Console.WriteLine("Idade invalida.");
    }
    else if (escolha == 3)
    {
        if (pagamentos[i]) { Console.WriteLine("Nao pode trocar o plano apos o pagamento."); return; }
        ListarPlanos();
        Console.Write("Novo plano: ");
        int plano = int.Parse(Console.ReadLine());
        if (plano >= 1 && plano <= totalPlanos) { planosDosAlunos[i] = plano - 1; Console.WriteLine("Plano atualizado."); }
        else Console.WriteLine("Plano inexistente.");
    }
    else
    {
        Console.WriteLine("Opcao invalida.");
    }
}

void RegistrarPagamento()
{
    Console.Write("Matricula do aluno: ");
    int i = BuscarMatricula(int.Parse(Console.ReadLine()));
    if (i == -1) { Console.WriteLine("Aluno nao encontrado."); return; }
    if (pagamentos[i]) { Console.WriteLine("Essa mensalidade ja foi paga."); return; }

    double mensalidade = valoresPlanos[planosDosAlunos[i]];
    Console.WriteLine($"Valor a pagar: R$ {mensalidade:F2}");

    Console.Write("Valor recebido: R$ ");
    double recebido = double.Parse(Console.ReadLine());
    if (recebido < mensalidade) { Console.WriteLine("Pagamento recusado: valor insuficiente."); return; }

    pagamentos[i] = true;
    datasPagamento[i] = DateTime.Today;
    Console.WriteLine($"Pagamento registrado! Troco: R$ {recebido - mensalidade:F2}");
}

void MostrarRelatorios()
{
    if (totalAlunos == 0) { Console.WriteLine("Cadastre alunos antes de gerar relatorios."); return; }

    int alunosPagos = 0, somaIdades = 0, maiorIdade = idades[0];
    double arrecadado = 0, aReceber = 0;

    for (int i = 0; i < totalAlunos; i++)
    {
        somaIdades += idades[i];
        if (idades[i] > maiorIdade) maiorIdade = idades[i];

        double mensalidade = valoresPlanos[planosDosAlunos[i]];
        if (pagamentos[i]) { alunosPagos++; arrecadado += mensalidade; }
        else aReceber += mensalidade;
    }

    Console.WriteLine("\n===== RELATORIOS =====");
    Console.WriteLine($"1. Total de alunos: {totalAlunos}");
    Console.WriteLine($"2. Mensalidades pagas: {alunosPagos}");
    Console.WriteLine($"3. Mensalidades pendentes: {totalAlunos - alunosPagos}");
    Console.WriteLine($"4. Total arrecadado: R$ {arrecadado:F2}");
    Console.WriteLine($"5. Total a receber: R$ {aReceber:F2}");
    Console.WriteLine($"6. Media de idade: {(double)somaIdades / totalAlunos:F1}");
    Console.WriteLine($"7. Maior idade: {maiorIdade}");
    Console.WriteLine($"8. Porcentagem paga: {(double)alunosPagos * 100 / totalAlunos:F1}%");

    Console.WriteLine("\nALUNOS POR PLANO:");
    for (int p = 0; p < totalPlanos; p++)
    {
        int qtd = 0;
        for (int i = 0; i < totalAlunos; i++)
        {
            if (planosDosAlunos[i] == p) qtd++;
        }
        Console.WriteLine($"- {nomesPlanos[p]}: {qtd} aluno(s)");
    }
}