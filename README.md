# .NET Alura (Identity)

- Depois de rodar a migration, é necessário fazer o _update database_. No meu caso, fiz no Rider. Só assim o db aparece no workbench.
- Para fazer queries nessa tabela, é necessário alguns comandos prévios:
  - `show databases` (apenas para mostrar a lista de dbs)
  - `use FilmeConnection` (ativar o banco)
    `show tables` (mostrar as tabelas)
    `describe FilmeConnection` (printar a tabela inteira)

- Outra coisa sobre a migration é que ela serve para aplicar o modelamento do banco. Eu achava que ela servia para aplicar dados em si. Na verdade, o que gerencia a entrada de dados no banco é o próprio CRUD que é construído. Por exemplo, rodamos uma migration quando alteramos uma tabela, quando adicionamos ou removemos alguma coluna, quando criamos uma tabela nova através de uma nova model.

## Relacionando entidades

- Uma tabela de Cinema prescinde uma tabela de endereços, ou seja, um Cinema _precisa_ de um endereço físico. Já o contrário não se aplica: uma rua pode existir sem que exista um cinema nela.
- Porém, para o nosso exemplo, teremos 1 endereço para 1 cinema, então a relação é de 1:1 mesmo.

Dentro de um **Cinema A**, precisamos de uma informação que conecte um endereço em outra tabela a esse **Cinema A**. Teremos, então uma coluna para isso.

---

# To Do List

- [ ] Translate all code to English. Alura's course professor wrote all code in Portuguese for accessibility reasons, but that doesn't make any sense to me.

# User-Secrets

`~/.microsoft/usersecrets`
