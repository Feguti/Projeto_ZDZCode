<template>
  <v-row justify="center" align="flex-start" class="main-row">
    <v-col cols="12" sm="8" md="6">
      <v-card class="static-card">
        <v-card-title class="headline">

          <router-link to="/gerenciarFun" class="title-link" style="text-decoration: none; color: white;">
            Funcionários registrados
          </router-link>
        </v-card-title>
        <v-card-text>
          <div v-if="funcionarios && funcionarios.length">
            <ul>
              <li v-for="(funcionario, index) in funcionarios" :key="index">
                <div>{{ funcionario.nome }}</div>
                <div>ID: {{ funcionario.id }}</div>
                <div>Cargo: {{ funcionario.funcao }}</div>
                <div>Idade: {{ funcionario.idade }}</div>
                <v-list-item-action>
                  <div class="actions">
                    <v-btn color="blue" @click="editarFuncionario(funcionario)">
                      Editar
                    </v-btn>
                    <v-btn @click="excluirFuncionario(funcionario)">
                      Excluir
                    </v-btn>
                  </div>
                </v-list-item-action>
                <div>
                  <hr>
                </div>
              </li>
            </ul>
          </div>
          <div v-else>
            Nenhum funcionário foi registrado ainda.
          </div>
        </v-card-text>
      </v-card>
    </v-col>

    <v-col cols="12" sm="8" md="6">
      <v-card class="static-card">
        <v-card-title class="headline">
          <router-link to="/gerenciarProd" class="title-link" style="text-decoration: none; color: white;">
            Produtos em estoque
          </router-link>
        </v-card-title>
        <v-card-text>
          <div v-if="resultados && resultados.length">
            <ul>
              <li v-for="(produtos, index) in resultados" :key="index">
                <div>{{ produtos.nome }}</div>
                <div>ID: {{ produtos.id }}</div>
                <div>Categoria: {{ produtos.categoria }}</div>
                <div>Descrição: {{ produtos.descricao }}</div>
                <v-list-item-action>

                  <div class="actions">
                    <v-btn color="blue" @click="editarProduto(produtos)">
                      Editar
                    </v-btn>
                    <v-btn @click="excluirProduto(produtos)">
                      Excluir
                    </v-btn>
                  </div>

                </v-list-item-action>
                <div>
                  <hr>
                </div>
              </li>
            </ul>
          </div>
          <div v-else>
            Nenhum produto em estoque no momento.
          </div>
        </v-card-text>
      </v-card>
    </v-col>
  </v-row>
</template>

<script>
export default {
  data() {
    return {
      resultados: [],
      funcionarios: [],
    };
  },
  async mounted() {
    await this.fetchData();
  },
  methods: {
    async fetchData() {
      try {
        const [produtosResponse, funcionariosResponse] = await Promise.all([
          this.$axios({
            method: 'GET',
            url: 'https://localhost:44353/Produtos/',
          }),
          this.$axios({
            method: 'GET',
            url: 'https://localhost:44353/Funcionario',
          })
        ]);
        this.resultados = produtosResponse.data;
        this.funcionarios = funcionariosResponse.data;
      } catch (error) {
        console.error("Erro ao buscar dados:", error);
      }
    },

    editarProduto(produtos) {
      this.$router.push({
        name: 'atualizarProd',
        params: {
          id: produtos.id,
          nome: produtos.nome,
          categoria: produtos.categoria,
          tipo: produtos.tipo,
          descricao: produtos.descricao,
        },
      });
    },

    async excluirProduto(produtos) {
      if (confirm(`Tem certeza que deseja excluir o produto ${produtos.nome}?`)) {
        try {
          await this.$axios({
            method: 'DELETE',
            url: `https://localhost:44353/Produtos/excluir/${produtos.id}`,
          });
          this.resultados = this.resultados.filter(f => f.id !== produtos.id);
        } catch (error) {
          console.error("Erro ao excluir produto:", error);
        }
      }
    },

    editarFuncionario(funcionario) {
      this.$router.push({
        name: 'atualizarFun',
        params: {
          id: funcionario.id,
          nome: funcionario.nome,
          funcao: funcionario.funcao,
          idade: funcionario.idade,
        },
      });
    },
    async excluirFuncionario(funcionario) {
      if (confirm(`Tem certeza que deseja excluir o funcionário ${funcionario.nome}?`)) {
        try {
          await this.$axios({
            method: 'DELETE',
            url: `https://localhost:44353/Funcionario/excluir/${funcionario.id}`,
          });
          this.resultados = this.resultados.filter(f => f.id !== funcionario.id);
        } catch (error) {
          console.error("Erro ao excluir funcionário:", error);
        }
      }
    }

  }
};
</script>

<style scoped>
.main-row {
  height: 100vh;
  padding: 20px;
  overflow-y: auto;
}

.static-card {
  position: sticky;
  top: 20px;
  margin-bottom: 20px;
  overflow-y: auto;
  max-height: calc(100vh - 40px);
  background-color: #AC0B0B;
}

.headline {
  font-weight: bold;
  color: White;
}

ul {
  padding: 0;
  list-style: none;
  color: aliceblue;
}

li {
  margin-bottom: 10px;
}

.actions {
  display: flex;
  justify-content: flex-end;
  gap: 10px;
  margin-top: 10px;
}

.actions .v-btn {
  background-color: orange;
  color: white;
}
</style>
