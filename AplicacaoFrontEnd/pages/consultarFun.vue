<template>
    <v-sheet class="mx-auto">
        <div class="title-container">
            <h2 class="title-text">Consultar funcionário</h2>
        </div>
        <v-form ref="form" class="form-container">
            <v-text-field v-model="dados.nome" label="Nome"></v-text-field>
            <v-text-field v-model="dados.funcao" label="Função"></v-text-field>
            <div class="button">
                <v-btn color="#11A634" block @click="buscar">
                    Buscar
                </v-btn>
            </div>
            <v-list v-if="resultados.length">
                <v-list-item v-for="(funcionario, index) in resultados" :key="index">
                    <v-list-item-content>
                        <v-list-item-title>{{ funcionario.nome }}</v-list-item-title>
                        <v-list-item-subtitle>ID: {{ funcionario.id }}</v-list-item-subtitle>
                        <v-list-item-subtitle>Cargo: {{ funcionario.funcao }}</v-list-item-subtitle>
                        <v-list-item-subtitle>Idade: {{ funcionario.idade }}</v-list-item-subtitle>
                    </v-list-item-content>
                    <v-list-item-action>
                        <v-btn color="primary" @click="editarFuncionario(funcionario)">
                            Editar
                        </v-btn>
                        <v-btn color="error" @click="excluirFuncionario(funcionario)">
                            Excluir
                        </v-btn>
                    </v-list-item-action>
                </v-list-item>
            </v-list>
        </v-form>
    </v-sheet>
</template>

<style scoped>
.title-container {
    background-color: white;
    padding: 10px;
    text-align: center;
    border-top-left-radius: 10px;
    border-top-right-radius: 10px;
}

.title-text {
    color: black;
    margin: 0;
}

.button {
    margin-top: 20px;
}

.mx-auto {
    background-color: #272728;
    border-radius: 50px;
}
</style>

<script>
export default {
    data: () => ({
        dados: {
            id: null,
            nome: '',
            id: '',
            idade: '',
            funcao: '',
        },
        resultados: [],
    }),
    methods: {
        async buscar() {
            try {
                if (!this.dados.nome && !this.dados.funcao) {
                    alert('Pelo menos um dos campos deve ser preenchido');
                    return;
                }

                const params = new URLSearchParams();
                if (this.dados.nome) params.append('nome', this.dados.nome);
                if (this.dados.funcao) params.append('funcao', this.dados.funcao);

                const response = await this.$axios({
                    method: 'GET',
                    url: `https://localhost:44353/Funcionario/consultar?${params.toString()}`,
                });

                this.resultados = response.data;
            } catch (error) {
                console.error("Erro ao buscar dados:", error);
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
}
</script>