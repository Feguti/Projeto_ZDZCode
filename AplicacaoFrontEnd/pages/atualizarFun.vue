<template>
  <div>
    <v-text-field v-model="dados.id" label="ID" readonly></v-text-field>
    <v-text-field v-model="dados.nome" label="Nome"></v-text-field>
    <v-text-field v-model="dados.funcao" label="Função"></v-text-field>
    <v-text-field v-model="dados.idade" label="Idade"></v-text-field>
    <div class="d-flex flex-column">
      <v-btn class="mt-4" color="success" block @click="updateFuncionario">
        Atualizar
      </v-btn>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      dados: {
        id: '',
        nome: '',
        funcao: '',
        idade: '',
      },
    };
  },
  created() {
    this.dados.id = this.$route.params.id;
    this.dados.nome = this.$route.params.nome;
    this.dados.funcao = this.$route.params.funcao;
    this.dados.idade = this.$route.params.idade;
  },
  methods: {
    async updateFuncionario() {
      try {
        const response = await fetch(`https://localhost:44353/Funcionario/${this.dados.id}`, {
          method: 'PUT',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify(this.dados),
        });

        if (!response.ok) {
          throw new Error('Erro ao atualizar funcionário');
        }

        const funcionarioAtualizado = await response.json();
        console.log('Funcionário atualizado:', funcionarioAtualizado);

        // Mostre uma mensagem de sucesso ou redirecione para outra página
        alert('Funcionário atualizado com sucesso!');
      } catch (error) {
        console.error('Erro:', error.message);
        alert('Erro ao atualizar funcionário. Por favor, tente novamente.');
      }
    },
  },
};
</script>